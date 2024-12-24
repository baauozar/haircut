using System.ComponentModel.DataAnnotations;

namespace HaircuteUI.ViewModel
{
    public class BeautyCardInfoViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Card Name is required.")]
        [StringLength(100, ErrorMessage = "Card Name cannot exceed 100 characters.")]
        public string? Title { get; set; } 

        [StringLength(500, ErrorMessage = "Description cannot exceed 500 characters.")]
        public string? Description { get; set; } 
        public string? ImagePath { get; set; }
        public bool IsDeleted { get; set; }
    }
}
