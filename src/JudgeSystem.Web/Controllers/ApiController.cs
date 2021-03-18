using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JudgeSystem.Application.Models;
using JudgeSystem.Application.Services.Interfaces;

namespace JudgeSystem.Web.Controllers
{
    [Route("/api")]
    public class ApiController : Controller
    {
        private readonly IScoreService _scoreService;
        private readonly ISubmissionService _submissionService;
        private readonly ITeamService _teamService;

        public ApiController(IScoreService scoreService,
            ISubmissionService submissionService,
            ITeamService teamService)
        {
            _scoreService = scoreService;
            _submissionService = submissionService;
            _teamService = teamService;
        }

        [HttpGet("scores")]
        public IEnumerable<TeamScore> GetScores()
        {
            return _scoreService.GetBestScoresOfAllTeams();
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
    }
}
