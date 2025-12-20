using System;
using LingEdu.BuildingBlocks.Domain.Models;

namespace LingEdu.Exercises.Domain.Exercises
{
    public sealed class QuizOption : Entity
    {
        private QuizOption()
        {
        }

        public QuizOption(Guid id, Guid questionId, string text, bool isCorrect)
            : base(id)
        {
            QuestionId = questionId;
            Text = text;
            IsCorrect = isCorrect;
        }

        public Guid QuestionId { get; private set; }

        public string Text { get; private set; } = default!;

        public bool IsCorrect { get; private set; }
    }
}
