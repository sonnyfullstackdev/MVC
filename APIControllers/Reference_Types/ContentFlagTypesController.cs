
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ProjectName..Controllers.Api.RefType
{
    [RoutePrefix("api/contentflagtypes")]
    public class ContentFlagTypesController : ApiController
    {
        IContentFlagTypeService _contentFlagTypeService;

        public ContentFlagTypesController(IContentFlagTypeService contentFlagTypeService)
        {
            _contentFlagTypeService = contentFlagTypeService;
        }


        [Route, HttpGet]
        public HttpResponseMessage GetAll()
        {
            ItemsResponse<ContentFlagType> response = new ItemsResponse<ContentFlagType>();

            response.Items = _contentFlagTypeService.SelectAll();

            return Request.CreateResponse(HttpStatusCode.OK, response);
        }

        [Route, HttpGet]
        public HttpResponseMessage GetById(int id)
        {
            ItemResponse<ContentFlagType> response = new ItemResponse<ContentFlagType>();
            response.Item = _contentFlagTypeService.SelectById(id);

            return Request.CreateResponse(HttpStatusCode.OK, response);
        }

        [Route("{id:int}"), HttpDelete]
        public HttpResponseMessage Delete(int id)
        {
            SuccessResponse response = new SuccessResponse();
            try
            {
                _contentFlagTypeService.DeleteById(id);
                return Request.CreateResponse(HttpStatusCode.OK, response);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex);

            }
        }

        [Route, HttpPost]
        public HttpResponseMessage Insert(ContentFlagTypeAddRequest model)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadGateway, ModelState);

            }
            SuccessResponse response = new SuccessResponse();
            try
            {
                _contentFlagTypeService.Insert(model);
                return Request.CreateResponse(HttpStatusCode.OK, response);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex);
            }
        }

        [Route("{id:int}"), HttpPut]
        public HttpResponseMessage Update(ContentFlagTypeUpdateRequest model)
        {
            SuccessResponse response = new SuccessResponse();
            try
            {
                _contentFlagTypeService.UpdateById(model);
                return Request.CreateResponse(HttpStatusCode.OK, response);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex);

            }
        }
    }
}
