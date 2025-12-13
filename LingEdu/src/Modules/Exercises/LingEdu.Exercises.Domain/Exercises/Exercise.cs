using LingEdu.BuildingBlocks.Domain;

namespace LingEdu.Exercises.Domain.Exercises;

public class Exercise : AggregateRoot
{
    public string Title { get; private set; } = string.Empty;
    public string Level { get; private set; } = string.Empty;
    public bool IsPremium { get; private set; }
    public ICollection<QuizQuestion> Questions { get; private set; } = new List<QuizQuestion>();
}

public class QuizQuestion : Entity
{
    public string Question { get; private set; } = string.Empty;
    public Guid ExerciseId { get; private set; }
    public Exercise Exercise { get; private set; } = null!;
    public ICollection<QuizOption> Options { get; private set; } = new List<QuizOption>();
}

public class QuizOption : Entity
{
    public string Text { get; private set; } = string.Empty;
    public bool IsCorrect { get; private set; }
    public Guid QuizQuestionId { get; private set; }
    public QuizQuestion QuizQuestion { get; private set; } = null!;
}

public class UserProgress : Entity
{
    public Guid UserId { get; private set; }
    public Guid ExerciseId { get; private set; }
    public bool Completed { get; private set; }
    public int Score { get; private set; }
}
