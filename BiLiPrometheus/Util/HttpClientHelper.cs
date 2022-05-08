using System.Net;

namespace BiLiPrometheus.Util
{
    public class HttpClientHelper
    {
        private readonly IHttpClientFactory _clientFactory;

        public HttpClientHelper(IHttpClientFactory clientFactory) =>
            _clientFactory = clientFactory;

        /// <summary>
        /// 下载文件
        /// </summary>
        /// <param name="url"></param>
        /// <param name="savePath"></param>
        /// <param name="action"></param>
        /// <returns></returns>
        public async Task DownWithGetAsync(string url, string savePath, Action<double> action)
        {
            var httpClient = _clientFactory.CreateClient();

            DateTimeOffset? lastTime = File.Exists(savePath) ? new FileInfo(savePath).LastWriteTimeUtc : null;
            using (var fileStream = new FileStream(savePath, FileMode.OpenOrCreate))
            {
                fileStream.Seek(0, SeekOrigin.End);
                var downloadedBytes = fileStream.Position;

                using var httpRequestMessage = new HttpRequestMessage();
                httpRequestMessage.Headers.TryAddWithoutValidation("Referer", "https://www.bilibili.com");
                httpRequestMessage.Headers.TryAddWithoutValidation("User-Agent", "Mozilla/5.0");
                //httpRequestMessage.Headers.TryAddWithoutValidation("Cookie", Core.Config.COOKIE);
                httpRequestMessage.Headers.Range = new(downloadedBytes, null);
                httpRequestMessage.Headers.IfRange = lastTime != null ? new(lastTime.Value) : null;
                httpRequestMessage.RequestUri = new(url);

                using var response = (await httpClient.SendAsync(httpRequestMessage, HttpCompletionOption.ResponseHeadersRead)).EnsureSuccessStatusCode();

                if (response.StatusCode == HttpStatusCode.OK) // server doesn't response a partial content
                {
                    downloadedBytes = 0;
                    fileStream.Seek(0, SeekOrigin.Begin);
                }

                using var stream = await response.Content.ReadAsStreamAsync();
                var totalBytes = downloadedBytes + (response.Content.Headers.ContentLength ?? long.MaxValue - downloadedBytes);

                const int blockSize = 1048576 / 4;
                var buffer = new byte[blockSize];

                while (downloadedBytes < totalBytes)
                {
                    var recevied = await stream.ReadAsync(buffer);
                    if (recevied == 0)
                        break;
                    await fileStream.WriteAsync(buffer.AsMemory(0, recevied));
                    await fileStream.FlushAsync();
                    downloadedBytes += recevied;

                    action((double)downloadedBytes / totalBytes);
                }
            }

        }
    }
}
