using DataLayer.Concrete;

namespace HaircutApi.EntityDto
{
    public class BeautyCategoryDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public List<BeautyItemsDto>? Items { get; set; }
    }
}
