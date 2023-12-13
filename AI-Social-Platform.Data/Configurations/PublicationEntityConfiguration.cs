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
                .OnDelete(DeleteBehavior.Cascade);

            builder
                .HasMany(x => x.Likes)
                .WithOne(x => x.Publication)
                .HasForeignKey(x => x.PublicationId)
                .OnDelete(DeleteBehavior.Cascade);

            builder
                .HasMany(x => x.Shares)
                .WithOne(x => x.Publication)
                .HasForeignKey(x => x.PublicationId)
                .OnDelete(DeleteBehavior.Cascade);

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
                Id = Guid.Parse("97a22373-3f82-417d-bf0f-26ec43b4460b"),
                Content = "This is the first seeded publication Content from Ivan",
                AuthorId = Guid.Parse("949a14ed-2e82-4f5a-a684-a9c7e3ccb52e"),
                DateCreated = DateTime.Parse("2023/12/12"),
                LatestActivity = DateTime.Parse("2023/12/12"),
            };

        
            publications.Add(publication);

            publication = new Publication()
            {
                Id = Guid.Parse("c6cb4c37-8ea5-4881-ac48-986d7f2af5fc"),
                Content = "This is the second seeded publication Content from Georgi",
                AuthorId = Guid.Parse("6d5800ce-d726-4fc8-83d9-d6b3ac1f591e"),
                DateCreated = DateTime.Parse("2023/12/12"),
                LatestActivity = DateTime.Parse("2023/12/12"),
            };

            publications.Add(publication);

            return publications.ToArray();
        }
    }
}
