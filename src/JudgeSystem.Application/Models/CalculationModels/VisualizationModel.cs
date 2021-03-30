using System.Collections.Generic;
using System.Linq;

namespace JudgeSystem.Application.Models.CalculationModels
{
    public class VisualizationModel
    {
        public OutputRide[] rides;
        public int width;
        public int height;

        public VisualizationModel(IEnumerable<OutputRide> rides, int width, int height)
        {
            this.rides = rides.ToArray();
            this.width = width;
            this.height = height;
        }
    }
}
