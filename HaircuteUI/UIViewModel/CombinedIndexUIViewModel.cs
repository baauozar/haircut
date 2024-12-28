namespace HaircuteUI.UIViewModel
{
    public class CombinedIndexUIViewModel
    {
        public List<HaircutMenuUIViewModel>? MenuCategories { get; set; }
        public StoryUIViewModel? Story { get; set; }
        public List<HaircutServicesCategoryUIViewModel>? ServiceCategories { get; set; }
        public List<HaircutServicesUIViewModel>? Services { get; set; }
    }
}
