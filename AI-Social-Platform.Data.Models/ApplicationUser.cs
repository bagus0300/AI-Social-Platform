namespace AI_Social_Platform.Data.Models
{
    using Microsoft.AspNetCore.Identity;
    using System.ComponentModel.DataAnnotations;
    using Publication;

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
        }

        [Required]
        [MaxLength(FirstNameMaxLength)]
        public string FirstName { get; set; } = null!;

        [Required]
        [MaxLength(LastNameMaxLength)]
        public string LastName { get; set; } = null!;

        public ICollection<Publication.Publication> Publications { get; set; }
        public ICollection<Comment> Comments { get; set; }
        public ICollection<Like> Likes { get; set; }
        public ICollection<Share> Shares { get; set; }
        public ICollection<ApplicationUser> Friends { get; set; }
    }
}