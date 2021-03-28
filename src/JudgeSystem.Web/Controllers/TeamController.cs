using JudgeSystem.Application.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Text.RegularExpressions;

namespace JudgeSystem.Web.Controllers
{
    public class TeamController : Controller
    {
        private static readonly Regex SpecialCharacterRegex = new Regex(@"[^a-zA-z0-9\s!@#&$+\-\*]");

        private readonly ITeamService _teamService;
        private readonly ISubmissionService _submissionService;

        public TeamController(ITeamService teamService,
            ISubmissionService submissionService)
        {
            _teamService = teamService;
            _submissionService = submissionService;
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(string teamName)
        {
            if (string.IsNullOrWhiteSpace(teamName))
            {
                ViewBag.Error = "No name entered.";
                return View();
            }

            if (teamName.Length > 25)
            {
                ViewBag.Error = "Team name exceeds the maximum length of 25 characters.";
                return View();
            }

            if (SpecialCharacterRegex.IsMatch(teamName))
            {
                var specialCharacters = SpecialCharacterRegex.Matches(teamName).Select(m => m.Value);

                ViewBag.Error = $"Following characters are not allowed '{string.Join("", specialCharacters)}' in team name";
                return View();
            }

            if (_teamService.NameExists(teamName))
            {
                ViewBag.Error = $"Team name {teamName} already exists.";
                return View();
            }

            var token = _teamService.CreateTeam(teamName);
            ViewBag.Token = token;
            ViewBag.TeamName = teamName;

            return View("Success");
        }

        [HttpGet("/Team/{teamId}")]
        public IActionResult TeamOverview(Guid teamId)
        {
            var team = _teamService.GetTeamById(teamId);

            if (team == null)
            {
                return new NotFoundResult();
            }

            ViewBag.TeamName = team.Name;

            var submissions = _submissionService.GetTeamSubmissions(teamId);
            return View(submissions);
        }
    }
}
