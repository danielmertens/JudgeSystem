using JudgeSystem.Application.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

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
        public IActionResult AddInput(string name, IFormFile file)
        {
            using (MemoryStream memStream = new MemoryStream())
            {
                file.CopyTo(memStream);
                memStream.Position = 0;
                _problemService.SaveProblem(name, memStream.ToArray());
            }

            return Ok();
        }
    }
}
