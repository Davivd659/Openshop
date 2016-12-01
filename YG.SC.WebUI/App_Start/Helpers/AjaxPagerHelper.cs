using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.Collections.Specialized;

using System.Text;
using System.Web.Routing;
namespace System.Web.Mvc
{
    public static partial class PagerHelper
    {
        public static string Pager(this HtmlHelper helper, int currentPage, int currentPageSize, int totalRecords, string urlPrefix)
        {
            StringBuilder sb1 = new StringBuilder();

            int seed = currentPage % currentPageSize == 0 ? currentPage : currentPage - (currentPage % currentPageSize);

            if (currentPage > 1)
                sb1.AppendLine(String.Format("<a href=\"{0}/{1}\">上一页</a>", urlPrefix, currentPage));

            if (currentPage - currentPageSize >= 0)
                sb1.AppendLine(String.Format("<a href=\"{0}/{1}\">...</a>", urlPrefix, (currentPage - currentPageSize) + 1));

            for (int i = seed; i < Math.Round((totalRecords / 10) + 0.5) && i < seed + currentPageSize; i++)
            {
                sb1.AppendLine(String.Format("<a href=\"{0}/{1}\">{1}</a>", urlPrefix, i + 1));
            }

            if (currentPage + currentPageSize <= (Math.Round((totalRecords / 10) + 0.5) - 1))
                sb1.AppendLine(String.Format("<a href=\"{0}/{1}\">...</a>", urlPrefix, (currentPage + currentPageSize) + 1));

            if (currentPage < (Math.Round((totalRecords / 10) + 0.5) - 1))
                sb1.AppendLine(String.Format("<a href=\"{0}/{1}\">Next</a>", urlPrefix, currentPage + 2));

            return sb1.ToString();
        }

        private static string GetNumericPage(int currentPageIndex, int pageSize, int recordCount, int pageCount, string status, string urlTag)
        {
            int k = currentPageIndex / 10;
            int m = currentPageIndex % 10;
            StringBuilder sb = new StringBuilder();
            if (currentPageIndex / 10 == pageCount / 10)
            {
                if (m == 0)
                {
                    k--;
                    m = 10;
                }
                else
                    m = pageCount % 10;
            }
            else
                m = 10;
            for (int i = k * 10 + 1; i <= k * 10 + m; i++)
            {
                if (i == currentPageIndex)
                    sb.AppendFormat("<span><font class='CurrentPageIndex'><b>{0}</b></font></span>&nbsp;", i);
                else
                {
                    sb.AppendFormat("<a href='#{2}' class='ajaxPagerItem' onclick='getContentTab(\"{0}\",\"{1}\")'>{1}</a>", status, i, urlTag);
                }
            }
            return sb.ToString();
        }
        /// <summary>
        /// Ajax js 异步分页
        /// </summary>
        /// <param name="helper"></param>
        /// <param name="currentPage"></param>
        /// <param name="currentPageSize"></param>
        /// <param name="totalRecords"></param>
        /// <param name="status"></param>
        /// <param name="urlTag"></param>
        /// <returns></returns>
        public static IHtmlString Pager(this HtmlHelper helper, int currentPage, int currentPageSize, int totalRecords, string status, string urlTag)
        {
            int pageCount = (totalRecords % currentPageSize == 0 ? totalRecords / currentPageSize : totalRecords / currentPageSize + 1);

            if (currentPage == 0) { currentPage = 1; }

            StringBuilder sb1 = new StringBuilder();

            //int seed = currentPage % currentPageSize == 0 ? currentPage - currentPageSize + 1 : currentPage - (currentPage % currentPageSize) + 1;

            if (currentPage > 1)
                sb1.AppendLine(String.Format("<span class=\"item\"><a href='#{2}' onclick=getContentTab(\"{0}\",\"{1}\") >上一页</a></span>", status, currentPage - 1, urlTag));
            string NumericPage = GetNumericPage(currentPage, currentPageSize, totalRecords, pageCount, status,urlTag);
            sb1.AppendLine(NumericPage);

            if (currentPage < (Math.Round((totalRecords / currentPageSize) + 0.5) - 1))
                sb1.AppendLine(String.Format("<span class=\"item\"><a href='#{2}'  onclick=getContentTab(\"{0}\",\"{1}\") >下一页</a></span>", status, currentPage + 1, urlTag));

            HtmlString html = new HtmlString(sb1.ToString());
            return html;
        }   

    }
}