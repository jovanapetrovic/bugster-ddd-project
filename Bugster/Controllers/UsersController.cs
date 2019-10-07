using Bugster.Business.Models;
using Bugster.Business.UserApplicationService;
using Bugster.ViewModels;
using Mapster;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Threading.Tasks;

namespace Bugster.Controllers
{
    [Route("[controller]/[action]")]
    public class UsersController : 
        Controller
    {
        private readonly IUserApplicationService _userApplicationService;

        public UsersController(IUserApplicationService userApplicationService)
        {
            _userApplicationService = userApplicationService;
        }

        public async Task<IActionResult> Index()
        {
            var readUsersRequest = new ReadUsersRequest();
            var response = await _userApplicationService.Handle(readUsersRequest);

            return View(response);
        }

        public IActionResult Create()
        {
            var viewModel = new CreateUserViewModel();
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([FromForm, Bind("Username,Password,ConfirmPassword,Email,FirstName,LastName,Role")] CreateUserViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var createUserRequest = new CreateUserRequest
            {
                Username = model.Username,
                Role = model.Role,
                Password = model.Password,
                Email = model.Email,
                FullName = $"{model.FirstName} {model.LastName}"
            };
            var response = await _userApplicationService.Handle(createUserRequest);

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Update([BindRequired] long? userId)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction(nameof(Index));
            }

            var readUserRequest = new ReadUserRequest
            {
                UserId = userId.Value
            };
            var response = await _userApplicationService.Handle(readUserRequest);
            var viewModel = response.Adapt<UpdateUserViewModel>();

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update([FromForm, Bind("UserId,Email,FirstName,LastName,Role")] UpdateUserViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var updateUserRequest = new UpdateUserRequest
            {
                UserId = model.UserId.Value,
                Role = model.Role,
                Email = model.Email,
                FullName = $"{model.FirstName} {model.LastName}"
            };
            var response = await _userApplicationService.Handle(updateUserRequest);

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete([BindRequired] long? userId)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction(nameof(Index));
            }

            var deleteUserRequest = new DeleteUserRequest
            {
                UserId = userId.Value
            };
            var response = await _userApplicationService.Handle(deleteUserRequest);

            return RedirectToAction(nameof(Index));
        }
    }
}
