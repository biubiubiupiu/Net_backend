using Dapper;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web_backhand.Models;

namespace Web_backhand.Dao
{
    public class PersonDao
    {
        private string ConnStr = "server=120.79.83.167;uid=root;pwd=YangGuangda2019@;database=Blog;charset=utf8;sslMode=None";

        public List<Post> getLike(int uid)
        {

            MySqlConnection con = new MySqlConnection(ConnStr);

            string sql = "select * from Post where postID in (" +
                "select postID from Favorite where userID = @ID) order by time desc";

            Console.WriteLine(sql);

            IEnumerable<Post> list = con.Query<Post>(sql, new { ID = uid});

            con.Close();

            return list.AsList();
        }

        public List<Post> getPublish(int uid)
        {

            MySqlConnection con = new MySqlConnection(ConnStr);

            string sql = "select * from Post where editor = @ID order by time desc";

            Console.WriteLine(sql);

            IEnumerable<Post> list = con.Query<Post>(sql, new { ID = uid });

            con.Close();

            return list.AsList();
        }

        public int saveInfo(int uid, string introduction)
        {
            MySqlConnection con = new MySqlConnection(ConnStr);

            string sql = "update User set introduction = @Introduction where userID = @ID";

            Console.WriteLine(sql);

            int result = con.Execute(sql, new { ID = uid, Introduction = introduction });

            con.Close();

            return result;
        }
           
        public int deleteFavorite(int pid, int uid)
        {
            MySqlConnection con = new MySqlConnection(ConnStr);

            string sql = "delete from Favorite where postID = @Pid and userID = @Uid";

            int result = con.Execute(sql, new { Pid = pid, Uid = uid });
            Console.WriteLine("==================");
            Console.WriteLine("deleteFavorite:" + result);
            Console.WriteLine("==================");
            con.Close();

            return result;
        }

        public int reduceFavorite(int pid)
        {
            MySqlConnection con = new MySqlConnection(ConnStr);

            string sql = "update Post Set favorites = favorites - 1 where postID = @Pid";

            int result = con.Execute(sql, new { Pid = pid});
            Console.WriteLine("==================");
            Console.WriteLine("reduceFavorite:" + result);
            Console.WriteLine("==================");
            con.Close();

            return result;
        }

        public int checkEditor(int pid, int uid)
        {
            MySqlConnection con = new MySqlConnection(ConnStr);

            string sql = "select count(*) from Post where postID = @Pid and editor = @Uid";
            IEnumerable<int> result = con.Query<int>(sql, new { Pid = pid , Uid = uid});
            Console.WriteLine("==================");
            Console.WriteLine("checkEditor:" + result.FirstOrDefault());
            Console.WriteLine("==================");
            con.Close();

            return result.FirstOrDefault();
        }

        public void deletePost(int pid)
        {
            MySqlConnection con = new MySqlConnection(ConnStr);

            string sql = "delete from Favorite where postID = @Pid";
            int result = con.Execute(sql, new { Pid = pid});
            Console.WriteLine("==================");
            Console.WriteLine("deleteFavorite:" + result);
            Console.WriteLine("==================");

            sql = "delete from Comment where postID = @Pid";
            result = con.Execute(sql, new { Pid = pid });
            Console.WriteLine("==================");
            Console.WriteLine("deleteComment:" + result);
            Console.WriteLine("==================");

            sql = "delete from Post where postID = @Pid";
            result = con.Execute(sql, new { Pid = pid });
            Console.WriteLine("==================");
            Console.WriteLine("deletePost:" + result);
            Console.WriteLine("==================");

            con.Close();

        }
    }
}
