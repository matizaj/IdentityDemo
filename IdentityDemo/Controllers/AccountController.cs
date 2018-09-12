using IdentityDemo.Data;
using IdentityDemo.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityDemo.Controllers
{
    [Authorize]
    public class AccountController:Controller
    {

        private UserManager<AppUser> userManager;
        private SignInManager<AppUser> signManager;
        private AppIdentityDbContext context;

        public AccountController(UserManager<AppUser> userMgr, SignInManager<AppUser> signMgr, AppIdentityDbContext ctx)
        {
            userManager = userMgr;
            signManager = signMgr;
            context = ctx;
        }


        [AllowAnonymous]
        public IActionResult Login(string returnUrl)
        {
            ViewBag.returnurl = returnUrl;
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login(LoginModel details, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                AppUser user = await userManager.FindByEmailAsync(details.Email);
                user.Counter++;
                context.Users.Attach(user);
                context.Entry(user).Property("Counter").IsModified = true;
                context.SaveChanges();
                if (user.Counter < 3)
                {
                    return RedirectToAction("Create", "Question");
                }
                if (user != null)
                {
                    await signManager.SignOutAsync();
                    Microsoft.AspNetCore.Identity.SignInResult result = await signManager.PasswordSignInAsync(user, details.Password, false, false);
                    if (result.Succeeded)
                    {
                        return Redirect(returnUrl ?? "/");
                    }
                }
            }
            
            return View(details);
        }
    }
}
