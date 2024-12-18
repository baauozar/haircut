namespace HaircuteUI.ViewModel
{
    public class HaircutServicesCategoryViewModel
    {
        public int Id { get; set; }
        public string ?Name { get; set; }
        public List<HaircutServicesViewModel> Services { get; set; } = new();
    }
}
