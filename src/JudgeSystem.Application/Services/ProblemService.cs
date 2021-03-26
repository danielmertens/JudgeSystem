using System;
using System.Linq;
using JudgeSystem.Application.Services.Interfaces;
using JudgeSystem.Entities;
using JudgeSystem.Entities.Models;

namespace JudgeSystem.Application.Services
{
    public class ProblemService : IProblemService
    {
        private readonly ApplicationDbContext _context;

        public ProblemService(ApplicationDbContext context)
        {
            _context = context;
        }

        public Guid[] GetProblemIds()
        {
            return _context.Problems.Select(p => p.Id).ToArray();
        }

        public Problem GetProblem(Guid problemId)
        {
            return _context.Problems.SingleOrDefault(p => p.Id.Equals(problemId));
        }
    }
}