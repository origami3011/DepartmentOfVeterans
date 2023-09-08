using Microsoft.AspNetCore.Mvc;

namespace DepartmentOfVeterans.WebApp.Controllers
{
    public class UserController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
