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
    public class HomeController : Controller
    {
        private readonly HomeService homeService;

        public HomeController()
        {
            homeService = new HomeService();
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

        [HttpGet("getHot")]
        public Result getHot()
        {
            Console.WriteLine("===");
            foreach (string s in HttpContext.Request.Cookies.Keys) Console.WriteLine(s);
            Console.WriteLine("===");

            List<Post> hots = homeService.getHot();
            Console.WriteLine("length:{0}",hots.Count);
            return ResultFactory.buildSuccessResult(hots);
        }

        // POST api/<controller>
        [HttpPost]
        public Result Post([FromBody]SearchForm searchForm)
        {
            Console.WriteLine(searchForm.searchText);
            Console.WriteLine(searchForm.page);

            int total = homeService.getPostTotal(searchForm.searchText);
            List<Post> list = homeService.getPostBySearch(searchForm.searchText, searchForm.page);
            foreach(Post i in list)
            {
                int index = i.content.IndexOf("https");
                if(index < 0)
                {
                    i.imgURL = "https://localhost:8889/imgs/default.png";
                    continue;
                }
                int index2 = index;
                while (i.content[index2] != '\"') index2++;
                i.imgURL = i.content.Substring(index, index2 - index);
            }

            return ResultFactory.buildSuccessResult(new { total, list});
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
