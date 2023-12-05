namespace AI_Social_Platform.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    using static Common.EntityValidationConstants.State;

    public class State
    {
        public State()
        {
            this.UsersInThisState = new HashSet<ApplicationUser>();
        }
        
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(StateMaxLength)]
        public string Name { get; set; } = null!;

        public ICollection<ApplicationUser> UsersInThisState { get; set; }
    }
}
