using Dapper;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web_backhand.Models;

namespace Web_backhand.Dao
{
    public class DetailDao
    {
        private string ConnStr = "server=120.79.83.167;uid=root;pwd=YangGuangda2019@;database=Blog;charset=utf8;sslMode=None";

        public IEnumerable<Post> getDetail(int id)
        {
            MySqlConnection con = new MySqlConnection(ConnStr);

            con.Execute("update Post set Post.reads = Post.reads + 1 where postID = @ID", new { ID = id});

            IEnumerable<Post> list = con.Query<Post>("select * from Post where postID = @ID "
                , new { ID = id }); //查询数据

            con.Close();

            return list;
        }

        public int checkFavorite(int pid, int uid)
        {
            MySqlConnection con = new MySqlConnection(ConnStr);

            IEnumerable<int> total = con.Query<int>("select count(*) from Favorite where postID = @Pid and userID = @Uid"
                , new { Pid = pid, Uid = uid }); //查询数据

            con.Close();

            return total.FirstOrDefault();
        }

        public int addComment(int postID, string commenter, string content)
        {
            MySqlConnection con = new MySqlConnection(ConnStr);

            int result = con.Execute("Insert into Comment(postID,commenter,content) " +
                "values(@PostID,@Commenter,@Content)", new { PostID = postID, Commenter = commenter, Content = content });

            con.Close();

            return result;
        }

        public IEnumerable<CommentInfo> getComments(int id)
        {
            MySqlConnection con = new MySqlConnection(ConnStr);

            IEnumerable<CommentInfo> list = con.Query<CommentInfo>("select commenter,content,time from Comment " +
                "where postID = @Id", new { Id = id});

            con.Close();

            return list;
        }

        public int addFavorite(int postID, int userID)
        {
            MySqlConnection con = new MySqlConnection(ConnStr);

            con.Execute("update Post set Post.favorites = Post.favorites + 1 where postID = @ID", new { ID = postID });

            int result = con.Execute("Insert into Favorite(postID,userID) " +
                "values(@PostID,@UserID)", new { PostID = postID, UserID = userID});

            con.Close();

            return result;
        }
    }
}
