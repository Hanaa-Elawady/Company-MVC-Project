using Company.Data.Models;
using Company.service.Helper;
using Company.service.Interfaces;
using Company.Services.Helper;
using Company2.Web.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
namespace Company2.Web.Controllers
{
    public class AuthController : Controller
    {
        #region dependancy injection

        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IEmailService _emailSetting;
        private readonly ISMSService _smsSetting;

        public AuthController(UserManager<ApplicationUser> userManager 
                            , SignInManager<ApplicationUser> signInManager 
                            , IEmailService emailSetting
                            ,ISMSService smsSetting)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _emailSetting = emailSetting;
            _smsSetting = smsSetting;
        }
        #endregion

        #region SignUp
        public IActionResult SignUP()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> SignUP(SignUpModelView input)
        {
            if (ModelState.IsValid) 
            {
                var user = new ApplicationUser
                {
                    UserName = input.Email.Split("@")[0],
                    Email = input.Email,
                    FirstName = input.FirstName,
                    LastName = input.LastName,
                    IsActive = true,
                };

                var result =await _userManager.CreateAsync(user ,input.Password);

                if (result.Succeeded)
                    return RedirectToAction("LogIn");

                foreach (var item in result.Errors)
                {
                    ModelState.AddModelError("", item.Description);
                }
            }

            return View(input);
        }
        #endregion

        #region LogIn
        public IActionResult LogIn()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> LogIn(LogInViewModel input)
        {
            if (ModelState.IsValid)
            {
                var User = await _userManager.FindByEmailAsync(input.Email);
                if (User is not null)
                {
                    if (await _userManager.CheckPasswordAsync(User , input.Password))
                    {
                        var result = await _signInManager.PasswordSignInAsync(User, input.Password, true, true);
                        if (result.Succeeded)
                            return RedirectToAction("Index", "Home");
                    }
                }
                ModelState.AddModelError("", "incorrect Password");

                return View(input);
            }
            return View(input);
        }
        #endregion

        #region SignOut
        public async Task<IActionResult> SignOut()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("LogIn");
        }
        #endregion

        #region password forgotten
        #region forget password
        public IActionResult ForgetPassword()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> ForgetPassword(ForgetPasswordViewModel input , string submitButton)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(input.Email);
                if (user is not null)
                {
                    var token = await _userManager.GeneratePasswordResetTokenAsync(user);
                    var url = Url.Action("ResetPassword", "Auth", new
                    {
                        input.Email,
                        Token = token
                    }, Request.Scheme);

                    if (submitButton == "SendEmail")
                    {
                        var email = new Email
                        {
                            To = input.Email,
                            Subject = "Reset Password",
                            Body = url
                        };

                        _emailSetting.SendEmail(email);
                    }
                    else if (submitButton == "SendSMS")
                    {
                        var sms = new SMS
                        {
                            PhoneNumber = user.PhoneNumber,
                            Body = url!
                        };

                        _smsSetting.SendSms(sms);
                    }

                    return RedirectToAction(nameof(EmailCheck));
                }
            }
            return View(input);
        }

        [HttpPost]
        public async Task<IActionResult> ForgetPasswordBySMS(ForgetPasswordViewModel input)
        {
            if (ModelState.IsValid)
            {
                var User = await _userManager.FindByEmailAsync(input.Email);
                if (User is not null)
                {
                    var token = await _userManager.GeneratePasswordResetTokenAsync(User);
                    var url = Url.Action("ResetPassword", "Auth", new
                    {
                        input.Email,
                        Token = token
                    },Request.Scheme);

                    var sms = new SMS()
                    {
                        PhoneNumber = User.PhoneNumber,
                        Body = url!
                    };

                    _smsSetting.SendSms(sms);
                    return RedirectToAction(nameof(EmailCheck));
                }
            }
            return View(input);
        }
        #endregion

        
        #region Check Mail
        public IActionResult EmailCheck()
        {
            return View();
        }

        #endregion        

        #region Reset Password
        public IActionResult ResetPassword(string Email, string Token)
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> ResetPassword(ResetPasswordViewModel input)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(input.Email);
                if (user is not null)
                {
                    var result = await _userManager.ResetPasswordAsync(user , input.Token , input.Password);
                    if (result.Succeeded)
                    return RedirectToAction(nameof(LogIn));

                    foreach (var item in result.Errors)
                    {
                        ModelState.AddModelError("", item.Description);
                    }
                }
            }
            return View(input);
        }

        #endregion
        #endregion

        //public IActionResult googleLogin()
        //{

        //    var prop = new AuthenticationProperties
        //    {
        //        RedirectUri = Url.Action("googleResponse")
        //    };

        //    return Challenge(prop, GoogleDefaults.AuthenticationScheme);
        //}

        //public async Task<IActionResult> googleResponse()
        //{
        //    var result = await HttpContext.AuthenticateAsync(GoogleDefaults.AuthenticationScheme);
        //    var claims = result.Principal.Identities.FirstOrDefault().Claims.Select(claim => new
        //    {
        //        claim.Issuer,
        //        claim.OriginalIssuer,
        //        claim.Type,
        //        claim.Value
        //    });

        //    return RedirectToAction("Index", "Home");

        //}
    }
}
