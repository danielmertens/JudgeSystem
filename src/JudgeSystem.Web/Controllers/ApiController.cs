using JudgeSystem.Application.Models;
using JudgeSystem.Application.Models.CalculationModels;
using JudgeSystem.Application.Services.Interfaces;
using JudgeSystem.Web.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Primitives;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace JudgeSystem.Web.Controllers
{
    [Route("/api")]
    public class ApiController : Controller
    {
        public static string ScoreCacheKey => "Scores";
        public static string ProblemIdCacheKey => "ProblemIds";
        public static string ProblemPrependCacheKey => "Problem_";

        public static int ScoreCacheExpirationInSeconds = 30;
        public static int ProblemCacheExpirationInMinutes = 5;

        private readonly IScoreService _scoreService;
        private readonly ISubmissionService _submissionService;
        private readonly ITeamService _teamService;
        private readonly IMemoryCache _memoryCache;
        private readonly IProblemService _problemService;
        private readonly ISettingsService _settingsService;

        public ApiController(IScoreService scoreService,
            ISubmissionService submissionService,
            IProblemService problemService,
            ITeamService teamService,
            IMemoryCache memoryCache,
            ISettingsService settingsService)
        {
            _scoreService = scoreService;
            _submissionService = submissionService;
            _teamService = teamService;
            _memoryCache = memoryCache;
            _problemService = problemService;
            _settingsService = settingsService;
        }

        [HttpGet("scores")]
        public IEnumerable<TeamScore> GetScores()
        {
            IEnumerable<TeamScore> scores;

            if (!_memoryCache.TryGetValue(ScoreCacheKey, out scores))
            {
                scores = _scoreService.GetBestScoresOfAllTeams().ToList();
                var options = new MemoryCacheEntryOptions()
                    .SetAbsoluteExpiration(TimeSpan.FromSeconds(ScoreCacheExpirationInSeconds));
                _memoryCache.Set(ScoreCacheKey, scores, options);
            }

            return scores;
        }

        [HttpPost("submit/{problemId}")]
        public IActionResult Submit([FromRoute] Guid problemId, [FromBody] byte[] output, [FromHeader] string apiKey)
        {
            var team = _teamService.GetTeamByApiKey(apiKey);

            if (team == null)
            {
                return new UnauthorizedResult();
            }

            var score = _submissionService.SubmitOutput(team.Id, problemId, output);

            return Ok(score);
        }

        [HttpGet("problem")]
        public IActionResult GetProblems([FromHeader] string apiKey)
        {
            var team = _teamService.GetTeamByApiKey(apiKey);

            if (team == null)
            {
                return new UnauthorizedResult();
            }

            if (!_settingsService.Settings.CompetitionStarted)
            {
                return new UnauthorizedResult();
            }

            var problems = _memoryCache.GetOrCreate(ProblemIdCacheKey, (entry) =>
            {
                entry.SetAbsoluteExpiration(TimeSpan.FromMinutes(ProblemCacheExpirationInMinutes));
                return _problemService.GetProblemIds();
            });

            if (problems.Length == 0) return NoContent();

            return Ok(problems);
        }

        [HttpGet("problem/{problemId}")]
        public IActionResult GetProblemById([FromRoute] Guid problemId, [FromHeader] string apiKey)
        {
            var team = _teamService.GetTeamByApiKey(apiKey);

            if (team == null)
            {
                return new UnauthorizedResult();
            }

            byte[] problem;

            if (!_memoryCache.TryGetValue(ProblemPrependCacheKey + problemId.ToString(), out problem))
            {
                problem = _problemService.GetProblem(problemId);
                if (problem == null) return NotFound();
                if (problem.Length == 0) return NoContent();

                var options = new MemoryCacheEntryOptions()
                    .SetAbsoluteExpiration(TimeSpan.FromMinutes(ProblemCacheExpirationInMinutes));
                _memoryCache.Set(ProblemPrependCacheKey + problemId.ToString(), problem, options);
            }

            return Ok(problem);
        }
    }
}