using DepartmentOfVeterans.WebMVC.Models;
using DepartmentOfVeterans.WebMVC.Models.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using System.Text;

namespace DepartmentOfVeterans.WebMVC.Controllers
{
    public class UserController : Controller
    {
        private readonly string _randomRserRestAPI = "https://localhost:7227/api/RandomUser/Pagination";

        private readonly IUserDataService _userDataService;
        private readonly ILogger<AccountController> _logger;

        public UserController(IUserDataService userDataService, ILogger<AccountController> logger)
        {
            _userDataService = userDataService;
            _logger = logger;
        }

        public async Task<IActionResult> Index()
        {
            var _userList = await _userDataService.GetAllUsers();
            return View(_userList);
        }

        public ViewResult AddUser() => View();

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddUser(UserViewModel User)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var apiResponse = await _userDataService.CreateUser(User);
                    if (apiResponse.Success)
                        ViewBag.Result = "Success";
                    else
                        ModelState.AddModelError("Error", apiResponse.ValidationErrors ?? apiResponse.Message);
                }
                catch (Exception ex)
                {
                    _logger.LogError($"AddUser : error {ex.Message}");
                }

            }
            return View(User);
        }

        public async Task<IActionResult> UpdateUser(Guid id)
        {
            var user = await _userDataService.GetUserById(id);
            return View(user);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateUser(UserViewModel User)
        {
            var currentUser = await _userDataService.GetUserById(User.UserId);
            if (currentUser != null)
            {
                try
                {
                    var apiResponse = await _userDataService.UpdateUser(User);
                    if (apiResponse.Success)
                        return RedirectToAction("Index");
                    else
                        ModelState.AddModelError("Error", apiResponse.ValidationErrors ?? apiResponse.Message);
                }
                catch (Exception ex)
                {
                    _logger.LogError($"UpdateUser : Username {User.UserName} error {ex.Message}");
                }
            }
            else
                ModelState.AddModelError("", "User Not Found");
            return View(User);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteUser(Guid UserId)
        {
            try { var uuid = await _userDataService.DeleteUser(UserId); }
            catch (Exception ex) { _logger.LogError($"DeleteUser : error {ex.Message}"); }

            return RedirectToAction("Index");
        }

        /// <summary>
        /// This should be called from _userDataService to avoid DRY pattern, this is a example of DRY vaiolation
        /// </summary>
        /// <param name="page"></param>
        /// <param name="results"></param>
        /// <param name="seed"></param>
        /// <returns></returns>
        public async Task<IActionResult> LoadRandomUsers(int page = 1, int results = 10, string seed = "abc")
        {
            var users = new RandomUser();
            using (var httpClient = new HttpClient())
            {
                var builder = new UriBuilder(_randomRserRestAPI);
                builder.Query = "page=" + page + "&results=" + results + "&seed=" + seed;

                using (var response = await httpClient.GetAsync(builder.ToString()))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    users = JsonConvert.DeserializeObject<RandomUser>(apiResponse);
                }
            }
            return View(users);
        }

        [HttpPost]
        public JsonResult SaveUserAjax(string usename, string email, string firstname, string lastname, string gender, string avatar)
        {
            var user = new UserViewModel()
            {
                UserId = Guid.NewGuid(),
                UserName = usename,
                UserEmail = email,
                FirstName = firstname,
                LastName = lastname,
                Gender = gender,
                Picture = avatar ?? string.Empty
            };

            //this.SaveUser(user);
            _userDataService.CreateUser(user);

            return Json("User " + usename + " is saved successfully ");
        }
    }
}
