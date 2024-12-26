using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer
{
    public class HairStyleSuggestion
    {
        public int Id { get; set; }

        public int UserId { get; set; } // Assuming you have user profiles

        public string ?UploadedPhotoPath { get; set; } // Path to the uploaded photo

        public string? SuggestedStyle { get; set; } // AI-generated hairstyle suggestion
        public string ?SuggestedColor { get; set; } // AI-generated color suggestion

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
