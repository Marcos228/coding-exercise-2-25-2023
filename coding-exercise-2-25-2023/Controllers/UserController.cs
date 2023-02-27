using coding_exercise_2_25_2023.Services;
using Microsoft.AspNetCore.Mvc;
using coding_exercise_2_25_2023.Models;
using System.Net;
using Microsoft.AspNetCore.Http.Extensions;
using coding_exercise_2_25_2023.Exceptions;

namespace coding_exercise_2_25_2023.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : Controller
    {
        IUserServices _userService;
        public UserController(IUserServices userServices)
        {
            _userService = userServices;
        }

        [HttpGet]
        [Route("{email}")]
        public async Task<IActionResult> Get(string email)
        {
            try
            {
                UserModel value = await _userService.Retrieve(email);
                if (value == null)
                {
                    return NotFound(String.Concat("The user ", email, " was not found on the storage."));
                }
                return Ok(await Task.Run(() => value));
            }
            catch
            {
                return Content(HttpStatusCode.InternalServerError.ToString(), "The server encounter an error while processing the request. If this problem persists please contact support.");
            }
        }

        [HttpGet]
        [Route("/Users")]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                return Ok(await Task.Run(() => _userService.RetrieveAll()));
            }
            catch
            {
                return Content(HttpStatusCode.InternalServerError.ToString(), "The server encounter an error while processing the request. If this problem persists please contact support.");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Insert(UserModel reg)
        {
            try
            {
                _userService.Create(reg);
                return Created(Request.GetDisplayUrl(), reg);
            }
            catch (ValidationException ex)
            {
                return ValidationProblem(ex.Message);
            }

            catch
            {
                return Content(HttpStatusCode.InternalServerError.ToString(), "The server encounter an error while processing the request. If this problem persists please contact support.");
            }

        }
        [HttpPut]
        public async Task<IActionResult> Update(UserModel reg)
        {
            try
            {
                if (await _userService.Update(reg))
                {
                    return Ok("user updated successfully.");
                }
                else
                {
                    return NotFound(String.Concat("The user ", reg.Email, " was not found on the storage."));
                }
            }
            catch (ValidationException ex)
            {
                return ValidationProblem(ex.Message);
            }
            catch
            {
                return Content(HttpStatusCode.InternalServerError.ToString(), "The server encounter an error while processing the request. If this problem persists please contact support.");
            }

        }

        [HttpDelete]
        public async Task<IActionResult> Delete(string email)
        {
            try
            {
                if (await _userService.Delete(email))
                {
                    return Ok("user deleted successfully.");
                }
                else
                {
                    return NotFound(String.Concat("The user ", email, " was not found on the storage."));
                }
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.InternalServerError.ToString(), "The server encounter an error while processing the request. If this problem persists please contact support.");
            }
        }
    }
}
