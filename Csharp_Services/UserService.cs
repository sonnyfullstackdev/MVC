using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using System.Security.Claims;
using System.IO;


namespace ProjectName.Services
{
    public class UserService : IUserService
    {
        IBaseService _baseService;
        IEmailConfirmationService _emailConfirmationService;
        IAdminService _adminService;



        public UserService(IBaseService baseService, IErrorLogService errorLogService, IEmailConfirmationService emailConfirmationService, IAdminService adminService)
        {
            _baseService = baseService;
            _baseService.ErrorLog = errorLogService;
            _emailConfirmationService = emailConfirmationService;
            _adminService = adminService;
        }

        private  ApplicationUserManager GetUserManager()
        {
            try
            {
                return HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            catch (Exception ex)
            {
                _baseService.LogError(System.Reflection.MethodBase.GetCurrentMethod().Name, ex, "UserService");
                throw;
            }
        }

        public IdentityUser CreateUser(string email, string password)
        {
            try
            {
                ApplicationUserManager userManager = GetUserManager();

                ApplicationUser newUser = new ApplicationUser { UserName = email, Email = email, LockoutEnabled = false };
                IdentityResult result = null;
                //try
                //{
                result = userManager.Create(newUser, password);
                //string[] userRoles = new string[] { "User" };
                //userManager.AddToRoles(newUser.Id, userRoles);

                if (result.Succeeded)
                {
                    return newUser;
                }
                else
                {
                    throw new Exception(/*string.Join(",", result.Errors*/result.Errors.FirstOrDefault());
                }
            }
            catch(Exception ex)
            {
                _baseService.LogError(System.Reflection.MethodBase.GetCurrentMethod().Name, ex, "SendEmailService");
                throw;
            }

        }

        public bool IsEmailConfirmed()
        {
            return GetUserManager().IsEmailConfirmed(GetCurrentUserId());
        }

        //create a function to confirm the email from app token 

        public ServiceResponse Signin(string emailaddress, string password)
        {
            try
            {
                bool result = false;

                if (!IsUser(emailaddress))
                {
                    return new ServiceResponse { IsSuccessful = false, ResponseMessage = "Email was Not Found" };
                }

                ApplicationUserManager userManager = GetUserManager();
                IAuthenticationManager authenticationManager = HttpContext.Current.GetOwinContext().Authentication;
                ApplicationUser user = userManager.Find(emailaddress, password);

                if (user != null && !user.EmailConfirmed)
                {
                   return new ServiceResponse { IsSuccessful = false, ResponseMessage = "Email Not Confirmed" };
                }

                if (_adminService.IsUserBanned(emailaddress))
                {
                    return new ServiceResponse { IsSuccessful = false, ResponseMessage = "You have been banned. Bye, Felicia!" };
                }

                if (user != null && user.EmailConfirmed)
                {
                    ClaimsIdentity signin = userManager.CreateIdentity(user, DefaultAuthenticationTypes.ApplicationCookie);
                    authenticationManager.SignIn(new AuthenticationProperties() { IsPersistent = true }, signin);
                    return new ServiceResponse { IsSuccessful = true, ResponseMessage = "Success" };
                }
                return new ServiceResponse { IsSuccessful = result, ResponseMessage = "Password was Invalid" };
            }
            catch (Exception ex)
            {
                _baseService.LogError(System.Reflection.MethodBase.GetCurrentMethod().Name, ex, "UserService");
                throw;
            }
        }

        public bool IsUser(string emailaddress)
        {
            try
            {
                bool result = false;

                ApplicationUserManager userManager = GetUserManager();
                IAuthenticationManager authenticationManager = HttpContext.Current.GetOwinContext().Authentication;

                ApplicationUser user = userManager.FindByEmail(emailaddress);

                if (user != null)
                {
                    result = true;
                }
                return result;
            }
            catch (Exception ex)
            {
                _baseService.LogError(System.Reflection.MethodBase.GetCurrentMethod().Name, ex, "UserService");
                throw;
            }
        }

        public ApplicationUser GetUser(string emailaddress)
        {
            try
            {
                ApplicationUserManager userManager = GetUserManager();
                IAuthenticationManager authenticationManager = HttpContext.Current.GetOwinContext().Authentication;

                ApplicationUser user = userManager.FindByEmail(emailaddress);

                return user;
            }
            catch (Exception ex)
            {
                _baseService.LogError(System.Reflection.MethodBase.GetCurrentMethod().Name, ex, "UserService");
                throw;
            }
        }

        public ApplicationUser GetUserById(string userId)
        {
            try
            {
                ApplicationUserManager userManager = GetUserManager();
                IAuthenticationManager authenticationManager = HttpContext.Current.GetOwinContext().Authentication;

                ApplicationUser user = userManager.FindById(userId);

                return user;
            }
            catch (Exception ex)
            {
                _baseService.LogError(System.Reflection.MethodBase.GetCurrentMethod().Name, ex, "UserService");
                throw;
            }
        }


        public bool ChangePassWord(string userId, string newPassword)
        {
            try
            {
                bool result = false;

                if (string.IsNullOrEmpty(userId) || string.IsNullOrEmpty(newPassword))
                {
                    throw new Exception("You must provide a userId and a password");
                }

                ApplicationUser user = GetUserById(userId);

                if (user != null)
                {
                    ApplicationUserManager userManager = GetUserManager();

                    user.PasswordHash = userManager.PasswordHasher.HashPassword(newPassword);
                    IdentityResult res = userManager.Update(user);

                    //userManager.RemovePassword(userId);
                    //IdentityResult res = userManager.AddPassword(userId, newPassword);

                    result = res.Succeeded;
                }
                return result;
            }
            catch (Exception ex)
            {
                _baseService.LogError(System.Reflection.MethodBase.GetCurrentMethod().Name, ex, "UserService");
                throw;
            }
        }

        public bool ResetPassword(ResetPasswordUpdateRequest model)
        {
            bool result = false;
            try
            {
                string UserId = _emailConfirmationService.SelectById(model.TokenGuid);

                if(!String.IsNullOrEmpty(UserId))
                {
                    ChangePassWord(UserId, model.NewPassword);
                    result = true;
                }
                return result;
            }
            catch (Exception ex)
            {
                _baseService.LogError(System.Reflection.MethodBase.GetCurrentMethod().Name, ex, "User Service Reset Password");
                throw;
            }
        }

        public bool Logout()
        {
            try
            {
                bool result = false;

                IdentityUser user = GetCurrentUser();

                if (user != null)
                {
                    IAuthenticationManager authenticationManager = HttpContext.Current.GetOwinContext().Authentication;
                    authenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
                    result = true;
                }
                return result;
            }
            catch (Exception ex)
            {
                _baseService.LogError(System.Reflection.MethodBase.GetCurrentMethod().Name, ex, "UserService");
                throw;
            }
        }


        public IdentityUser GetCurrentUser()
        {
            try
            {
                if (!IsLoggedIn())
                {
                    return null;
                }
                ApplicationUserManager userManager = GetUserManager();

                IdentityUser currentUserId = userManager.FindById(GetCurrentUserId());
                return currentUserId;
            }
            catch (Exception ex)
            {
                _baseService.LogError(System.Reflection.MethodBase.GetCurrentMethod().Name, ex, "UserService");
                throw;
            }
        }

        public string GetCurrentUserId()
        {
            try
            {
                // return "e77ba321-00a5-42c9-b06a-be249f3ce521";
                return HttpContext.Current.User.Identity.GetUserId();
            }
            catch (Exception ex)
            {
                _baseService.LogError(System.Reflection.MethodBase.GetCurrentMethod().Name, ex, "UserService");
                throw;
            }
        }


        public bool IsLoggedIn()
        {
            try
            {
                return !string.IsNullOrEmpty(GetCurrentUserId());
            }
            catch (Exception ex)
            {
                _baseService.LogError(System.Reflection.MethodBase.GetCurrentMethod().Name, ex, "UserService");
                throw;
            }
        }

        //public ApplicationUser ConfirmEmail(string userId, string guid)
        //{
        //    try
        //    {
        //        bool result = false;
        //        ApplicationUser user = GetUserById(userId);

        //        if (user != null)
        //        {
        //            ApplicationUserManager userManager = GetUserManager();

        //            userManager.FindById(userId);

        //            //IdentityResult res = userManager.AddPassword(userId, newPassword);

        //            IdentityResult confirmResult = userManager.ConfirmEmail(userId, guid);

        //            result = confirmResult.Succeeded;
        //        }
        //        return user;
        //    }
        //    catch (Exception ex)
        //    {
        //        _baseService.LogError(System.Reflection.MethodBase.GetCurrentMethod().Name, ex, "ConfirmEmailService");
        //        throw;
        //    }
        //}
    }
}