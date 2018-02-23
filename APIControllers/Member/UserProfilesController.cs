
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace ProjectName.Controllers.Api.Member
{
    [RoutePrefix("api/userprofiles")]
    public class UserProfilesController : ApiController
    {
        IMemberProfileService _memberProfileService;
        IMemberProfileVisitService _memberProfileVisitService;
        ISmsService _smsService;
        ISendEmailService _sendEmailService;
        IAdminService _adminService;
        IUserService _userService;

        public MemberProfilesController(IMemberProfileService memberProfileService, IMemberProfileVisitService memberProfileVisitService, ISmsService smsService, ISendEmailService sendEmailService, IAdminService adminService, IUserService userService)
        {
            _memberProfileService = memberProfileService;
            _memberProfileVisitService = memberProfileVisitService;
            _smsService = smsService;
            _sendEmailService = sendEmailService;
            _adminService = adminService;
            _userService = userService;
        }

        [Route, HttpGet]
        public HttpResponseMessage GetAll()
        {
            ItemsResponse<MemberProfile> response = new ItemsResponse<MemberProfile>();
            try
            {
                response.Items = _memberProfileService.SelectAll();
                return Request.CreateResponse(HttpStatusCode.OK, response);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }
        [Route("{id:int}"), HttpGet]
        public HttpResponseMessage GetById(int id)
        {
            ItemResponse<MemberProfileResponse> response = new ItemResponse<MemberProfileResponse>();
            try
            {
                response.Item = _memberProfileService.SelectById(id);
                return Request.CreateResponse(HttpStatusCode.OK, response);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [Route("getcurrentmember"), HttpGet]
        public HttpResponseMessage GetCurrentMemberProfile()
        {
            ItemResponse<MemberProfile> response = new ItemResponse<MemberProfile>();
            try
            {
                response.Item = _memberProfileService.GetCurrentMemberProfile();
                return Request.CreateResponse(HttpStatusCode.OK, response);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [Route("getBannedMember"), HttpGet]
        public HttpResponseMessage GetBannedMember()
        {
            string email = _userService.GetCurrentUser().Email;
            ItemResponse<bool> response = new ItemResponse<bool>();
            try
            {
                response.Item = _adminService.IsUserBanned(email);
                return Request.CreateResponse(HttpStatusCode.OK, response);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [Route("getallbycurrentmember"), HttpGet]
        public HttpResponseMessage GetAllByCurrentMemberProfilePublic()
        {
            int id = _memberProfileService.GetCurrentMemberProfile().Id;
            ItemResponse<MemberProfileResponse> response = new ItemResponse<MemberProfileResponse>();
            try
            {
                response.Item = _memberProfileService.SelectById(id);
                //response.Item.MemberProfileSet.Id = 0;
                return Request.CreateResponse(HttpStatusCode.OK, response);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [Route("getprofilebyaspnetuserid/{aspNetUserId}"), HttpGet]
        public HttpResponseMessage GetAllByAspNetUserId(string aspNetUserId)
        {
            int id = _memberProfileService.GetMemberProfileByAspNetUserId(aspNetUserId).Id;
            ItemResponse<MemberProfileResponse> response = new ItemResponse<MemberProfileResponse>();
            try
            {
                response.Item = _memberProfileService.SelectById(id);
                response.Item.MemberProfileSet.Id = 0;
                return Request.CreateResponse(HttpStatusCode.OK, response);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [Route("getByEmail"), HttpGet]
        public HttpResponseMessage GetByEmail(MemberProfileEmailRequest model)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotAcceptable, string.Join(", ", ModelState.Values.SelectMany(x => x.Errors).Select(x => x.ErrorMessage)));
            }
            ItemResponse<MemberProfile> response = new ItemResponse<MemberProfile>();
            try
            {
                response.Item = _memberProfileService.SelectByEmail(model.Email);
                return Request.CreateResponse(HttpStatusCode.OK, response);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [Route("{id:int}"), HttpDelete]
        public HttpResponseMessage Delete(int id)
        {
            SuccessResponse response = new SuccessResponse();
            try
            {
                _memberProfileService.Delete(id);
                return Request.CreateResponse(HttpStatusCode.OK, response);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [Route, HttpPost]
        public HttpResponseMessage Insert(MemberProfileAddRequest model)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotAcceptable, string.Join(", ", ModelState.Values.SelectMany(x => x.Errors).Select(x => x.ErrorMessage)));
            }
            SuccessResponse response = new SuccessResponse();
            try
            {
                _memberProfileService.Insert(model);
                return Request.CreateResponse(HttpStatusCode.OK, response);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [Route("{id:int}"), HttpPut]
        public HttpResponseMessage Update(MemberProfileUpdateRequest model)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotAcceptable, string.Join(", ", ModelState.Values.SelectMany(x => x.Errors).Select(x => x.ErrorMessage)));
            }
            SuccessResponse response = new SuccessResponse();
            try
            {
                _memberProfileService.Update(model);
                return Request.CreateResponse(HttpStatusCode.OK, response);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [Route("logvisit/{aspNetUserId}"), HttpPut]
        public HttpResponseMessage UpdateVisitCount(string aspNetUserId)
        {
            int MemberProfileId = _memberProfileService.GetCurrentMemberProfile().Id;
            int VisitedProfileId = _memberProfileService.GetMemberProfileByAspNetUserId(aspNetUserId).Id;

            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotAcceptable, string.Join(", ", ModelState.Values.SelectMany(x => x.Errors).Select(x => x.ErrorMessage)));
            }
            SuccessResponse response = new SuccessResponse();
            try
            {
                _memberProfileVisitService.LogVisit(MemberProfileId, VisitedProfileId);
                return Request.CreateResponse(HttpStatusCode.OK, response);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [Route("updatebio"), HttpPut]
        public HttpResponseMessage UpdateBio(MemberBioUpdateRequest model)
        {
            int id = _memberProfileService.GetCurrentMemberProfile().Id;

            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotAcceptable, string.Join(", ", ModelState.Values.SelectMany(x => x.Errors).Select(x => x.ErrorMessage)));
            }
            SuccessResponse response = new SuccessResponse();
            try
            {
                _memberProfileService.UpdateBio(id, model.Bio);
                return Request.CreateResponse(HttpStatusCode.OK, response);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [Route("updatepersonalinterests"), HttpPut]
        public HttpResponseMessage UpdatePersonalInterests(MemberPersonalInterestsUpdateRequest model)
        {
            int id = _memberProfileService.GetCurrentMemberProfile().Id;
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotAcceptable, string.Join(", ", ModelState.Values.SelectMany(x => x.Errors).Select(x => x.ErrorMessage)));
            }
            SuccessResponse response = new SuccessResponse();
            try
            {
                _memberProfileService.UpdatePersonalInterests(id, model.PersonalInterests);
                return Request.CreateResponse(HttpStatusCode.OK, response);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [Route("updateProfileSettings"), HttpPut]
        public HttpResponseMessage UpdateProfileSettings(MemberProfileSettingsUpdateRequest model)
        {
            model.Id = _memberProfileService.GetCurrentMemberProfile().Id;
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotAcceptable, string.Join(", ", ModelState.Values.SelectMany(x => x.Errors).Select(x => x.ErrorMessage)));
            }
            SuccessResponse response = new SuccessResponse();
            try
            {
                _memberProfileService.UpdateMemberProfileSettings(model);
                return Request.CreateResponse(HttpStatusCode.OK, response);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [Route("updatecrossfitlevel"), HttpPut]
        public HttpResponseMessage UpdateCrossfitLevel(MemberCrossfitLevelUpdateRequest model)
        {
            int id = _memberProfileService.GetCurrentMemberProfile().Id;
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotAcceptable, string.Join(", ", ModelState.Values.SelectMany(x => x.Errors).Select(x => x.ErrorMessage)));
            }
            SuccessResponse response = new SuccessResponse();
            try
            {
                _memberProfileService.UpdateCrossfitLevel(id, model.CrossfitLevelId);
                return Request.CreateResponse(HttpStatusCode.OK, response);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [Route("updateisonline/{isOnline}"), HttpPut]
        public HttpResponseMessage UpdateIsOnline(bool isOnline)
        {
            string aspNetUserId = _memberProfileService.GetCurrentMemberProfile().AspNetUserID;
            SuccessResponse response = new SuccessResponse();
            try
            {
                _memberProfileService.UpdateIsOnline(aspNetUserId,isOnline);
                return Request.CreateResponse(HttpStatusCode.OK, response);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [Route("notification/{aspNetUserId}"), HttpGet]
        public async Task<HttpResponseMessage> GetMemberNotification(string aspNetUserId)
        {
            ItemResponse<MemberNotification> response = new ItemResponse<MemberNotification>();
            try
            {
                response.Item = _memberProfileService.NotifyMember(aspNetUserId);

                if (response.Item.IsOnline == false)
                {
                    if (response.Item.AlertUsingTextMessage == true)
                    {
                        string message = "You got a new match! Log on to Snatched to see who it is!";
                        _smsService.SendSms(response.Item.PhoneNumber, message);
                    }

                    if (response.Item.AlertUsingEmail == true)
                    {
                        await _sendEmailService.SendEmailMatchNotification(response.Item.Email);
                    }
                }

                return Request.CreateResponse(HttpStatusCode.OK, response);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }

    }
}