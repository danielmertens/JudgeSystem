using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace JudgeSystem.Entities.Models
{
    public class Team
    {
        [Key]
        public Guid Id { get; set; }
        public string ApiKey { get; set; }
        public string Name { get; set; }

        public List<Solution> Solutions { get; set; }
    }
}
