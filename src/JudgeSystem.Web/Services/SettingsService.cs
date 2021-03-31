using JudgeSystem.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JudgeSystem.Web.Services
{
    public class SettingsService : ISettingsService
    {
        public Settings Settings { get; set; }

        public SettingsService()
        {
            Settings = new Settings();
        }
    }
}
