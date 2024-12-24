namespace HaircuteUI.ViewModel
{
    public class HaircutMenuItemViewModel
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public decimal Price { get; set; }
        public int HaircutMenuCategoryId { get; set; }
        public bool IsDeleted {  get; set; }
        public string? CategoryName { get; set; } // For display in the index view
        public IEnumerable<HaircutMenuCategoryViewModel> ?Categories
        {
            get; set;
        }
    }
}
