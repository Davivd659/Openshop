using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YG.SC.DataAccess;
using YG.SC.Model;
namespace YG.SC.Service.IService
{
	public interface IOrderService : IYgService
	{
		void Insert(O_Order order);
		void Updata(O_Order order);
		O_Order GetByID(int id);
		Tuple<O_Order[], PagerEntity> GetSeachOrder(OrderSearchCriteria order);

		Dictionary<int, string> GetFundFlowList();

		Dictionary<int, string> GetOrderTypeList();

		Tuple<OrderSearchResult[], PagerEntity> GetOrderList(OrderSearchFilter filter);

		List<OrderSearchResult> GetOrderTopN(Customer c, int n = 5);
	}
}
