using System;

namespace JudgeSystem.Application.Models
{
    public class TeamScore
    {
        public string TeamName { get; set; }
        public long Score { get; set; }
        public Guid Id { get; internal set; }
    }
}
