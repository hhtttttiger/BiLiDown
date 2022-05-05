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
        private ObservableCollection<VideoGridData> videoGridDatas = new ObservableCollection<VideoGridData>();
        private bool All_Click_Tag = false; //是否全选标识
        private string VideoFormat = "mp4";

        public MainWindow(IBHttp bHttp)
        {
            InitializeComponent();
            _bHttp = bHttp;

            VideoFormat = this.VideoFormatSel.Text;
            this.SavePathText.Text = AppDomain.CurrentDomain.SetupInformation.ApplicationBase; //保存路径，默认程序路径
        }

        private async void UrlParse_Click(object sender, RoutedEventArgs e)
        {
#if DEBUG
            this.UrlText.Text = "https://www.bilibili.com/video/BV15D4y1S76Z#reply109762458112";
#endif

            //提取BV号
            var url = this.UrlText.Text;
            var bvWhitPata = url.Split('/')[4];
            var bv = bvWhitPata.Split('?')[0];
            bv = bv.Split('#')[0];

            //获取视频基本信息
            var videoInfo = await _bHttp.GetVideoInfo(bv);
            videoInfo.data.pages.ForEach(x =>
            {
                videoGridDatas.Add(new VideoGridData
                {
                    BVId = bv,
                    Title = x.part,
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
                await Aria2c.DownloadFileByAria2Async(x.VideoPlayUrl, save + ".tmp", async (s, e) =>
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
                            if(rbraces1 == "OK")
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
                                await this.Dispatcher.BeginInvoke(() => x.Schedule = $"{rbraces1}%");
                            }
                        }
                            
                    }
                });
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
            System.Windows.Forms.FolderBrowserDialog openFileDialog = new System.Windows.Forms.FolderBrowserDialog();  //选择文件夹
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
    }
}
