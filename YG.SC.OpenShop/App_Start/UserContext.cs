using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using YG.SC.Service;
using YG.SC.DataAccess;
using YG.SC.Service.IService;
using YG.SC.OpenShop.Helper;
namespace YG.SC.OpenShop
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
                        Mobile = account.Mobile,
                        Groupid=account.GroupId == null ? 0 : account.GroupId.Value,
                        Companyid = account.Companyid == null ? 0 : account.Companyid
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

        //public bool HasAuthority(string authorityName)
        //{
        //    if (this == UserContext.Empty)
        //    {
        //        return false;
        //    }
        //    var bllAccount = DependencyResolver.Current.GetService<IAccountBussinessLogic>();
        //    return bllAccount.HasAuthority(UserContext.Current.Id, authorityName);
        //}

        public int Id { get; set; }
        public string Mobile { get; set; }
        public string Name { get; set; }
        public int Companyid { get; set; }
        public int Groupid { get; set; }
    }
}