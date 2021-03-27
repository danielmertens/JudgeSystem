using System;

namespace JudgeSystem.Application.Models.CalculationModels
{
    public static class Util
    {
        public static int Distance(int x1, int y1, int x2, int y2)
        {
            return Math.Abs(x2 - x1) + Math.Abs(y2 - y1);
        }
    }
}
