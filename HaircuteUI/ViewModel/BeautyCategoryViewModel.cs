using System.ComponentModel.DataAnnotations;

namespace HaircuteUI.ViewModel
{
    public class BeautyCategoryViewModel
    {
        public int Id { get; set; }

        [Required, StringLength(100)]
        public string Name { get; set; } = string.Empty;

        


        public List<BeautyItemsViewModel> Items { get; set; } = new();
    }
}
