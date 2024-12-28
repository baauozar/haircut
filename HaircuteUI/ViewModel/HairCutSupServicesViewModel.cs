using System.ComponentModel.DataAnnotations;

namespace HaircuteUI.ViewModel
{
    public class HairCutSupServicesViewModel
    {
        public int Id { get; set; }
        [Required, StringLength(100)]
        public string? Name { get; set; } = string.Empty;
        public string? Description { get; set; }
        public bool IsDeleted { get; set; }
        public int ServiceId { get; set; }
        public string? ServiceName { get; set; } // For displaying related HaircutService
    }
}
