using JudgeSystem.Application.Models;
using System.Collections.Generic;

namespace JudgeSystem.Application.Services.Interfaces
{
    public interface IScoreService
    {
        IEnumerable<TeamScore> GetBestScoresOfAllTeams();
    }
}