using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FurnitureShop.BLL.BusinessModels
{
    public class SearchBlogModel
    {
        public string? Title { get; set; }
        public DateTime? CreatedAt { get; set; }
        public int PageNo { get; set; } = 1;
        public int PageSize { get; set; } = 6;
    }
}
