using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;


namespace ProjectName.Services.RefType
{
    public class UserFoodDiaryMealService : IMemberFoodDiaryService
    {
        IBaseService _baseService;

        public MemberFoodDiaryMealService(IBaseService baseService, IErrorLogService errorLogService)
        {
            _baseService = baseService;
            _baseService.ErrorLog = errorLogService;
        }

        public int Insert(MemberFoodDiaryMealAddRequest model)
        {
            try
            {
                int id = 0;
                _baseService.DataProvider.ExecuteNonQuery(_baseService.GetConnection, "dbo.MemberFoodDiaryMeal_Insert", inputParamMapper: delegate (SqlParameterCollection paramCollection)
                {
                    paramCollection.AddWithValue("@FoodDiaryMealTypeId", model.FoodDiaryMealTypeId);
                    paramCollection.AddWithValue("@MealDescription", model.MealDescription);
                    paramCollection.AddWithValue("@Calories", model.Calories);
                    paramCollection.AddWithValue("@Carbs", model.Carbs);
                    paramCollection.AddWithValue("@Protein", model.Protein);
                    paramCollection.AddWithValue("@MemberProfileId", model.MemberProfileId);

                    SqlParameter p = new SqlParameter("@Id", System.Data.SqlDbType.Int);
                    p.Direction = System.Data.ParameterDirection.Output;

                    paramCollection.Add(p);

                }, returnParameters: delegate (SqlParameterCollection param)
                {
                    int.TryParse(param["@Id"].Value.ToString(), out id);
                });
                return id;
            }
            catch (Exception ex)
            {
                _baseService.LogError(System.Reflection.MethodBase.GetCurrentMethod().Name, ex, "MemberFoodDiaryMealService");
                throw;
            }
        }

        public int Save(MemberFoodDiaryMealUpdateRequest model)
        {
            try
            {
                int id = 0;
                _baseService.DataProvider.ExecuteNonQuery(_baseService.GetConnection, "dbo.MemberFoodDiaryMeal_Save", inputParamMapper: delegate (SqlParameterCollection paramCollection)
                {
                    paramCollection.AddWithValue("@FoodDiaryMealTypeId", model.FoodDiaryMealTypeId);
                    paramCollection.AddWithValue("@MealDescription", model.MealDescription);
                    paramCollection.AddWithValue("@Calories", model.Calories);
                    paramCollection.AddWithValue("@Carbs", model.Carbs);
                    paramCollection.AddWithValue("@Protein", model.Protein);
                    paramCollection.AddWithValue("@MemberProfileId", model.MemberProfileId);

                    SqlParameter p = new SqlParameter("@Id", System.Data.SqlDbType.Int);
                    p.Direction = System.Data.ParameterDirection.InputOutput;
                    p.Value = model.Id;

                    paramCollection.Add(p);

                }, returnParameters: delegate (SqlParameterCollection param)
                {
                    int.TryParse(param["@Id"].Value.ToString(), out id);
                });
                return id;
            }
            catch (Exception ex)
            {
                _baseService.LogError(System.Reflection.MethodBase.GetCurrentMethod().Name, ex, "MemberFoodDiaryMealService");
                throw;
            }
        }

        public void UpdateById(MemberFoodDiaryMealUpdateRequest model)
        {
            try
            {
                _baseService.DataProvider.ExecuteNonQuery(_baseService.GetConnection, "dbo.MemberFoodDiaryMeal_UpdateById", inputParamMapper: delegate (SqlParameterCollection paramCollection)
                {
                    paramCollection.AddWithValue("@Id", model.Id);
                    paramCollection.AddWithValue("@FoodDiaryMealTypeId", model.FoodDiaryMealTypeId);
                    paramCollection.AddWithValue("@MealDescription", model.MealDescription);
                    paramCollection.AddWithValue("@Calories", model.Calories);
                    paramCollection.AddWithValue("@Carbs", model.Carbs);
                    paramCollection.AddWithValue("@Protein", model.Protein);
                    paramCollection.AddWithValue("@MemberProfileId", model.MemberProfileId);

                }); 
            }
            catch (Exception ex)
            {
                _baseService.LogError(System.Reflection.MethodBase.GetCurrentMethod().Name, ex, "MemberFoodDiaryMealService");
                throw;
            }
        }

        public void DeleteById(int id)
        {
            try
            {
                _baseService.DataProvider.ExecuteNonQuery(_baseService.GetConnection, "dbo.MemberFoodDiaryMeal_DeleteById", inputParamMapper: delegate (SqlParameterCollection paramCollection)
                {
                    paramCollection.AddWithValue("@Id", id);
                });
            }
            catch (Exception ex)
            {
                _baseService.LogError(System.Reflection.MethodBase.GetCurrentMethod().Name, ex, "MemberFoodDiaryMealService");
                throw;
            }
        }

        public List<MemberFoodDiaryMeal> SelectAll()
        {
            try
            {
                List<MemberFoodDiaryMeal> list = null;

                _baseService.DataProvider.ExecuteCmd(_baseService.GetConnection, "dbo.MemberFoodDiaryMeal_SelectAll", null
                , map: delegate (IDataReader reader, short set)
                {
                    if (list == null)
                    {
                        list = new List<MemberFoodDiaryMeal>();
                    }
                    list.Add(_baseService.MapToObject<MemberFoodDiaryMeal>(reader));
                });
                return list;
            }
            catch (Exception ex)
            {
                _baseService.LogError(System.Reflection.MethodBase.GetCurrentMethod().Name, ex, "MemberFoodDiaryMealService");
                throw;
            }
        }
        //ASPNetUserId start
        public List<MemberFoodDiaryMeal> SelectAllByAspNetUserId(string aspnetuserid)
        {
            try
            {
                List<MemberFoodDiaryMeal> list = null;

                _baseService.DataProvider.ExecuteCmd(_baseService.GetConnection, "dbo.MemberFoodDiaryMeal_SelectByAspNetUserId",
                    inputParamMapper: delegate (SqlParameterCollection paramCollection)
                    {
                        paramCollection.AddWithValue("@AspNetUserId", aspnetuserid);
                    }
                , map: delegate (IDataReader reader, short set)
                {
                    if (list == null)
                    {
                        list = new List<MemberFoodDiaryMeal>();
                    }
                    list.Add(_baseService.MapToObject<MemberFoodDiaryMeal>(reader));
                });
                return list;
            }
            catch (Exception ex)
            {
                _baseService.LogError(System.Reflection.MethodBase.GetCurrentMethod().Name, ex, "MemberFoodDiaryMealService");
                throw;
            }
        }

        private MemberFoodDiaryMeal MemberFoodDiaryMealMap(IDataReader reader)
        {
            try
            {
                int startingIndex = 0;
                MemberFoodDiaryMeal _MemberFoodDiaryMeal = new MemberFoodDiaryMeal();
                _MemberFoodDiaryMeal.Id = reader.GetSafeInt32(startingIndex++);
                _MemberFoodDiaryMeal.FoodDiaryMealTypeId = reader.GetSafeInt32(startingIndex++);
                _MemberFoodDiaryMeal.MealDescription = reader.GetSafeString(startingIndex++);
                _MemberFoodDiaryMeal.Calories = reader.GetSafeInt32(startingIndex++);
                _MemberFoodDiaryMeal.Carbs = reader.GetSafeInt32(startingIndex++);
                _MemberFoodDiaryMeal.Fat = reader.GetSafeInt32(startingIndex++);
                _MemberFoodDiaryMeal.Protein = reader.GetSafeInt32(startingIndex++);
                _MemberFoodDiaryMeal.MemberProfileId = reader.GetSafeInt32(startingIndex++);
                _MemberFoodDiaryMeal.CreatedDate = reader.GetSafeDateTime(startingIndex++);
                _MemberFoodDiaryMeal.ModifiedDate = reader.GetSafeDateTime(startingIndex++);

                startingIndex++;

                return _MemberFoodDiaryMeal;
            }
            catch (Exception ex)
            {
                _baseService.LogError(System.Reflection.MethodBase.GetCurrentMethod().Name, ex, "MemberFoodDiaryMealService");
                throw;
            }
        }

        public MemberFoodDiaryMeal SelectById(int id = 0)
        {
            try
            {
                MemberFoodDiaryMeal _MemberFoodDiaryMeal = null;

                _baseService.DataProvider.ExecuteCmd(_baseService.GetConnection, "dbo.MemberFoodDiaryMeal_SelectById",
                inputParamMapper: delegate (SqlParameterCollection paramCollection)
                {
                    paramCollection.AddWithValue("@Id", id);//if its not select by id then its null here 
                },
                map: delegate (IDataReader reader, short set)
                {
                    _MemberFoodDiaryMeal = MemberFoodDiaryMealMap(reader);
                });
                return _MemberFoodDiaryMeal;
            }
            catch (Exception ex)
            {
                _baseService.LogError(System.Reflection.MethodBase.GetCurrentMethod().Name, ex, "MemberFoodDiaryMealService");
                throw;
            }
        }
        public List<MemberFoodDiaryMeal> SelectAllByDate(int pageNumber, int pageSize, int sortBy, string aspNetUserId)
        {
            try
            {
                List<MemberFoodDiaryMeal> _memberFoodDiaryMeal = null;

                _baseService.DataProvider.ExecuteCmd(_baseService.GetConnection, "dbo.MemberFoodDiaryMeal_Pagination",
                inputParamMapper: delegate (SqlParameterCollection paramCollection)
                {
                    paramCollection.AddWithValue("@AspNetUserId", aspNetUserId);
                    paramCollection.AddWithValue("@PageNumber", pageNumber);
                    paramCollection.AddWithValue("@PageSize", pageSize);
                    paramCollection.AddWithValue("@SortBy", sortBy);
                },
                map: delegate (IDataReader reader, short set)
                {
                    if (_memberFoodDiaryMeal == null)
                        _memberFoodDiaryMeal = new List<MemberFoodDiaryMeal>();

                    _memberFoodDiaryMeal.Add(_baseService.MapToObject<MemberFoodDiaryMeal>(reader));
                });
                return _memberFoodDiaryMeal;
            }
            catch (Exception ex)
            {
                _baseService.LogError(System.Reflection.MethodBase.GetCurrentMethod().Name, ex, "MemberFoodDiaryMealService");
                throw;
            }
        }
    }
}