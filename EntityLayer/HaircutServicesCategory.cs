using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer
{
    public class HaircutServicesCategory
    {
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string? Name { get; set; }

        public bool IsDeleted { get; set; } = false;
        public ICollection<HaircutService>? HaircutServices { get; set; }
    }
    
}
