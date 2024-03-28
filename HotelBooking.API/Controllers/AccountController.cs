using HotelBooking.Domain.Request.Account;
using HotelBooking.Domain.Response.Account;
using HotelBooking.Domain.User;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HotelBooking.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly SignInManager<ApplicationUser> signInManager;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly IWebHostEnvironment webHostEnvironment;
        private readonly IHttpContextAccessor httpContextAccessor;

        public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, RoleManager<IdentityRole> roleManager, IWebHostEnvironment webHostEnvironment, IHttpContextAccessor httpContextAccessor)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.roleManager = roleManager;
            this.webHostEnvironment = webHostEnvironment;
            httpContextAccessor = httpContextAccessor;
        }

        [HttpPost]
        [Route("/api/account/login")]
        public async Task<LoginResult> Login(LoginRequest request)
        {
            var result = new LoginResult()
            {
                Message = "something went wrong, please try again",
                Success = false,
                UserId = string.Empty
                
            };
         
            var siginResult = await signInManager.PasswordSignInAsync(request.Email, request.Password, false, false);

            if (siginResult.Succeeded)
            {
                var user = await userManager.FindByNameAsync(request.Email);
                if (user != null)
                {
                    result.Success = siginResult.Succeeded;
                    result.UserId = user.Id;
                    result.Message = "Login success";
                    //var roles = await userManager.GetRolesAsync(user);
                    //httpContextAccessor.HttpContext.Session.SetString("UserRoles", string.Join(",", roles));

                }
            }

            return result;
        }

        [HttpPost]
        [Route("/api/account/register")]
        public async Task<RegisterResult> Register([FromBody]RegisterRequest request)
        {
            var result = new RegisterResult()
            {
                Message = "something went wrong, please try again",
                Success = false
            };

            var user = new ApplicationUser()
            {
                Email = request.Email,
                UserName = request.Email,
                PhoneNumber = request.PhoneNumber,
                Name = request.Name,
                Avatar = request.Avatar
                
            };
            var registerResult = await userManager.CreateAsync(user, request.Password);
            
            //await userManager.AddToRoleAsync(user, "Customer");
            if (registerResult.Succeeded)
            {
                await userManager.AddToRoleAsync(user, "Customer");

                result.Message = "Register success";
                result.Success = registerResult.Succeeded;
            }
            return result;
        }
        [HttpGet]
        [Route("/api/account/roles/{userId}")]
        public async Task<IActionResult> GetUserRoles(string userId)
        {
            var user = await userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return NotFound("User not found");
            }

            var roles = await userManager.GetRolesAsync(user);
            return Ok(roles);
        }

    }
}