using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using AI_Social_Platform.Common;

namespace AI_Social_Platform.Data.Models.Publication;

using static EntityValidationConstants.Publication;

public class Publication
{
    public Publication()
    {
        Id = Guid.NewGuid();
        DateCreated = DateTime.UtcNow;
        LatestActivity = DateTime.UtcNow;
        Comments = new HashSet<Comment>();
        Likes = new HashSet<Like>();
        Shares = new HashSet<Share>();
        MediaFiles = new HashSet<Media>();
    }

    [Key]
    public Guid Id { get; set; }

    [Required]
    [MaxLength(PublicationContentMaxLength)]
    public string Content { get; set; } = null!;

    [Required] 
    public DateTime DateCreated { get; set; }

    public DateTime? DateModified { get; set; }

    public DateTime? LastCommented { get; set; }

    [Required]
    public DateTime LatestActivity { get; set; }
       

    //Relations
    [Required]
    [ForeignKey(nameof(Author))]
    public Guid AuthorId { get; set; }

    public ApplicationUser Author { get; set; } = null!;

    public Topic.Topic? Topic { get; set; }

    [ForeignKey(nameof(Topic))]
    public Guid? TopicId { get; set; }

    public ICollection<Comment> Comments { get; set; }

    public ICollection<Like> Likes { get; set; }

    public ICollection<Share> Shares { get; set; }

    public ICollection<Media> MediaFiles{ get; set; } 
}