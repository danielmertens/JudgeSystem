using JudgeSystem.Entities.Models;

namespace JudgeSystem.Application.Services.Interfaces
{
    public interface ITeamService
    {
        string CreateTeam(string name);
        Team GetTeam(string apiKey);
    }
}