namespace AI_Social_Platform.Data.Models
{
    using System.ComponentModel.DataAnnotations.Schema;

    using static AI_Social_Platform.Common.EntityValidationConstants;

    public class UserSchool
    {
        [ForeignKey(nameof(User))]
        public Guid UserId { get; set; }
        public ApplicationUser User { get; set; } = null!;

        [ForeignKey(nameof(School))]
        public int SchoolId { get; set; }
        public School School { get; set; } = null!;
    }
}
