using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web_backhand.Dao;
using Web_backhand.Models;
using Dapper;

namespace Web_backhand.Service
{
    public class DetailService
    {
        private readonly DetailDao detailDao;

        public DetailService()
        {
            detailDao = new DetailDao();
        }

        public Post getDetail(int id)
        {
            IEnumerable<Post> list = detailDao.getDetail(id);
            return list.FirstOrDefault();
        }

        public bool checkFavorite(int pid, int uid)
        {
            if (detailDao.checkFavorite(pid, uid) > 0) return true;
            return false;
        }

        public bool addComment(int postID, string commenter, string content)
        {
            if (detailDao.addComment(postID, commenter, content) > 0) return true;
            return false;
        }

        public List<CommentInfo> getComments(int id)
        {
            return detailDao.getComments(id).AsList();
        }

        public bool addFavorite(int postID, int userID)
        {
            if (detailDao.addFavorite(postID, userID) > 0) return true;
            return false;
        }
    }
}
