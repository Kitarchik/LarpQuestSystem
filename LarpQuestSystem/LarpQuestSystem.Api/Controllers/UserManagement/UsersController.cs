using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LarpQuestSystem.Data.Model.Security;
using LarpQuestSystem.Data.Model.UserManagement;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace LarpQuestSystem.Api.Controllers.UserManagement
{
    [Authorize(Policy = Policies.IsSuperUser)]
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly UserManager<IdentityUser> _userManager;

        public UsersController(UserManager<IdentityUser> userManager)
        {
            _userManager = userManager;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<string>>> Get()
        {
            var allUsers = _userManager.Users.ToList();
            var superUsers = await _userManager.GetUsersInRoleAsync("SuperUser");
            var users = allUsers.Except(superUsers);
            return users.Select(x => x.Email).ToList();
        }

        [HttpGet("{email}")]
        public async Task<ActionResult<UserInfo>> GetUser(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            var roles = await _userManager.GetRolesAsync(user);
            return Ok(new UserInfo { Email = email, Roles = roles.ToList() });
        }

        [HttpPut]
        public async Task<ActionResult<UserInfo>> EditUser(UserInfo user)
        {
            var oldUser = await _userManager.FindByEmailAsync(user.Email);
            var oldUserRoles = await _userManager.GetRolesAsync(oldUser);
            var rolesToAdd = user.Roles.Except(oldUserRoles);
            var rolesToDelete = oldUserRoles.Except(user.Roles);
            foreach (var role in rolesToAdd)
            {
                await _userManager.AddToRoleAsync(oldUser, role);
            }

            foreach (var role in rolesToDelete)
            {
                await _userManager.RemoveFromRoleAsync(oldUser, role);
            }

            var newUser = await _userManager.FindByEmailAsync(user.Email);
            return Ok(newUser);
        }

        [HttpDelete("{email}")]
        public async Task<ActionResult<UserInfo>> DeleteUser(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            var result = await _userManager.DeleteAsync(user);
            if (result.Succeeded)
            {
                return Ok(user);
            }

            return BadRequest();
        }
    }
}