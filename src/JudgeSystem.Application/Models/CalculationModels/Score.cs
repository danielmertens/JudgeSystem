using System;
using System.Collections.Generic;
using System.Text;

namespace JudgeSystem.Application.Models.CalculationModels
{
    public class Score
    {
        // scores
        public int rawScore;
        public int bonusScore;

        // Diagnostic values
        public int taken;
        public int unassigned;
        public int late;
        public int bonus;
        public int waitTime;

        public string errorMessage;

        public int Total => rawScore + bonusScore;
    }
}
