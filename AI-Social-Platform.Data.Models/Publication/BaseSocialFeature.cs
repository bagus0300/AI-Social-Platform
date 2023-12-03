using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace AI_Social_Platform.Data.Models.Publication
{
    public class BaseSocialFeature
    {
        public BaseSocialFeature()
        {
            this.Id = Guid.NewGuid();
            this.DateCreated = DateTime.UtcNow;
        }

        [Key]
        public Guid Id { get; set; }

        [Required]
        public DateTime DateCreated { get; set; }

        [Required]
        [ForeignKey(nameof(Publication))]
        public Guid PublicationId { get; set; }
        public Publication Publication { get; set; } = null!;

        [Required]
        [ForeignKey(nameof(User))]
        public Guid UserId { get; set; }
        public ApplicationUser User { get; set; } = null!;
    }
}
