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
    }
}
