using System;
using System.Collections.Generic;

namespace LingEdu.WebApi.Contracts.Requests
{
    public sealed record SubmitExerciseAnswerRequest(Guid QuestionId, Guid SelectedOptionId);

    public sealed record SubmitExerciseRequest(List<SubmitExerciseAnswerRequest> Answers);
}
