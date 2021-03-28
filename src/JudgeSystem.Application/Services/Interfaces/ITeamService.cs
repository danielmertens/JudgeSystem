using JudgeSystem.Entities.Models;
using System;

namespace JudgeSystem.Application.Services.Interfaces
{
    public interface ITeamService
    {
        string CreateTeam(string name);
        Team GetTeamByApiKey(string apiKey);
        Team GetTeamById(Guid teamid);
        bool NameExists(string name);
    }
}