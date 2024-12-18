using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer
{
    public class HairCutTeammember
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string? Name { get; set; }

        [StringLength(100)]
        public string? Position { get; set; }

        [DataType(DataType.MultilineText)]
        public string? Bio { get; set; }

        [Required]
        public string? ImagePath { get; set; }

      
    }
}
