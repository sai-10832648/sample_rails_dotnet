using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace sample_rails_app_8th_edNT.Models
{
    public class Relationship
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int FollowerId { get; set; }
        [ForeignKey("FollowerId")]
        public User Follower { get; set; }

        [Required]
        public int FollowedId { get; set; }
        [ForeignKey("FollowedId")]
        public User Followed { get; set; }
    }
}
