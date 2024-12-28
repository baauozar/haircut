using System.ComponentModel.DataAnnotations;

namespace HaircuteUI.ViewModel
{
    public class BeautyCategoryViewModel
    {
        public int Id { get; set; }

        [Required, StringLength(100)]
        public string? Name { get; set; }

        [StringLength(200)]
        public string? IconPath { get; set; }
        public string? ImagePath { get; set; }

        public bool IsDeleted { get; set; }

       
    }
}
