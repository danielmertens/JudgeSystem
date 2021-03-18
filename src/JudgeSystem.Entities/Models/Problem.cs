using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

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
