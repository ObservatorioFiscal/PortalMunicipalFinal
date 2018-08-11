using GastoTransparenteMunicipal.Models;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace GastoTransparenteMunicipal.Controllers
{
    public class CuentaController : BaseController
    {
        #region login
        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;

        public CuentaController()
        {
        }

        public CuentaController(ApplicationUserManager userManager, ApplicationSignInManager signInManager)
        {
            UserManager = userManager;
            SignInManager = signInManager;
        }

        public ApplicationSignInManager SignInManager
        {
            get
            {
                return _signInManager ?? HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
            }
            private set
            {
                _signInManager = value;
            }
        }

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }
        #endregion

        [AllowAnonymous]
        public async Task<ActionResult> ConfirmEmail(string userId, string code)
        {
            ViewBag.administracion = true;
            if (userId == null || code == null)
            {
                return View("Error");
            }
            var result = await UserManager.ConfirmEmailAsync(userId, code);
            if (result.Succeeded)
            {
                return RedirectToAction("ResetPassword", new { userId = userId });
            }
            return View("Error");
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult> ResetPassword(string userId)
        {
            //var id = userId.ToString();
            ViewBag.administracion = true;
            var municipalidad = GetCurrentIdMunicipality();
            ViewBag.administracion = true;
            ViewBag.logo = (municipalidad==null)?"municipalidad.png":municipalidad.DireccionWeb + ".png";

            var user = await UserManager.FindByIdAsync(userId);
            ResetPasswordViewModel model = new ResetPasswordViewModel();
            model.Email = user.Email;

            ViewBag.Message = "";            
            
            return View(model);
        }        

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ResetPassword(ResetPasswordViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var user = await UserManager.FindByNameAsync(model.Email);
            if (user == null)
            {
                // No revelar que el usuario no existe
                ViewBag.Message = "";
                return RedirectToAction("ResetPassword", "Account");
            }

            string code = await UserManager.GeneratePasswordResetTokenAsync(user.Id);
            var result = await UserManager.ResetPasswordAsync(user.Id, code, model.Password);
            if (result.Succeeded)
            {                
                var roles = await UserManager.GetRolesAsync(user.Id);
                var roleName = roles[0].ToString();

                roleName = roleName.Substring(0, 1).ToUpper() + roleName.Substring(1);
                var login = await SignInManager.PasswordSignInAsync(model.Email, model.Password, false , shouldLockout: false);
                Url.RequestContext.RouteData.Values["municipality"] = roleName;

                return RedirectToAction("CargaDatos", "AdminComuna");
            }

            ViewBag.Message = result.Errors;
            return View();
        }

        public ActionResult
           
           ResetPasswordConfirmation()
        {
            return View();
        }
    }
}