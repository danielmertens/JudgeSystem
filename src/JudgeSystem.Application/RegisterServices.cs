using JudgeSystem.Application.Services;
using JudgeSystem.Application.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace JudgeSystem.Application
{
    public class RegisterServices
    {
        public static void Register(IServiceCollection services)
        {
            services.AddTransient<IScoreService, ScoreService>();
            services.AddTransient<ISubmissionService, SubmissionService>();
            services.AddTransient<ITeamService, TeamService>();
        }
    }
}
