using DataLayer.Concrete;

namespace HaircutApi.EntityDto
{
    public class HaircutMenuCategoryDto
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public List<HaircutMenuItemDto>? MenuItems { get; set; } = new();
    }
}
