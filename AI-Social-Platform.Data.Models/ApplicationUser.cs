namespace AI_Social_Platform.Data.Models
{
    using Microsoft.AspNetCore.Identity;
    using System.ComponentModel.DataAnnotations;
    using Publication;

    using static Common.EntityValidationConstants.User;
    using Enums;
    using System.ComponentModel.DataAnnotations.Schema;
    using AI_Social_Platform.Data.Models.Topic;

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
            this.UserSchools = new HashSet<UserSchool>();
            this.FollowedTopics = new HashSet<UserTopic>();
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


        public Gender? Gender { get; set; }

        public DateTime? Birthday { get; set; }

        public RelationshipStatus? Relationship { get; set; }

        public ICollection<Publication.Publication> Publications { get; set; }
        public ICollection<Comment> Comments { get; set; }
        public ICollection<Like> Likes { get; set; }
        public ICollection<Share> Shares { get; set; }
        public ICollection<ApplicationUser> Friends { get; set; }
        public virtual ICollection<UserSchool> UserSchools { get; set; }
        public ICollection<UserTopic> FollowedTopics { get; set; }
    }
}