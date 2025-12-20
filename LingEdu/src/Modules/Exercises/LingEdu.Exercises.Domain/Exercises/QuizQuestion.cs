using LingEdu.BuildingBlocks.Domain.Models;
using Microsoft.VisualBasic.FileIO;
using System;
using System.Collections.Generic;

namespace LingEdu.Exercises.Domain.Exercises
{
    public sealed class QuizQuestion : Entity
    {
        private QuizQuestion()
        {
        }

        public QuizQuestion(Guid id, Guid exerciseId, string prompt)
            : base(id)
        {
            ExerciseId = exerciseId;
            Prompt = prompt;
        }

        public Guid ExerciseId { get; private set; }

        public string Prompt { get; private set; } = default!;

        public ICollection<QuizOption> Options { get; private set; } = new List<QuizOption>();
    }
}
