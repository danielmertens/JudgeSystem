using System;
using System.ComponentModel.DataAnnotations;

namespace JudgeSystem.Entities.Models
{
    // Unsure if this is going to be used atm.
    public class CodeFile
    {
        [Key]
        public Guid Id { get; set; }
        public Guid ProblemId { get; set; }
        public Guid Teamid { get; set; }
        public DateTime Timestamp { get; set; }
        public byte[] File { get; set; }

        public Problem Problem { get; set; }
        public Team Team { get; set; }
    }
}
