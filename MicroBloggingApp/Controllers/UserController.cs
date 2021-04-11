using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using MicroBloggingApp.Models;
using MicroBloggingApp.Services;
using MicroBloggingApp.VIewModels;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace MicroBloggingApp.Controllers
{
    public class UserController : Controller
    {
        private UserService _userService;

        private IConfiguration _configuration;

        public UserController(UserService userService, IConfiguration configuration)
        {
            this._userService = userService;
            this._configuration = configuration;
        }

        [HttpGet]
        [Route("/user/login")]
        public IActionResult Index()
        {
            return this.View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginModel model)
        {
            bool flag;
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            else
            {
                UserModel user = await _userService.GetUser(model);
                
                if (user == null)
                {
                    ViewBag.Message = "User not found";
                    return View("Index");
                }
                else if(user.Password != model.password)
                {
                    ViewBag.Message = "Incorrect credentials";
                    return View("Index");
                }
                else
                {
                    ClaimsPrincipal claims = _userService.GetClaims(user);
                    await HttpContext.SignInAsync(claims);
                    return RedirectToAction("index", "Home");
                }
            }
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            return this.RedirectToAction("Index", "Home");
        }

        public IActionResult Register()
        {
            return this.View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegistrationModel model)
        {
            if (ModelState.IsValid)
            {
                LoginModel loginModel = new LoginModel()
                {
                    email = model.Email,
                    password = model.Password
                };
                UserModel user = await _userService.GetUser(loginModel);
                if (user == null)
                {
                    UserModel userModel = await _userService.AddUser(model);
                    if (userModel == null)
                    {
                        return BadRequest();
                    }
                    else
                    {
                        ClaimsPrincipal claims = this._userService.GetClaims(userModel);
                        await HttpContext.SignInAsync(claims);
                        return RedirectToAction("index", "Home");
                    }
                }
                else
                {
                    user = null;
                    ViewBag.Message = "User already exists";
                    return View();
                }
            }
            else
            {
                return View(model);
            }
        }
    }
}
