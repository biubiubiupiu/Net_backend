using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Web_backhand.Models;
using Web_backhand.Service;
using Web_backhand.Utils;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Web_backhand.Controllers
{
    [Route("api/[controller]")]
    public class LoginController : Controller
    {
        private readonly UserService userService; 

        public LoginController()
        {
            userService = new UserService();
        }

        // GET: api/<controller>
        [HttpGet]
        public Result Get()
        {
            Console.WriteLine("!!!!!!!!!{0}", AppContext.BaseDirectory);
            return ResultFactory.buildSuccessResult("helloworld!");
        }

        // GET api/<controller>/5
        [HttpGet("{id}")]
        public Result Get(int id)
        {
            return ResultFactory.buildSuccessResult("helloworld!");
        }

        // POST api/<controller>
        [HttpPost]
        public Result Post([FromBody]LoginForm loginForm)
        {
            
            if(loginForm == null) return ResultFactory.buildFailResult("用户名或密码不正确");

            Console.WriteLine("helloworld");

            User user = userService.getUserByUsername(loginForm.username);
            if (user != null && user.password == loginForm.password)
            {
                if (HttpContext.Request.Cookies.ContainsKey(loginForm.username))
                    HttpContext.Response.Cookies.Delete(loginForm.username);

                CookieOptions cookie = new CookieOptions();
                cookie.Path = "/";
                cookie.Expires = DateTimeOffset.Now.AddDays(1);
                cookie.HttpOnly = false;
                cookie.Secure = false;
                cookie.SameSite = SameSiteMode.None;
                HttpContext.Response.Cookies.Append("loginStatus", loginForm.username + "_" + user.userID, cookie);
                return ResultFactory.buildSuccessResult("登录成功");
            }
            else
                return ResultFactory.buildFailResult("用户名或密码不正确");
        }

        // PUT api/<controller>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/<controller>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
