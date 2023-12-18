using System.Linq;
using System.Threading.Tasks;
using FreeCourse.IdentityServer.Dtos;
using FreeCourse.IdentityServer.Models;
using FreeCourse.Shared.ControllerBases;
using FreeCourse.Shared.Dtos;
using IdentityServer4;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using static IdentityServer4.IdentityServerConstants;

namespace FreeCourse.IdentityServer.Controllers
{
    [Authorize(LocalApi.PolicyName)]
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class UserController : CustomBaseController
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public UserController(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        [HttpPost]
        public async Task<IActionResult> SignUp(SignupDto signupDto)
        {
            var result = await _userManager.CreateAsync(new ApplicationUser()
            {
                UserName = signupDto.UserName,
                Email = signupDto.Email,
                City = signupDto.City
            }, signupDto.Password);

            if (!result.Succeeded)
                return CreateActionResultInstance(Response<NoContent>.Fail(result.Errors.Select(x => x.Description).ToList(), 400));

            return CreateActionResultInstance(Response<NoContent>.Success(204));
        }
    }
}
