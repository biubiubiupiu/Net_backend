using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Web_backhand.Models;
using Web_backhand.Service;
using Web_backhand.Utils;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Web_backhand.Controllers
{
    [Route("api/[controller]")]
    public class SignupController : Controller
    {
        private readonly UserService userService;

        public SignupController()
        {
            userService = new UserService();
        }

        // GET: api/<controller>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<controller>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<controller>
        [HttpPost]
        public Result Post([FromBody]SignupForm signupForm)
        {
            if (signupForm.password != signupForm.password2) return ResultFactory.buildFailResult("两次密码不一致！");

            User user = userService.getUserByUsername(signupForm.username);
            if(user != null)return ResultFactory.buildFailResult("该用户名已存在！");

            if (userService.addUser(signupForm.username, signupForm.password))
                return ResultFactory.buildSuccessResult("注册成功！");
            else
                return ResultFactory.buildFailResult("出现未知错误!");
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
