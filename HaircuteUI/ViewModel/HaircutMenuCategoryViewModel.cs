namespace HaircuteUI.ViewModel
{
    public class HaircutMenuCategoryViewModel
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public List<HaircutMenuItemViewModel>? MenuItems { get; set; } = new();
    }
}
