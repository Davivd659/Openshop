using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YG.SC.Common;

namespace YG.SC.Model
{
    public class PagerSearchCriteria
    {
        /// <summary>
        /// 当前页码。
        /// </summary>
        public int pg { get; set; }

        /// <summary>
        /// 每页条数（View提交）。
        /// </summary>
        public int PageSize { get; set; }

        /// <summary>
        /// 每页条数（处理后，直接可用）。
        /// </summary>
        public int Top
        {
            get
            {
                int top = Define.PAGE_SIZE;
                if (PageSize > 0)
                {
                    top = PageSize;
                }
                return top;
            }
        }

        /// <summary>
        /// 前一页的页码（处理后，直接可用）。
        /// </summary>
        public int Idx
        {
            get
            {
                return (pg - 1) < 0 ? 0 : (pg - 1);
            }
        }


        public Tuple<T1[], PagerEntity> GetPagerData<T1>(IQueryable<T1> query)
        {
            int total = query.Count();
            var array = query.Skip(Idx * Top).Take(Top);
            PagerEntity pe = new PagerEntity { Total = total, PageIndex = Idx + 1, Top = Top };
            return Tuple.Create(query.Skip(Idx * Top).Take(Top).ToArray(), new PagerEntity { Total = total, PageIndex = Idx + 1, Top = Top });

        }
    }
}
