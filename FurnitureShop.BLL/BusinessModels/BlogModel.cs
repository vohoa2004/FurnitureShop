using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FurnitureShop.BLL.BusinessModels
{
    public class BlogModel
    {
        public int Id { get; set; }

        [MaxLength(255)]
        public required string Title { get; set; }

        public required string Content { get; set; }
        public required string ImageLink { get; set; }
        public DateTime CreatedAt { get; set; }

    }
}
