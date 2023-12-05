namespace AI_Social_Platform.Data.Configurations
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    
    using Models;

    public class CountryEntityConfiguration : IEntityTypeConfiguration<Country>
    {
        public void Configure(EntityTypeBuilder<Country> builder)
        {
            builder
                .HasData(this.GenerateCountries());

            builder
                .HasMany(c => c.UsersInThisCountry)
                .WithOne(u => u.Country)
                .HasForeignKey(u => u.CountryId)
                .OnDelete(DeleteBehavior.Restrict);
        }

        private Country[] GenerateCountries()
        {
            ICollection<Country> countries = new HashSet<Country>();

            Country country = new Country()
            {
                Id = 1,
                Name = "BULGARIA"
            };
            countries.Add(country);

            country = new Country()
            {
                Id = 2,
                Name = "ENGLAND"
            };

            countries.Add(country);

            country = new Country()
            {
                Id = 3,
                Name = "USA"
            };

            countries.Add(country);

            country = new Country()
            {
                Id = 4,
                Name = "RUSSIA"
            };

            countries.Add(country);

            country = new Country()
            {
                Id = 5,
                Name = "JAPAN"
            };

            countries.Add(country);

            return countries.ToArray();
        }
    }
}
