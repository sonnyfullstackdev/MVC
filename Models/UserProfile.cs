using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ProjectName.Models.Requests.Member
{
    public class UserProfileAddRequest
    {
        [Required]
        [StringLength(128, ErrorMessage = "AspNetUserID can not be longer than 128 characters.")]
        public string AspNetUserID { get; set; }

        [Required]
        [StringLength(50, ErrorMessage = "First Name can not be longer than 50 characters.")]
        public string FirstName { get; set; }

        [StringLength(50, ErrorMessage = "Middle Name can not be longer than 50 characters.")]
        public string MiddleName { get; set; }

        [Required]
        [StringLength(50, ErrorMessage = "Last Name can not be longer than 50 characters.")]
        public string LastName { get; set; }

        [Phone]
        [StringLength(50, ErrorMessage = "Phone can not be longer than 50 characters.")]
        public string PhoneNumber { get; set; }

        [StringLength(100, ErrorMessage = "Address1 can not be longer than 100 characters.")]
        public string Address1 { get; set; }

        [StringLength(100, ErrorMessage = "Address2 can not be longer than 100 characters.")]
        public string Address2 { get; set; }

        [StringLength(50, ErrorMessage = "City can not be longer than 50 characters.")]
        public string City { get; set; }

        public int StateProvinceId { get; set; }

        public int Zip { get; set; }

        [Required]
        [DataType(DataType.Date, ErrorMessage = "Please enter a valid date (ex: 2/14/2011)")]
        public DateTime DateOfBirth { get; set; }

        [Required]
        [EmailAddress]
        [StringLength(50, ErrorMessage = "Email Address can not be longer than 50 characters.")]
        public string Email { get; set; }

        [Required]
        [StringLength(1, ErrorMessage = "Gender can not be longer than 1 characters.")]

        public string Gender { get; set; }

        public bool IsActive { get; set; }

        public bool IsViewable { get; set; }

        public bool IsGymOwner { get; set; }

        public int CrossFitLevelID { get; set; }

        public bool IsPublic { get; set; }

        public string Tagline { get; set; }

        public string Bio { get; set; }

        public string PersonalInterests { get; set; }

        public DateTime LastLoginDate { get; set; }

        public bool AlertUsingTextMessage { get; set; }

        public bool AlertUsingEmail { get; set; }
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
	@Id int output
*/
