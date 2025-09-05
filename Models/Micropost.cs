using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace sample_rails_app_8th_edNT.Models
{
    public class Micropost
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int UserId { get; set; }
        [ForeignKey("UserId")]
        public User User { get; set; }

        [Required]
        [MaxLength(140)]
        public string Content { get; set; }

        public DateTime CreatedAt { get; set; }

        // For image attachment, use IFormFile or a custom type for EF Core
        // You may need to configure file storage separately
        public string ImagePath { get; set; }

        // Validation for image type and size should be handled in the controller or via custom attributes
    }
}
