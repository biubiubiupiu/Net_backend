using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web_backhand.Models;

namespace Web_backhand.Utils
{
    public class ResultFactory
    {
        public static Result buildSuccessResult(object data)
        {
            return buildResult(200, "成功", data);
        }

        public static Result buildFailResult(string message)
        {
            return buildResult(400, message, null);
        }

        public static Result buildAuthFailResult(string message)
        {
            return buildResult(401, message, null);
        }

        public static Result buildResult(int resultCode, string message, object data)
        {
            return new Result(resultCode, message, data);
        }
    }
}
