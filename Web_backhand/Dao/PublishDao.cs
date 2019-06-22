using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web_backhand.Models;
using Dapper;
using MySql.Data.MySqlClient;

namespace Web_backhand.Dao
{
    public class PublishDao
    {
        private string ConnStr = "server=120.79.83.167;uid=root;pwd=YangGuangda2019@;database=Blog;charset=utf8;sslMode=None";

        // 新增帖子
        public int addPost(int editor, string detailTitle, string detailContent)
        {
            MySqlConnection con = new MySqlConnection(ConnStr);

            int result = con.Execute("Insert into Post(editor,title,content) " +
                "values(@Editor,@Title,@Content)", new { Editor = editor, Title = detailTitle, Content = detailContent });

            con.Close();

            return result;
        }

    }
}
