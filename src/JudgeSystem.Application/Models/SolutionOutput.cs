using JudgeSystem.Application.Models.CalculationModels;
using System;

namespace JudgeSystem.Application.Models
{
    public class SolutionOutput
    {
        public string ProblemName { get; set; }
        public DateTime Timestamp { get; set; }
        public Score ScoreOutput { get; set; }
    }
}
