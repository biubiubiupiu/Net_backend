using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web_backhand.Dao;
using Web_backhand.Models;

namespace Web_backhand.Service
{
    public class PublishService
    {
        private readonly PublishDao publishDao;

        public PublishService()
        {
            publishDao = new PublishDao();
        }

        public bool addPost(int editor, string detailTitle, string detailContent)
        {
            if (publishDao.addPost(editor, detailTitle, detailContent) > 0) return true;
            else return false;
        }
    }
}
