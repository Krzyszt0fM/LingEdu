using System;
using System.Collections.Generic;
using LingEdu.BuildingBlocks.Domain.Models;
using LingEdu.Exercises.Domain.Enums;

namespace LingEdu.Exercises.Domain.Exercises
{
    public sealed class Exercise : AggregateRoot
    {
        private Exercise()
        {
        }

        public Exercise(Guid id, string title, ExerciseLevel level, ExerciseType type, bool isPremium)
            : base(id)
        {
            Title = title;
            Level = level;
            Type = type;
            IsPremium = isPremium;
        }

        public string Title { get; private set; } = default!;

        public ExerciseLevel Level { get; private set; }

        public ExerciseType Type { get; private set; }

        public bool IsPremium { get; private set; }

        public ICollection<QuizQuestion> Questions { get; private set; } = new List<QuizQuestion>();
    }
}
