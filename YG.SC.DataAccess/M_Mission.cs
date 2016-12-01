
//------------------------------------------------------------------------------
// <auto-generated>
//     此代码已从模板生成。
//
//     手动更改此文件可能导致应用程序出现意外的行为。
//     如果重新生成代码，将覆盖对此文件的手动更改。
// </auto-generated>
//------------------------------------------------------------------------------


namespace YG.SC.DataAccess
{

using System;
    using System.Collections.Generic;
    
public partial class M_Mission
{

    public M_Mission()
    {

        this.PeriodList = new HashSet<M_Period>();

    }


    public int Id { get; set; }

    public int MissionType { get; set; }

    public string Title { get; set; }

    public int Publisher { get; set; }

    public string PublisherContact { get; set; }

    public System.DateTime PublisheTime { get; set; }

    public int Receiver { get; set; }

    public Nullable<System.DateTime> ReceiveTime { get; set; }

    public int Bd { get; set; }

    public decimal TotalPrice { get; set; }

    public Nullable<decimal> Rate { get; set; }

    public int Status { get; set; }

    public bool IsDelete { get; set; }

    public System.DateTime LastTime { get; set; }

    public string Description { get; set; }

    public string DecideResult { get; set; }

    public Nullable<decimal> DecideRefund { get; set; }

    public Nullable<decimal> DecidePay { get; set; }

    public string CloseReason { get; set; }

    public Nullable<System.DateTime> ConnectingTime { get; set; }

    public Nullable<System.DateTime> ConfirmContractTime { get; set; }

    public Nullable<System.DateTime> ConfirmPaymentTime { get; set; }

    public Nullable<System.DateTime> CompleteTime { get; set; }

    public Nullable<System.DateTime> DecideTime { get; set; }

    public Nullable<System.DateTime> AppealTime { get; set; }

    public bool IsOffLinePayment { get; set; }

    public System.DateTime LimitDate { get; set; }

    public string AppealReason { get; set; }

    public string PublisherMobile { get; set; }

    public string ReceiverContact { get; set; }

    public string ReceiverMobile { get; set; }

    public Nullable<System.DateTime> ContactTime { get; set; }



    public virtual Customer FkBd { get; set; }

    public virtual Customer FkPublisher { get; set; }

    public virtual Customer FkReceiver { get; set; }

    public virtual ICollection<M_Period> PeriodList { get; set; }

    public virtual ShopAttributeValues FkMissionType { get; set; }

}

}
