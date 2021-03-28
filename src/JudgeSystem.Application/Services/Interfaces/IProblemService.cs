using JudgeSystem.Entities.Models;
using System;

namespace JudgeSystem.Application.Services.Interfaces
{
    public interface IProblemService
    {
        ProblemDetails[] GetProblemIds();
        byte[] GetProblem(Guid problem);
        void SaveProblem(string name, byte[] content);
    }
}