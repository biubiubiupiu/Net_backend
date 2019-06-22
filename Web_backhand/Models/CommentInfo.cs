using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web_backhand.Models
{
    public class CommentInfo
    {
        //评论人
        public string commenter { get; set; }
        //内容
        public string content { get; set; }
        //时间
        public DateTime time { get; set; }
    }
}
