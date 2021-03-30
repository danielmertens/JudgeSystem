using JudgeSystem.Application.Models.CalculationModels;
using System;
using System.Collections.Generic;

namespace JudgeSystem.Application.Services.Interfaces
{
    internal interface ICalculationService
    {
        Score CalculateScore(System.Guid problemId, string output);
        VisualizationModel CreateVisualization(Guid problemId, string output);
    }
}