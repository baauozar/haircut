using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer
{
    public class BeautyCategory
    {
        public int Id { get; set; }

        [Required, StringLength(100)]
        public string? Name { get; set; } // e.g., "Facial", "Nail"

        [StringLength(200)]
        public string? IconPath { get; set; } // If you want icons dynamic, otherwise hardcode them

        
        public bool IsDeleted { get; set; } = false;

        public ICollection<BeautyItem>? BeautyItems { get; set; }
    }
}
