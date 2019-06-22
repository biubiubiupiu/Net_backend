using Dapper;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web_backhand.Models;

namespace Web_backhand.Dao
{
    public class HomeDao
    {
        private string ConnStr = "server=120.79.83.167;uid=root;pwd=YangGuangda2019@;database=Blog;charset=utf8;sslMode=None";

        private const int size = 20; 

        public List<Post> getPostBySearch(string searchText, int page)
        {
            Console.WriteLine("==================1111=============");
            Console.WriteLine(searchText);
            Console.WriteLine(page);

            MySqlConnection con = new MySqlConnection(ConnStr);

            if (page < 1) page = 1;
            int offset = 20 * (page - 1);

            string sql = "select * from Blog.Post where title LIKE @SearchText order by time desc limit @Offset,@Size";
            if(searchText == null || searchText == "")
                sql = "select * from Blog.Post order by time desc limit @Offset,@Size";

            Console.WriteLine("==================222=============");
            Console.WriteLine(sql);

            IEnumerable<Post> list = con.Query<Post>(sql, new { SearchText = "%" + searchText + "%", Offset = offset, Size = size }); 


            con.Close();

            return list.AsList();
        }

        public int getPostTotal(string searchText)
        {
            MySqlConnection con = new MySqlConnection(ConnStr);

            string sql = "select count(*) from Post where title LIKE '%@SearchText%'";

            if (searchText == null || searchText == "")
                sql = "select count(*) from Post";

            IEnumerable<int> total = con.Query<int>(sql, new { SearchText = searchText }); //查询数据

            con.Close();

            return total.FirstOrDefault();
        }

        public List<Post> getHot()
        {
            MySqlConnection con = new MySqlConnection(ConnStr);

            string sql = "select * from Blog.Post order by Post.reads desc limit @num";
            Console.WriteLine(sql);

            IEnumerable<Post> list = con.Query<Post>(sql, new { num = 5});

            return list.AsList();
        }
    }
}
