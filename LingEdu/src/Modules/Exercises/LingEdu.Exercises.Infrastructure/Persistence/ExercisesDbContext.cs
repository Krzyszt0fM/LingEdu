using LingEdu.Exercises.Domain.Exercises;
using LingEdu.Exercises.Domain.Progress;
using LingEdu.Exercises.Infrastructure.Persistence.Configurations;
using Microsoft.EntityFrameworkCore;

namespace LingEdu.Exercises.Infrastructure.Persistence
{
    public sealed class ExercisesDbContext : DbContext
    {
        public ExercisesDbContext(DbContextOptions<ExercisesDbContext> options)
            : base(options)
        {
        }

        public DbSet<Exercise> Exercises => Set<Exercise>();

        public DbSet<QuizQuestion> QuizQuestions => Set<QuizQuestion>();

        public DbSet<QuizOption> QuizOptions => Set<QuizOption>();

        public DbSet<UserProgress> UserProgress => Set<UserProgress>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new ExerciseConfiguration());
            modelBuilder.ApplyConfiguration(new QuizQuestionConfiguration());
            modelBuilder.ApplyConfiguration(new QuizOptionConfiguration());
            modelBuilder.ApplyConfiguration(new UserProgressConfiguration());

            base.OnModelCreating(modelBuilder);
        }
    }
}
