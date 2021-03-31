using JudgeSystem.Application.Services.Interfaces;
using JudgeSystem.Web.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.IO;

namespace JudgeSystem.Web.Controllers
{
    [Authorize]
    public class AdminController : Controller
    {
        private readonly IProblemService _problemService;
        private readonly ISettingsService _settingsService;

        public AdminController(IProblemService problemService, 
            ISettingsService settingsService)
        {
            _problemService = problemService;
            _settingsService = settingsService;
        }

        public IActionResult Index()
        {
            return View(_settingsService.Settings);
        }

        public IActionResult AddInput()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddInput([Required] string name, [Required] IFormFile file)
        {
            if (ModelState.IsValid)
            {
                using (MemoryStream memStream = new MemoryStream())
                {
                    file.CopyTo(memStream);
                    memStream.Position = 0;
                    _problemService.SaveProblem(name, memStream.ToArray());
                }
                return View("Index");
            }

            return View();
        }

        [HttpGet("ToggleCompetition")]
        public IActionResult ToggleCompetition()
        {
            _settingsService.Settings.CompetitionStarted = !_settingsService.Settings.CompetitionStarted;
            return RedirectToAction("Index");
        }
    }
}
