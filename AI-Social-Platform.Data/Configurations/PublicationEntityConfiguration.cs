namespace AI_Social_Platform.Data.Configurations
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using Models.Publication;

    public class PublicationEntityConfiguration : IEntityTypeConfiguration<Publication>
    {
        public void Configure(EntityTypeBuilder<Publication> builder)
        {
            builder.HasData(Seed());

            builder
                .HasMany(x => x.Comments)
                .WithOne(x => x.Publication)
                .HasForeignKey(x => x.PublicationId)
                .OnDelete(DeleteBehavior.NoAction);

            builder
                .HasOne(x => x.Author)
                .WithMany(x => x.Publications)
                .HasForeignKey(x => x.AuthorId)
                .OnDelete(DeleteBehavior.NoAction);
        }

        private Publication[] Seed()
        {
            ICollection<Publication> publications = new HashSet<Publication>();

            Publication publication;

            publication = new Publication()
            {
                Content = "This is the first seeded publication Content from Ivan",
                AuthorId = Guid.Parse("949a14ed-2e82-4f5a-a684-a9c7e3ccb52e"),
            };

        
            publications.Add(publication);

            publication = new Publication()
            {
                Content = "This is the second seeded publication Content from Georgi",
                AuthorId = Guid.Parse("6d5800ce-d726-4fc8-83d9-d6b3ac1f591e"),
            };

            publications.Add(publication);

            return publications.ToArray();
        }
    }
}
