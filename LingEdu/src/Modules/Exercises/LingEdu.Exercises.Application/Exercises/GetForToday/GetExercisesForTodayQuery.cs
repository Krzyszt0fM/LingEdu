using System.Collections.Generic;
using LingEdu.BuildingBlocks.Application.Cqrs;
using LingEdu.Contracts.Exercises;

namespace LingEdu.Exercises.Application.Exercises.GetForToday
{
    public sealed class GetExercisesForTodayQuery : IQuery<List<ExerciseContractDto>>
    {
        public GetExercisesForTodayQuery()
        {
        }
    }
}
