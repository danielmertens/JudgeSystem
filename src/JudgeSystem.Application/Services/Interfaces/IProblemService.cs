using System;
using JudgeSystem.Entities.Models;

namespace JudgeSystem.Application.Services.Interfaces
{
    public interface IProblemService
    {
        Guid[] GetProblemIds();
        Problem GetProblem(Guid problem);
    }
}