using JudgeSystem.Application.Models;
using JudgeSystem.Application.Models.CalculationModels;
using JudgeSystem.Application.Services.Interfaces;
using JudgeSystem.Entities;
using JudgeSystem.Entities.Models;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JudgeSystem.Application.Services
{
    internal class SubmissionService : ISubmissionService
    {
        private readonly ICalculationService _calculationService;
        private readonly ApplicationDbContext _context;

        public SubmissionService(ApplicationDbContext context, ICalculationService calculationService)
        {
            _calculationService = calculationService;
            _context = context;
        }

        public long SubmitOutput(Guid teamId, Guid problemId, byte[] output)
        {
            var text = Encoding.UTF8.GetString(output);

            var score = _calculationService.CalculateScore(problemId, text);

            if (!string.IsNullOrEmpty(score.errorMessage))
            {
                score.bonusScore = 0;
                score.rawScore = 0;
            }

            var solution = new Solution
            {
                Id = Guid.NewGuid(),
                Output = output,
                ProblemId = problemId,
                Score = score.Total,
                ScoreOutput = JsonConvert.SerializeObject(score),
                TeamId = teamId,
                Timestamp = DateTime.UtcNow
            };

            _context.Add(solution);
            _context.SaveChanges();

            return score.Total;
        }

        public IEnumerable<SolutionOutput> GetTeamSubmissions(Guid teamId)
        {
            return _context.Solutions
                .Include(s => s.Problem)
                .AsNoTracking()
                .Where(s => s.TeamId == teamId)
                .Select(s => new
                {
                    s.Id,
                    s.ScoreOutput,
                    s.Problem.Name,
                    s.Timestamp
                })
                .OrderByDescending(s => s.Timestamp)
                .ToList()
                .Select(s =>
                    new SolutionOutput
                    {
                        SolutionId = s.Id,
                        ProblemName = s.Name,
                        Timestamp = s.Timestamp,
                        ScoreOutput = JsonConvert.DeserializeObject<Score>(s.ScoreOutput)
                    });
        }

        public VisualizationModel GetVisualization(Guid solutionId)
        {
            var solution = _context.Solutions
                .AsNoTracking()
                .Where(s => s.Id == solutionId)
                .Select(s => s.ScoreOutput)
                .ToList();
            if (!solution.Any()) return null;
            Score score = JsonConvert.DeserializeObject<Score>(solution[0]);

            if (!string.IsNullOrEmpty(score.errorMessage)) return null;

            var solutionDetails = _context.Solutions
                .AsNoTracking()
                .Where(s => s.Id == solutionId)
                .Select(s => new
                {
                    s.Id,
                    s.Output,
                    s.ProblemId
                })
                .First();

            var text = Encoding.UTF8.GetString(solutionDetails.Output);
            return _calculationService.CreateVisualization(solutionDetails.ProblemId, text);
        }

        public (string, Score) GetScore(Guid solutionId)
        {
            var result = _context.Solutions
                .Include(s => s.Problem)
                .AsNoTracking()
                .Where(s => s.Id == solutionId)
                .Select(s => new
                {
                    s.ScoreOutput,
                    s.Problem.Name
                })
                .FirstOrDefault();

            if (result == null) return ("", null);

            var score = JsonConvert.DeserializeObject<Score>(result.ScoreOutput);
            return (result.Name, score);
        }
    }
}
