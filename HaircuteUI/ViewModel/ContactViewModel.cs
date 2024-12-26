using System.ComponentModel.DataAnnotations;

namespace HaircuteUI.ViewModel
{
    public class ContactViewModel
    {
        public int Id { get; set; }
        [Required, MaxLength(20)]
        public string? Name { get; set; }
        [Required, MaxLength(20)]
        public string? LastName { get; set; }
        public string? phonenumber { get; set; }
        [MaxLength(100), EmailAddress]
        public string? Email { get; set; }
        [Required, MaxLength(100)]
        public string? Message { get; set; }
        public bool IsDeleted { get; set; }
 
    }
}
