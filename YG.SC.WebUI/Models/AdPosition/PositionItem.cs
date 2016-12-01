using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace YG.SC.WebUI.Models
{
    [Serializable]
    public class PositionItem
    {
        public static string ShowHtml(List<YG.SC.WebUI.Models.PositionItem> array,string key)
        {
            if (array == null) { return ""; }
            var item = array.Where(m => m.Key == key).FirstOrDefault();
            if (item == null)
            {
                return "";
            }
            else
            {
                return item.Values;
            }
        }
        public string GetHtmlValues()
        {
            if (this == null)
            {
                return "";
            }
            else
            {
                return this.Values;
            }
        }
        public string Key { get; set; }
        public string Values { get; set; }
    }
}