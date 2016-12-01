using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace YG.SC.WeiXin.Authentication
{
    public interface IAuthenticationService
    {
        void SignIn(string userId, bool createPersistentCookie);
        void SignOut();
    }
}