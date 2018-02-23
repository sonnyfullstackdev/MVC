
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace ProjectName.Controllers.Api.Login
{
    //[RoutePrefix("api/logins")]
    //public class LoginController : ApiController
    //{
    //    [Route, HttpPost]
    //    public HttpResponseMessage Login(LoginAddRequest model)
    //    {
    //        if (!ModelState.IsValid)
    //        {
    //            return Request.CreateErrorResponse(HttpStatusCode.BadGateway, ModelState);

    //        }
    //        SuccessResponse response = new SuccessResponse();
    //        try
    //        {
    //            bool status = LoginService.UserLogin(model.Email, model.Password);

    //            if (status)
    //            {
    //                return Request.CreateResponse(HttpStatusCode.OK, response);
    //            }
    //            else
    //            {
                    
    //                return Request.CreateResponse(HttpStatusCode.Unauthorized, "Wrong Password or Username");

    //            }
    //        }
    //        catch (Exception ex)
    //        {
    //            return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex);
    //        }
    //    }

    //    //Make sure for Ajax call to Add slash at the end:: logins/forgotpassword/"email.com"/

    //    [Route("forgotpassword"), HttpPost]
    //    public async Task<HttpResponseMessage> ForgotPassword(LoginEmailRequest model)
    //    {
    //        if (!ModelState.IsValid)
    //        {
    //            return Request.CreateErrorResponse(HttpStatusCode.BadGateway, ModelState);

    //        }
    //        SuccessResponse response = new SuccessResponse();
    //        try
    //        {
    //            var status = await LoginService.UserForgotPassword(model.Email);
    //            return Request.CreateResponse(HttpStatusCode.OK, response);
                
    //        }
    //        catch (Exception)
    //        {
    //            return Request.CreateResponse(HttpStatusCode.Unauthorized, "Incorrect Username");
    //        }
    //    }

    //    [Route("forgotpassword/{tokenGuid:Guid}"), HttpGet]
    //    public HttpResponseMessage ForgotPassword(Guid tokenGuid)
    //    {
    //        if (!ModelState.IsValid)
    //        {
    //            return Request.CreateErrorResponse(HttpStatusCode.BadGateway, ModelState);

    //        }
    //        try
    //        {
    //            ItemsResponse<AppToken> response = new ItemsResponse<AppToken>();
    //            response.Items = AppTokenService.SelectByGuid(tokenGuid, 2);
    //            if(response.Items.Count == 1)
    //            {
    //                return Request.CreateResponse(HttpStatusCode.OK, response);
    //            }
    //            else
    //            {
    //                return Request.CreateResponse(HttpStatusCode.Unauthorized, "Incorrect Username");
    //            }
    //        }
    //        catch (Exception)
    //        {
    //            return Request.CreateResponse(HttpStatusCode.Unauthorized, "Incorrect Username");
    //        }
    //    }

    //    [Route("logout"), HttpGet]
    //    public HttpResponseMessage UserLogout()
    //    {
            
    //        SuccessResponse response = new SuccessResponse();
    //        try
    //        {
    //            LoginService.UserLogout();

              
    //            return Request.CreateResponse(HttpStatusCode.OK, response);
                
    //        }
    //        catch (Exception ex)
    //        {
    //            return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex);
    //        }
    //    }

    //    [Route("changepassword"), HttpPut]
    //    public HttpResponseMessage Changepassword(ChangePasswordUpdateRequest model)
    //    {
    //        if (!ModelState.IsValid)
    //        {
    //            return Request.CreateErrorResponse(HttpStatusCode.BadGateway, ModelState);

    //        }
    //        SuccessResponse response = new SuccessResponse();
    //        try
    //        {
    //            bool status = LoginService.UserChangePassword(model.OldPassword, model.NewPassword);

    //            if (status)
    //            {
    //                return Request.CreateResponse(HttpStatusCode.OK, response);
    //            }
    //            else
    //            {

    //                return Request.CreateResponse(HttpStatusCode.Unauthorized, "Wrong Password or Username");

    //            }
    //        }
    //        catch (Exception ex)
    //        {
    //            return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex);
    //        }
    //    }
    //}





}


