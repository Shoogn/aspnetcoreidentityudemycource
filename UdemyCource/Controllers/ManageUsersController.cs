using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Linq;
using System.Threading.Tasks;
using UdemyCource.Models;
using UdemyCource.ViewModels;

namespace UdemyCource.Controllers
{
    // Controller:
    // ManageUsers
    [Authorize]
    public class ManageUsersController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        public ManageUsersController(UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public IActionResult Index()
        {

            var model = _userManager.Users.Select(x => new AllUsers
            {
                Id = x.Id,
                Email = x.Email,
                Name = x.UserName,
            }).ToList();

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> UserRoles(string userId)
        {
            var model = new AllUsers();


            var user = await _userManager.FindByIdAsync(userId);
            if (user != null)
            {
                model.Name = user.UserName;
                model.Email = user.Email;
                model.Id = user.Id;

                foreach (var role in _roleManager.Roles.ToList())
                {
                    var isInRole = await _userManager.IsInRoleAsync(user, role.Name);
                    if (isInRole)
                        model.Roles.Add(new RoleModel { Id = role.Id, RoleName = role.Name });
                }
            }

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteUserFromRole(string userId, string roleId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user != null)
            {
                // check role
                var role = await _roleManager.FindByIdAsync(roleId);
                if (role != null)
                {
                    var removeResult = await _userManager.RemoveFromRoleAsync(user, role.Name);
                    if (removeResult.Succeeded)
                        RedirectToAction("UserRoles", "ManageUsers", new { userId = user.Id });
                }
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> DeleteUser(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user != null)
            {
                var deleteResult = await _userManager.DeleteAsync(user);
                if (deleteResult.Succeeded)
                    return RedirectToAction("Index");
            }
            // You have to show the error message for the user
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> AddRole(string Id)
        {
            if (!string.IsNullOrEmpty(Id))
            {
                var role = await _roleManager.FindByIdAsync(Id);
                if (role != null)
                    return View(new RoleModel { Id = Id, RoleName = role.Name });
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddRole(RoleModel model)
        {
            if (model.Id != null & model.RoleName != null)
            {
                // Edit Mode

                var updateResult = await _roleManager.UpdateAsync(new IdentityRole { Id = model.Id, Name = model.RoleName });

                if (updateResult.Succeeded)
                {
                    return RedirectToAction("Index");
                }
                return View(model);
            }
            else
            {
                // Create Mode
                // This is for Demo, in real work consider to create a model for create an another one for update
                // or you can consider to create a new action method for update
                var createResult = await _roleManager.CreateAsync(new IdentityRole { Name = model.RoleName });
                if (createResult.Succeeded)
                {
                    return RedirectToAction("Index");
                }
                return View(model);
            }
        }


        [HttpGet]
        public IActionResult AddUsersToRole()
        {
            var AllRoles = _roleManager.Roles
                .Select(x => new SelectListItem
                {
                    Text = x.Name,
                    Value = x.Id,
                }).ToList();

            ViewBag.RoleList = AllRoles;

            var model = new AddUsersToRoleRequest();

            var users = _userManager.Users.Select(x => new AllUsers
            {
                Id = x.Id,
                Email = x.Email,
                Name = x.UserName,
            }).ToList();
            model.Users = users;
            return View(model);
        }


        [HttpPost]
        public async Task<IActionResult> AddUsersToRole(AddUsersToRoleRequest roleRequest)
        {
            // First I have to check if the role is exis, if not redirect the user to the Index Page
            // In real work app you have to show the error message to user

            var isRoleExist = await _roleManager.FindByIdAsync(roleRequest.RoleId);
            if (isRoleExist == null)
                return RedirectToAction("Index");


            var selectedUser = roleRequest.Users.Where(x => x.IsSelectedToAddToRole).ToList();
            if (selectedUser.Any())
            {
                // Fisrt loop over user and check if the suer is not aleardy added to this role befor
                foreach (var user in selectedUser)
                {
                    // Get the User by Id

                    var userFromBackStore = await _userManager.FindByIdAsync(user.Id);
                    if (userFromBackStore != null)
                    {
                        var userIsNotInRole = await _userManager.IsInRoleAsync(userFromBackStore, isRoleExist.Name);
                        if (!userIsNotInRole)
                        {
                            var addToRoleResult = await _userManager.AddToRoleAsync(userFromBackStore, isRoleExist.Name);
                            if (!addToRoleResult.Succeeded)
                                continue;
                        }
                    }

                }

                return RedirectToAction("Index");
            }

            // You have to select at least one user
            return View(roleRequest);
        }
    }
}
