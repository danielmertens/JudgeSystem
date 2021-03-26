using System;
using System.ComponentModel.DataAnnotations;

namespace JudgeSystem.Entities.Models
{
    public class Problem
    {
        [Key]
        public Guid Id { get; set; }
        public string Name { get; set; }
        public byte[] Input { get; set; }
    }
}
