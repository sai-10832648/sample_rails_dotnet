using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Cryptography;
using System.Text.RegularExpressions;
using BCrypt.Net;

namespace sample_rails_app_8th_edNT.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        [Required]
        [MaxLength(255)]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [MinLength(6)]
        public string PasswordHash { get; set; }

        public bool Activated { get; set; }
        public DateTime? ActivatedAt { get; set; }
        public string RememberDigest { get; set; }
        public string ActivationDigest { get; set; }
        public string ResetDigest { get; set; }
        public DateTime? ResetSentAt { get; set; }

        // Navigation properties
        public ICollection<Micropost> Microposts { get; set; }
        public ICollection<Relationship> ActiveRelationships { get; set; }
        public ICollection<Relationship> PassiveRelationships { get; set; }
        public ICollection<User> Following { get; set; }
        public ICollection<User> Followers { get; set; }

        // Tokens (not mapped)
        [NotMapped]
        public string RememberToken { get; set; }
        [NotMapped]
        public string ActivationToken { get; set; }
        [NotMapped]
        public string ResetToken { get; set; }
        [NotMapped]
        public string PasswordConfirmation { get; set; }

        // Methods
        public static string Digest(string input)
        {
            // Use BCrypt.Net-Next or similar for password hashing
            return BCrypt.Net.BCrypt.HashPassword(input);
        }

        public static string NewToken()
        {
            return Convert.ToBase64String(RandomNumberGenerator.GetBytes(32));
        }

        public void Remember()
        {
            RememberToken = NewToken();
            RememberDigest = Digest(RememberToken);
        }

        public string SessionToken()
        {
            if (RememberDigest != null)
                return RememberDigest;
            Remember();
            return RememberDigest;
        }

        public bool Authenticated(string attribute, string token)
        {
            var digest = attribute switch
            {
                "remember" => RememberDigest,
                "activation" => ActivationDigest,
                "reset" => ResetDigest,
                _ => null
            };
            if (digest == null) return false;
            return BCrypt.Net.BCrypt.Verify(token, digest);
        }

        public void Forget()
        {
            RememberDigest = null;
        }

        public void Activate()
        {
            Activated = true;
            ActivatedAt = DateTime.UtcNow;
        }

        public void CreateResetDigest()
        {
            ResetToken = NewToken();
            ResetDigest = Digest(ResetToken);
            ResetSentAt = DateTime.UtcNow;
        }

        public bool PasswordResetExpired()
        {
            return ResetSentAt.HasValue && ResetSentAt.Value < DateTime.UtcNow.AddHours(-2);
        }

        // Feed, Follow, Unfollow, Following? methods would require additional logic and context
        // ...
    }
}
