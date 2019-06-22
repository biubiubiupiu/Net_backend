using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web_backhand.Models
{
    public class Post
    {
        //帖子id
        public int postID { get; set; }
        //作者id
        public int editor { get; set; }
        //标题
        public string title { get; set; }
        //内容
        public string content { get; set; }
        //阅读量
        public int reads { get; set; }
        //收藏
        public int favorites { get; set; }
        //时间
        public DateTime time { get; set; }
        //首页图片
        public string imgURL { get; set; }
    }
}
