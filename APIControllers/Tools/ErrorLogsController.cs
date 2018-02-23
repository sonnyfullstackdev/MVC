
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ProjectName.Controllers.Api.Tools
{
    [RoutePrefix("api/errorlogs")]

    public class ErrorLogsController : ApiController
    {
        IErrorLogService _errorLogService;

        public ErrorLogsController(IErrorLogService errorLogService)
        {
            _errorLogService = errorLogService;
        }

        public HttpResponseMessage GetAll()
        {
            ItemsResponse<ErrorLog> response = new ItemsResponse<ErrorLog>();
            response.Items = _errorLogService.SelectAll();

            return Request.CreateResponse(HttpStatusCode.OK, response);
        }

        public HttpResponseMessage Insert(ErrorLogAddRequest model)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadGateway, ModelState);
            }

            SuccessResponse response = new SuccessResponse();
            try
            {
                _errorLogService.Insert(model);
                return Request.CreateResponse(HttpStatusCode.OK, response);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex);
            }
        }

    }
}
