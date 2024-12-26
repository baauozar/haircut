using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer
{
    public class BeautyService
    {

        public int Id { get; set; }

        [Required, StringLength(200)]
        public string? Heading { get; set; }

        [StringLength(500)]
        public string? Subheading { get; set; }

        [StringLength(1000)]
        public string? Description { get; set; }

        public bool IsDeleted { get; set; } = false;

        // Foreign key to BeautyServicesItem
        public int BeautyServicesItemId { get; set; }

        // Navigation property
        public virtual BeautyServicesItem BeautyServicesItem { get; set; } = null!;





    }
}
