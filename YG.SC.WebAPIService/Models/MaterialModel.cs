﻿
namespace YG.SC.WebAPIService.Models
{
    /// <summary>
    ///     商品服务
    /// 命名空间：YG.SC.WebAPIService.Models
    /// 类功能：
    /// </summary>
    /// 创建者：边亮
    /// 创建日期：2014-09-24 20:08
    /// 修改者：
    /// 修改时间：
    /// ----------------------------------------------------------------------------------------
    public class MaterialModel
    {
        public int Id { get; set; }
        /// <summary>
        /// 商品名称
        /// </summary>
        /// <value>
        /// The Name
        /// </value>
        /// 创建者：边亮
        /// 创建日期：2014-09-25 17:54
        /// 修改者：
        /// 修改时间：
        /// ----------------------------------------------------------------------------------------
        public string Name { get; set; }
        /// <summary>
        /// 价格
        /// </summary>
        /// <value>
        /// The Price
        /// </value>
        /// 创建者：边亮
        /// 创建日期：2014-09-25 17:55
        /// 修改者：
        /// 修改时间：
        /// ----------------------------------------------------------------------------------------
        public decimal Price { get; set; }
        /// <summary>
        ///单位
        /// </summary>
        /// <value>
        /// The Unit
        /// </value>
        /// 创建者：边亮
        /// 创建日期：2014-09-25 17:55
        /// 修改者：
        /// 修改时间：
        /// ----------------------------------------------------------------------------------------
        public string Unit { get; set; }
        /// <summary>
        ///图片路径
        /// </summary>
        /// <value>
        /// The Image
        /// </value>
        /// 创建者：边亮
        /// 创建日期：2014-09-25 17:55
        /// 修改者：
        /// 修改时间：
        /// ----------------------------------------------------------------------------------------
        public string Image { get; set; }

        /// <summary>
        /// 缩略图
        /// </summary>
        /// <value>
        /// The ImageSmall
        /// </value>
        /// 创建者：孟祺宙
        /// 创建日期：2014/10/16 15:57
        /// 修改者：
        /// 修改时间：
        /// ----------------------------------------------------------------------------------------
        public string ImageSmall { get; set; }

        /// <summary>
        /// 一级分类名称
        /// </summary>
        /// <value>
        /// The name of the classify.
        /// </value>
        /// 创建者：边亮
        /// 创建日期：2014-09-25 17:56
        /// 修改者：
        /// 修改时间：
        /// ----------------------------------------------------------------------------------------
        public string ClassifyName { get; set; }
        /// <summary>
        ///一级分类id
        /// </summary>
        /// <value>
        /// The ClassifyId
        /// </value>
        /// 创建者：边亮
        /// 创建日期：2014-09-25 17:56
        /// 修改者：
        /// 修改时间：
        /// ----------------------------------------------------------------------------------------
        public int ClassifyId { get; set; }
        /// <summary>
        /// 二级分类Id
        /// </summary>
        /// <value>
        /// The ClassifySecondId
        /// </value>
        /// 创建者：边亮
        /// 创建日期：2014-09-25 17:56
        /// 修改者：
        /// 修改时间：
        /// ----------------------------------------------------------------------------------------
        public int ClassifySecondId { get; set; }
        /// <summary>
        /// 品牌
        /// </summary>
        /// <value>
        /// The Brand
        /// </value>
        /// 创建者：边亮
        /// 创建日期：2014-09-25 17:56
        /// 修改者：
        /// 修改时间：
        /// ----------------------------------------------------------------------------------------
        public string Brand { get; set; }
        /// <summary>
        /// 生产商
        /// </summary>
        /// <value>
        /// The Producer
        /// </value>
        /// 创建者：边亮
        /// 创建日期：2014-09-25 17:56
        /// 修改者：
        /// 修改时间：
        /// ----------------------------------------------------------------------------------------
        public string Producer { get; set; }
        /// <summary>
        /// 储存方式
        /// </summary>
        /// <value>
        /// The Storage
        /// </value>
        /// 创建者：边亮
        /// 创建日期：2014-09-25 17:57
        /// 修改者：
        /// 修改时间：
        /// ----------------------------------------------------------------------------------------
        public string Storage { get; set; }

        /// <summary>
        /// 商品规格
        /// </summary>
        /// <value>
        /// The Specification
        /// </value>
        /// 创建者：边亮
        /// 创建日期：2014/10/10 11:32
        /// 修改者：
        /// 修改时间：
        /// ----------------------------------------------------------------------------------------
        public string Specification { get; set; }

        /// <summary>
        /// 商品所属二级分类名称
        /// </summary>
        /// <value>
        /// The name of the classify second.
        /// </value>
        /// 创建者：边亮
        /// 创建日期：2014/10/10 13:54
        /// 修改者：
        /// 修改时间：
        /// ----------------------------------------------------------------------------------------
        public string ClassifySecondName { get; set; }
    }
}