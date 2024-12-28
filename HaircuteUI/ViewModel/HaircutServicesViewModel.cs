using EntityLayer;
using System.ComponentModel.DataAnnotations;

namespace HaircuteUI.ViewModel
{
    public class HaircutServicesViewModel
    {
        public int Id { get; set; }

        [Required, StringLength(100)]
        public string? Title { get; set; }

        [StringLength(500)]
        public string? Description { get; set; }

        [Required]
        public string? ImagePath { get; set; } 

        [Required]
        public int ServiceCategoryId { get; set; }

        // For the dropdown to select category
        public string? ServiceCategory { get; set; }
        public bool IsDeleted  { get; set; }

       

    }
}
