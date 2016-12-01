using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YG.SC.Common
{
    public class SendMessageHelper
    {
        /*http://www.winic.org/Development.asp 
         * message api 
         * 账户 rjzh
         * 密码 a12345
         */
        public const string account = "rjzh";
        public const string password = "a12345";
        public string SendMessage(string Mobile, string Message)
        {
            ServiceSendMessage.Service1SoapClient client = new ServiceSendMessage.Service1SoapClient();

            string t = DateTime.Now.Ticks.ToString();

            string sends = client.SendMessages(account, password, Mobile, Message, t);

            return sends;
        }
    }
}
