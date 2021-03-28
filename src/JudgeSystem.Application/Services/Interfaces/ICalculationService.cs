using JudgeSystem.Application.Models.CalculationModels;

namespace JudgeSystem.Application.Services.Interfaces
{
    internal interface ICalculationService
    {
        Score CalculateScore(System.Guid problemId, string output);
    }
}