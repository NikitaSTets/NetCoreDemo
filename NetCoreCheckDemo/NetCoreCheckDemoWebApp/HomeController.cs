using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace NetCoreCheckDemoWebApp
{
    public class HomeController : Controller
    {
        private readonly IOptionsSnapshot<AppSettings> _appSettingsOptionsSnapshot;


        public HomeController(IOptionsSnapshot<AppSettings> appSettingsOptionsSnapshot)
        {
            _appSettingsOptionsSnapshot = appSettingsOptionsSnapshot;
        }

        public IActionResult Index()
        {
            var appSettings  = _appSettingsOptionsSnapshot.Value;

            return View(appSettings);
        }
    }
}