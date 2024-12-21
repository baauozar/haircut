using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer
{
    public class BeautyServiesItem
    {
        public int Id { get; set; }

       

        [Required, StringLength(50)]
        public string? NumberText { get; set; } // e.g., "01", "02"

        [Required, StringLength(200)]
        public string? Title { get; set; }
        // If you have two lines like "Facial / Treatments", you can store them combined or in two separate fields.

        [StringLength(200)]
        public string? ImagePath { get; set; }
        public bool IsDeleted { get; set; } = false;
    }
}
