using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace MSJennings.PersonalFinance.WebApp.Controllers
{
    public class HomeController : Controller
    {
        #region Private Fields

        private readonly ILogger<HomeController> _logger;

        #endregion Private Fields

        #region Public Constructors

        public HomeController(
            ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        #endregion Public Constructors

        #region Public Methods

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        #endregion Public Methods
    }
}
