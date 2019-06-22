using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web_backhand.Models;
using Dapper;
using MySql.Data.MySqlClient;

namespace Web_backhand.Dao
{
    public class UserDao
    {
        private string ConnStr = "server=120.79.83.167;uid=root;pwd=YangGuangda2019@;database=Blog;charset=utf8;sslMode=None";


        // 通过id获取用户
        public IEnumerable<User> getUserByID(int id)
        {
            MySqlConnection con = new MySqlConnection(ConnStr);

            Console.WriteLine("id:{0}", id);

            IEnumerable<User> list = con.Query<User>("select * from User where userID = @ID", new {ID = id }); //查询数据

            con.Close();

            return list;
        }

        // 通过用户名获取用户
        public IEnumerable<User> getUserByUsername(string username)
        {
            MySqlConnection con = new MySqlConnection(ConnStr);

            IEnumerable<User> list = con.Query<User>("select * from User where username = @Username"
                , new { Username = username }); //查询数据

            con.Close();

            return list;
        }

        // 新增用户
        public int addUser(string username, string password)
        {
            MySqlConnection con = new MySqlConnection(ConnStr);

            int result = con.Execute("Insert into User(username,password) " +
                "values(@Username,@Password)", new { Username = username , Password = password}); 

            con.Close();

            return result;
        }
    }
}
