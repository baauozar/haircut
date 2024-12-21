using System.ComponentModel.DataAnnotations;

namespace HaircuteUI.ViewModel
{
    public class BeautyItemsViewModel
    {

        public int Id { get; set; }

        public int BeautyCategoryId { get; set; }

        [Required, StringLength(200)]
        public string? ServiceName { get; set; }

        [StringLength(200)]
        public string? Duration { get; set; }

        public decimal Price { get; set; }

        [StringLength(1000)]
        public string? Description { get; set; }

        public bool IsDeleted { get; set; }
    }
}
