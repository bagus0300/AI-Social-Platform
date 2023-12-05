namespace AI_Social_Platform.Data.Models
{
    using System.ComponentModel.DataAnnotations;
    using static Common.EntityValidationConstants.Country;

    public class Country
    {
        public Country()
        {
            this.UsersInThisCountry = new HashSet<ApplicationUser>();
        }
        
        [Key]
        public int Id { get; set; }

        [Required] 
        [MaxLength(CountryMaxLength)]
        public string Name { get; set; } = null!;

        public ICollection<ApplicationUser> UsersInThisCountry { get; set; }
    }
}
