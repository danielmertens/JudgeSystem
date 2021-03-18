using System;

namespace JudgeSystem.Application.Services.Interfaces
{
    public interface ISubmissionService
    {
        long SubmitOutput(Guid teamId, Guid problemId, byte[] output);
    }
}