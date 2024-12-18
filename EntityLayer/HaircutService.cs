using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer
{
    public class HaircutService
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string ?Title { get; set; }

        [StringLength(500)]
        public string? Description { get; set; }

        [Required]
        public string? ImagePath { get; set; }

        // Foreign Key
        public bool IsDeleted { get; set; } = false;
        public int ServiceCategoryId { get; set; }

        public HaircutServicesCategory? HaircutServicesCategory { get; set; }
        public ICollection<HaircutSupService>? HairCutSupServices { get; set; }
    }
}
