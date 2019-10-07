using Microsoft.AspNetCore.Mvc;

namespace Bugster.Controllers
{
    public class HomeController : 
        Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}