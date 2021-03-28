using System.Linq;

namespace JudgeSystem.Application.Models.CalculationModels
{
    public class Ride
    {
        public readonly int startRow;
        public readonly int startColumn;

        public readonly int endRow;
        public readonly int endColumn;

        public readonly int earliest;
        public readonly int latest;

        public Ride(int startRow, int startColumn,
            int endRow, int endColumn,
            int earliest, int latest)
        {
            this.startRow = startRow;
            this.startColumn = startColumn;

            this.endRow = endRow;
            this.endColumn = endColumn;

            this.earliest = earliest;
            this.latest = latest;
        }

        public int Distance => Util.Distance(startRow, startColumn, endRow, endColumn);

        public static Ride FromString(string line)
        {
            var split = line.Split(' ').Select(x => int.Parse(x)).ToArray();
            return new Ride(split[0], split[1], split[2],
                split[3], split[4], split[5]);
        }
    }
}
