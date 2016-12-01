using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YG.SC.Model.Project
{
    public class ProjectViewModel
    {
        public int Id { get; set; }
        public int ProjectMainId { get; set; }
        #region 首页
        /// <summary>
        /// 行业
        /// </summary>
        [DisplayName("行业")]
        public string Trade { get; set; }
        /// <summary>
        /// 区域
        /// </summary>
        [DisplayName("区域")]
        public string Region { get; set; }
        /// <summary>
        /// 商圈
        /// </summary>
        [DisplayName("商圈")]
        public string TradingArea { get; set; }
        /// <summary>
        /// 环线
        /// </summary>
        [DisplayName("环线")]
        public string CircleLine { get; set; }
        /// <summary>
        /// 售价
        /// </summary>
        [DisplayName("售价(元/㎡)")]
        public string SalePrice { get; set; }
        /// <summary>
        /// 租价
        /// </summary>
        [DisplayName("租价(元/㎡.天)")]
        public string RentalPrice { get; set; }
        /// <summary>
        /// 地铁
        /// </summary>
        [DisplayName("地铁")]
        public string Metro { get; set; }
        /// <summary>
        /// 状态
        /// </summary>
        [DisplayName("状态")]
        public string State { get; set; }
        /// <summary>
        /// 开盘时间
        /// </summary>
        [DisplayName("开盘时间")]
        public string OpenDate { get; set; }
        #endregion

        #region 基本信息
        [DisplayName("名称")]
        public string Name { get; set; }
        /// <summary>
        /// 项目特色
        /// </summary>
        [DisplayName("项目特色")]
        public string ProjectFeature { get; set; }
        /// <summary>
        /// 项目优势
        /// </summary>
        [DisplayName("项目优势")]
        public string ProjectAdvantage { get; set; }
        /// <summary>
        /// 项目团队介绍
        /// </summary>
        [DisplayName("项目团队介绍")]
        public string TuanDuiInfo { get; set; }
        /// <summary>
        /// 项目介绍
        /// </summary>
        [DisplayName("项目介绍")]
        public string ProjectIntro { get; set; }

        /// <summary>
        /// 项目团队介绍
        /// </summary>
        [DisplayName("房源介绍")]
        public string Listings { get; set; }
        /// <summary>
        /// 项目电话
        /// </summary>
        [DisplayName("项目电话")]
        public string ContactPhone { get; set; }
        /// <summary>
        /// 项目地址
        /// </summary>
        [DisplayName("项目地址")]
        public string AddressLine { get; set; }
        /// <summary>
        /// 业态
        /// </summary>
        [DisplayName("物业类型")]
        public string TypeOfOperation { get; set; }
        /// <summary>
        /// 权属性质
        /// </summary>
        [DisplayName("权属性质")]
        public string OwnerShip { get; set; }
        /// <summary>
        /// 外立面形式
        /// </summary>
        [DisplayName("外立面形式")]
        public string Facade { get; set; }
        /// <summary>
        /// 建筑结构
        /// </summary>
        [DisplayName("建筑结构")]
        public string Architecture { get; set; }
        #endregion

        #region 建筑指标
        /// <summary>
        /// 总建筑面积
        /// </summary>
        [DisplayName("总建筑面积")]
        public string TotalFloorArea { get; set; }
        /// <summary>
        /// 总层数
        /// </summary>
        [DisplayName("总层数")]
        public int TotalLayerNum { get; set; }
        /// <summary>
        /// 标准层净高
        /// </summary>
        [DisplayName("标准层净高")]
        public double SignalLayerHeight { get; set; }
        #endregion

        #region 平面布局
        /// <summary>
        /// 标准层面积
        /// </summary>
        [DisplayName("标准层面积")]
        public double SignalLayerArea { get; set; }
        /// <summary>
        /// 主力户型 开间面积
        /// </summary>
        [DisplayName("主力户型")]
        public double MainUnit { get; set; }
        /// <summary>
        /// 窗户是否可开
        /// </summary>
        [DisplayName("窗户是否可开")]
        public bool WinIsOpen { get; set; }
        /// <summary>
        /// 承重柱位置
        /// </summary>
        [DisplayName("承重柱位置")]
        public string KingTowerPosition { get; set; }
        /// <summary>
        /// 核心筒布局
        /// </summary>
        [DisplayName("核心筒布局")]
        public string CoreTubeLayout { get; set; }
        /// <summary>
        /// 有无黑间 / 面积多少
        /// </summary>
        [DisplayName("黑间面积")]
        public string BlackArea { get; set; }
        #endregion

        #region 硬件配套
        /// <summary>
        /// 电梯品牌
        /// </summary>
        [DisplayName("电梯品牌")]
        public string ElevatorBrand { get; set; }
        /// <summary>
        /// 客梯数量
        /// </summary>
        [DisplayName("客梯数量")]
        public string PassengerElevatorNum { get; set; }
        /// <summary>
        /// 是否分区可控
        /// </summary>
        [DisplayName("是否分区可控")]
        public bool IsPartitionControl { get; set; }
        /// <summary>
        /// 空调品牌
        /// </summary>
        [DisplayName("空调品牌")]
        public string AirConditionBrand { get; set; }
        /// <summary>
        /// 中央空调制冷方式
        /// </summary>
        [DisplayName("中央空调制式")]
        public string CenterAirCoolWay { get; set; }
        /// <summary>
        /// 梯速
        /// </summary>
        [DisplayName("梯速")]
        public string ElevatorSpeed { get; set; }
        /// <summary>
        /// 车位
        /// </summary>
        [DisplayName("车位")]
        public string  ParkingPlace { get; set; }
        #endregion

        #region 地图信息 
        [DisplayName("百度经度")]
        public double Long { get; set; }
        [DisplayName("百度纬度")]
        public double Lat { get; set; }
       
        #endregion 

        [DisplayName("封面图")]
        public string CoverPhoto { get; set; }
    }
}
