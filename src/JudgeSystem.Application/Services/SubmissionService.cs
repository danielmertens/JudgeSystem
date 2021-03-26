using JudgeSystem.Application.Services.Interfaces;
using JudgeSystem.Entities;
using JudgeSystem.Entities.Models;
using System;
using System.Collections;
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
                return 0;
            }

            var solution = new Solution
            {
                Id = Guid.NewGuid(),
                Output = output,
                ProblemId = problemId,
                Score = score.Total,
                TeamId = teamId,
                Timestamp = DateTime.UtcNow
            };

            _context.Add(solution);

            return score.Total;
        }
    }
}
