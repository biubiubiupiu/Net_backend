using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web_backhand.Dao;
using Web_backhand.Models;

namespace Web_backhand.Service
{
    public class HomeService
    {
        private readonly HomeDao homeDao;

        public HomeService()
        {
            homeDao = new HomeDao();
        }

        public List<Post> getPostBySearch(string searchText, int page)
        {
            return homeDao.getPostBySearch(searchText, page);
        }

        public int getPostTotal(string searchText)
        {
            return homeDao.getPostTotal(searchText);
        }

        public List<Post> getHot()
        {
            return homeDao.getHot();
        }
    }
}
