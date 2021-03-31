using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JudgeSystem.Web.Models
{
    public class Settings
    {
        public bool CompetitionStarted { get; set; }

        public Settings()
        {
            CompetitionStarted = false;
        }
    }
}
