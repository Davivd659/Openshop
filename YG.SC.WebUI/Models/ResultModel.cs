using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace YG.SC.WebUI.Models
{
    public class ResultModel
    {
        public ResultModel(bool isSuccess)
            : this(isSuccess, string.Empty)
        {
        }

        public ResultModel(bool isSuccess, string message)
            : this(isSuccess, message, null)
        {
        }

        public ResultModel(bool isSuccess, string message, object data)
        {
            this.IsSuccess = isSuccess;
            this.Message = message;
            this.Data = data;
        }

        public bool IsSuccess { get; private set; }
        public string Message { get; private set; }
        public object Data { get; set; }
    }
}