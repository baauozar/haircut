using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer
{
    public class Faq
    {
        public int Id { get; set; }

        [Required, MaxLength(100)]
        public string? quastion { get; set; }
        [Required, MaxLength(1000)]
        public string? Answer { get; set; }
        public bool IsDeleted { get; set; } = false;
    }
}
