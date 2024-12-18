using EntityLayer;
using System.ComponentModel.DataAnnotations;

namespace HaircuteUI.ViewModel
{
    public class HaircutServicesViewModel
    {
        public int Id { get; set; }

        [Required, StringLength(100)]
        public string Title { get; set; } = string.Empty;

        [StringLength(500)]
        public string? Description { get; set; }

        [Required]
        public string ImagePath { get; set; } = string.Empty;

        [Required]
        public int ServiceCategoryId { get; set; }

        // For the dropdown to select category
        public IEnumerable<HaircutServicesCategory>? Categories { get; set; }

        // For managing sub-services in the UI (e.g., list them and allow adding)
        public List<HairCutSupServicesViewModel> SubServices { get; set; } = new();

    }
}
