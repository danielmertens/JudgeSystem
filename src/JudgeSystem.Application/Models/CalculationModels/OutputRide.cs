using System;
using System.Collections.Generic;
using System.Text;

namespace JudgeSystem.Application.Models.CalculationModels
{
    public class OutputRide
    {
        public readonly int x1;
        public readonly int y1;

        public readonly int x2;
        public readonly int y2;

        public readonly byte state; // 0 = taken, 1 = bonus, 2 = late

        public OutputRide(int x1, int y1, int x2, int y2, byte state)
        {
            this.x1 = x1;
            this.y1 = y1;
            this.x2 = x2;
            this.y2 = y2;
            this.state = state;
        }
    }
}
