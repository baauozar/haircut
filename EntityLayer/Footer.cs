using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer
{
    public class Footer
    {
        public int Id { get; set; } // Primary Key
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Address { get; set; }
        public string? OpeningHours { get; set; }
        public string? NewsletterText { get; set; }
        public string? SocialMediaLinksJson { get; set; } 
        public string?   Services { get; set; } 

    }
}
