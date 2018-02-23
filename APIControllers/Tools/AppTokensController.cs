
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ProjectName.Controllers.Api.Tools
{
    [RoutePrefix("api/apptokens")]
    public class AppTokensController : ApiController
    {
        IAppTokenService _appTokenService;

        public AppTokensController(IAppTokenService appTokenService)
        {
            _appTokenService = appTokenService;
        }
        [Route, HttpPost]

        public HttpResponseMessage Insert(AppTokenAddRequest model)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadGateway, ModelState);
            }
            SuccessResponse response = new SuccessResponse();
            try
            {
                _appTokenService.Insert(model);
                return Request.CreateResponse(HttpStatusCode.OK, response);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex);
            }
        }

        [Route("{tokenGuid:Guid}/{TokenTypeId:int}"), HttpGet]
        public HttpResponseMessage GetByGuid(Guid tokenGuid, int TokenTypeId)
        {
            ItemsResponse<AppToken> response = new ItemsResponse<AppToken>();
            response.Items = _appTokenService.SelectByGuid(tokenGuid, TokenTypeId);

            return Request.CreateResponse(HttpStatusCode.OK, response);
        }

    }
}
