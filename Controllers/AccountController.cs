using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using StudentRecordManagementSystem.Repository;
using StudentRecordManagementSystem.Models;

namespace StudentRecordManagementSystem.Controllers
{
    public class AccountController : Controller
    {
        private readonly IUserRepository _users;
        public AccountController(IUserRepository users)
        {
            _users = users;
        }

        [HttpGet]
        public IActionResult Login(string? returnUrl = null)
        {
            return View(new LoginViewModel { ReturnUrl = returnUrl });
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (!ModelState.IsValid) return View(model);
            var auth = await _users.LoginAsync(model.Username, model.Password);
            if (auth == null)
            {
                ModelState.AddModelError(string.Empty, "Invalid credentials");
                return View(model);
            }

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, auth.UserId.ToString()),
                new Claim(ClaimTypes.Name, auth.Username),
                new Claim(ClaimTypes.Role, auth.RoleName)
            };
            if (auth.StudentId.HasValue)
            {
                claims.Add(new Claim("student_id", auth.StudentId.Value.ToString()));
            }
            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var principal = new ClaimsPrincipal(identity);
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

            if (!string.IsNullOrEmpty(model.ReturnUrl) && Url.IsLocalUrl(model.ReturnUrl))
                return Redirect(model.ReturnUrl);

            if (auth.RoleName == "Invigilator") return RedirectToAction("Index", "Invigilator");
            return RedirectToAction("Index", "Student");
        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            return RedirectToAction("Login");
        }

        [HttpGet]
        public IActionResult AccessDenied()
        {
            return View();
        }
    }
}
