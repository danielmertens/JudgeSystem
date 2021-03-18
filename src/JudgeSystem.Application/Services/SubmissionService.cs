using JudgeSystem.Application.Services.Interfaces;
using JudgeSystem.Entities;
using JudgeSystem.Entities.Models;
using System;
using System.Text;

namespace JudgeSystem.Application.Services
{
    internal class SubmissionService : ISubmissionService
    {
        private readonly ICalculationService _calculationService;
        private readonly ApplicationDbContext _context;

        public SubmissionService(ApplicationDbContext context)
        {
            _calculationService = new CalculationService();
            _context = context;
        }

        public long SubmitOutput(Guid teamId, Guid problemId, byte[] output)
        {
            var text = Encoding.UTF8.GetString(output);

            var score = _calculationService.CalculateScore(text);

            var solution = new Solution
            {
                Id = Guid.NewGuid(),
                Output = output,
                ProblemId = problemId,
                Score = score,
                TeamId = teamId,
                Timestamp = DateTime.UtcNow
            };

            _context.Add(solution);

            return score;
        }
    }
}
