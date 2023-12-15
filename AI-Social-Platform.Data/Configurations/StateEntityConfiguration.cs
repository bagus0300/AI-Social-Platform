namespace AI_Social_Platform.Data.Configurations
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    using Models;

    public class StateEntityConfiguration : IEntityTypeConfiguration<State>
    {
        public void Configure(EntityTypeBuilder<State> builder)
        {
            builder
                .HasData(this.GenerateStates());

            builder
                .HasMany(s => s.UsersInThisState)
                .WithOne(u => u.State)
                .HasForeignKey(u => u.StateId)
                .OnDelete(DeleteBehavior.Restrict);
        }

        private State[] GenerateStates()
        {
            ICollection<State> states = new HashSet<State>();

            State state = new State()
            {
                Id = 1,
                Name = "Sofia"
            };

            states.Add(state);

            state = new State()
            {
                Id = 2,
                Name = "Varna"
            };

            states.Add(state);

            return states.ToArray();
        }
    }
}
