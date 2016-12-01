using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace YG.SC.WebCrawler.Models
{
    public class PageModel
    {
        public static PageModel Instance = new PageModel();

        public static PageModel Current
        {
            get
            {
                if (Instance == null)
                {
                    Instance = new PageModel();
                }

                return Instance;
            }
        }
        public int PageIndex = 0;

    }
}