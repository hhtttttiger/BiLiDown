using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BiLiPrometheus.Util
{
    public static class FFmpeg
    {
        public static async Task RunFFmpeg( string parms, string exe = "ffmpeg")
        {
            if (string.IsNullOrEmpty(exe))
                exe = "ffmpeg";

            void Output(object sender, DataReceivedEventArgs args)
            {
                if (string.IsNullOrWhiteSpace(args.Data))
                {
                    return;
                }
                Console.WriteLine("ffmpeg:{0}", args.Data?.Trim());
            }

            var info = new ProcessStartInfo(exe, parms)
            {
                UseShellExecute = false,
                CreateNoWindow = true,
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                StandardErrorEncoding = Encoding.UTF8
        };

            using (Process p = new Process() { StartInfo = info, EnableRaisingEvents = true })
            {
                if (!p.Start())
                {
                    throw new Exception("FFmpeg 启动失败");
                }
                p.ErrorDataReceived += Output;
                p.OutputDataReceived += Output;
                p.BeginOutputReadLine();
                p.BeginErrorReadLine();
                await p.WaitForExitAsync();
                p.OutputDataReceived -= Output;
                p.ErrorDataReceived -= Output;
            }
        }
    }
}
