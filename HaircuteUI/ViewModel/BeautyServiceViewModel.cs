using System.ComponentModel.DataAnnotations;

namespace HaircuteUI.ViewModel
{
    public class BeautyServiceViewModel
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

        // Reference to the related BeautyServicesItem
        public string? BeautyServicesItem { get; set; }
    }
}
