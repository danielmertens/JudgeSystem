using Newtonsoft.Json;

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

        [JsonIgnore]
        public int Total => rawScore + bonusScore;
    }
}
