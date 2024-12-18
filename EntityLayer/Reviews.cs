using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer
{
    public class Reviews
    {
        public int Id { get; set; } 
        public string? FullName { get; set; }
        public string ?Email { get; set; }
        public int? Rating { get; set; }
        public string? Service { get; set; }
        public string ?Opinion { get; set; }
    }
}
