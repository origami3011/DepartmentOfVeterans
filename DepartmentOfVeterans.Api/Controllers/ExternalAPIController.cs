using DepartmentOfVeterans.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace DepartmentOfVeterans.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RandomUserController : ControllerBase
    {
        private static string _apiURL = "https://randomuser.me/api/";


        [HttpGet("Pagination", Name = "Pagination")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<RandomUser>> GetPagination(int page, int results, string seed)
        {
            var users = new RandomUser();
            using (var httpClient = new HttpClient())
            {
                var builder = new UriBuilder(_apiURL);
                builder.Query = "?page=" + page + "&results="+ results + "&seed="+ seed ;

                using (var response = await httpClient.GetAsync(builder.ToString()))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    users = JsonConvert.DeserializeObject<RandomUser>(apiResponse);
                }
            }

            return Ok(users);
        }
    }
}
