namespace BiLiPrometheus.Models
{
    public class VideoPlay
    {
        /// <summary>
        /// 
        /// </summary>
        public string from { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string result { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string message { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int quality { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string format { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int timelength { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string accept_format { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public List<string> accept_description { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public List<int> accept_quality { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int video_codecid { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string seek_param { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string seek_type { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public List<DurlItem> durl { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public List<Support_formatsItem> support_formats { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string high_format { get; set; }
    }
    public class DurlItem
    {
        /// <summary>
        /// 
        /// </summary>
        public int order { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int length { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int size { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string ahead { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string vhead { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string url { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public List<string> backup_url { get; set; }
    }

    public class Support_formatsItem
    {
        /// <summary>
        /// 
        /// </summary>
        public int quality { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string format { get; set; }
        /// <summary>
        /// 1080P 高码率
        /// </summary>
        public string new_description { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string display_desc { get; set; }
        /// <summary>
        /// 高码率
        /// </summary>
        public string superscript { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string codecs { get; set; }
    }
}
