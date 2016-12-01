
namespace YG.SC.Common
{
    using log4net.Config;
    using System;
    using System.Collections;
    using System.IO;
    using System.Text;
    using System.Threading.Tasks;

    /// <summary>
    /// 类名称：Log4Utility
    /// 命名空间：YG.SC.Common
    /// 类功能：
    /// </summary>
    /// 创建者：孟祺宙
    /// 创建日期：2014/9/12 15:20
    /// 修改者：
    /// 修改时间：
    /// ----------------------------------------------------------------------------------------
    public class Log4Utility
    {
        public static void Register(string pstPath)
        {
            if (Log4.ILog4 != null) return;

            var filePath = string.Format("{0}/{1}", AppDomain.CurrentDomain.BaseDirectory.TrimEnd('\\', '/'), pstPath.TrimStart('\\', '/'));
            XmlConfigurator.ConfigureAndWatch(new FileInfo(filePath));
            Log4.ILog4 = new LogWriter(log4net.LogManager.GetLogger("Info"), log4net.LogManager.GetLogger("Error"));
        }
    }
    internal interface ILog4
    {
        void LogSuccessAsyn(string pstCls, string pstMethodNm, object[] pobPara, object pobRet);
        void LogExceptionAsyn(string pstCls, string pstMethodNm, object[] pobPara, Exception pobEx);
    }
    public static class Log4
    {
        internal static ILog4 ILog4 { get; set; }
        public static void LogSuccess(string pstCls, string pstMethodNm, object[] pobPara, object pobRet)
        {
            ILog4.LogSuccessAsyn(pstCls, pstMethodNm, pobPara, pobRet);
        }
        public static void LogException(string pstCls, string pstMethodNm, object[] pobPara, Exception pobEx)
        {
            ILog4.LogExceptionAsyn(pstCls, pstMethodNm, pobPara, pobEx);
        }
    }
    internal class LogWriter : ILog4
    {
        private readonly log4net.ILog fobLogInfo;
        private readonly log4net.ILog fobLogError;
        public LogWriter(log4net.ILog pobLogInfo, log4net.ILog pobLogError)
        {
            this.fobLogInfo = pobLogInfo;
            this.fobLogError = pobLogError;
        }
        public void LogSuccessAsyn(string pstCls, string pstMethodNm, object[] pobPara, object pobRet)
        {
            // ThreadPool.QueueUserWorkItem(Wrap(delegate { LogSuccess(pstCls, pstMethodNm, pobPara, pobRet); }));
            Task.Factory.StartNew(() => LogSuccess(pstCls, pstMethodNm, pobPara, pobRet));
        }
        public void LogExceptionAsyn(string pstCls, string pstMethodNm, object[] pobPara, Exception pobEx)
        {
            //ThreadPool.QueueUserWorkItem(Wrap(delegate { LogException(pstCls, pstMethodNm, pobPara, pobEx); }));
            Task.Factory.StartNew(() => LogException(pstCls, pstMethodNm, pobPara, pobEx));
        }
        private void LogSuccess(string pstCls, string pstMethodNm, object[] pobPara, object pobRet)
        {
            var info = new StringBuilder();

            info.Append("========================================================================\r\n");
            info.AppendFormat("Start at: {0}\r\n", DateTime.Now);
            info.Append("\r\n--------Traget & Properties--------\r\n");
            info.AppendFormat("CalledObject: {0}\r\n", pstCls);
            info.AppendFormat("\r\n--------Method & Parameters--------\r\n");
            info.AppendFormat("CalledMethod: {0}\r\n", pstMethodNm);
            info.Append(GetParamtersStr(pobPara));
            info.AppendFormat("\r\n--------Results & Parameters--------\r\n");
            info.AppendFormat("{0}\r\n", pobRet == null ? "Return: NULL" : ResultFormat(pobRet));
            info.AppendFormat("\r\nEnd at: {0}", DateTime.Now);
            info.Append("***Successful***\r\n\r\n\r\n");

            fobLogInfo.Info(info);
        }
        private void LogException(string pstCls, string pstMethodNm, object[] pobPara, Exception pobEx)
        {
            var error = new StringBuilder();

            error.Append("\r\n========================================================================\r\n");
            error.AppendFormat("Start at: {0}\r\n", DateTime.Now);
            error.Append("\r\n--------Traget & Properties--------\r\n");
            error.AppendFormat("CalledObject: {0}\r\n", pstCls);
            error.AppendFormat("\r\n--------Method & Parameters--------\r\n");
            error.AppendFormat("CalledMethod: {0}()\r\n", pstMethodNm);
            error.Append(GetParamtersStr(pobPara));
            error.AppendFormat("\r\n--------Exception--------\r\n");
            error.AppendFormat("ExceptionType: {0}\r\n", pobEx.GetType());
            error.AppendFormat("Message: {0}\r\n", pobEx.Message);
            error.AppendFormat("Source: {0}\r\n", pobEx.Source);
            error.Append("StackTrace: \r\n");
            error.AppendFormat("{0}\r\n", pobEx.StackTrace);

            error.AppendFormat("\r\nEnd at: {0}", DateTime.Now);
            error.Append("***Failed***\r\n\r\n\r\n");

            fobLogError.Error(error.ToString());
        }
        private static string GetParamtersStr(object[] para)
        {
            if (para == null) return "";
            var str = new StringBuilder(para.Length);
            foreach (var item in para)
            {
                str.Append(item);
            }
            return str.ToString();
        }
        private static string ResultFormat(object pobResult)
        {
            var str = new StringBuilder(pobResult.ToString().Length);
            var enumResult = pobResult as IEnumerable;
            if (enumResult != null)
            {
                foreach (var item in enumResult)
                {
                    str.Append(item);
                }
            }
            else { str.Append(pobResult); }
            return str.ToString();
        }

    }
}
