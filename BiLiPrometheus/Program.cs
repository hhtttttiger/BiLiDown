using BiLiPrometheus.Models;
using BiLiPrometheus.Util;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Text.RegularExpressions;
using WebApiClient;


namespace BiLiPrometheus
{
    /// <summary>
    /// 测试用控制台
    /// </summary>
    internal class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("Hello World FUCK THE BILIBILI!");

            await FFmpeg.RunFFmpeg($"-loglevel warning -y  -i \"【轩辕剑：大道之行】01：黄沙起兮风云涌.flv\"  -disposition:v:1 attached_pic  -metadata title=\"[P1]粤语.mp4\" -metadata description=\"\" -metadata album=\"「四月十六号下午三点之前的一分钟」.补档\" -c copy  -c:s mov_text -movflags faststart -strict unofficial \"【轩辕剑：大道之行】01：黄沙起兮风云涌.mp4\"");

            //Console.WriteLine("输入BV号");
            //var bv = Console.ReadLine();

            //ServiceCollection services = new ServiceCollection();
            //services.AddHttpApi<IBHttp>();
            //var provider = services.BuildServiceProvider();
            //var http = provider.GetService<IBHttp>();


            //var videoInfo = await http.GetVideoInfo(bv);

            //var videoPlay = await http.GetVideoPlay(new VideoPlayReq
            //{
            //    bvid = bv,
            //    cid = videoInfo.data.cid
            //});

            //var url = videoPlay.data.durl[0].url;

            //var save = $@"D:\{videoInfo.data.Title.Trim()}.flv";

            //url = UnicodeDecode(url);
            //await Aria2c.Download(url, save);
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
       Host.CreateDefaultBuilder(args)
           .ConfigureServices((hostContext, services) =>
           {
               services.AddHttpApi<IBHttp>();
           });

        public static string UnicodeDecode(string unicodeStr)
        {
            if (string.IsNullOrWhiteSpace(unicodeStr) || (!unicodeStr.Contains("\\u") && !unicodeStr.Contains("\\U")))
            {
                return unicodeStr;
            }

            string newStr = Regex.Replace(unicodeStr, @"\\[uU](.{4})", (m) =>
            {
                string unicode = m.Groups[1].Value;
                if (int.TryParse(unicode, System.Globalization.NumberStyles.HexNumber, null, out int temp))
                {
                    return ((char)temp).ToString();
                }
                else
                {
                    return m.Groups[0].Value;
                }
            }, RegexOptions.Singleline);

            return newStr;
        }



    }
}