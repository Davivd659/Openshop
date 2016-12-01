using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace YG.SC.WebUI.Models.AdPosition
{
    [Serializable]
    public class AddPositionDayItem
    {
        public string AdWords { get; set; }
        public int KeyId { get; set; }
        public int Sort { get; set; }
        public int Id { get; set; }
        public string Url { get; set; }
        public DateTime StartDate { get; set; }
        public string AdPic { get; set; }
        public string AdPicSmall { get; set; }
        public int TypesId { get; set; }
        public int PositionId { get; set; }
    }
}