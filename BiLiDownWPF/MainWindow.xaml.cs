using BiLiPrometheus;
using BiLiPrometheus.Models;
using BiLiPrometheus.Util;
using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace BiLiDownWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly IBHttp _bHttp;
        private readonly HttpClientHelper _httpClientHelper;
        private ObservableCollection<VideoGridData> videoGridDatas = new ObservableCollection<VideoGridData>();
        private bool All_Click_Tag = false; //是否全选标识
        private string VideoFormat = "mp4";

        public MainWindow(IBHttp bHttp, HttpClientHelper httpClientHelper)
        {
            InitializeComponent();
            _bHttp = bHttp;
            _httpClientHelper = httpClientHelper;

            VideoFormat = this.VideoFormatSel.Text;
            this.SavePathText.Text = AppDomain.CurrentDomain.SetupInformation.ApplicationBase; //保存路径，默认程序路径
        }

        private async void UrlParse_Click(object sender, RoutedEventArgs e)
        {
#if DEBUG
            //this.UrlText.Text = "https://www.bilibili.com/video/BV15D4y1S76Z#reply109762458112";
#endif

            //提取BV号
            var url = this.UrlText.Text;
            var bv = "";

            try
            {
                var bvWhitPata = url.Split('/')[4];
                bv = bvWhitPata.Split('?')[0];
                bv = bv.Split('#')[0];
            }
            catch (Exception ex)
            {
                MessageBox.Show("请输入正确的url，例：https://www.bilibili.com/video/BV1hT4y1v7Vg");
                return;
            }

            //获取视频基本信息
            var videoInfo = await _bHttp.GetVideoInfo(bv);
            videoInfo.data.pages.ForEach(x =>
            {
                var title = videoInfo.data.pages.Count == 1 ? videoInfo.data.Title : x.part;
                videoGridDatas.Add(new VideoGridData
                {
                    BVId = bv,
                    Title = title,
                    cId = x.cid,
                    FirstFrame = x.first_frame
                });
            });

            this.VideoGrid.ItemsSource = videoGridDatas;
        }


        private void CheckAll_Click(object sender, RoutedEventArgs e)
        {
            All_Click_Tag = !All_Click_Tag;

            foreach(var item in videoGridDatas)
            {
                item.IsChecked = All_Click_Tag;
            }
            this.VideoGrid.ItemsSource = videoGridDatas;
        }

        /// <summary>
        /// 下载所选按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void DownBtn_Click(object sender, RoutedEventArgs e)
        {
            var downList = videoGridDatas.Where(x => x.IsChecked).ToList();

            //获取视频流地址
            var getPlayTask = downList.Select(async x =>
            {
                await this.Dispatcher.BeginInvoke(() => x.Schedule = "获取视频流地址...");

                VideoPlayRes? videoPlay = await _bHttp.GetVideoPlay(new VideoPlayReq
                {
                    bvid = x.BVId,
                    cid = x.cId
                });

                x.VideoPlayUrl = videoPlay.data.durl[0].url;

                await this.Dispatcher.BeginInvoke(() => x.Schedule = "获取视频流完成，开始下载...");
            });
            await Task.WhenAll(getPlayTask);

            //用于正则提取
            string re1 = ".*?";
            string re2 = "(\\(.*\\))";
            var downTask = downList.Select(async x =>
            {
                var save = $@"{this.SavePathText.Text}{x.Title}";

                //使用Aria2c下载
                if (this.Aria2cChe.IsChecked.Value)
                {
                    var aria2c = this.Aria2cPath.Text;
                    var ffmpeg = this.FFmpegPath.Text;

                    await Aria2c.DownloadFileByAria2Async(x.VideoPlayUrl, save + ".tmp", aria2c, async (s, e) =>
                    {
                        if (e.Data == null) return;

                        //标识为已下载
                        await this.Dispatcher.BeginInvoke(() => x.IsStart = true);

                        var r = new Regex(re1 + re2, RegexOptions.IgnoreCase | RegexOptions.Singleline);
                        var m = r.Match(e.Data);
                        if (m.Success)
                        {
                            //正则匹配aria的返回获取进度
                            var rbraces1 = m.Groups[1].ToString().Replace("(", "").Replace(")", "").Replace("%", "").Replace("s", "");
                            if (!string.IsNullOrEmpty(rbraces1))
                            {
                                if (rbraces1 == "OK")
                                {
                                    await this.Dispatcher.BeginInvoke(() => x.Schedule = "100%");

                                    if (File.Exists(save + ".tmp"))
                                    {
                                        //存在 
                                        await this.Dispatcher.BeginInvoke(() => x.Schedule = "正在视频转码...");
                                       
                                        await FFmpeg.RunFFmpeg($"-loglevel warning -y  -i \"{save}.tmp\"  -disposition:v:1 attached_pic  -metadata title=\"{save}.mp4\" -metadata description=\"\" -metadata album=\"\" -c copy  -c:s mov_text -movflags faststart -strict unofficial \"{save}.{VideoFormat}\"", ffmpeg);

                                        File.Delete(save + ".tmp");
                                        await this.Dispatcher.BeginInvoke(() => x.Schedule = "视频转码完成！");
                                    }
                                }
                                else
                                {
                                    await this.Dispatcher.BeginInvoke(() => x.Schedule = $"{rbraces1}%");
                                }
                            }

                        }
                    });
                }
                else
                {
                    await _httpClientHelper.DownWithGetAsync(x.VideoPlayUrl, save + ".tmp", async (z) =>
                    {
                        if(z == 1)
                        {
                            await this.Dispatcher.BeginInvoke(() => x.Schedule = "100%");

                            if (File.Exists(save + ".tmp"))
                            {
                                //存在 
                                await this.Dispatcher.BeginInvoke(() => x.Schedule = "正在视频转码...");
                                await FFmpeg.RunFFmpeg($"-loglevel warning -y  -i \"{save}.tmp\"  -disposition:v:1 attached_pic  -metadata title=\"{save}.mp4\" -metadata description=\"\" -metadata album=\"\" -c copy  -c:s mov_text -movflags faststart -strict unofficial \"{save}.{VideoFormat}\"");

                                File.Delete(save + ".tmp");
                                await this.Dispatcher.BeginInvoke(() => x.Schedule = "视频转码完成！");
                            }
                        }
                        else
                        {
                            await this.Dispatcher.BeginInvoke(() => x.Schedule = z.ToString("P"));
                        }                        
                    });
                }
            });

            await Task.WhenAll(downTask);
        }

        /// <summary>
        /// 设置文件保存路径
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SavePathBtn_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Forms.FolderBrowserDialog openFileDialog = new System.Windows.Forms.FolderBrowserDialog();
            if (openFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                this.SavePathText.Text = openFileDialog.SelectedPath;
            }
        }

        /// <summary>
        /// 转码格式选择
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void VideoFormatSel_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            var item = (System.Windows.Controls.ComboBoxItem)e.AddedItems[0];
            VideoFormat = item.Content == null ? "" : item.Content.ToString();
        }

        /// <summary>
        /// Ari2c选择
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Aria2cSel_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Forms.OpenFileDialog openFileDialog = new System.Windows.Forms.OpenFileDialog();
            if (openFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                this.Aria2cPath.Text = openFileDialog.FileName;
            }
        }

        /// <summary>
        /// FFmpeg路劲选择
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FFmpegSel_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Forms.OpenFileDialog openFileDialog = new System.Windows.Forms.OpenFileDialog();
            if (openFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                this.FFmpegPath.Text = openFileDialog.FileName;
            }
        }
    }
}
