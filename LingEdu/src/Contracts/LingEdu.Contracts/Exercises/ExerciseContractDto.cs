namespace LingEdu.Contracts.Exercises;

public record ExerciseContractDto(Guid Id, string Title, string Level, bool IsPremium, IReadOnlyList<QuizQuestionContractDto> Questions);

public record QuizQuestionContractDto(Guid Id, string Question, IReadOnlyList<QuizOptionContractDto> Options);

public record QuizOptionContractDto(Guid Id, string Text);

public record SubmitSolutionRequest(IReadOnlyList<Guid> SelectedOptionIds);

public record ExerciseResultDto(bool IsCorrect, int ScoreAwarded);
