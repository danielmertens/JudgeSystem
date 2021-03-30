using JudgeSystem.Application.Models;
using JudgeSystem.Application.Models.CalculationModels;
using JudgeSystem.Application.Services.Interfaces;
using JudgeSystem.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace JudgeSystem.Application.Services
{
    internal class ScoreService : IScoreService
    {
        private readonly ApplicationDbContext _context;

        public ScoreService(ApplicationDbContext context)
        {
            _context = context;
        }

        public IEnumerable<TeamScore> GetBestScoresOfAllTeams()
        {
            return _context.Teams
                .Include(t => t.Solutions)
                .Select(t => new
                {
                    Id = t.Id,
                    Name = t.Name,
                    Solutions = t.Solutions.Select(x => new { x.Id, x.ProblemId, x.Score })
                })
                .ToList()
                .GroupBy(t => t.Id)
                .Select(g =>
                {
                    var bestScore = g
                        .SelectMany(t => t.Solutions)
                        .GroupBy(s => s.ProblemId)
                        .Select(pg =>
                        {
                            return pg.Max(s => s.Score);
                        })
                        .Sum();

                    return new TeamScore
                    {
                        Id = g.Key,
                        TeamName = g.First().Name,
                        Score = bestScore
                    };
                })
                .OrderByDescending(ts => ts.Score)
                .ToList();
        }
    }
}
