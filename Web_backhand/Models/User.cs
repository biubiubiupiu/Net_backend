using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using MySql.Data.EntityFrameworkCore;

namespace Web_backhand.Models
{
    public class User
    {
        [Key]
        //用户Id
        public int userID { get; set; }
        //用户名
        public string username { get; set; }
        //用户密码
        public string password { get; set; }
        //用户昵称
        public string nickname { get; set; }
        //简介
        public string introduction { get; set; }
    }
}
