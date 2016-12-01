using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using YG.SC.Common;
using System;

namespace YG.SC.WeiXin
{

    public class SimpleResultModel
    {
        private SimpleResultModel(int status)
            : this(status, string.Empty)
        {

        }

        private SimpleResultModel(int status, string message)
        {
            this.Status = status;
            this.Message = message;
        }

        //public static SimpleResultModel Success()
        //{
        //    return new SimpleResultModel(0);
        //}

        public static SimpleResultModel Conclude(System.Enum status)
        {
            var enumItem = EnumExtenstion.GetEnumItem(status.GetType(), status);
            return new SimpleResultModel(enumItem.Value, enumItem.Text);
        }

        public int Status { get; set; }
        public string Message { get; set; }
    }

    public class IntResultModel : ResultModel<int>
    {
        private IntResultModel(int status, string message, int id)
            : base(status, message, id)
        {
        }

        public new static IntResultModel Conclude(Enum status)
        {
            return Conclude(status, 0);
        }

        public new static IntResultModel Conclude(Enum status, int result)
        {
            var enumItem = EnumExtenstion.GetEnumItem(status.GetType(), status);
            return new IntResultModel(enumItem.Value, enumItem.Text, result);
        }

    }

    public class ResultModel<TResult>
    {
        public ResultModel(int status, string message, TResult result)
        {
            this.Status = status;
            this.Message = message;
            this.Result = result;
        }

        public static ResultModel<TResult> Conclude(Enum status)
        {
            return Conclude(status, default(TResult));
        }

        public static ResultModel<TResult> Conclude(Enum status, TResult result)
        {
            var enumItem = EnumExtenstion.GetEnumItem(status.GetType(), status);
            return new ResultModel<TResult>(enumItem.Value, enumItem.Text, result);
        }

        public int Status { get; protected set; }
        public string Message { get; protected set; }
        public TResult Result { get; protected set; }
    }

}