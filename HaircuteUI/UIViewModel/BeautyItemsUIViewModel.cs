using System.ComponentModel.DataAnnotations;

namespace HaircuteUI.UIViewModel
{
    public class BeautyItemsUIViewModel
    {
        public string? ServiceName { get; set; }

        public string? Duration { get; set; }

        public decimal Price { get; set; }

        public string? Description { get; set; }
    }
}
