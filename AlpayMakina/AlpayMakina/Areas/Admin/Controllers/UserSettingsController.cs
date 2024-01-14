using AlpayMakina.Dtos.UserDtos;
using AlpayMakina.Repositories.UserRepositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AlpayMakina.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("Admin/UserSettings")]
    [Authorize]
    public class UserSettingsController : Controller
    {
        private readonly IUserRepository _UserRepository;

        public UserSettingsController(IUserRepository UserRepository)
        {
            _UserRepository = UserRepository;
        }

        [Route("")]
        [Route("Index")]
        public async Task<IActionResult> Index()
        {
            var values = await _UserRepository.GetAllUserAsync();
            return View(values);
        }

        [Route("CreateUser")]
        public IActionResult CreateUser()
        {
            return View();
        }

        [HttpPost]
        [Route("CreateUser")]
        public async Task<IActionResult> CreateUser(CreateUserDto createUserDto)
        {
            await _UserRepository.AddUserAsync(createUserDto);

            return RedirectToAction("Index", "UserSettings", new { area = "Admin" });
        }
        [Route("RemoveUser/{id}")]
        public async Task<IActionResult> RemoveUser(int id)
        {
            await _UserRepository.RemoveUserAsync(id);

            return RedirectToAction("Index", "UserSettings", new { area = "Admin" });
        }

        [HttpGet]
        [Route("UpdateUser/{id}")]
        public async Task<IActionResult> UpdateUser(int id)
        {
            var value = await _UserRepository.GetUserWithUpdateAsync(id);

            return View(value);
        }

        [HttpPost]
        [Route("UpdateUser/{id}")]
        public async Task<IActionResult> UpdateUser(UpdateUserDto updateUserDto)
        {
            await _UserRepository.UpdateUserAsync(updateUserDto);

            return RedirectToAction("Index", "UserSettings", new { area = "Admin" });

        }
    }
}
