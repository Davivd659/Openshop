using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YG.SC.DataAccess;
using YG.SC.Model;

namespace YG.SC.Service
{
    public interface IMessageService : IYgService
    {
        Tuple<DataAccess.S_Message[], PagerEntity> GetEntitsList(MessageSearchCriteria filter);

        Dictionary<int, string> GetSourceList();

		void Send(DataAccess.S_Message msg);

		/// <summary>
		/// 获取最新5条消息。
		/// </summary>
		/// <param name="user"></param>
		/// <param name="n"></param>
		/// <returns></returns>
		S_Message[] GetTopN(Customer user, int n = 5);
	}
}
