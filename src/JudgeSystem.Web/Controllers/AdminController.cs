using JudgeSystem.Application.Services.Interfaces;
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

        public AdminController(IProblemService problemService)
        {
            _problemService = problemService;
        }

        public IActionResult Index()
        {
            return View();
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
    }
}
