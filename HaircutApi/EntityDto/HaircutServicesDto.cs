using DataLayer.Concrete;

namespace HaircutApi.EntityDto
{
    public class HaircutServicesDto
    {
        public int Id { get; set; }
        public string? Title { get; set; } = string.Empty;
        public string? Description { get; set; }
        public string ImagePath { get; set; } = string.Empty;
        public int ServiceCategoryId { get; set; }
        public List<HairCutSupServicesDto>? SubServices { get; set; }
    }
}
