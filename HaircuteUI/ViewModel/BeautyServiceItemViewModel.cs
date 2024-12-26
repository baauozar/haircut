using System.ComponentModel.DataAnnotations;

namespace HaircuteUI.ViewModel
{
    public class BeautyServiceItemViewModel
    {
        public int Id { get; set; }

        [Required, StringLength(200)]
        public string? Title { get; set; }

        [StringLength(200)]
        public string? ImagePath { get; set; }

        public bool IsDeleted { get; set; } = false;
       
    }
}
