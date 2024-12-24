namespace HaircutApi.EntityDto
{
    public class BeautyItemsDto
    {
        public int Id { get; set; }
        public string? ServiceName { get; set; }
        public decimal Price { get; set; }
        public string ?Duration{get;set;}
        public string ? Description { get;set;}

        public List<BeautyCategoryDto>? beautyCategories { get; set; }
    }
}
