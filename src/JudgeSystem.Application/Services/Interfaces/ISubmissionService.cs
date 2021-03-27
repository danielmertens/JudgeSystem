using JudgeSystem.Application.Models;
using System;
using System.Collections.Generic;

namespace JudgeSystem.Application.Services.Interfaces
{
    public interface ISubmissionService
    {
        IEnumerable<SolutionOutput> GetTeamSubmissions(Guid teamId);
        long SubmitOutput(Guid teamId, Guid problemId, byte[] output);
    }
}