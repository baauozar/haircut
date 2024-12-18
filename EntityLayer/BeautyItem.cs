using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer
{
    public class BeautyItem
    {
        public int Id { get; set; }
        public int BeautyCategoryId { get; set; }

        public BeautyCategory? BeautyCategory { get; set; }

        [Required, StringLength(200)]
        public string? ServiceName { get; set; }

        [StringLength(200)]
        public string? Duration { get; set; }

        public decimal Price { get; set; }

        [StringLength(1000)]
        public string? Description { get; set; }

   

        public bool IsDeleted { get; set; } = false;
    }
}
