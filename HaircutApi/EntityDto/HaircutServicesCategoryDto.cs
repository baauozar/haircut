namespace HaircutApi.EntityDto
{
    public class HaircutServicesCategoryDto
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public List<HaircutServicesDto> Services { get; set; } = new();
    }
}
