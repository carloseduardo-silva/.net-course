﻿using LanchesMac.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace LanchesMac.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _singInManager;

        public AccountController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> singInManager)
        {
            _userManager = userManager;
            _singInManager = singInManager;
        }

        public IActionResult Login(string returnUrl)
        {
            return View(new LoginViewModel()
            {
                ReturnUrl = returnUrl,
            });
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel loginVM)
        {
            //verifica se o login foi valido
            if (!ModelState.IsValid)
                return View(loginVM);

            //armazena usuario que tentou logar
            var user = _userManager.FindByNameAsync(loginVM.UserName);

            //verifica se o usuario esta no banco
            if(user != null)
            {
                //verifica se a senha passa coincide com o login do usuario no banco
                var result = await _singInManager.PasswordSignInAsync(await user, loginVM.Password, false, false);
                if (result.Succeeded)
                {
                    if(string.IsNullOrEmpty(loginVM.ReturnUrl))
                    {
                        return RedirectToAction("Index", "Home");
                    }
                    return View(loginVM.ReturnUrl);
                }
            }
            ModelState.AddModelError("", "Falha ao realizar o login");
            return View(loginVM);
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register( LoginViewModel registroVM)
        {
            if(ModelState.IsValid)
            {
                var user = new IdentityUser
                {
                    UserName = registroVM.UserName
                };

                var result = await _userManager.CreateAsync(user, registroVM.Password);

                if(result.Succeeded)
                {
                    return RedirectToAction("Login", "Account");
                }
                else
                {

                    this.ModelState.AddModelError("", "Falha ao registrar o usuário");
                }
               
            }
            return View(registroVM);

        }
    }

}
