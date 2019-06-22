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
    public class DetailController : Controller
    {

        private readonly DetailService detailService;
        private readonly UserService userService;

        public DetailController()
        {
            detailService = new DetailService();
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

        // POST api/detail/id
        [HttpPost("{id}")]
        public Result Post(int id)
        {
            Post post = detailService.getDetail(id);
            bool isFavorites = false;

            if(!HttpContext.Request.Cookies.ContainsKey("loginStatus"))
                return ResultFactory.buildSuccessResult(new {post, isFavorites});
            string loginStatus = HttpContext.Request.Cookies["loginStatus"];
            int uid = int.Parse(loginStatus.Split("_")[1]);

            if (detailService.checkFavorite(id, uid)) isFavorites = true;

            return ResultFactory.buildSuccessResult(new { post, isFavorites });
        }

        // POST api/detail/editor/id
        [HttpPost("editor/{id}")]
        public Result detailEditor(int id)
        {
            User user = userService.getUserByID(id);
            if (user == null) return ResultFactory.buildFailResult("用户不存在");
            user.password = "";
            return ResultFactory.buildSuccessResult(user);
        }

        [HttpPost("comments/{id}")]
        public Result getComments(int id)
        {
        
            return ResultFactory.buildSuccessResult(detailService.getComments(id));
        }

        [HttpPost("pushComment/{id}")]
        public Result pushComment([FromBody]CommentInfo commentInfo, int id)
        {
            if (!HttpContext.Request.Cookies.ContainsKey("loginStatus"))
                return ResultFactory.buildAuthFailResult("用户未登录！");
            string loginStatus = HttpContext.Request.Cookies["loginStatus"];
            commentInfo.commenter = loginStatus.Split("_")[0];

            if (detailService.addComment(id, commentInfo.commenter, commentInfo.content))
                return ResultFactory.buildSuccessResult("评论成功！");
            else
                return ResultFactory.buildFailResult("评论失败！");
        }

        [HttpPost("pushFavorite/{id}")]
        public Result pushFavorite(int id)
        {
           
            if (!HttpContext.Request.Cookies.ContainsKey("loginStatus"))
                return ResultFactory.buildAuthFailResult("用户未登录！");
            string loginStatus = HttpContext.Request.Cookies["loginStatus"];
            int uid = int.Parse(loginStatus.Split("_")[1]);

            if (detailService.checkFavorite(id, uid))
                return ResultFactory.buildFailResult("您已收藏过！");

            if(detailService.addFavorite(id, uid))
                return ResultFactory.buildSuccessResult("收藏成功！");
            else
                return ResultFactory.buildFailResult("收藏失败！");

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
