namespace AI_Social_Platform.Data.Models
{
    using System.ComponentModel.DataAnnotations.Schema;
    using System.ComponentModel.DataAnnotations;

    using Microsoft.AspNetCore.Identity;

    using Publication;
    using Enums;
    using Topic;

    using static Common.EntityValidationConstants.User;

    public class ApplicationUser : IdentityUser<Guid>
    {
        public ApplicationUser()
        {
            this.Id = Guid.NewGuid();
            this.Publications = new HashSet<Publication.Publication>();
            this.Friends = new HashSet<ApplicationUser>();
            this.Comments = new HashSet<Comment>();
            this.Likes = new HashSet<Like>();
            this.Shares = new HashSet<Share>();
            this.FollowedTopics = new HashSet<UserTopic>();
            this.CreatingNotifications = new HashSet<Notification>();
            this.ReceivingNotifications = new HashSet<Notification>();
        }

        [Required]
        [MaxLength(FirstNameMaxLength)]
        public string FirstName { get; set; } = null!;


        [Required]
        [MaxLength(LastNameMaxLength)]
        public string LastName { get; set; } = null!;

        public byte[]? ProfilePicture { get; set; }

        public byte[]? CoverPhoto { get; set; }

        public bool IsActive { get; set; }

        [ForeignKey(nameof(Country))]
        public int? CountryId { get; set; }
        public virtual Country? Country { get; set; }


        [ForeignKey(nameof(State))]
        public int? StateId { get; set; }
        public virtual State? State { get; set; }

        [ForeignKey(nameof(School))]
        public int? SchoolId { get; set; }
        public virtual School? School { get; set; }

        public Gender? Gender { get; set; }

        public DateTime? Birthday { get; set; }

        public RelationshipStatus? Relationship { get; set; }


        public ICollection<Publication.Publication> Publications { get; set; }
        public ICollection<Comment> Comments { get; set; }
        public ICollection<Like> Likes { get; set; }
        public ICollection<Share> Shares { get; set; }
        public ICollection<ApplicationUser> Friends { get; set; }
        public ICollection<UserTopic> FollowedTopics { get; set; }
        public ICollection<Notification> CreatingNotifications { get; set; }
        public ICollection<Notification> ReceivingNotifications { get; set; }
    }
}