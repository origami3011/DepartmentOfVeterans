using Microsoft.AspNetCore.Mvc;

namespace DepartmentOfVeterans.WebApp.Controllers
{
    public class AppController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
