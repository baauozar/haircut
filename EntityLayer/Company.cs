using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer
{
    public class Company
    {
        public int Id { get; set; }
        public string ?smallTitle { get; set; }
        public string? bigTitle { get; set; }
        public string? BaackgroundTitle { get; set; }
        public string? Section { get; set; }
        public string? Signature { get; set; }
        public bool IsDeleted { get; set; } = false;
    }
}
