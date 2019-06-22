using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Internal;
using Microsoft.AspNetCore.Mvc;
using Web_backhand.Models;
using Web_backhand.Utils;
using Web_backhand.Service;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Web_backhand.Controllers
{
    [Route("api/[controller]")]
    public class PublishController : Controller
    {
        private readonly IHostingEnvironment _hostingEnvironment;

        private readonly PublishService publishService;

        public PublishController(IHostingEnvironment hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment;
            publishService = new PublishService();
        }

        // GET: api/<controller>
        [HttpGet]
        public Result Get()
        {
            if (!HttpContext.Request.Cookies.ContainsKey("loginStatus"))
                return ResultFactory.buildAuthFailResult("您未登录！");
            else
                return ResultFactory.buildSuccessResult("您已登录！");
        }

        // GET api/<controller>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            //F:\eclipse\projects\Web\Web_backhand\Web_backhand\wwwroot
            string webRootPath = _hostingEnvironment.WebRootPath;
            //F:\eclipse\projects\Web\Web_backhand\Web_backhand
            string contentRootPath = _hostingEnvironment.ContentRootPath;

            return webRootPath + "\n" + contentRootPath;
        }

        // POST api/<controller>
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }

        // POST api/<controller>/UploadImg
        [HttpPost("UploadImg")]
        public Result ImageUpload([FromForm]IFormCollection formCollection)
        {
            //wwwroot路径
            string webRootPath = _hostingEnvironment.WebRootPath;
            //项目路径
            string contentRootPath = _hostingEnvironment.ContentRootPath;


            FormFileCollection filelist = (FormFileCollection)formCollection.Files;
            if (filelist == null) return ResultFactory.buildFailResult("图片为空");

            if (!HttpContext.Request.Cookies.ContainsKey("loginStatus"))
                return ResultFactory.buildAuthFailResult("您未登录！");

            string username = HttpContext.Request.Cookies["loginStatus"];
            username = username.Split("_")[0];

            string imgpath = "";

            foreach (IFormFile file in filelist)
            {
                string dirPath = webRootPath + "/imgs/" + username + "/";
                string name = file.FileName;
                string FileName = DateTime.Now.ToString("yyyyMMddHHmmssfff");
                string type = System.IO.Path.GetExtension(name);

                imgpath = "/imgs/" + username + "/" + FileName + type;

                Console.WriteLine(name);
                Console.WriteLine(FileName);
                Console.WriteLine(type);

                DirectoryInfo di = new DirectoryInfo(dirPath);
                if (!di.Exists)
                    di.Create();

                using (FileStream fs = System.IO.File.Create(dirPath + FileName + type))
                {
                    file.CopyTo(fs);
                    fs.Flush();
                }
            }

            return ResultFactory.buildSuccessResult(imgpath);

        }

        // POST api/<controller>/UploadAll
        [HttpPost("UploadAll")]
        public Result UploadAll([FromBody]PostForm postForm)
        {

            if (!HttpContext.Request.Cookies.ContainsKey("loginStatus"))
                return ResultFactory.buildAuthFailResult("用户未登录！");
            string loginStatus = HttpContext.Request.Cookies["loginStatus"];
            int editor = int.Parse(loginStatus.Split("_")[1]);

            if (publishService.addPost(editor, postForm.detailTitle,postForm.detailContent))
                return ResultFactory.buildSuccessResult("发布成功！");
            else
                return ResultFactory.buildFailResult("发布失败!");
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
