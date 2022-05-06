using BusinessLogic;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Models.DTOS;
using System;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Week10API.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    [Authorize]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }


        [HttpPost]
        public async Task<IActionResult> Register(RegistrationRequest registrationRequest)
        {
            try
            {
                var result = await _userService.Register(registrationRequest);
                return Created("", result);
            }
            catch (MissingFieldException msex)
            {
                return BadRequest(msex.Message);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpGet("UserId")]
        public async Task<IActionResult> Get(string userId)
        {
            try
            {
                return Ok(await _userService.GetUser(userId));
            }
            catch (ArgumentException argex)
            {
                return BadRequest(argex.Message);
            }
            catch (Exception)
            {
                return StatusCode(500);
            }
        }


        [HttpGet("email")]
        public async Task<IActionResult> GetByEmail(string email)
        {
            try
            {
                return Ok(await _userService.GetUserByEmail(email));
            }
            catch (ArgumentException argex)
            {
                return BadRequest(argex.Message);
            }
            catch (Exception)
            {
                return StatusCode(500);
            }
        }


        [HttpPut]
        public async Task<IActionResult> Update(UpdateUserRequest updateUserRequest)
        {
            try
            {
                var UserId = HttpContext.User.FindFirst(x => x.Type == ClaimTypes.NameIdentifier).Value;

                var result = await _userService.Update("", updateUserRequest);
                return NoContent();
            }
            catch(MissingMemberException msex)
            {
                return BadRequest(msex.Message);
            }
            catch (ArgumentException argex)
            {
              return BadRequest(argex.Message);
            }
            catch (Exception)
            {
                return StatusCode(500);
            }
        }

        [HttpDelete]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(string userId)
        {
            try
            {
                return Ok(await _userService.DeleteUser(userId));
            }
            catch (MissingMemberException msex)
            {
                return BadRequest(msex.Message);
            }
            catch (ArgumentException argex)
            {
                return BadRequest(argex.Message);
            }
            catch (Exception)
            {
                return StatusCode(500);
            }
        }
    }
}
