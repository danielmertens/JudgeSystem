using JudgeSystem.Entities.Models;
using System;
using System.Collections.Generic;

namespace JudgeSystem.Application.Services.Interfaces
{
    public interface ISubmissionService
    {
        IEnumerable<Guid> GetActiveInputFileIds();
        Problem GetProblemDetails(Guid id);
        long SubmitOutput(Guid teamId, Guid problemId, byte[] output);
    }
}