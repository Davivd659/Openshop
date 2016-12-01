using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using YG.SC.Service;
using YG.SC.DataAccess;
using YG.SC.Service.IService;
namespace YG.SC.Weixin
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
                    var CustomerService = DependencyResolver.Current.GetService<ICustomerService>();
                    var account = CustomerService.GetEntityByName(user.Name);
                    if (account == null)
                    {
                        return UserContext.Empty;
                    }

                    var userContext = new UserContext
                    {
                        Id = account.Id,
                        Name = account.LoginName,
                        Mobile = account.Mobile
                    };

                    //if (userContext.AccountType == AccountType.Gallery)
                    //{
                    //    var gallery = DependencyResolver.Current.GetService<IGalleryBussinessLogic>().GetByAccountId(userContext.Id);
                    //    userContext.Gallery = gallery;
                    //}

                    HttpContext.Current.Items[CurrentUserContextCacheKey] = userContext;
                }
                return HttpContext.Current.Items[CurrentUserContextCacheKey] as UserContext;
            }
        }

        public int Id { get; set; }
        public string Mobile { get; set; }
        public string Name { get; set; }     
    }
}