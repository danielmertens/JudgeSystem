using System.Linq;

namespace JudgeSystem.Application.Models.CalculationModels
{
    public class Input
    {
        public readonly int rows;
        public readonly int columns;
        public readonly int fleetSize;
        public readonly int numbOfRides;
        public readonly int bonus;
        public readonly int steps;

        public readonly Ride[] rides;

        public Input(int rows, int columns, 
            int fleetSize, int numbOfRides, 
            int bonus, int steps, Ride[] rides)
        {
            this.rows = rows;
            this.columns = columns;
            this.fleetSize = fleetSize;
            this.numbOfRides = numbOfRides;
            this.bonus = bonus;
            this.steps = steps;
            this.rides = rides;
        }

        public static Input CreateFrom(string inputFile)
        {
            var lines = inputFile.Trim().Split('\n');
            var settingsNumbers = lines[0].Split(' ')
                .Select(x => int.Parse(x))
                .ToArray();

            var rides = new Ride[lines.Length - 1];
            for (int i = 1; i < lines.Length; i++)
            {
                rides[i - 1] = Ride.FromString(lines[i]);
            }

            return new Input(settingsNumbers[0], settingsNumbers[1],
                settingsNumbers[2], settingsNumbers[3],
                settingsNumbers[4], settingsNumbers[5], rides);
        }
    }
}
