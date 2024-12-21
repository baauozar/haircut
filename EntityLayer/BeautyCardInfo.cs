using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer
{
    public class BeautyCardInfo
    {
        public int Id { get; set; }

        [Required, StringLength(100)]
        public string? Title { get; set; }

        [StringLength(500)]
        public string? Description { get; set; }

        // Path to the image displayed in the info card (stored in wwwroot)
        [StringLength(200)]
        public string? ImagePath { get; set; }
        public bool IsDeleted { get; set; } = false;


    }
}
