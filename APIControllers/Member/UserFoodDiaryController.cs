using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;


namespace ProjectName.Controllers.Api.Member
{
    [RoutePrefix("api/userfooddiarymeals")]
    public class UserFoodDiaryController : ApiController
    {
        IUserService _userService;
        IMemberFoodDiaryService _memberFoodDiaryMealService;
        IMemberProfileService _memberProfileService;

        public MemberFoodDiaryMealsController(IUserService userService, IMemberFoodDiaryService memberFoodDiaryMealService, IMemberProfileService memberProfileService)
        {
            _userService = userService;
            _memberFoodDiaryMealService = memberFoodDiaryMealService;
            _memberProfileService = memberProfileService;
        }

        [Route("forcurrentmember"), HttpGet]
        public HttpResponseMessage GetFoodDiaryIdForCurrentMember()
        {
            ItemsResponse<MemberFoodDiaryMeal> response = new ItemsResponse<MemberFoodDiaryMeal>();
            string getUser = _userService.GetCurrentUserId();
            response.Items = _memberFoodDiaryMealService.SelectAllByAspNetUserId(getUser);
            return Request.CreateResponse(HttpStatusCode.OK, response);
        }

        [Route("fordifferentmember/{aspNetUserId}"), HttpGet]
        public HttpResponseMessage GetFoodDiaryIdForDifferentMember(string aspNetUserId)
        {
            ItemsResponse<MemberFoodDiaryMeal> response = new ItemsResponse<MemberFoodDiaryMeal>();

            response.Items = _memberFoodDiaryMealService.SelectAllByAspNetUserId(aspNetUserId);
            return Request.CreateResponse(HttpStatusCode.OK, response);
        }

        [Route, HttpGet]
        public HttpResponseMessage GetAll()
        {
            ItemsResponse<MemberFoodDiaryMeal> response = new ItemsResponse<MemberFoodDiaryMeal>();
            try
            {
                response.Items = _memberFoodDiaryMealService.SelectAll();
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
            ItemResponse<MemberFoodDiaryMeal> response = new ItemResponse<MemberFoodDiaryMeal>();
            try
            {
                response.Item = _memberFoodDiaryMealService.SelectById(id);
                return Request.CreateResponse(HttpStatusCode.OK, response);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }

        }

        [Route("{id:int}"), HttpDelete]
        public HttpResponseMessage DeleteById(int id)
        {
            SuccessResponse response = new SuccessResponse();
            try
            {
                _memberFoodDiaryMealService.DeleteById(id);
                return Request.CreateResponse(HttpStatusCode.OK, response);
            }
            catch(Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }

        }

        [Route, HttpPost]
        public HttpResponseMessage Insert(MemberFoodDiaryMealAddRequest model)
        {
            model.MemberProfileId = _memberProfileService.GetCurrentMemberProfile().Id; 
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadGateway, string.Join(", ", ModelState.Values.SelectMany(x => x.Errors).Select(x => x.ErrorMessage)));
            }
            SuccessResponse response = new SuccessResponse();
            try
            {
                _memberFoodDiaryMealService.Insert(model);
                return Request.CreateResponse(HttpStatusCode.OK, response);
            }
            catch(Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [Route("{id:int}"), HttpPut]
        public HttpResponseMessage UpdateById(MemberFoodDiaryMealUpdateRequest model)
        {
            SuccessResponse response = new SuccessResponse();
            try
            {
                _memberFoodDiaryMealService.UpdateById(model);
                return Request.CreateResponse(HttpStatusCode.OK, response);
            }
            catch(Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [Route("save"), HttpPut]
        public HttpResponseMessage Save(MemberFoodDiaryMealUpdateRequest model)
        {
            SuccessResponse response = new SuccessResponse();
            model.MemberProfileId = _memberProfileService.GetCurrentMemberProfile().Id;
            try
            {
                _memberFoodDiaryMealService.Save(model);
                return Request.CreateResponse(HttpStatusCode.OK, response);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [Route("{aspNetUserId}/{pagenumber:int}/{pagesize:int}/{sortby:int}"), HttpGet]
        public HttpResponseMessage GetAllByDate(int pageNumber, int pageSize, int sortBy, string aspNetUserId)
        {
            ItemsResponse<MemberFoodDiaryMeal> response = new ItemsResponse<MemberFoodDiaryMeal>();

            response.Items = _memberFoodDiaryMealService.SelectAllByDate(pageNumber, pageSize, sortBy, aspNetUserId);

            return Request.CreateResponse(HttpStatusCode.OK, response);
        }


    }
}
