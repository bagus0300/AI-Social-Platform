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
        Comments = new HashSet<Comment>();
    }

    [Key]
    public Guid Id { get; set; }

    [Required]
    [MaxLength(PublicationContentMaxLength)]
    public string Content { get; set; } = null!;

    [Required] 
    public DateTime DateCreated { get; set; }

    //Relations

    [Required]
    [ForeignKey(nameof(Author))]
    public Guid AuthorId { get; set; }

    public ApplicationUser Author { get; set; } = null!;

    public ICollection<Comment> Comments { get; set; }
}