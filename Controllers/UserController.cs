
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProjectName.Controllers.Member
{
    public class UserController : BaseController
    {
        IUserService _userService;
        IMemberProfileService _memberProfileService;
        IMemberLikeService _memberLikeService;
        IMemberMatchingConfigService _memberMatchingConfigService;
        IMemberNotificationBadgeService _memberNotificationBadgeService;
        IAdminService _adminService;

        public MemberController(IUserService userService, IMemberLikeService memberLikeService, IMemberMatchingConfigService memberMatchingConfigService, 
            IMemberProfileService memberProfileService, IMemberNotificationBadgeService memberNotificationBadgeService, IAdminService adminService)
        {
            _userService = userService;
            _memberLikeService = memberLikeService;
            _memberMatchingConfigService = memberMatchingConfigService;
            _memberProfileService = memberProfileService;
            _memberNotificationBadgeService = memberNotificationBadgeService;
            _adminService = adminService;
             
        }
        //Redirects USER to splashpage if they're not logged in
        protected void ValidateUserStatus()
        {
            if(!_userService.IsLoggedIn())
            {
                Response.Redirect("/Home");
            }
            //if(_adminService.IsUserBanned(_userService.GetCurrentUser().Email))
            //{
            //    Response.Redirect("/Home");
            //}
        }

        // GET: Member
        public ActionResult Index()
        {
            return View();
        }

        [Route("Member/ViewPotentials/{onlyLikes}")]
        [Route("Member/ViewPotentials")]
        public ActionResult ViewPotentials(bool onlyLikes = false)
        {
            ValidateUserStatus();
            ViewPotentialsModel bpm = GetViewModel<ViewPotentialsModel>();
            bpm.OnlyShowLikes = onlyLikes;
            return View(bpm);
        }

        public ActionResult InterestPage()
        {
            ValidateUserStatus();
            MemberProfile mp = _memberProfileService.GetCurrentMemberProfile();
            bool hasConfig = (_memberMatchingConfigService.SelectById(mp.Id) != null);
            if (!hasConfig)
            {
                return View();
            }
            else
            {
                return RedirectToAction("ViewPotentials", "Member");
            }
        }
        public ActionResult SplashPage()
        {
            return View();
        }
        public ActionResult LoginPage()
        {
            return View();
        } 

        public ActionResult MyMatches()
        {
            ValidateUserStatus();
            _memberNotificationBadgeService.Reset(_userService.GetCurrentUserId(), (int)NotificationBadgeType.Matches);
            return View();
        }

        [Route("Member/MemberProfile/{aspNetUserId}")]
        [Route("Member/MemberProfile")]
        public ActionResult MemberProfile(string aspNetUserId = null)
        {
            ValidateUserStatus();
            BaseViewModel bvm = GetViewModel<BaseViewModel>();
            if (string.IsNullOrEmpty(aspNetUserId))
            {
                bvm.IsCurrentUser = true;
                aspNetUserId = _userService.GetCurrentUserId();
            }
            else
            {
                string MyAspNetUserId = _userService.GetCurrentUserId();
                string TheirAspNetUserId = aspNetUserId;
                bool isMatched = _memberLikeService.IsMatched(MyAspNetUserId, TheirAspNetUserId);

                string AspNetUserId = _userService.GetCurrentUserId();
                bool isAdmin = _adminService.IsAdmin(AspNetUserId);

                if (!isMatched && !isAdmin) {
                    return RedirectToAction("MemberProfile", "Member");
                }
            }
            bvm.AspNetUserId = aspNetUserId;

            return View(bvm);
        }

        public ActionResult Photos()
        {
            ValidateUserStatus();
            return View();
        }

        public ActionResult LayoutChecker()
        {
            ValidateUserStatus();
            return View();
        }

        public ActionResult MySettings()
        {
            ValidateUserStatus();
            return View();
        }

        [Route("Member/Messages/{aspNetUserIdSentMessage}")]
        [Route("Member/Messages")]
        public ActionResult Messages(string aspNetUserIdSentMessage = null)
        {
            ValidateUserStatus();
            _memberNotificationBadgeService.Reset(_userService.GetCurrentUserId(), (int)NotificationBadgeType.Messages);
            MessagesModel mm = GetViewModel<MessagesModel>();
            mm.AspNetUserId = aspNetUserIdSentMessage; 
            return View(mm);
        }

        public ActionResult Wall()
        {
            ValidateUserStatus();
            return View();
        }
        public ActionResult EmailConfirmedPage()
        {
            return View();
        }
        [Route("Member/fooddiarypage/{aspNetUserId}")]
        [Route("Member/fooddiarypage")]
        public ActionResult FoodDiaryPage(string aspNetUserId = null)
        {
            ValidateUserStatus();
            BaseViewModel bvm = GetViewModel<BaseViewModel>();
            if (string.IsNullOrEmpty(aspNetUserId))
            {
                bvm.IsCurrentUser = true;
                aspNetUserId = _userService.GetCurrentUserId();
            }
            else
            {
                string MyAspNetUserId = _userService.GetCurrentUserId();
                string TheirAspNetUserId = aspNetUserId;
                bool isMatched = _memberLikeService.IsMatched(MyAspNetUserId, TheirAspNetUserId);

                string AspNetUserId = _userService.GetCurrentUserId();
                bool isAdmin = _adminService.IsAdmin(AspNetUserId);

                if (!isMatched && !isAdmin)
                {
                    return RedirectToAction("FoodDiarypage", "Member");
                }
            }
            MemberProfile mp = _memberProfileService.GetMemberProfileByAspNetUserId(aspNetUserId);
            bvm.AspNetUserId = aspNetUserId;
            bvm.DisplayName = mp.DisplayName;
            bvm.ProfileImage = mp.FileName;
            return View(bvm);
        }

        public ActionResult Modal()
        {
            return View();
        }

        [Route("Member/ResetPassword/{tokenGuid:guid}")]
        public ActionResult ResetPassword(Guid tokenGuid)
        {
            return View(new TokenGuid
            {
                VarTokenGuid = tokenGuid
            });
        }

        public ActionResult FileUploadTest()
        {
            ValidateUserStatus();
            return View();
        }

        [Route("Member/MyWorkout/{aspNetUserId}")]
        [Route("Member/MyWorkout")]
        public ActionResult MyWorkout(string aspNetUserId = null)
        {
            ValidateUserStatus();
            BaseViewModel bvm = GetViewModel<BaseViewModel>(); 
            if(string.IsNullOrEmpty(aspNetUserId))
            {
                bvm.IsCurrentUser = true;
                aspNetUserId = _userService.GetCurrentUserId(); 
            }
            else
            {
                string MyAspNetUserId = _userService.GetCurrentUserId();
                string TheirAspNetUserId = aspNetUserId;
                bool isMatched = _memberLikeService.IsMatched(MyAspNetUserId, TheirAspNetUserId);

                string AspNetUserId = _userService.GetCurrentUserId();
                bool isAdmin = _adminService.IsAdmin(AspNetUserId);

                if (!isMatched && !isAdmin)
                {
                    return RedirectToAction("MyWorkout", "Member");
                }
            }
            MemberProfile mp = _memberProfileService.GetMemberProfileByAspNetUserId(aspNetUserId);
            bvm.AspNetUserId = aspNetUserId;
            bvm.DisplayName = mp.DisplayName;
            bvm.ProfileImage = mp.FileName;
            return View(bvm);
        }

        public ActionResult GymMap()
        {
            ValidateUserStatus();
            return View();
        }

        public ActionResult AddGymEvent()
        {
            ValidateUserStatus();
            return View();
        }

        public ActionResult ViewGymEvents()
        {
            ValidateUserStatus();
            return View();
        }

        public ActionResult AboutUs()
        {
            return View();
        }
        public ActionResult ContactUs()
        {
            return View();
        }
        public ActionResult FAQs()
        {
            return View();
        }
    }

    public class TokenGuid
    {
        public Guid VarTokenGuid { get; set; }
    }

   
}