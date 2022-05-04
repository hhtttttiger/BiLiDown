namespace BiLiPrometheus.Models
{
    public class VideoInfo
    {
        /// <summary>
        /// 
        /// </summary>
        public string bvid { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int aid { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int videos { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int tid { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string tname { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int copyright { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string pic { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int pubdate { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int ctime { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string desc { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public List<Desc_v2Item> desc_v2 { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int state { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int duration { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int mission_id { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public Rights rights { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public Owner owner { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public Stat stat { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string dynamic { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int cid { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public Dimension dimension { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int season_id { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string premiere { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string no_cache { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public List<PagesItem> pages { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public Subtitle subtitle { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public Ugc_season ugc_season { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string is_season_display { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public User_garb user_garb { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public Honor_reply honor_reply { get; set; }
    }

    public class Desc_v2Item
    {
        /// <summary>
        /// 首先是犯下了傲慢之罪的1.04补丁
        /// </summary>
        public string raw_text { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int type { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int biz_id { get; set; }
    }

    public class Rights
    {
        /// <summary>
        /// 
        /// </summary>
        public int bp { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int elec { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int download { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int movie { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int pay { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int hd5 { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int no_reprint { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int autoplay { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int ugc_pay { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int is_cooperation { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int ugc_pay_preview { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int no_background { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int clean_mode { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int is_stein_gate { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int is_360 { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int no_share { get; set; }
    }

    public class Owner
    {
        /// <summary>
        /// 
        /// </summary>
        public int mid { get; set; }
        /// <summary>
        /// 熊人族嗷非秀
        /// </summary>
        public string name { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string face { get; set; }
    }

    public class Stat
    {
        /// <summary>
        /// 
        /// </summary>
        public int aid { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int view { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int danmaku { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int reply { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int favorite { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int coin { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int share { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int now_rank { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int his_rank { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int like { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int dislike { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string evaluation { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string argue_msg { get; set; }
    }

    public class Dimension
    {
        /// <summary>
        /// 
        /// </summary>
        public int width { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int height { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int rotate { get; set; }
    }

    public class PagesItem
    {
        /// <summary>
        /// 
        /// </summary>
        public int cid { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int page { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string from { get; set; }
        /// <summary>
        /// 未命名
        /// </summary>
        public string part { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int duration { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string vid { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string weblink { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public Dimension dimension { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string first_frame { get; set; }
    }

    public class Subtitle
    {
        /// <summary>
        /// 
        /// </summary>
        public string allow_submit { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public List<string> list { get; set; }
    }

    public class Author
    {
        /// <summary>
        /// 
        /// </summary>
        public int mid { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string name { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string face { get; set; }
    }



    public class Arc
    {
        /// <summary>
        /// 
        /// </summary>
        public int aid { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int videos { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int type_id { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string type_name { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int copyright { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string pic { get; set; }
        /// <summary>
        /// [邪道]《老头环》首个爬山邪道 双基佬AI掉线
        /// </summary>
        public string title { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int pubdate { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int ctime { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string desc { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int state { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int duration { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public Rights rights { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public Author author { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public Stat stat { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string dynamic { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public Dimension dimension { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string desc_v2 { get; set; }
    }


    public class Page
    {
        /// <summary>
        /// 
        /// </summary>
        public int cid { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int page { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string from { get; set; }
        /// <summary>
        /// 《老头环》爬山邪道双基佬罚站
        /// </summary>
        public string part { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int duration { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string vid { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string weblink { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public Dimension dimension { get; set; }
    }

    public class EpisodesItem
    {
        /// <summary>
        /// 
        /// </summary>
        public int season_id { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int section_id { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int id { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int aid { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int cid { get; set; }
        /// <summary>
        /// [邪道]《老头环》首个爬山邪道 双基佬AI掉线
        /// </summary>
        public string title { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int attribute { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public Arc arc { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public Page page { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string bvid { get; set; }
    }

    public class SectionsItem
    {
        /// <summary>
        /// 
        /// </summary>
        public int season_id { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int id { get; set; }
        /// <summary>
        /// 正片
        /// </summary>
        public string title { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int type { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public List<EpisodesItem> episodes { get; set; }
    }


    public class Ugc_season
    {
        /// <summary>
        /// 
        /// </summary>
        public int id { get; set; }
        /// <summary>
        /// 艾尔登法环
        /// </summary>
        public string title { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string cover { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int mid { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string intro { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int sign_state { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int attribute { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public List<SectionsItem> sections { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public Stat stat { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int ep_count { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int season_type { get; set; }
    }

    public class User_garb
    {
        /// <summary>
        /// 
        /// </summary>
        public string url_image_ani_cut { get; set; }
    }

    public class Honor_reply
    {
    }
}
