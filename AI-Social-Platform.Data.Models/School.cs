namespace AI_Social_Platform.Data.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using static Common.EntityValidationConstants.School;

    public class School
    {
        public School()
        {
            this.UserSchools = new HashSet<UserSchool>();
        }
        
        [Key]
        public int Id { get; set; }

        [MaxLength(SchoolMaxLength)]
        public string Name { get; set; } = null!;

        [ForeignKey(nameof(State))] 
        public int StateId { get; set; }
        public virtual State State { get; set; } = null!;

        public ICollection<UserSchool> UserSchools { get; set; }
    }
}
