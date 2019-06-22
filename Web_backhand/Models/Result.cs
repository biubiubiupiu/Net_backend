using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web_backhand.Models
{
    public class Result
    {
        /*
         *  成功
         *  SUCCESS(200),
         * 
         *  失败
         *  FAIL(400),
         * 
         *  未认证（签名错误）
         *  UNAUTHORIZED(401),
         */
        public int code { get; set; }  

        // 消息提示
        public string message { get; set; }

        // 数据
        public object data { get; set; } 

        public Result(int resultCode, string message, object data)
        {
            this.code = resultCode;
            this.message = message;
            this.data = data;
        }
    }
}
