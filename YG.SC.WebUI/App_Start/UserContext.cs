using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using YG.SC.Service;
using YG.SC.Service.IService;
namespace YG.SC.WebUI
{
    public class UserContext
    {
        private static readonly UserContext Empty = new UserContext();

        private const string CurrentUserContextCacheKey = "UserContext";

        public static UserContext Current
        {
            get
            {
                if (HttpContext.Current.Items[CurrentUserContextCacheKey] == null)
                {
                    var user = HttpContext.Current.User.Identity;
                    var bllAccount = DependencyResolver.Current.GetService<ICustomerGroupOnService>();
                    var account = bllAccount.GetEntityByName(user.Name);
                    if (account == null)
                    {
                        return UserContext.Empty;
                    }
                    var userContext = new UserContext
                    {
                        Id = account.Id,
                        Name = account.NAME
                    };

                    HttpContext.Current.Items[CurrentUserContextCacheKey] = userContext;
                }
                return HttpContext.Current.Items[CurrentUserContextCacheKey] as UserContext;
            }
        }

        public bool HasAuthority(string authorityName)
        {
            return true;
            if (this == UserContext.Empty)
            {
                return false;
            }
            //var bllAccount = DependencyResolver.Current.GetService<IAuthorityService>();
            //return bllAccount.HasAuthority(UserContext.Current.Id, authorityName);
        }

        public int Id { get; set; }
        public string Name { get; set; }

    }
}