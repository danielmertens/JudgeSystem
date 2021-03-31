using JudgeSystem.Application.Models.CalculationModels;
using JudgeSystem.Application.Services.Interfaces;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JudgeSystem.Application.Services
{
    internal class CalculationService : ICalculationService
    {
        private readonly IProblemService _problemService;
        private readonly IMemoryCache _memoryCache;

        public CalculationService(IProblemService problemService,
            IMemoryCache memoryCache)
        {
            _problemService = problemService;
            _memoryCache = memoryCache;
        }

        private Input GetInput(Guid problemId)
        {
            return _memoryCache.GetOrCreate(problemId.ToString(), entry =>
            {
                entry.SetAbsoluteExpiration(TimeSpan.FromMinutes(30));

                var bytes = _problemService.GetProblem(problemId);
                return Input.CreateFrom(Encoding.UTF8.GetString(bytes));
            });
        }

        public Score CalculateScore(Guid problemId, string output)
        {
            var score = new Score();
            var input = GetInput(problemId);

            var allVehicleRides = output.Trim().Split('\n');

            if (input.fleetSize < allVehicleRides.Length)
            {
                score.errorMessage = "Output size is not correct. There should be one entry for every car in the fleet.";
                return score;
            }

            var rideHash = new HashSet<int>();

            for (int i = 0; i < allVehicleRides.Length; i++)
            {
                var car = new Car();
                var idList = allVehicleRides[i].Trim().Split(' ');

                for (int j = 1; j < idList.Length; j++)
                {
                    int id;
                    // Check that number can be converted
                    if (!int.TryParse(idList[j], out id))
                    {
                        score = new Score();
                        score.errorMessage = "Failed to parse a number in the output. Check that your output is correct.";
                        return score;
                    }

                    // Check parsed number is valid
                    if (id < 0 || id >= input.numbOfRides)
                    {
                        score = new Score();
                        score.errorMessage = "Invalid ride number passed.";
                        return score;
                    }

                    // Check that the ride is only used once.
                    if (rideHash.Contains(id))
                    {
                        score = new Score();
                        score.errorMessage = "Same ride was used twice in the output.";
                        return score;
                    }

                    rideHash.Add(id);

                    // Actual calculation here
                    Evaluate(car, input.rides[id], score, input.bonus, input.steps);
                }
            }

            score.unassigned = input.numbOfRides - rideHash.Count;

            return score;
        }

        private byte Evaluate(Car car, Ride ride, Score score, int bonus, int steps)
        {
            byte state = 0;
            score.taken++;
            if (car.CanFinishOnTime(ride, steps))
            {
                if (car.CanStartOnTime(ride))
                {
                    score.bonusScore += bonus;
                    score.bonus++;
                    score.waitTime += car.WaitTime(ride);
                    state = 1;
                }
                score.rawScore += ride.Distance;
                //car.Add(ride);
            }
            else
            {
                //car.step = car.Arrival(ride);
                //car.x = ride.endRow;
                //car.y = ride.endColumn;
                //car.Add(ride);
                score.late++;
                state = 2;
            }

            car.Add(ride);
            return state;
        }

        public VisualizationModel CreateVisualization(Guid problemId, string output)
        {
            var score = new Score();
            var list = new List<OutputRide>(100);
            var input = GetInput(problemId);

            var allVehicleRides = output.Trim().Split('\n');
            var rideHash = new HashSet<int>();
            var random = new Random();

            var selection = new List<string[]>(50);
            var count = 0;

            if (allVehicleRides.Length < 20)
            {
                selection = allVehicleRides.Select(x => x.Trim().Split(' ')).ToList();
            }
            else
            {
                for (int i = 0; i < 100 && count < 100; i++)
                {
                    var numb = random.Next(0, allVehicleRides.Length);
                    if (rideHash.Contains(numb))
                    {
                        i--;
                        continue;
                    }
                    rideHash.Add(numb);
                    var ids = allVehicleRides[numb].Trim().Split(' ');
                    count += ids.Length;
                    selection.Add(ids);
                }
            }

            for (int i = 0; i < selection.Count; i++)
            {
                var car = new Car();
                
                for (int j = 1; j < selection[i].Length; j++)
                {
                    int id = int.Parse(selection[i][j]);
                    var ride = input.rides[id];
                    var state = Evaluate(car, ride, score, input.bonus, input.steps);
                    list.Add(new OutputRide(ride.startColumn, ride.startRow,
                        ride.endColumn, ride.endRow, state));
                }
            }

            return new VisualizationModel(list, input.columns, input.rows);
        }
    }
}
