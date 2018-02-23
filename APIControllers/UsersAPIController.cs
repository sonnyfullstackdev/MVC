using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
//needs alot more usings
 
namespace ProjectName.Controllers.Api
{
    [RoutePrefix("api/users")]
    public class UsersApiController : ApiController
    {
        IUserService _userService;
        IMemberProfileService _memberProfileService;
        IAppTokenService _appTokenService;
        ISendEmailService _sendEmailService;
        IEmailConfirmationService _emailConfirmationService;
        ISmsService _smsService;
        IAdminService _adminService;

        public UsersApiController(IUserService userService, IMemberProfileService memberProfileService, IAppTokenService appTokenService, ISendEmailService sendEmailService, IEmailConfirmationService emailConfirmationService, ISmsService smsService, IAdminService adminService)
        {
            _userService = userService;
            _memberProfileService = memberProfileService;
            _appTokenService = appTokenService;
            _sendEmailService = sendEmailService;
            _emailConfirmationService = emailConfirmationService;
            _smsService = smsService;
            _adminService = adminService;
        }

        [AllowAnonymous]
        [Route("registration"), HttpPost]
        public async Task<HttpResponseMessage> Register(RegistrationAddRequest model)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotAcceptable, string.Join(", ", ModelState.Values.SelectMany(x => x.Errors).Select(x => x.ErrorMessage)));
            }
            try
            {              
                if(model.DateOfBirth <= DateTime.MinValue)
                {
                    throw new ArgumentException("Please Select your Date of Birth");
                }
                IdentityUser user = _userService.CreateUser(model.Email, model.Password);           
           
                if (user != null)
                {
                    RegistrationResponse response = new RegistrationResponse();
                    response.AspNetUserID = user.Id;
                    response.Email = model.Email;
                    response.MemberProfileId = _memberProfileService.Insert(new MemberProfileAddRequest
                    {
                        FirstName = model.FirstName,
                        LastName = model.LastName,
                        Email = model.Email,
                        Gender = model.Gender,
                        DateOfBirth = model.DateOfBirth,
                        AspNetUserID = user.Id,
                        IsActive = true,
                        IsPublic = true,
                        IsViewable = true
                    });
                    
                    if (response.MemberProfileId > 0)
                    {
                        try
                        {
                            AppTokenResponse tokenResponse = new AppTokenResponse();
                            AppTokenAddRequest tokenObject = new AppTokenAddRequest()
                            {
                                MemberProfileId = response.MemberProfileId,
                                TokenTypeId = (int)AppTokenType.ConfirmRegistration
                            };
                            tokenResponse.TokenGuid = _appTokenService.Insert(tokenObject);

                            await _sendEmailService.SendEmailRegConfirm(response.Email, tokenResponse.TokenGuid);

                            return Request.CreateResponse(HttpStatusCode.OK, response);
                        }
                        catch
                        {
                            throw;
                        }
                    }
                }
                return Request.CreateResponse(HttpStatusCode.OK, "Registration Success");
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        } 

        [AllowAnonymous]
        [Route("confirmemail/{tokenGuid:Guid}"), HttpGet]
        public async Task<HttpResponseMessage> ConfirmEmail(Guid tokenGuid)
        {
           
            HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.RedirectMethod);
            ItemsResponse<AdminSettings> notify = new ItemsResponse<AdminSettings>();
            try
            {
                notify.Items = _adminService.SelectByNotifications();
                _emailConfirmationService.UpdateById(tokenGuid);
                string domain = HttpContext.Current.Request.IsLocal ? "http://localhost:1552" : "https://snatched.azurewebsites.net";

                foreach(AdminSettings item in notify.Items)
                {
                    if(item.NewUserEmail == true)
                    {
                        await _sendEmailService.SendEmailNewUser(item.Email);
                    }
                    if(item.NewUserText == true)
                    {
                        string message = "A new user has registered with our amazing app.";
                        _smsService.SendSms(item.PhoneNumber, message);
                    }
                }

                response.Headers.Location = new Uri(domain + "/Member/EmailConfirmedPage");

                return response;
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [AllowAnonymous]
        [Route("login"), HttpPost]
        public HttpResponseMessage Login(LoginAddRequest model)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotAcceptable, string.Join(",", ModelState.Values.SelectMany(x => x.Errors).Select(x => x.ErrorMessage)));
            }
            SuccessResponse response = new SuccessResponse();
            try
            {
                ServiceResponse status = _userService.Signin(model.Email, model.Password);
                if (status.IsSuccessful)
                {
                    ItemResponse<bool> iresponse = new ItemResponse<bool> { Item = status.IsSuccessful };
                    return Request.CreateResponse(HttpStatusCode.OK, iresponse);
                }
                else
                {
                    return Request.CreateErrorResponse(HttpStatusCode.Unauthorized, status.ResponseMessage);
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }
        [AllowAnonymous]
        [Route("forgotpassword"), HttpPost]
        public async Task<HttpResponseMessage> ForgotPassword(LoginEmailRequest model)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotAcceptable, string.Join(",", ModelState.Values.SelectMany(x => x.Errors).Select(x => x.ErrorMessage)));
            }
            SuccessResponse response = new SuccessResponse();
            try
            {
                bool validUser = _userService.IsUser(model.Email);
                if (validUser)
                { 
                    AppTokenAddRequest token = new AppTokenAddRequest();
                    MemberProfile _memberProfile = _memberProfileService.SelectByEmail(model.Email);
                    token.MemberProfileId = _memberProfile.Id;
                    token.TokenTypeId = (int)AppTokenType.ResetPassword;
                    Guid _guid = _appTokenService.Insert(token);
                    await _sendEmailService.SendEmailForgotPassword(model.Email, _guid);


                    return Request.CreateResponse(HttpStatusCode.OK, response);
                }
                else
                {
                    return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "Email Doesn't Exist");
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }

        }

        [Route("logout"), HttpGet]
        public HttpResponseMessage UserLogout()
        {
            SuccessResponse response = new SuccessResponse();
            try
            {
                _userService.Logout();
                return Request.CreateResponse(HttpStatusCode.OK, response);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }
        [AllowAnonymous]
        [Route("forgotpassword/{tokenGuid:Guid}"), HttpGet]
        public HttpResponseMessage ForgotPassword(Guid tokenGuid)
        {
            HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.RedirectMethod);
            try
            {
                _emailConfirmationService.SelectById(tokenGuid);
                string domain = HttpContext.Current.Request.IsLocal ? "http://localhost:1552" : "https://snatched.azurewebsites.net";
                response.Headers.Location = new Uri(domain + "/Member/ResetPassword/" + tokenGuid );

                return response;
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }
        //[AllowAnonymous]
        //[Route("forgotpassword/{tokenGuid:Guid}"), HttpGet]
        //public HttpResponseMessage ContactUs(Guid tokenGuid)
        //{
        //    HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.RedirectMethod);
        //    try
        //    {
        //        _emailConfirmationService.SelectById(tokenGuid);
        //        string domain = HttpContext.Current.Request.IsLocal ? "http://localhost:1552" : "https://snatched.azurewebsites.net";
        //        response.Headers.Location = new Uri(domain + "/Member/ResetPassword/" + tokenGuid);

        //        return response;
        //    }
        //    catch (Exception ex)
        //    {
        //        return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
        //    }
        //}

        [AllowAnonymous]
        [Route("updatepassword"), HttpPost]
        public HttpResponseMessage UpdatePassword(ResetPasswordUpdateRequest model)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotAcceptable, string.Join(",", ModelState.Values.SelectMany(x => x.Errors).Select(x => x.ErrorMessage)));
            }
            HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.RedirectMethod);
            try
            {
                _userService.ResetPassword(model);

                //_emailConfirmationService.ResetPasswordById(model);
                string domain = HttpContext.Current.Request.IsLocal ? "http://localhost:1552" : "https://snatched.azurewebsites.net";
                response.Headers.Location = new Uri(domain + "/Member/SplashPage");

                return response;
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        //public async Task<IdentityResult> ResetPasswordAsync(TKey userId, string token, string newPassword)
        //{
        //    SuccessResponse response = new SuccessResponse();
        //    try
        //    {
        //        IdentityUser currentUserEmail = _userService.GetCurrentUser();

        //    }
        //}

        [Route("changepassword"), HttpPut]
        public HttpResponseMessage Changepassword(ChangePasswordUpdateRequest model)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotAcceptable, string.Join(",", ModelState.Values.SelectMany(x => x.Errors).Select(x => x.ErrorMessage)));
            }
            SuccessResponse response = new SuccessResponse();
            try
            {
                string uId = _userService.GetCurrentUserId();
                IdentityUser currentUserId = _userService.GetCurrentUser();
                ServiceResponse loginResponse = _userService.Signin(currentUserId.Email, model.OldPassword);
                if (loginResponse.IsSuccessful)
                {
                    bool result = _userService.ChangePassWord(currentUserId.Id, model.NewPassword);
                    return Request.CreateResponse(HttpStatusCode.OK, new ItemResponse<bool> { Item = result });
                }
                else
                {
                    return Request.CreateErrorResponse(HttpStatusCode.Unauthorized, loginResponse.ResponseMessage);
                }

            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }


    }
}
