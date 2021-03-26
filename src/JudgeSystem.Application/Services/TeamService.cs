using JudgeSystem.Application.Services.Interfaces;
using JudgeSystem.Entities;
using JudgeSystem.Entities.Models;
using System;
using System.Linq;

namespace JudgeSystem.Application.Services
{
    internal class TeamService : ITeamService
    {
        private readonly ApplicationDbContext _context;

        public TeamService(ApplicationDbContext context)
        {
            _context = context;
        }

        public Team GetTeam(string apiKey)
        {
            return _context.Teams.FirstOrDefault(t => t.ApiKey == apiKey);
        }

        public string CreateTeam(string name)
        {
            var team = new Team
            {
                Id = Guid.NewGuid(),
                ApiKey = Guid.NewGuid().ToString(),
                Name = name
            };

            _context.Teams.Add(team);
            _context.SaveChanges();

            return team.ApiKey;
        }

        public bool NameExists(string name)
        {
            return _context.Teams.Any(t => t.Name == name);
        }
    }
}
