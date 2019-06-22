using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web_backhand.Dao;
using Web_backhand.Models;

namespace Web_backhand.Service
{
    public class UserService
    {
        private readonly UserDao userDao;

        public UserService()
        {
            userDao = new UserDao();
        }

        public User getUserByID(int id)
        {
            User user = userDao.getUserByID(id).FirstOrDefault();

            return user;
        }

        public User getUserByUsername(string username)
        {
            User user = userDao.getUserByUsername(username).FirstOrDefault();

            return user;
        }

        public bool addUser(string username, string password)
        {
            if (userDao.addUser(username, password) > 0) return true;
            else return false;
        }
    }
}
