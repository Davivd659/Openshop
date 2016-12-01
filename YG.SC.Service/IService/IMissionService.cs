using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YG.SC.DataAccess;
using YG.SC.Model;

namespace YG.SC.Service
{
    /// <summary>
    /// 任务。
    /// </summary>
    public interface IMissionService : IYgService
    {
        void Create(M_Mission mission);

        void Edit(M_Mission mission);

        M_Mission GetById(int id);
        Tuple<M_Mission[], PagerEntity> GetEntitsList(MissionSearchCriteria criteria);

		Dictionary<int, string> GetMissionTypeList();

        Dictionary<int, string> GetMissionStatusList();

        M_Period GetPeriodById(int id);

        void CreatePeriod(M_Period p);

        int GetPeriodNumber(int missionId);

        void UpdatePeriod(M_Period p);

        List<M_Period> GetPeriodList(int missionId);

        M_Contract GetContractByMission(int p);

        void CreateContract(int missionId, string fileName);

        bool ExistContract(int missionId, out C_File fDb);

        void DeletePeriod(int id);

		void Issue(int id, Customer publisher);

		void ConfirmContract(int id);

		void ConfirmPayment(int id);

		void Check(int id);

		void Pass(int id);

		void Failed(int id, string failedReason);

		void Appeal(int id, string appealReason);

		void ConfirmPaymentPeriod(int id);

		void Connecting(int id, Customer customer);

		void Contract(int p);

		List<M_Mission> GetMissionListTopN(int p);

		void Cancel(int id);

		Dictionary<int, string> GetPaymentTypeList();

		int CountOfPendingMission(Customer customer);
	}
}
