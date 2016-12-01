
using System.Web.Razor.Text;

namespace YG.SC.WebAPIService.Models
{
    /// <summary>
    /// 一级分类信息
    /// 命名空间：YG.SC.WebAPIService.Models
    /// 类功能：
    /// </summary>
    /// 创建者：边亮
    /// 创建日期：2014-09-24 19:55
    /// 修改者：
    /// 修改时间：
    /// ----------------------------------------------------------------------------------------
    public class ClassifyModel
    {
        public int  Id { get; set; }
        public string  Name { get; set; }
        public string Picture { get; set; }
        public SubClassifyModel[] SubClassify { get; set; }
    }

    /// <summary>
    /// 二级分类信息
    /// 命名空间：YG.SC.WebAPIService.Models
    /// 类功能：
    /// </summary>
    /// 创建者：边亮
    /// 创建日期：2014-09-24 19:55
    /// 修改者：
    /// 修改时间：
    /// ----------------------------------------------------------------------------------------
    public class SubClassifyModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
      
    }
}