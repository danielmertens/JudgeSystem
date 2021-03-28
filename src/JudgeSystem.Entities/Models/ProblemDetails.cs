using System;
using System.ComponentModel.DataAnnotations;

namespace JudgeSystem.Entities.Models
{
    public class ProblemDetails
    {
        [Key]
        public Guid Id { get; set; }
        public string Name { get; set; }
    }
}