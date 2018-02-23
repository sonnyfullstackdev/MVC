
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProjectName.Controllers
{
    public class BaseController : Controller
    {

        [Microsoft.Practices.Unity.Dependency]
        public Services.Interfaces.IUserService UserService { get; set; }
        [Microsoft.Practices.Unity.Dependency]
        public Services.Interfaces.IMemberProfileService MemberProfileService { get; set; }
        [Microsoft.Practices.Unity.Dependency]
        public Services.Interfaces.IMemberNotificationBadgeService MemberNotificationBadgeService { get; set; }

        protected T GetViewModel<T>() where T : BaseViewModel, new()
        {
            T model = new T();

            //customize base view model here
            model.IsLoggedIn = this.UserService.IsLoggedIn();
            if (model.IsLoggedIn) {
                MemberProfile mbr = this.MemberProfileService.GetCurrentMemberProfile();
                model.LoggedInUser = mbr.DisplayName;
                List<NotificationBadge> Badges = MemberNotificationBadgeService.SelectByAspNetUserId(mbr.AspNetUserID);
                if (Badges != null)
                {
                    foreach (NotificationBadge itm in Badges)
                    {
                        switch (itm.NotificationBadgeId)
                        {
                            case (int)NotificationBadgeType.Matches:
                                model.MatchesBadge = itm.Counter;
                                break;
                            case (int)NotificationBadgeType.Messages:
                                model.MessagesBadge = itm.Counter;
                                break;
                        }
                    }
                }
                model.ActiveUsers = (int)HttpContext.Application["OnlineUsers"];
                TimeSpan span = DateTime.UtcNow - mbr.LastLoginDate;
                double totalMinutes = span.TotalMinutes;
                if ((mbr.IsOnline == false) || (span.TotalMinutes > 10))
                {
                    this.MemberProfileService.UpdateIsOnline(mbr.AspNetUserID, true);
                }

            }
            
            return model;
        }

        protected new ViewResult View()
        {
            BaseViewModel model = GetViewModel<BaseViewModel>();
            return base.View(model);
        }

        protected new ViewResult View(string viewName)
        {
            BaseViewModel model = GetViewModel<BaseViewModel>();
            return base.View(viewName, model);
        }
        

    }
}