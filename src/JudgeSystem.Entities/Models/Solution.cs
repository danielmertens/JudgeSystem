using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace JudgeSystem.Entities.Models
{
    public class Solution
    {
        [Key]
        public Guid Id { get; set; }
        public Guid ProblemId { get; set; }
        public Guid TeamId { get; set; }
        public DateTime Timestamp { get; set; }
        public long Score { get; set; }
        public byte[] Output { get; set; }
        public string ScoreOutput { get; set; }

        public Problem Problem { get; set; }
        public Team Team { get; set; }
    }
}
