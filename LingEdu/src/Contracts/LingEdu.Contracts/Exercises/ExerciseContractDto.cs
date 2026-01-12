using System;
using System.Collections.Generic;
using System.Text;

namespace LingEdu.Contracts.Exercises
{
    public sealed class ExerciseContractDto
    {
        public Guid Id { get; init; }

        public string Title { get; init; } = default!;

        public string Type { get; init; } = default!;

        public string Level { get; init; } = default!;

        public bool IsPremium { get; init; }
        public List<QuizQuestionContractDto> Questions { get; init; } = new();
    }

    public sealed class QuizQuestionContractDto
    {
        public Guid Id { get; init; }

        public string Prompt { get; init; } = default!;

        public List<QuizOptionContractDto> Options { get; init; } = new();
    }

    public sealed class QuizOptionContractDto
    {
        public Guid Id { get; init; }

        public string Text { get; init; } = default!;
    }
}
