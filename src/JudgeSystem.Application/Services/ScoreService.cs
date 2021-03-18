using JudgeSystem.Application.Models;
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
                .ThenInclude(s => s.Problem)
                .GroupBy(t => t.Id)
                .ToList()
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
                        TeamName = g.First().Name,
                        Score = bestScore
                    };
                })
                .OrderByDescending(ts => ts.Score)
                .ToList();
        }
    }
}
