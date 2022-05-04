using System.ComponentModel;

namespace BiLiPrometheus.Models
{
    public class VideoGridData : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private string? title;
        /// <summary>
        /// 视频标题
        /// </summary>
        public string Title
        {
            get { return title; }

            set
            {
                title = value;
                if (PropertyChanged != null)
                {
                    PropertyChanged.Invoke(this, new PropertyChangedEventArgs("Title"));
                }
            }
        }

        /// <summary>
        /// cid
        /// </summary>
        public int cId { get; set; }

        /// <summary>
        /// 视频流地址
        /// </summary>
        public string VideoPlayUrl { get; set; }

        /// <summary>
        /// 视频首屏画面
        /// </summary>
        public string FirstFrame { get; set; }

        /// <summary>
        /// 视频BV
        /// </summary>
        public string BVId { get; set; }

        private bool isChecked;
        /// <summary>
        /// 是否已选
        /// </summary>
        public bool IsChecked {
           get { return isChecked; }

            set
            {
                isChecked = value;
                if (PropertyChanged != null)
                {
                    PropertyChanged.Invoke(this, new PropertyChangedEventArgs("IsChecked"));
                }
            }
        }


        private string? schedule { get; set; }

        /// <summary>
        /// 下载进度
        /// </summary>
        public string Schedule
        {
            get { return schedule; }

            set
            {
                schedule = value;
                if (PropertyChanged != null)
                {
                    PropertyChanged.Invoke(this, new PropertyChangedEventArgs("Schedule"));
                }
            }
        }
    }
}
