using System;
using JudgeSystem.Entities.Models;

namespace JudgeSystem.Application.Services.Interfaces
{
    public interface IProblemService
    {
        ProblemDetails[] GetProblemIds();
        byte[] GetProblem(Guid problem);
    }
}