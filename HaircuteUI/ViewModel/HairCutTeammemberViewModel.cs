using System.ComponentModel.DataAnnotations;

namespace HaircuteUI.ViewModel
{
    public class HairCutTeammemberViewModel
    {
        public int Id { get; set; }

     
        public string? Name { get; set; }


        public string? Position { get; set; }

        public string? Bio { get; set; }

  
        public string? ImagePath { get; set; }
    }
}
