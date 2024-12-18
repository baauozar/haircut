using System.ComponentModel.DataAnnotations;

namespace EntityLayer
{
    public class HaircutMenuItem
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string? Name { get; set; }

        public decimal  Price { get; set; }

        // Foreign Key
        public int HaircutMenuCategoryId { get; set; }

        public bool IsDeleted { get; set; } = false;
        public HaircutMenuCategory? HaircutMenuCategory { get; set; }
    }
}