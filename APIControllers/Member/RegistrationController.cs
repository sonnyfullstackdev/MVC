
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace ProjectName.Controllers.Api.Member
{
    //[RoutePrefix("api/registrations")]
    //public class RegistrationController : ApiController
    //{
    //    [Route, HttpPost]
    //    public async Task<HttpResponseMessage> Register(RegistrationAddRequest model)
    //    {
    //        if (!ModelState.IsValid)
    //        {
    //            return Request.CreateErrorResponse(HttpStatusCode.BadGateway, ModelState);
    //        }

    //        ItemResponse<RegistrationResponse> response = new ItemResponse<RegistrationResponse>();
    //        try
    //        {
    //            //response.Item = RegistrationService.RegisterUser(model);
    //            //await RegistrationService.RegistrationEmail(model.Email);
    //            return Request.CreateResponse(HttpStatusCode.OK, response);
    //        }
    //        catch (Exception ex)
    //        {
    //            return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex);
    //        }
    //    }

        //[Route("confirmemail"), HttpPost]
        //public async Task<HttpResponseMessage> Register(ConfirmEmailAddRequest model)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return Request.CreateErrorResponse(HttpStatusCode.BadGateway, ModelState);
        //    }
        //    ItemResponse<RegistrationResponse> response = new ItemResponse<RegistrationResponse>();
        //    try
        //    {

        //        await RegistrationService.RegistrationEmail(model.Email);
        //        return Request.CreateResponse(HttpStatusCode.OK, response);
        //        //return Request.CreateResponse(HttpStatusCode.OK, rsp);
        //    }
        //    catch (Exception ex)
        //    {
        //        return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex);
        //    }
        //}

        //[Route("confirmemail"), HttpPost]
        //public async Task<HttpResponseMessage> Register(ConfirmEmailAddRequest model)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return Request.CreateErrorResponse(HttpStatusCode.BadGateway, ModelState);
        //    }
        //    ItemResponse<RegistrationResponse> response = new ItemResponse<RegistrationResponse>();
        //    try
        //    {
        //[Route("confirmemail/{tokenGuid:Guid}"), HttpGet]
        //public HttpResponseMessage ConfirmEmail(Guid tokenGuid)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return Request.CreateErrorResponse(HttpStatusCode.BadGateway, ModelState);

        //    }
        //    try
        //    {
        //        ItemsResponse<AppToken> response = new ItemsResponse<AppToken>();
        //        response.Items = AppTokenService.SelectByGuid(tokenGuid, 1);
        //        if (response.Items.Count == 1)
        //        {
        //            return Request.CreateResponse(HttpStatusCode.OK, response);
        //        }
        //        else
        //        {
        //            return Request.CreateResponse(HttpStatusCode.Unauthorized, "Incorrect Username");
        //        }
        //    }
        //    catch (Exception)
        //    {
        //        return Request.CreateResponse(HttpStatusCode.Unauthorized, "Incorrect Username");
        //    }
        //}



        //[Route, HttpGet]
        //public HttpResponseMessage Get()
        //{
        //    ItemResponse<RegistrationResponse> response = new ItemResponse<RegistrationResponse>();

        //    response.Item = RegistrationService.SelectByIds();

        //    return Request.CreateResponse(HttpStatusCode.OK, response);
        //}
    //}
}
