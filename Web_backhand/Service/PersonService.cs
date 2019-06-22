using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web_backhand.Dao;
using Web_backhand.Models;

namespace Web_backhand.Service
{
    public class PersonService
    {
        private readonly PersonDao personDao;

        public PersonService()
        {
            personDao = new PersonDao();
        }

        public List<Post> getLike(int uid)
        {
            return personDao.getLike(uid);
        }

        public List<Post> getPublish(int uid)
        {
            return personDao.getPublish(uid);
        }

        public bool saveInfo(int uid, string introduction)
        {
            if (personDao.saveInfo(uid, introduction) > 0) return true;
            return false;
        }

        public bool cancelFavorite(int pid, int uid)
        {
            if (personDao.deleteFavorite(pid, uid) > 0 && personDao.reduceFavorite(pid) > 0)
                return true;
            else return false;
        }

        public bool checkEditor(int pid, int uid)
        {
            if (personDao.checkEditor(pid, uid) > 0)
                return true;
            else return false;
        }

        public void deletePost(int pid)
        {
            personDao.deletePost(pid);
        }
    }
}
