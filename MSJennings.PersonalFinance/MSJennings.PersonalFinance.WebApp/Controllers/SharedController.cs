using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using MSJennings.PersonalFinance.WebApp.ViewModels.Shared;

namespace MSJennings.PersonalFinance.WebApp.Controllers
{
    public class SharedController : Controller
    {
        #region Public Methods

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        #endregion Public Methods
    }
}
