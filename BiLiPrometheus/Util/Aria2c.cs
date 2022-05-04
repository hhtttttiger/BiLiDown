using System.Diagnostics;
using System.Text.RegularExpressions;

namespace BiLiPrometheus.Util
{
    public static class Aria2c
    {
        /// <summary>
        /// 下载文件
        /// </summary>
        /// <param name="url">url</param>
        /// <param name="path">文件路径，带文件名以及后缀</param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        /// <exception cref="FileNotFoundException"></exception>
        public static async Task Download(string url, string path)
        {
            var exe = "aria2c";
            var dir = Path.GetDirectoryName(path);
            var name = Path.GetFileName(path);

            void Output(object sender, DataReceivedEventArgs args)
            {
                if (string.IsNullOrWhiteSpace(args.Data))
                {
                    return;
                }
                Console.WriteLine("Aria:{0}", args.Data?.Trim());
            }

            var headerArgs = " --header=\"User-Agent: Mozilla/5.0\"";
            headerArgs += " --header=\"Referer: https://www.bilibili.com\"";
            var args = $"-x 10 -s 10 --dir={dir} --allow-overwrite=true --out={name} {headerArgs} {url}";

            var info = new ProcessStartInfo(exe, args)
            {
                UseShellExecute = false,
                CreateNoWindow = true,
                RedirectStandardOutput = true,
                RedirectStandardError = true,
            };
            if (File.Exists(path))
            {
                File.Delete(path);
            }

            Console.WriteLine("启动 aria2c： {0}", args);
            using (var p = new Process { StartInfo = info, EnableRaisingEvents = true })
            {
                if (!p.Start())
                {
                    throw new Exception("aria 启动失败");
                }
                p.ErrorDataReceived += Output;
                p.OutputDataReceived += Output;
                p.BeginOutputReadLine();
                p.BeginErrorReadLine();
                await p.WaitForExitAsync();
                p.OutputDataReceived -= Output;
                p.ErrorDataReceived -= Output;
            }

            var fi = new FileInfo(path);
            if (!fi.Exists || fi.Length == 0)
            {
                throw new FileNotFoundException("文件下载失败", path);
            }
        }

        /// <summary>
        /// 功能：使用Aria2c进行文件下载
        /// </summary>
        /// <param name="url"></param>
        /// <param name="strFileName"></param>
        /// <returns></returns>
        public static async Task<bool> DownloadFileByAria2Async(string url, string path, DataReceivedEventHandler output)
        {
            var tool = "aria2c";

            var dir = Path.GetDirectoryName(path);
            var name = Path.GetFileName(path);

            var headerArgs = " --header=\"User-Agent: Mozilla/5.0\"";
            headerArgs += " --header=\"Referer: https://www.bilibili.com\"";
            var args = $"-c -x 10 -s 10 --dir={dir} --allow-overwrite=true --out={name} {headerArgs} {url}";

            var info = new ProcessStartInfo(tool, args)
            {
                UseShellExecute = false,
                CreateNoWindow = true,
                RedirectStandardOutput = true,
                RedirectStandardError = true,
            };
            if (File.Exists(path))
            {
                File.Delete(path);
            }

            using (var p = new Process { StartInfo = info, EnableRaisingEvents = true })
            {
                if (!p.Start())
                {
                    throw new Exception("aria 启动失败");
                }
                p.ErrorDataReceived += output;
                p.OutputDataReceived += output;
                p.BeginOutputReadLine();
                p.BeginErrorReadLine();
                await p.WaitForExitAsync();
                p.OutputDataReceived -= output;
                p.ErrorDataReceived -= output;
            }
            return File.Exists(path) && new FileInfo(path).Length > 0;
        }

        private static void ShowInfo(string url, string a)
        {
            if (a == null) return;

            const string re1 = ".*?"; // Non-greedy match on filler
            const string re2 = "(\\(.*\\))"; // Round Braces 1

            var r = new Regex(re1 + re2, RegexOptions.IgnoreCase | RegexOptions.Singleline);
            var m = r.Match(a);
            if (m.Success)
            {
                var rbraces1 = m.Groups[1].ToString().Replace("(", "").Replace(")", "").Replace("%", "").Replace("s", "");
                if (rbraces1 == "OK")
                {
                    rbraces1 = "";
                }
                Console.WriteLine(DateTime.Now.ToString().Replace("/", "-") + "    " + url + "    下载进度:" + rbraces1 + "%");
            }
        }
    }
}
