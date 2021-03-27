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

        public Team GetTeamByApiKey(string apiKey)
        {
            return _context.Teams.FirstOrDefault(t => t.ApiKey == apiKey);
        }

        public Team GetTeamById(Guid teamid)
        {
            return _context.Teams.FirstOrDefault(t => t.Id == teamid);
        }

        public string CreateTeam(string name)
        {
            var team = new Team
            {
                Id = Guid.NewGuid(),
                ApiKey = CreateSecureGuid().ToString(),
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

        // Just a different way to generate them for the apikey
        // to discurage any shananigans.
        private Guid CreateSecureGuid()
        {
            var rng = new System.Security.Cryptography.RNGCryptoServiceProvider();
            var data = new byte[16];
            rng.GetBytes(data);
            return new Guid(data);
        }
    }
}
