using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web_backhand.Models
{
    public class SearchForm
    {
        // 搜索内容
        public string searchText { get; set; }
        // 页码
        public int page { get; set; }
    }
}