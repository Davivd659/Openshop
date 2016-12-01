using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace YG.SC.OpenShop.Authentication
{
    public interface IAuthenticationService
    {
        void SignIn(string userId, bool createPersistentCookie);
        void SignOut();
    }
}