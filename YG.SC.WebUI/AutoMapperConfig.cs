using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using AutoMapper;
using YG.SC.DataAccess;
using YG.SC.Model.Project;
using YG.SC.WebUI.Models.AdPosition;
using YG.SC.WebUI.Views.AdPositionManager;
using System.Web.Mvc;
using YG.SC.Service.IService;
using YG.SC.Model;

namespace YG.SC.WebUI
{
    public abstract class AutoMapperConfig
    {
        public static void RegisterMappings()
        {
            Mapper.CreateMap<ShopProject, ProjectViewModel>()
                .ForMember(src => src.Id, det => det.MapFrom(src => src.Id))
                .ForMember(src => src.ProjectMainId,
                    det => det.MapFrom(src => src.ShopProjectMain.FirstOrDefault(p => p.ShopProjectId == src.Id).Id))
                .ForMember(src => src.AirConditionBrand, det => det.MapFrom(src => src.YAirConditoningBrand))
                .ForMember(src => src.Architecture, det => det.MapFrom(src => src.BJieGou))
                .ForMember(src => src.BlackArea, det => det.MapFrom(src => src.PHeiFangJian))
                .ForMember(src => src.CenterAirCoolWay, det => det.MapFrom(src => src.YAirConditoningControl))
                .ForMember(src => src.CircleLine,
                    det =>
                        det.MapFrom(
                            src => src.ShopProjectMain.FirstOrDefault(p => p.ShopProjectId == src.Id).CircleLineId))
                .ForMember(src => src.CoreTubeLayout, det => det.MapFrom(src => src.PHeXinTong))
                .ForMember(src => src.ElevatorBrand, det => det.MapFrom(src => src.YLiftBrand))
                .ForMember(src => src.ElevatorSpeed, det => det.MapFrom(src => src.YLiftSpeed))
                .ForMember(src => src.Facade, det => det.MapFrom(src => src.BWaiLiMian))
                .ForMember(src => src.IsPartitionControl, det => det.MapFrom(src => src.YLiftFenQu))
                .ForMember(src => src.KingTowerPosition, det => det.MapFrom(src => src.PChengZhongZhu))
                .ForMember(src => src.MainUnit, det => det.MapFrom(src => src.PZhuLiHuxing))
                .ForMember(src => src.Metro,
                    det =>
                        det.MapFrom(
                            src => src.ShopProjectMain.FirstOrDefault(p => p.ShopProjectId == src.Id).MetroId))
                .ForMember(src => src.Name, det => det.MapFrom(src => src.NAME))
                .ForMember(src => src.OpenDate,
                    det =>
                        det.MapFrom(
                            src =>GetOpenDateTime(src)))
                // src.ShopProjectMain.FirstOrDefault(p => p.ShopProjectId == src.Id).OpenDate.Value))
                .ForMember(src => src.OwnerShip, det => det.MapFrom(src => src.BQuanShu))
                .ForMember(src => src.ParkingPlace, det => det.MapFrom(src => src.YPrkingSpace))
                .ForMember(src => src.PassengerElevatorNum, det => det.MapFrom(src => src.TeaIntroduction))
                //客梯数量是哪个字段，待定。暂时写 TeaIntroduction
                .ForMember(src => src.ProjectAdvantage, det => det.MapFrom(src => src.YouShi))
                .ForMember(src => src.ProjectFeature, det => det.MapFrom(src => src.TeShe))
                .ForMember(src => src.Region,
                    det =>
                        det.MapFrom(
                            src => src.ShopProjectMain.FirstOrDefault(p => p.ShopProjectId == src.Id).RegionId.Value))
                .ForMember(src => src.RentalPrice,
                    det =>
                        det.MapFrom(
                            src =>
                                src.ShopProjectMain.FirstOrDefault(p => p.ShopProjectId == src.Id).RentalPrice.ToString()))
                .ForMember(src => src.SalePrice,
                    det =>
                        det.MapFrom(
                            src => src.ShopProjectMain.FirstOrDefault(p => p.ShopProjectId == src.Id).SalePrice.ToString()))
                .ForMember(src => src.SignalLayerArea, det => det.MapFrom(src => src.PChengMianJi.Value))
                .ForMember(src => src.SignalLayerHeight, det => det.MapFrom(src => src.JChengGao.Value))
                .ForMember(src => src.State,
                    det =>
                        det.MapFrom(
                            src => src.ShopProjectMain.FirstOrDefault(p => p.ShopProjectId == src.Id).StateId))
                .ForMember(src => src.TotalFloorArea, det => det.MapFrom(src => src.JZongJianZhuMianJi.Value))
                .ForMember(src => src.TotalLayerNum, det => det.MapFrom(src => src.JZongChengShu.Value))
                .ForMember(src => src.Trade,
                    det =>
                        det.MapFrom(src => src.ShopProjectMain.FirstOrDefault(p => p.ShopProjectId == src.Id).TradeId))
                .ForMember(src => src.TradingArea,
                    det =>
                        det.MapFrom(
                            src =>
                                src.ShopProjectMain.FirstOrDefault(p => p.ShopProjectId == src.Id).TradingAreaId.Value))
                //.ForMember(src => src.TypeOfOperation,
                //    det =>det.MapFrom(src => src.ShopProjectMain.FirstOrDefault(p => p.ShopProjectId == src.Id).TypeOfOperationId))
                .ForMember(src => src.TypeOfOperation,
                    det => det.MapFrom(src => GetShopAttributeValues(src)))
                .ForMember(src => src.WinIsOpen, det => det.MapFrom(src => src.PWindowOpen))
                ;
            Mapper.CreateMap<ShopAdPosition, AddPositionDayItem>();
            // 品牌
            Mapper.CreateMap<ShopBrand, AdPositionSearchPartial>()
                .ForMember(src=> src.Id,dt=> dt.MapFrom(s =>s.Id))
                .ForMember(src => src.Name, dt => dt.MapFrom(s => s.Name))
                .ForMember(src => src.TypeId, dt => dt.MapFrom(s => 1));
            // 项目
            Mapper.CreateMap<ShopProject, AdPositionSearchPartial>()
                .ForMember(src => src.Id, dt => dt.MapFrom(s => s.Id))
                .ForMember(src => src.Name, dt => dt.MapFrom(s => s.NAME))
                .ForMember(src => src.TypeId, dt => dt.MapFrom(s => 2));
            // 装修
            Mapper.CreateMap<OpenShop, AdPositionSearchPartial>()
                .ForMember(src => src.Id, dt => dt.MapFrom(s => s.Id))
                .ForMember(src => src.Name, dt => dt.MapFrom(s => s.Name))
                .ForMember(src => src.TypeId, dt => dt.MapFrom(s => 3));

            // 开店帮
            Mapper.CreateMap<OpenShop, OpenShopViewModel>();
        }

        public static string GetOpenDateTime(ShopProject proj)
        {
            var projMain = proj.ShopProjectMain.FirstOrDefault();
            if (projMain != null)
            {
                return projMain.OpenDate.HasValue ? projMain.OpenDate.Value.ToString("yyyy-MM-dd") : "";
            }
            else
            {
                return "";
            }
        }

        public static string GetShopAttributeValues(ShopProject model)
        {
            string val = "";
            var AttrValues =  model.ShopProjectMain.FirstOrDefault( m=> m.ShopProjectId == model.Id).TypeOfOperationId;

            var AttributeService = DependencyResolver.Current.GetService<IShopAttributesService>();

            List<int> listId = new List<int>();
            if (!string.IsNullOrEmpty(AttrValues))
            {
                string[] array = AttrValues.Split(',');

                foreach (var str in array)
                {
                    if (!string.IsNullOrEmpty(str))
                    {
                        int atid = 0;
                        int.TryParse(str, out atid);
                        if (atid > 0)
                        {
                            listId.Add(Convert.ToInt32(str));

                            // 
                            var atv = AttributeService.GetById(atid);
                            val += atv.AttributeName + ",";
                        }
                    }
                }
            }
           //  DependencyResolver.Current.GetService<ICustomerGroupOnService>();
            val = val.Trim(',');
            return val;
        }
    }
}