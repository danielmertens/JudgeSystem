using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JudgeSystem.Application.Models;
using JudgeSystem.Application.Services.Interfaces;
using Microsoft.Extensions.Caching.Memory;
using JudgeSystem.Entities.Models;

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

        public ApiController(IScoreService scoreService,
            ISubmissionService submissionService,
            ITeamService teamService,
            IMemoryCache memoryCache)
        {
            _scoreService = scoreService;
            _submissionService = submissionService;
            _teamService = teamService;
            _memoryCache = memoryCache;
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
            var team = _teamService.GetTeam(apiKey);

            if (team == null)
            {
                return new UnauthorizedResult();
            }

            var score = _submissionService.SubmitOutput(team.Id, problemId, output);

            return Ok(score);
        }

        [HttpGet("problem")]
        public IActionResult GetProblemIds()
        {
            IEnumerable<Guid> ids;

            if (!_memoryCache.TryGetValue(ProblemIdCacheKey, out ids))
            {
                ids = _submissionService.GetActiveInputFileIds().ToList();
                var options = new MemoryCacheEntryOptions()
                    .SetAbsoluteExpiration(TimeSpan.FromMinutes(ProblemCacheExpirationInMinutes));
                _memoryCache.Set(ProblemIdCacheKey, ids, options);
            }

            return Ok(ids);
        }

        [HttpGet("problem/{id}")]
        public IActionResult GetProblemDetails([FromRoute] string id)
        {
            Problem problem;

            id = id.ToUpperInvariant();

            if (!_memoryCache.TryGetValue(ProblemPrependCacheKey + id, out problem))
            {
                if (Guid.TryParse(id, out Guid guidId))
                {
                    problem = _submissionService.GetProblemDetails(guidId);
                    if (problem == null) return BadRequest("Problem not found.");

                    var options = new MemoryCacheEntryOptions()
                        .SetAbsoluteExpiration(TimeSpan.FromMinutes(ProblemCacheExpirationInMinutes));
                    _memoryCache.Set(ProblemPrependCacheKey + id, problem, options);

                    return Ok(problem);
                }
                return BadRequest("No valid Id passed in Url.");
            }

            return Ok(problem);            
        }
    }
}
