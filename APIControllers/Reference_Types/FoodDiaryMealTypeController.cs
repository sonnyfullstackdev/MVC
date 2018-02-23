
using System;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ProjectName.Controllers.Api.RefType
{
    [RoutePrefix("api/foodDiaryMealTypes")]
    public class FoodDiaryMealTypeController : ApiController
    {
        IFoodDiaryMealTypeService _foodDiaryMealTypeService;

        public FoodDiaryMealTypeController(IFoodDiaryMealTypeService foodDiaryMealTypeService)
        {
            _foodDiaryMealTypeService = foodDiaryMealTypeService;
        }
        [Route, HttpPost]
        public HttpResponseMessage Insert(FoodDiaryMealTypeAddRequest model)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadGateway, ModelState);
            }
            SuccessResponse response = new SuccessResponse();
            try
            {
                _foodDiaryMealTypeService.Insert(model);
                return Request.CreateResponse(HttpStatusCode.OK, response);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex);
            } 
        }

        [Route, HttpGet]
        public HttpResponseMessage GetAll()
        {
            ItemsResponse<FoodDiaryMealType> response = new ItemsResponse<FoodDiaryMealType>();

            response.Items = _foodDiaryMealTypeService.SelectAll();

            return Request.CreateResponse(HttpStatusCode.OK, response);

        }

        [Route("{id:int}"), HttpGet]
        public HttpResponseMessage GetById(int Id = 0)
        {
            ItemsResponse<FoodDiaryMealType> response = new ItemsResponse<FoodDiaryMealType>();

            response.Items = _foodDiaryMealTypeService.SelectById(Id);

            return Request.CreateResponse(HttpStatusCode.OK, response);

        }

        [Route("{id:int}"), HttpDelete]
        public HttpResponseMessage Delete(int Id)
        {
            SuccessResponse response = new SuccessResponse();

            try
            {
                _foodDiaryMealTypeService.Delete(Id);
                return Request.CreateResponse(HttpStatusCode.OK, response);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex);
            }

        }

        [Route("{id:int}"), HttpPut]
        public HttpResponseMessage Update(FoodDiaryMealTypeUpdateRequest model)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadGateway, ModelState);
            }
            //by this point we know our model is valid.
            SuccessResponse response = new SuccessResponse();
            try
            {

                _foodDiaryMealTypeService.Update(model);
                return Request.CreateResponse(HttpStatusCode.OK, response);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex);
            }


        }
    }
}
