using System.Linq;
using LingEdu.Contracts.Exercises;
using LingEdu.Exercises.Domain.Exercises;

namespace LingEdu.Exercises.Application.Exercises
{
    internal static class ExerciseMappingExtensions
    {
        public static ExerciseContractDto ToContract(this Exercise exercise)
        {
            return new ExerciseContractDto
            {
                Id = exercise.Id,
                Title = exercise.Title,
                Type = exercise.Type.ToString(),
                Level = exercise.Level.ToString(),
                IsPremium = exercise.IsPremium
            };
        }

        public static ExerciseContractDto ToContractWithQuestions(this Exercise exercise)
        {
            return new ExerciseContractDto
            {
                Id = exercise.Id,
                Title = exercise.Title,
                Type = exercise.Type.ToString(),
                Level = exercise.Level.ToString(),
                IsPremium = exercise.IsPremium,
                Questions = exercise.Questions.Select(q => new QuizQuestionContractDto
                {
                    Id = q.Id,
                    Prompt = q.Prompt,
                    Options = q.Options.Select(o => new QuizOptionContractDto
                    {
                        Id = o.Id,
                        Text = o.Text
                    }).ToList()
                }).ToList()
            };
        }
    }
}
