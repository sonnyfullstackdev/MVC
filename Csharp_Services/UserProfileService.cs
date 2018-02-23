
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace ProjectName.Services.Member
{
    public class UserProfileService: IMemberProfileService
    {
        IBaseService _baseService;
        IUserService _userService;

        public MemberProfileService(IBaseService baseService, IErrorLogService errorLogService, IUserService userService)
        {
            _baseService = baseService;
            _baseService.ErrorLog = errorLogService;
            _userService = userService;
        }

        public int Insert(MemberProfileAddRequest model)
        {
            try
            {
                int id = 0;
                _baseService.DataProvider.ExecuteNonQuery(_baseService.GetConnection, "dbo.MemberProfile_Insert", inputParamMapper: delegate (SqlParameterCollection paramCollection)
                {
                    paramCollection.AddWithValue("@AspNetUserID", model.AspNetUserID);
                    paramCollection.AddWithValue("@FirstName", model.FirstName);
                    paramCollection.AddWithValue("@MiddleName", model.MiddleName);
                    paramCollection.AddWithValue("@LastName", model.LastName);
                    paramCollection.AddWithValue("@PhoneNumber", model.PhoneNumber);
                    paramCollection.AddWithValue("@Address1", model.Address1);
                    paramCollection.AddWithValue("@Address2", model.Address2);
                    paramCollection.AddWithValue("@City", model.City);
                    paramCollection.AddWithValue("@StateProvinceId", model.StateProvinceId);
                    paramCollection.AddWithValue("@Zip", model.Zip);
                    paramCollection.AddWithValue("@DateOfBirth", model.DateOfBirth);
                    paramCollection.AddWithValue("@Email", model.Email);
                    paramCollection.AddWithValue("@Gender", model.Gender);
                    paramCollection.AddWithValue("@IsActive", model.IsActive);
                    paramCollection.AddWithValue("@IsViewable", model.IsViewable);
                    paramCollection.AddWithValue("@IsGymOwner", model.IsGymOwner);
                    paramCollection.AddWithValue("@CrossfitLevelId", model.CrossFitLevelID);
                    paramCollection.AddWithValue("@IsPublic", model.IsPublic);
                    paramCollection.AddWithValue("@AlertUsingTextMessage", model.AlertUsingTextMessage);
                    paramCollection.AddWithValue("@AlertUsingEmail", model.AlertUsingEmail);

                    SqlParameter p = new SqlParameter("@Id", System.Data.SqlDbType.Int);
                    p.Direction = System.Data.ParameterDirection.Output;

                    paramCollection.Add(p);

                }, returnParameters: delegate (SqlParameterCollection param)
                {
                    int.TryParse(param["@id"].Value.ToString(), out id);
                });
                return id;
            }
            catch (Exception ex)
            {
                _baseService.LogError(System.Reflection.MethodBase.GetCurrentMethod().Name, ex, "MemberProfileService");
                throw;
            }
        }

        public void Update(MemberProfileUpdateRequest model)
        {
            try
            {
                _baseService.DataProvider.ExecuteNonQuery(_baseService.GetConnection, "dbo.MemberProfile_UpdateByID", inputParamMapper: delegate (SqlParameterCollection paramCollection)
                {
                    paramCollection.AddWithValue("@Id", model.Id);
                    paramCollection.AddWithValue("@AspNetUserID", model.AspNetUserID);
                    paramCollection.AddWithValue("@FirstName", model.FirstName);
                    paramCollection.AddWithValue("@MiddleName", model.MiddleName);
                    paramCollection.AddWithValue("@LastName", model.LastName);
                    paramCollection.AddWithValue("@PhoneNumber", model.PhoneNumber);
                    paramCollection.AddWithValue("@Address1", model.Address1);
                    paramCollection.AddWithValue("@Address2", model.Address2);
                    paramCollection.AddWithValue("@City", model.City);
                    paramCollection.AddWithValue("@StateProvinceId", model.StateProvinceId);
                    paramCollection.AddWithValue("@Zip", model.Zip);
                    paramCollection.AddWithValue("@DateOfBirth", model.DateOfBirth);
                    paramCollection.AddWithValue("@Email", model.Email);
                    paramCollection.AddWithValue("@Gender", model.Gender);
                    paramCollection.AddWithValue("@IsActive", model.IsActive);
                    paramCollection.AddWithValue("@IsViewable", model.IsViewable);
                    paramCollection.AddWithValue("@IsGymOwner", model.IsGymOwner);
                    paramCollection.AddWithValue("@CrossfitLevelId", model.CrossFitLevelID);
                    paramCollection.AddWithValue("@IsPublic", model.IsPublic);
                    paramCollection.AddWithValue("@AlertUsingTextMessage", model.AlertUsingTextMessage);
                    paramCollection.AddWithValue("@AlertUsingEmail", model.AlertUsingEmail);
                });
            }
            catch (Exception ex)
            {
                _baseService.LogError(System.Reflection.MethodBase.GetCurrentMethod().Name, ex, "MemberProfileService");
                throw;
            }
        }

        public void Delete(int id)
        {
            try
            {
                _baseService.DataProvider.ExecuteNonQuery(_baseService.GetConnection, "dbo.MemberProfile_DeleteById", inputParamMapper: delegate (SqlParameterCollection paramCollection)
                {
                    paramCollection.AddWithValue("@Id", id);
                });
            }
            catch (Exception ex)
            {
                _baseService.LogError(System.Reflection.MethodBase.GetCurrentMethod().Name, ex, "MemberProfileService");
                throw;
            }
        }

        public List<MemberProfile> SelectAll()
        {
            try
            {
                List<MemberProfile> list = null;

                _baseService.DataProvider.ExecuteCmd(_baseService.GetConnection, "dbo.MemberProfile_SelectAll", null,
                map: delegate (IDataReader reader, short set)
                {
                    if (list == null)
                    {
                        list = new List<MemberProfile>();
                    }
                    list.Add(_baseService.MapToObject<MemberProfile>(reader));
                });
                return list;
            }
            catch (Exception ex)
            {
                _baseService.LogError(System.Reflection.MethodBase.GetCurrentMethod().Name, ex, "MemberProfileService");
                throw;
            }
        }

        public MemberProfileResponse SelectById(int id)
        {
            try
            {
                MemberProfileResponse _memberProfileResponse = new MemberProfileResponse();

                _baseService.DataProvider.ExecuteCmd(_baseService.GetConnection, "dbo.MemberProfile_SelectById",
                inputParamMapper: delegate (SqlParameterCollection paramCollection)
                {
                    paramCollection.AddWithValue("@Id", id);
                },
                map: delegate (IDataReader reader, short set)
                {
                    switch (set) {
                        case 0:
                            _memberProfileResponse.MemberProfileSet = _baseService.MapToObject<MemberProfile>(reader);
                            break;
                        case 1:
                            if (_memberProfileResponse.MemberBenchmarkList == null)
                            {
                                _memberProfileResponse.MemberBenchmarkList = new List<MemberBenchmark>();
                            }
                            _memberProfileResponse.MemberBenchmarkList.Add(_baseService.MapToObject<MemberBenchmark>(reader));

                            break;
                    }                   
                });
                return _memberProfileResponse;
            }
            catch (Exception ex)
            {
                _baseService.LogError(System.Reflection.MethodBase.GetCurrentMethod().Name, ex, "MemberProfileService");
                throw;
            }
        }

        public MemberProfile SelectByEmail(string email)
        {
            try
            {
                MemberProfile _memberProfile = null;

                _baseService.DataProvider.ExecuteCmd(_baseService.GetConnection, "dbo.MemberProfile_SelectByEmail",
                    inputParamMapper: delegate (SqlParameterCollection paramCollection)
                    {
                        paramCollection.AddWithValue("@Email", email);
                    },
                    map: delegate (IDataReader reader, short set)
                    {
                        if (_memberProfile == null)
                        {
                            _memberProfile =_baseService.MapToObject<MemberProfile>(reader);
                        }
                    }
                    );

                return _memberProfile;
            }
            catch (Exception ex)
            {
                _baseService.LogError(System.Reflection.MethodBase.GetCurrentMethod().Name, ex, "MemberProfileService");
                throw;
            }
        }

        public MemberProfile GetMemberProfileByAspNetUserId(string aspNetUserId)
        {
            try
            {
                MemberProfile _memberProfile = null;
                _baseService.DataProvider.ExecuteCmd(_baseService.GetConnection, "dbo.MemberProfile_SelectByAspNetUserId",
                    inputParamMapper: delegate (SqlParameterCollection paramCollection)
                    {
                        paramCollection.AddWithValue("@AspNetUserId", aspNetUserId);
                    },
                    map: delegate (IDataReader reader, short set)
                    {
                       _memberProfile = _baseService.MapToObject<MemberProfile>(reader);

                    });
                return _memberProfile;
            }
            catch (Exception ex)
            {
                _baseService.LogError(System.Reflection.MethodBase.GetCurrentMethod().Name, ex, "MemberProfileService");
                throw;
            }
        }

        public MemberProfile GetCurrentMemberProfile()
        {
            return GetMemberProfileByAspNetUserId(_userService.GetCurrentUserId());
        }

        public MemberProfile GetCurrentMemberProfilePublic()
        {
            MemberProfile mbr = GetMemberProfileByAspNetUserId(_userService.GetCurrentUserId());
            mbr.AspNetUserID = null;
            mbr.Id = 0;

            return mbr;
        }

        public void UpdateBio(int id, string bio)
        {
            try
            {
                _baseService.DataProvider.ExecuteNonQuery(_baseService.GetConnection, "dbo.MemberProfile_SaveBio", inputParamMapper: delegate (SqlParameterCollection paramCollection)
                {
                    paramCollection.AddWithValue("@Id", id);
                    paramCollection.AddWithValue("@Bio", bio);
                });
            }
            catch (Exception ex)
            {
                _baseService.LogError(System.Reflection.MethodBase.GetCurrentMethod().Name, ex, "MemberProfileService");
                throw;
            }
        }

        public void UpdateMemberProfileSettings(MemberProfileSettingsUpdateRequest model)
        {
            try
            {
                _baseService.DataProvider.ExecuteNonQuery(_baseService.GetConnection, "dbo.MemberProfile_UpdateProfileSettings", inputParamMapper: delegate (SqlParameterCollection paramCollection)
                {
                    paramCollection.AddWithValue("@Id", model.Id);
                    paramCollection.AddWithValue("@FirstName", model.FirstName);
                    paramCollection.AddWithValue("@MiddleName", model.MiddleName);
                    paramCollection.AddWithValue("@LastName", model.LastName);
                    paramCollection.AddWithValue("@PhoneNumber", model.PhoneNumber);
                    paramCollection.AddWithValue("@Address1", model.Address1);
                    paramCollection.AddWithValue("@City", model.City);
                    paramCollection.AddWithValue("@StateProvinceId", model.StateProvinceId);
                    paramCollection.AddWithValue("@Zip", model.Zip);
                    paramCollection.AddWithValue("@DateOfBirth", model.DateOfBirth);
                    paramCollection.AddWithValue("@Email", model.Email);
                    paramCollection.AddWithValue("@Gender", model.Gender);
                    paramCollection.AddWithValue("@Tagline", model.Tagline);
                    paramCollection.AddWithValue("@IsGymOwner", model.IsGymOwner);
                    paramCollection.AddWithValue("@AlertUsingTextMessage", model.AlertUsingTextMessage);
                    paramCollection.AddWithValue("@AlertUsingEmail", model.AlertUsingEmail);

                });

            }
          catch (Exception ex)
            {
                _baseService.LogError(System.Reflection.MethodBase.GetCurrentMethod().Name, ex, "MemberProfileService");
                throw;
            }
        }

        public void UpdatePersonalInterests(int id, string personalInterests)
        {
            try
            {
                _baseService.DataProvider.ExecuteNonQuery(_baseService.GetConnection, "dbo.MemberProfile_SavePersonalInterests", inputParamMapper: delegate (SqlParameterCollection paramCollection)
                {
                    paramCollection.AddWithValue("@Id", id);
                    paramCollection.AddWithValue("@PersonalInterests", personalInterests);
                });
            }
            catch (Exception ex)
            {
                _baseService.LogError(System.Reflection.MethodBase.GetCurrentMethod().Name, ex, "MemberProfileService");
                throw;
            }
        }

        public void UpdateCrossfitLevel(int id, int crossfitLevelId)
        {
            try
            {
                _baseService.DataProvider.ExecuteNonQuery(_baseService.GetConnection, "dbo.MemberProfile_SaveCrossfitLevel", inputParamMapper: delegate (SqlParameterCollection paramCollection)
                {
                    paramCollection.AddWithValue("@Id", id);
                    paramCollection.AddWithValue("@CrossfitLevelId", crossfitLevelId);
                });
            }
            catch (Exception ex)
            {
                _baseService.LogError(System.Reflection.MethodBase.GetCurrentMethod().Name, ex, "MemberProfileService");
                throw;
            }
        }

        public void UpdateIsOnline(string aspNetUserId, bool isOnline)
        {
            try
            {
                _baseService.DataProvider.ExecuteNonQuery(_baseService.GetConnection, "dbo.MemberProfile_UpdateIsOnline", inputParamMapper: delegate (SqlParameterCollection paramCollection)
                {
                    paramCollection.AddWithValue("@AspNetUserId", aspNetUserId);
                    paramCollection.AddWithValue("@IsOnline", isOnline);
                });
            }
            catch (Exception ex)
            {
                _baseService.LogError(System.Reflection.MethodBase.GetCurrentMethod().Name, ex, "MemberProfileService");
                throw;
            }
        }

        //Mylinh needs to add Notify proc here
        public MemberNotification NotifyMember(string aspNetUserId)
        {
            try
            {
                MemberNotification _memberNotification = null;
                _baseService.DataProvider.ExecuteCmd(_baseService.GetConnection, "dbo.MemberProfile_Notify",
                    inputParamMapper: delegate (SqlParameterCollection paramCollection)
                    {
                        paramCollection.AddWithValue("@AspNetUserId", aspNetUserId);
                    },
                    map: delegate (IDataReader reader, short set)
                    {
                        _memberNotification = _baseService.MapToObject<MemberNotification>(reader);

                    });
                return _memberNotification;
            }
            catch (Exception ex)
            {
                _baseService.LogError(System.Reflection.MethodBase.GetCurrentMethod().Name, ex, "MemberProfileService");
                throw;
            }
        }
        //contact us
        public List<ContactUsSendResponse> ContactUs()
        {
            try
            {
                List<ContactUsSendResponse> list = null;

                _baseService.DataProvider.ExecuteCmd(_baseService.GetConnection, "dbo.AspNetUsers_isAdminEmails", null
                , map: delegate (IDataReader reader, short set)
                {
                    if (list == null)
                    {
                        list = new List<ContactUsSendResponse>();
                    }
                    list.Add(_baseService.MapToObject<ContactUsSendResponse>(reader));
                });
                return list;
            }
            catch (Exception ex)
            {
                _baseService.LogError(System.Reflection.MethodBase.GetCurrentMethod().Name, ex, "MemberProfileService");
                throw;
            }
        }


        private MemberProfile MemberProfileMap(IDataReader reader)
        {
            try
            {
                int startingIndex = 0;
                MemberProfile _memberProfile = new MemberProfile();
                _memberProfile.Id = reader.GetSafeInt32(startingIndex++);
                _memberProfile.AspNetUserID = reader.GetSafeString(startingIndex++);
                _memberProfile.FirstName = reader.GetSafeString(startingIndex++);
                _memberProfile.MiddleName = reader.GetSafeString(startingIndex++);
                _memberProfile.LastName = reader.GetSafeString(startingIndex++);
                _memberProfile.PhoneNumber = reader.GetSafeString(startingIndex++);
                _memberProfile.Address1 = reader.GetSafeString(startingIndex++);
                _memberProfile.Address2 = reader.GetSafeString(startingIndex++);
                _memberProfile.City = reader.GetSafeString(startingIndex++);
                _memberProfile.StateProvinceId = reader.GetSafeInt32(startingIndex++);
                _memberProfile.Zip = reader.GetSafeString(startingIndex++);
                _memberProfile.DateOfBirth = reader.GetSafeDateTime(startingIndex++);
                _memberProfile.Email = reader.GetSafeString(startingIndex++);
                _memberProfile.Gender = reader.GetSafeString(startingIndex++);
                _memberProfile.IsActive = reader.GetSafeBool(startingIndex++);
                _memberProfile.IsViewable = reader.GetSafeBool(startingIndex++);
                _memberProfile.GymInfoId = reader.GetSafeInt32(startingIndex++);
                _memberProfile.IsGymOwner = reader.GetSafeBool(startingIndex++);
                _memberProfile.CrossfitLevelId = reader.GetSafeInt32(startingIndex++);
                _memberProfile.IsPublic = reader.GetSafeBool(startingIndex++);
                _memberProfile.LastLoginDate = reader.GetSafeDateTime(startingIndex++);
                _memberProfile.AlertUsingTextMessage = reader.GetSafeBool(startingIndex++);
                _memberProfile.AlertUsingEmail = reader.GetSafeBool(startingIndex++);
                _memberProfile.CreatedDate = reader.GetSafeDateTime(startingIndex++);
                _memberProfile.ModifiedDate = reader.GetSafeDateTime(startingIndex++);

                return _memberProfile;
            }
            catch (Exception ex)
            {
                _baseService.LogError(System.Reflection.MethodBase.GetCurrentMethod().Name, ex, "MemberProfileService");
                throw;
            }
        }

    }
}

/*
    @AspNetUserID nvarchar(128),
    @FirstName nvarchar(50),
    @MiddleName nvarchar(50) = null,
    @LastName nvarchar(50),
    @PhoneNumber nvarchar(50) = null,
    @Address1 nvarchar(100) = null,
    @Address2 nvarchar(100) = null,
    @City nvarchar(50)= null,
    @StateProvinceId int = null,
    @DateOfBirth datetime2(7),
    @Email nvarchar(50) = null,
    @Gender char(1),
    @IsActive bit = null,
    @IsViewable bit= null,
    @IsGymOwner bit= null,
    @CrossFitLevelID int= null,
    @IsPublic bit= null,
    [AlertUsingTextMessage]
	[AlertUsingEmail]
	@Id int output
*/
