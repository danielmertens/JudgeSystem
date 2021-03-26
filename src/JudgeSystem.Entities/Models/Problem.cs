using System;
using System.ComponentModel.DataAnnotations;

namespace JudgeSystem.Entities.Models
{
    public class Problem : ProblemDetails
    {
        public byte[] Input { get; set; }
    }
}