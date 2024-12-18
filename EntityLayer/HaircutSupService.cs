using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer
{
    public class HaircutSupService
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string? Name { get; set; }

        public string? Description { get; set; }

        // Foreign Key
        public int ServiceId { get; set; }

        public bool IsDeleted { get; set; } = false;
        public HaircutService? HaircutService { get; set; }
    }
}
