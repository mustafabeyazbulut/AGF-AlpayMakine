using AlpayMakina.Dtos.AuthDtos;
using AlpayMakina.Repositories.UserRepositories;
using AlpayMakina.Utils;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace AlpayMakina.Controllers
{
    [Route("Login")]
    public class LoginController : Controller
    {
        private readonly IUserRepository _userRepository;
        private readonly HashHelper _hashHelper;

        public LoginController(IUserRepository userRepository, HashHelper hashHelper)
        {
            _userRepository = userRepository;
            _hashHelper = hashHelper;
        }
        
        [HttpGet]
        [Route("")]
        [Route("Index")]
        public IActionResult Index()
        {
            
            return View();
        }
        [HttpPost]
        [Route("")]
        [Route("Index")]
        public async Task<IActionResult> Index(ResultAuthDto resultAuthDto)
        {
            var user= await _userRepository.GetUserWithEmail(resultAuthDto.Email);
            if(user!=null)
            {
                if (_hashHelper.VerifyPassword(resultAuthDto.Password, user.Password))
                {
                    var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Name,user.Email),
                        new Claim(ClaimTypes.Role,"Admin"),
                    };
                    var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                    var authProperties = new AuthenticationProperties();

                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity), authProperties);

                    return RedirectToAction("Index", "Home", new { area = "Admin" });
                }
            }
            resultAuthDto.ErrorMessage = "Kullanıcı Adı veya Parola Hatalıdır.";
            return View(resultAuthDto);
        }

        [Route("Logout")]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            return RedirectToAction("Index", "Home");
        }
    }
}
