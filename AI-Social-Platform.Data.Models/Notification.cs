using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using AI_Social_Platform.Data.Models.Enums;

namespace AI_Social_Platform.Data.Models
{
    public class Notification
    {
        public Notification()
        {
            this.Id = Guid.NewGuid();
            this.DateCreated = DateTime.UtcNow;
        }

        [Key]
        public Guid Id { get; set; }

        [Required]
        public DateTime DateCreated { get; set; }

        [Required]
        [ForeignKey(nameof(CreatingUser))]
        public Guid CreatingUserId { get; set; }

        public ApplicationUser CreatingUser { get; set; } = null!;

        [Required]
        [ForeignKey(nameof(ReceivingUser))]
        public Guid ReceivingUserId { get; set; }

        [Required]
        public ApplicationUser ReceivingUser { get; set; } = null!;

        [Required]
        public string Content { get; set; } = null!;

        [Required]
        public bool IsRead { get; set; } = false;

        [Required]
        public NotificationType NotificationType { get; set; }

        [Required]
        public string RedirectUrl { get; set; } = null!;
    }
}
