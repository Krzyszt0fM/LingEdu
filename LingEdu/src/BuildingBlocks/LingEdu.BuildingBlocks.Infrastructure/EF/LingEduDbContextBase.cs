using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;

namespace LingEdu.BuildingBlocks.Infrastructure.EF
{
    public abstract class LingEduDbContextBase : DbContext
    {
        protected LingEduDbContextBase(DbContextOptions options) : base(options)
        {
        }

        protected LingEduDbContextBase(DbContextOptions options, bool disableTracking) : base(options)
        {
            if (disableTracking)
            {
                ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
