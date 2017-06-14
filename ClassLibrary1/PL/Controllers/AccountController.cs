using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Autofac;
using AutoMapper;
using BLL.DTO;
using BLL.Helpers;
using BLL.Interfaces;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using PL.Models;
using BLL.Resource;
using BLL.Infrastructure;

namespace PL.Controllers
{
    public class AccountController : Controller
    {
        private IUserService UserService;
        private IAuthenticationManager AuthenticationManager;
        public AccountController(IUserService UserService, IAuthenticationManager AuthenticationManager)
        {
            this.AuthenticationManager = AuthenticationManager;
            this.UserService = UserService;
        }
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(LoginModel model)
        {
            if (ModelState.IsValid)
            {
                UserDTO userDto = new UserDTO { Login = model.Login, Password = model.Password };
                ClaimsIdentity claim = await UserService.AuthenticateAsync(userDto);
                if (claim == null)
                {
                    ModelState.AddModelError("", "Wrong Login Or Password");
                }
                else
                {
                    AuthenticationManager.SignOut();
                    AuthenticationManager.SignIn(new AuthenticationProperties
                    {
                        IsPersistent = true
                    }, claim);
                    return RedirectToAction("Index", "Home");
                }
            }
            return View(model);
        }

        public ActionResult Logout()
        {
            AuthenticationManager.SignOut();
            return RedirectToAction("Login", "Account");
        }

        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Register(RegisterModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    Mapper.Initialize(cfg => cfg.CreateMap<RegisterModel, UserDTO>()
                    .ForMember("Role", opt => opt.MapFrom(src => "user")));
                    UserDTO userDto = Mapper.Map<RegisterModel, UserDTO>(model);

                    await UserService.CreateAsync(userDto);
                    return RedirectToAction("Index", "Home");
                }
                catch (ValidationException ex)
                {
                    ModelState.AddModelError(ex.Property, ex.Message);
                }
            }
            return View(model);
        }

        public ActionResult UnassignedUser()
        {
            return View();
        }
    }
}