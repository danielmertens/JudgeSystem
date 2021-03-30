using JudgeSystem.Application.Models;
using JudgeSystem.Application.Models.CalculationModels;
using System;
using System.Collections.Generic;

namespace JudgeSystem.Application.Services.Interfaces
{
    public interface ISubmissionService
    {
        IEnumerable<SolutionOutput> GetTeamSubmissions(Guid teamId);
        VisualizationModel GetVisualization(Guid solutionId);
        (string, Score) GetScore(Guid solutionId);
        long SubmitOutput(Guid teamId, Guid problemId, byte[] output);
    }
}