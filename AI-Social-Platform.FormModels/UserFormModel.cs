namespace AI_Social_Platform.FormModels
{
    using System.ComponentModel.DataAnnotations;
    
    using Data.Models.Enums;

    using static Common.EntityValidationConstants.User;
    using static Common.EntityValidationConstants.State;
    using static Common.EntityValidationConstants.Country;
    using static Common.EntityValidationConstants.School;
    using Microsoft.AspNetCore.Http;

    public class UserFormModel
    {
        public UserFormModel()
        {
            this.Schools = new HashSet<SelectSchoolFormModel>();
        }


        [Required]
        [StringLength(FirstNameMaxLength, MinimumLength = FirstNameMinLength)]
        [Display(Name = "First Name")]
        public string FirstName { get; set; } = null!;


        [Required]
        [StringLength(LastNameMaxLength, MinimumLength = LastNameMinLength)]
        [Display(Name = "Last Name")]
        public string LastName { get; set; } = null!;
        

        [StringLength(PhoneNumberMaxLength, MinimumLength = PhoneNumberMinLength)]
        [Phone]
        [Display(Name = "Phone")]
        public string? PhoneNumber { get; set; }

        public IFormFile? ProfilePicture { get; set; }

        public IFormFile? CoverPhoto { get; set; }

        [StringLength(CountryMaxLength, MinimumLength = CountryMinLength)]
        public string? Country { get; set; }


        [StringLength(StateMaxLength, MinimumLength = StateMinLength)]
        public string? State { get; set; }


        public Gender? Gender { get; set; }


        [StringLength(SchoolMaxLength, MinimumLength = SchoolMinLength)]
        public string? School { get; set; }


        public DateTime? Birthday { get; set;}


        public RelationshipStatus? Relationship { get; set; }

        public ICollection<SelectSchoolFormModel> Schools { get; set; }
    }
}
