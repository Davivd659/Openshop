using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YG.SC.WebUI.Authentication
{
    public interface IAuthenticationService
    {
        void SignIn(string userId, bool createPersistentCookie);
        void SignOut();
    }
}
