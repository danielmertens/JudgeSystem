using System;
using System.Collections.Generic;
using System.Text;

namespace JudgeSystem.Application.Models.CalculationModels
{
    public class Car
    {
        public int x;
        public int y;
        public int step;

        public Car() { }

        public int DistanceTo(int x, int y) => Util.Distance(this.x, this.y, x, y);

        public int DistanceToRideStart(Ride ride) => Util.Distance(this.x, this.y, ride.startRow, ride.startColumn);

        public int WaitTime(Ride ride) => Math.Max(0, ride.earliest - (step + DistanceToRideStart(ride)));

        public int Arrival(Ride ride) => step + DistanceToRideStart(ride) + WaitTime(ride) + ride.Distance;

        public bool CanStartOnTime(Ride ride) => step + DistanceToRideStart(ride) <= ride.earliest;

        public bool CanFinishOnTime(Ride ride, int maxSteps) => Arrival(ride) <= Math.Min(ride.latest, maxSteps);

        public void Add(Ride ride)
        {
            //var departureStep = Math.Max(ride.earliest, step + DistanceToRideStart(ride));
            //step = departureStep + ride.Distance;
            step = Arrival(ride);
            x = ride.endRow;
            y = ride.endColumn;
        }
    }
}
