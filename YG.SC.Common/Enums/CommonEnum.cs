using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Web;

namespace YG.SC.Common
{
    /// <summary>
    /// 枚举。
    /// </summary>
    public class CommonEnum
    {

        /// <summary>
        /// 数据库Object Type值。
        /// </summary>
        public enum TypeOfDbObject
        {
            index,
            [Description("分类")]
            Classification,
            [Description("分类报增")]
            ClassifiedReport,
            [Description("分类报增开店帮")]
            ClassifiedReportShop,
            [Description("分类报增回复")]
            ClassifiedReportReply,
            [Description("品名")]
            Product,
            [Description("品名报增")]
            ProductReport,
            [Description("品名报增开店帮")]
            ProductReportShop,
            [Description("品名报增回复")]
            ProductReportReply,
            [Description("商品")]
            Commodity,
            [Description("订单佣金比例")]
            Commission
        }

        /// <summary>
        /// 支付方式。
        /// </summary>
        public enum TypeOfPayment
        {
            /// <summary>
            /// 微信。
            /// </summary>
            [Description("微信")]
            WeiXin,

            /// <summary>
            /// 支付宝。
            /// </summary>
            [Description("支付宝")]
            ZhiFuBao,

            /// <summary>
            /// 线下。
            /// </summary>
            [Description("线下")]
            OffLine
        }

        /// <summary>
        /// 订单关联的业务数据类型。
        /// </summary>
        public enum BusinessDataType
        {
            /// <summary>
            /// 任务。
            /// </summary>
            [Description("任务")]
            Mission,

            /// <summary>
            /// 采买。
            /// </summary>
            [Description("采买")]
            Cart
        }

        /// <summary>
        /// 订单状态。
        /// </summary>
        public enum OrderStatus
        {
            /// <summary>
            /// 任务。
            /// </summary>
            [Description("任务")]
            Mission,

            /// <summary>
            /// 采买。
            /// </summary>
            [Description("采买")]
            Cart
        }

        /// <summary>
        /// 消息类型。
        /// </summary>
        public enum MessageType
        {
            /// <summary>
            /// 任务。
            /// </summary>
            [Description("任务")]
            Mission,

            /// <summary>
            /// 采买。
            /// </summary>
            [Description("采买")]
            Cart
        }

        /// <summary>
        /// 订单类型。
        /// </summary>
        public enum OrderType
        {
            /// <summary>
            /// 任务。
            /// </summary>
            [Description("任务")]
            Mission,

            /// <summary>
            /// 采买。
            /// </summary>
            [Description("采买")]
            Cart
        }

        /// <summary>
        /// 资金流向。
        /// </summary>
        public enum FundFlow
        {
            /// <summary>
            /// 收入。
            /// </summary>
            [Description("收入")]
            Income,

            /// <summary>
            /// 支出。
            /// </summary>
            [Description("支出")]
            Spend
        }

        /// <summary>
        /// 品牌申请状态。
        /// </summary>
        public enum StatusOfBrandApply
        {
            /// <summary>
            /// 申请中。
            /// </summary>
            [Description("申请中")]
            Applying,

            /// <summary>
            /// 通过。
            /// </summary>
            [Description("通过")]
            Pass,

            /// <summary>
            /// 拒绝。
            /// </summary>
            [Description("拒绝")]
            Reject
        }

        /// <summary>
        /// 消息来源。
        /// </summary>
        public enum TypeOfMessage
        {
            /// <summary>
            /// 发布任务。
            /// </summary>
            [Description("发布任务")]
            Mission,

            /// <summary>
            /// 采买商品。
            /// </summary>
            [Description("采买商品")]
            Order
        }

        /// <summary>
        /// 任务周期状态。
        /// </summary>
        public enum StatusOfPeriod
        {
            /// <summary>
            /// 待执行。
            /// </summary>
            [Description("待执行")]
            New,

            /// <summary>
            /// 进行中。
            /// </summary>
            [Description("进行中")]
            Run,

            /// <summary>
            /// 验收中。
            /// </summary>
            [Description("验收中")]
            Check,

            /// <summary>
            /// 验收不合格。
            /// </summary>
            [Description("验收不合格")]
            Failed,

            /// <summary>
            /// 付款中。
            /// </summary>
            [Description("付款中")]
            Payment,

            /// <summary>
            /// 已完成。
            /// </summary>
            [Description("已完成")]
            Finish
        }

        /// <summary>
        /// 任务状态。
        /// </summary>
        public enum StatusOfMission
        {
            /// <summary>
            /// 已取消。
            /// </summary>
            [Description("已取消")]
            Cancel,

            /// <summary>
            /// 已发布。
            /// </summary>
            [Description("已发布")]
            Issue,

            /// <summary>
            /// 已接洽。
            /// </summary>
            [Description("已接洽")]
            Connecting,

            /// <summary>
            /// 已上传合同。
            /// </summary>
            [Description("已上传合同")]
            Contract,

            /// <summary>
            /// 已确认合同。
            /// </summary>
            [Description("已确认合同")]
            ConfirmContract,

            /// <summary>
            /// 已确认支付。
            /// </summary>
            [Description("已确认支付")]
            ConfirmPayment,

            /// <summary>
            /// 已竣工。
            /// </summary>
            [Description("已竣工")]
            Complete,

            /// <summary>
            /// 已申诉。
            /// </summary>
            [Description("已申诉")]
            Appeal,

            /// <summary>
            /// 已裁决。
            /// </summary>
            [Description("已裁决")]
            Decide,

            /// <summary>
            /// 已结束。
            /// </summary>
            [Description("已结束")]
            End
        }

        /// <summary>
        /// 用户分组。
        /// </summary>
        public enum GroupOfCustomer
        {
            /// <summary>
            /// 游客。
            /// </summary>
            [Description("游客")]
            Vistor,

            /// <summary>
            /// 会员。
            /// </summary>
            [Description("会员")]
            Member,

            /// <summary>
            /// 商家。
            /// </summary>
            [Description("商家")]
            OpenShop,

            /// <summary>
            /// BD。
            /// </summary>
            [Description("BD")]
            BD,

            /// <summary>
            /// 管理员。
            /// </summary>
            [Description("管理员")]
            Admin
        }
        /// <summary>
        /// 团购状态
        /// </summary>
        public enum ProjectStatus
        {
            /// <summary>
            /// 进行中。
            /// </summary>
            [Description("进行中")]
            Conduct,

            /// <summary>
            /// 已结束。
            /// </summary>
            [Description("已结束")]
            End,

            /// <summary>
            /// 已取消。
            /// </summary>
            [Description("已取消")]
            Cancelled,
        }
        public enum ProjectProgress
        {
            /// <summary>
            /// 第一步。
            /// </summary>
            [Description("第一步")]
            one,

            /// <summary>
            /// 第二步。
            /// </summary>
            [Description("第二步")]
            two,

            /// <summary>
            /// 第三步。
            /// </summary>
            [Description("第三步")]
            Three,
            /// <summary>
            /// 第四步。
            /// </summary>
            [Description("第四步")]
            Four,
            /// <summary>
            /// 第五步。
            /// </summary>
            [Description("第五步")]
            Five,
        }
        public enum ProjectPhotoType
        {
            /// <summary>
            /// 沙盘全景。
            /// </summary>
            [Description("沙盘全景")]
            Panoramic,

            /// <summary>
            /// 户型图。
            /// </summary>
            [Description("户型图")]
            Apartment,

            /// <summary>
            /// 交通图。
            /// </summary>
            [Description("交通图")]
            Traffic,
            /// <summary>
            /// 外景图。
            /// </summary>
            [Description("外景图")]
            Location,
            /// <summary>
            /// 实景图。
            /// </summary>
            [Description("实景图")]
            Real,
            /// <summary>
            /// 效果图。
            /// </summary>
            [Description("效果图")]
            Effect,
            /// <summary>
            /// 配套图。
            /// </summary>
            [Description("配套图")]
            Supporting,
        }
        #region 枚举工具方法。

        /// <summary>
        /// 获取枚举项描述。
        /// </summary>
        /// <param name="enumValue">枚举类子项</param>        
        public static string GetDescription(object enumValue)
        {
            enumValue = (Enum)enumValue;
            string strValue = enumValue.ToString();
            FieldInfo fieldinfo = enumValue.GetType().GetField(strValue);
            if (fieldinfo == null)
            {
                throw new Exception("CommonEnum::GetEnumDescription。该枚举值没有。");
            }
            Object[] objs = fieldinfo.GetCustomAttributes(typeof(DescriptionAttribute), false);
            if (objs == null || objs.Length == 0)
            {
                throw new Exception("CommonEnum::GetEnumDescription。该枚举值没有描述。");
            }
            DescriptionAttribute da = (DescriptionAttribute)objs[0];
            return da.Description;
        }

        /// <summary>
        /// 从枚举类型和它的特性读出并返回一个键值对
        /// </summary>
        /// <param name="enumType">Type,该参数的格式为typeof(需要读的枚举类型)</param>
        /// <returns>键值对</returns>
        public static Dictionary<int, string> GetDictionaryFromEnum(Type enumType)
        {
            Dictionary<int, string> result = new Dictionary<int, string>();
            Type typeDescription = typeof(DescriptionAttribute);
            System.Reflection.FieldInfo[] fields = enumType.GetFields();
            foreach (FieldInfo field in fields)
            {
                if (field.FieldType.IsEnum == true)
                {
                    string text = string.Empty;
                    object[] arr = field.GetCustomAttributes(typeDescription, true);
                    if (arr.Length > 0)
                    {
                        DescriptionAttribute aa = (DescriptionAttribute)arr[0];
                        text = aa.Description;
                    }
                    else
                    {
                        text = field.Name;
                    }
                    result.Add((int)enumType.InvokeMember(field.Name, BindingFlags.GetField, null, null, null), text);
                }
            }
            return result;
        }

        #endregion

    }
}