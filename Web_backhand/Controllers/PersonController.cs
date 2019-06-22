using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Web_backhand.Models;
using Web_backhand.Service;
using Web_backhand.Utils;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Web_backhand.Controllers
{
    [Route("api/[controller]")]
    public class PersonController : Controller
    {
        private readonly PersonService personService;

        private readonly UserService userService;

        private readonly DetailService detailService;

        private readonly IHostingEnvironment _hostingEnvironment;

        public PersonController(IHostingEnvironment hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment;
            personService = new PersonService();
            userService = new UserService();
            detailService = new DetailService();
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

        [HttpGet("like")]
        public Result getLike()
        {
            if (!HttpContext.Request.Cookies.ContainsKey("loginStatus"))
                return ResultFactory.buildAuthFailResult("用户未登录！");
            string loginStatus = HttpContext.Request.Cookies["loginStatus"];
            int uid = int.Parse(loginStatus.Split("_")[1]);

            List<Post> posts = personService.getLike(uid);
            foreach (Post i in posts)
            {
                int index = i.content.IndexOf("https");
                if (index < 0)
                {
                    i.imgURL = "https://localhost:8889/imgs/default.png";
                    continue;
                }
                int index2 = index;
                while (i.content[index2] != '\"') index2++;
                i.imgURL = i.content.Substring(index, index2 - index);
            }

            return ResultFactory.buildSuccessResult(posts);
        }

        [HttpGet("publish")]
        public Result getPublish()
        {
            if (!HttpContext.Request.Cookies.ContainsKey("loginStatus"))
                return ResultFactory.buildAuthFailResult("用户未登录！");
            string loginStatus = HttpContext.Request.Cookies["loginStatus"];
            int uid = int.Parse(loginStatus.Split("_")[1]);

            List<Post> posts = personService.getPublish(uid);
            foreach (Post i in posts)
            {
                int index = i.content.IndexOf("https");
                if (index < 0)
                {
                    i.imgURL = "https://localhost:8889/imgs/default.png";
                    continue;
                }
                int index2 = index;
                while (i.content[index2] != '\"') index2++;
                i.imgURL = i.content.Substring(index, index2 - index);
            }

            return ResultFactory.buildSuccessResult(posts);
        }

        [HttpGet("info")]
        public Result getInfo()
        {
            if (!HttpContext.Request.Cookies.ContainsKey("loginStatus"))
                return ResultFactory.buildAuthFailResult("用户未登录！");
            string loginStatus = HttpContext.Request.Cookies["loginStatus"];
            int uid = int.Parse(loginStatus.Split("_")[1]);

            User user = userService.getUserByID(uid);
            user.password = "";
            return ResultFactory.buildSuccessResult(user);
        }

        [HttpPost("saveInfo")]
        public Result getInfo([FromBody]User user)
        {
            if (!HttpContext.Request.Cookies.ContainsKey("loginStatus"))
                return ResultFactory.buildAuthFailResult("用户未登录！");
            string loginStatus = HttpContext.Request.Cookies["loginStatus"];
            int uid = int.Parse(loginStatus.Split("_")[1]);

            if (personService.saveInfo(user.userID, user.introduction))
                return ResultFactory.buildSuccessResult("保存成功");
            else
                return ResultFactory.buildFailResult("保存失败");
        }

        [HttpPost("others/{id}")]
        public Result getOthers(int id)
        {
            if (!HttpContext.Request.Cookies.ContainsKey("loginStatus"))
                return ResultFactory.buildAuthFailResult("用户未登录！");
            string loginStatus = HttpContext.Request.Cookies["loginStatus"];
            int uid = int.Parse(loginStatus.Split("_")[1]);

            User user = userService.getUserByID(id);
            List<Post> posts = personService.getPublish(id);

            foreach (Post i in posts)
            {
                int index = i.content.IndexOf("https");
                if (index < 0)
                {
                    i.imgURL = "https://localhost:8889/imgs/default.png";
                    continue;
                }
                int index2 = index;
                while (i.content[index2] != '\"') index2++;
                i.imgURL = i.content.Substring(index, index2 - index);
            }

            return ResultFactory.buildSuccessResult(new {user.userID, user.username,
            user.introduction, posts});
        }

        // POST api/<controller>/cancel/{id}
        [HttpPost("cancel/{pid}")]
        public Result cancelFavorite(int pid)
        {
            if (!HttpContext.Request.Cookies.ContainsKey("loginStatus"))
                return ResultFactory.buildAuthFailResult("用户未登录！");
            string loginStatus = HttpContext.Request.Cookies["loginStatus"];
            int uid = int.Parse(loginStatus.Split("_")[1]);

            Console.WriteLine("==================");
            Console.WriteLine(pid + "," + uid);
            Console.WriteLine("==================");

            if (personService.cancelFavorite(pid, uid))
                return ResultFactory.buildSuccessResult("取消收藏成功!");
            else
                return ResultFactory.buildFailResult("收藏记录或帖子不存在!");
        }

        [HttpPost("delete/{pid}")]
        public Result deletePost(int pid)
        {
            if (!HttpContext.Request.Cookies.ContainsKey("loginStatus"))
                return ResultFactory.buildAuthFailResult("用户未登录！");
            string loginStatus = HttpContext.Request.Cookies["loginStatus"];
            int uid = int.Parse(loginStatus.Split("_")[1]);

            Console.WriteLine("==================");
            Console.WriteLine(pid + "," + uid);
            Console.WriteLine("==================");

            if (!personService.checkEditor(pid,uid))
                return ResultFactory.buildFailResult("您不是作者,无法删除！");

            Post post = detailService.getDetail(pid);

            personService.deletePost(pid);

            string content = post.content;

            int index = content.IndexOf("/imgs/");
           
            while(index >= 0)
            {
                int index2 = index;
                while (content[index2] != '\"') index2++;

                string path = content.Substring(index, index2 - index);
                string webRootPath = _hostingEnvironment.WebRootPath;
                string img = webRootPath + path;


                if (System.IO.File.Exists(img)) System.IO.File.Delete(img);

                content = content.Substring(index2, content.Length-index2);
                index = content.IndexOf("/imgs/");
            }

            return ResultFactory.buildSuccessResult("删除成功!");
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
