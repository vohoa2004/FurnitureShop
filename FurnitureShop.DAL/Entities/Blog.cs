using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FurnitureShop.DAL.Entities
{
    public class Blog
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [MaxLength(255)]
        public required string Title { get; set; }

        public required string Content { get; set; }
        public required string ImageLink { get; set; }
        public DateTime CreatedAt { get; set; }

    }
}
