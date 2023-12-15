namespace AI_Social_Platform.FormModels
{
    using System.ComponentModel.DataAnnotations;
    using static Common.EntityValidationConstants.School;
    using static Common.EntityValidationConstants.State;

    public class SchoolFormModel
    {
        [Required]
        [StringLength(SchoolMaxLength, MinimumLength = SchoolMinLength)]
        public string Name { get; set; } = null!;

        [Required]
        [StringLength(StateMaxLength, MinimumLength = StateMinLength)]
        public string State { get; set; } = null!;

    }
}
