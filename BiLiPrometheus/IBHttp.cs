
using BiLiPrometheus.Models;
using WebApiClient;
using WebApiClient.Attributes;

namespace BiLiPrometheus
{
    [HttpHost("https://api.bilibili.com/")]
    public interface IBHttp : IHttpApi
    {
        /// <summary>
        /// 视频信息
        /// </summary>
        /// <param name="bvid"></param>
        /// <returns></returns>
        [HttpGet("x/web-interface/view")]
        Task<VideoInfoRes> GetVideoInfo(string bvid);

        /// <summary>
        /// 视频播放信息
        /// </summary>
        /// <param name="bvid"></param>
        /// <returns></returns>
        [HttpGet("x/player/playurl")]
        Task<VideoPlayRes> GetVideoPlay([PathQuery]VideoPlayReq res);
    }
}
