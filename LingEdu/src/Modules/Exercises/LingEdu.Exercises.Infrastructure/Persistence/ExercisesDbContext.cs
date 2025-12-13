using LingEdu.BuildingBlocks.Infrastructure;
using LingEdu.Exercises.Domain.Exercises;
using Microsoft.EntityFrameworkCore;

namespace LingEdu.Exercises.Infrastructure.Persistence;

public class ExercisesDbContext : DbContextBase
{
    public DbSet<Exercise> Exercises => Set<Exercise>();
    public DbSet<QuizQuestion> QuizQuestions => Set<QuizQuestion>();
    public DbSet<QuizOption> QuizOptions => Set<QuizOption>();
    public DbSet<UserProgress> Progress => Set<UserProgress>();

    public ExercisesDbContext(DbContextOptions<ExercisesDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new Configurations.ExerciseConfiguration());
        modelBuilder.ApplyConfiguration(new Configurations.QuizQuestionConfiguration());
        modelBuilder.ApplyConfiguration(new Configurations.QuizOptionConfiguration());
        modelBuilder.ApplyConfiguration(new Configurations.UserProgressConfiguration());
        base.OnModelCreating(modelBuilder);
    }
}
