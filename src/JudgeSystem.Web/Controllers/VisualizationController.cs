using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JudgeSystem.Application.Models.CalculationModels;
using JudgeSystem.Application.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace JudgeSystem.Web.Controllers
{
    public class VisualizationController : Controller
    {
        private readonly ISubmissionService _submissionService;

        public VisualizationController(ISubmissionService submissionService)
        {
            _submissionService = submissionService;
        }

        [HttpGet("/visualization/model/{solutionId}")]
        public IActionResult GetVisualizationModel([FromRoute] Guid solutionId)
        {
            var model = _submissionService.GetVisualization(solutionId);

            //var txt = JsonConvert.SerializeObject(model);

            return Json(model);
        }

        [HttpGet("/visualization/{solutionId}")]
        public IActionResult Index(Guid solutionId)
        {
            var tup = _submissionService.GetScore(solutionId);
            if (tup.Item2 != null)
            {
                ViewBag.ProblemName = tup.Item1;
                return View(tup.Item2);
            }

            return NotFound();
        }
    }
}
