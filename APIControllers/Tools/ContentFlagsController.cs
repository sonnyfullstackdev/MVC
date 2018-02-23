
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace ProjectName.Controllers.Api.Tools
{

    [RoutePrefix("api/contentflags")]
    public class ContentFlagsController : ApiController
    {

        IContentFlagService _contentFlagService;
        IMemberProfileService _memberProfileService;
        ISendEmailService _sendEmailService;
        ISmsService _smsService;
        IAdminService _adminService;

        public ContentFlagsController(IContentFlagService contentFlagService, IMemberProfileService memberProfileService, ISendEmailService sendEmailService, IAdminService adminService, ISmsService smsService)
        {
            _contentFlagService = contentFlagService;
            _memberProfileService = memberProfileService;
            _sendEmailService = sendEmailService;
            _adminService = adminService;
            _smsService = smsService;
        }


        [Route, HttpGet]
        public HttpResponseMessage GetAll()
        {
            ItemsResponse<ContentFlag> response = new ItemsResponse<ContentFlag>();

            response.Items = _contentFlagService.SelectAll();

            return Request.CreateResponse(HttpStatusCode.OK, response);
        }

        [Route("{id:int}"), HttpDelete]
        public HttpResponseMessage Delete(int id)
        {
            SuccessResponse response = new SuccessResponse();
            try
            {
                _contentFlagService.DeleteByIds(id);
                return Request.CreateResponse(HttpStatusCode.OK, response);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex);
            }
        }

        [Route("{aspNetUserId}"), HttpPost]
        public async Task<HttpResponseMessage> Insert(ContentFlagAddRequest model, string aspNetUserId)
        {
            int ReportedByMemberId = _memberProfileService.GetCurrentMemberProfile().Id;
            int MemberProfileId = _memberProfileService.GetMemberProfileByAspNetUserId(aspNetUserId).Id;

            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadGateway, ModelState);
            }
            SuccessResponse response = new SuccessResponse();
            ItemsResponse<AdminSettings> notify = new ItemsResponse<AdminSettings>();
            try
            {
                notify.Items = _adminService.SelectByNotifications();
                _contentFlagService.Insert(ReportedByMemberId, MemberProfileId, model.FlagTypeId);

                foreach(AdminSettings item in notify.Items)
                {
                    if (item.FlagEmail == true)
                    {
                        await _sendEmailService.SendEmailFlaggedContent(item.Email);
                    }
                    if (item.FlagText == true)
                    {
                        string message = "A new content has been flagged! Please login and regulate!";
                        _smsService.SendSms(item.PhoneNumber, message);
                    }
                }
                return Request.CreateResponse(HttpStatusCode.OK, response);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex);
            }
        }

        [Route("{id:int}"), HttpPut]
        public HttpResponseMessage Update(ContentFlagUpdateRequest model)
        {
            SuccessResponse response = new SuccessResponse();
            try
            {
                _contentFlagService.UpdateById(model);
                return Request.CreateResponse(HttpStatusCode.OK, response);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex);
            }
        }

        [Route("{pagenumber:int}/{pagesize:int}/{sortby:int}"), HttpPost]
        public HttpResponseMessage GetAllByDate(int pageNumber, int pageSize, int sortBy)
        {
            ItemsResponse<ContentFlag> response = new ItemsResponse<ContentFlag>();

            response.Items = _contentFlagService.SelectAllByDate(pageNumber, pageSize, sortBy);

            return Request.CreateResponse(HttpStatusCode.OK, response);
        }
    }
}