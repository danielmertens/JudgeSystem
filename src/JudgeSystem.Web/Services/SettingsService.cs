using JudgeSystem.Web.Models;

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
