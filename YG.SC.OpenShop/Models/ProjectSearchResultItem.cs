using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace YG.SC.OpenShop.Models
{
    [Serializable]
    public class ProjectSearchResultItem
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string TeShe { get; set; }
        public string ImagesPath { get; set; }
        public decimal Price { get; set; }
    }
}