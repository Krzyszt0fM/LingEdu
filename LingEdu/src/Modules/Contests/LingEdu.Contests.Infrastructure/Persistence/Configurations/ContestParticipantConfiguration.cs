using LingEdu.Contests.Domain.Contests;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LingEdu.Contests.Infrastructure.Persistence.Configurations
{
    internal sealed class ContestParticipantConfiguration : IEntityTypeConfiguration<ContestParticipant>
    {
        public void Configure(EntityTypeBuilder<ContestParticipant> builder)
        {
            builder.ToTable("ContestParticipants");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.ContestId).IsRequired();
            builder.Property(x => x.UserId).IsRequired();
            builder.Property(x => x.JoinedAt).IsRequired();

            builder.HasIndex(x => new { x.ContestId, x.UserId }).IsUnique();
        }
    }
}
