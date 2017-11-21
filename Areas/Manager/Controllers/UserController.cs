using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Piranha.Areas.Manager.Controllers;
using Piranha.AspNetCore.Identity.EF;
using Piranha.AspNetCore.Identity.EF.Manager.Areas.Manager.Models;
using Piranha.Manager;

namespace Piranha.AspNetCore.Identity.EF.Manager.Area.Manager.Controllers
{
    [Area("Manager")]
    public class UserController : ManagerAreaControllerBase
    {
        private readonly AppUserRepository userRepository;

        public UserController(IApi api, AppUserRepository userRepository) : base(api)
        {
            this.userRepository = userRepository;
        }

        [Route("manager/users/{userId?}")]
        [Authorize(Policy = Permission.Users)]
        public IActionResult List(string userId = null)
        {
            return ListUser(userId);
        }

        [Route("manager/user/{userId?}")]
        [Authorize(Policy = Permission.Users)]
        public IActionResult ListUser(string userId)
        {
            var model = UserListModel.Get(userRepository, userId);

            return View("List", model);
        }
    }
}