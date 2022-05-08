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

            //BV1ra411B73z
            Console.WriteLine("输入BV号");
            var bv = Console.ReadLine();

#if DEBUG
            bv = "BV1ra411B73z";
#endif

            ServiceCollection services = new ServiceCollection();
            services.AddHttpApi<IBHttp>();
            services.AddHttpClient();
            services.AddTransient<HttpClientHelper>();
            
            var provider = services.BuildServiceProvider();
            var http = provider.GetService<IBHttp>();
            var downHelper = provider.GetService<HttpClientHelper>();
            

            var videoInfo = await http.GetVideoInfo(bv);

            var videoPlay = await http.GetVideoPlay(new VideoPlayReq
            {
                bvid = bv,
                cid = videoInfo.data.cid
            });

            var url = videoPlay.data.durl[0].url;

            var save = $@"D:\{videoInfo.data.Title.Trim()}.flv";

            //await Aria2c.Download(url, save);
            await downHelper.DownWithGetAsync(url, save, (double x) =>
            {
                Console.WriteLine(x);
            });

            Console.ReadLine();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
       Host.CreateDefaultBuilder(args)
           .ConfigureServices((hostContext, services) =>
           {
               services.AddHttpApi<IBHttp>();
           });
    }
}