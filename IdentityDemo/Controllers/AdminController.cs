using IdentityDemo.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityDemo.Controllers
{
    public class AdminController : Controller
    {
        private UserManager<AppUser> userManager;
        public AdminController(UserManager<AppUser> userMgr)
        {
            userManager = userMgr;
        }

        public ViewResult Index() => View(userManager.Users);

        public ViewResult Create() => View();

        [HttpPost]
        public async Task<IActionResult> Create(AppUser myUser)
        {
            if (ModelState.IsValid)
            {
                AppUser user = new AppUser
            {
                UserName = myUser.UserName,
                Email = myUser.Email,
                IsNewUser = myUser.IsNewUser
            };

                if (!user.IsNewUser)
                {
                    return RedirectToAction("Thanks");
                }

            IdentityResult result = await userManager.CreateAsync(user);
            if (result.Succeeded)
            {
                return RedirectToAction("Index");
            }
            else
            {
                foreach (IdentityError error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }

                }
            }
            return View(myUser);
        }

        public IActionResult Thanks() => View();
    }
}
