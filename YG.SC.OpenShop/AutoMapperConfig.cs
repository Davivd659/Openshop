using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;
using YG.SC.DataAccess;
using YG.SC.Model.Project;
using YG.SC.OpenShop.Controllers;
using YG.SC.OpenShop.Models.project;
using YG.SC.Service.IService;
using System.Web.Mvc;

namespace YG.SC.OpenShop
{
    public abstract class AutoMapperConfig
    {
        public static void RegisterMappings()
        {
            Mapper.CreateMap<ProjectService, ProjectServiceViewModel>()
                .ForMember(src => src.ProjectListId, det => det.MapFrom(src => src.ShopProjectId))
                .ForMember(src => src.IsVip, det => det.MapFrom(src => src.IsMvp))
                .ForMember(src => src.Name, det => det.MapFrom(src => src.Name))
                .ForMember(src => src.PicUrl, det => det.MapFrom(src => src.PicUrl))
                .ForMember(src => src.Status, det => det.MapFrom(src => src.Status));

            Mapper.CreateMap<ShopProject, ProjectDetailsViewModel>()
                .ForMember(src => src.Id, det => det.MapFrom(src => src.Id))
                .ForMember(src => src.ProjectMainId,
                    det => det.MapFrom(src => src.ShopProjectMain.FirstOrDefault(p => p.ShopProjectId == src.Id).Id))
                .ForMember(src => src.AirConditionBrand, det => det.MapFrom(src => src.YAirConditoningBrand))
                .ForMember(src => src.Architecture, det => det.MapFrom(src => GetShopAttributeValues(src.BJieGou)))//建筑结构
                .ForMember(src => src.BlackArea, det => det.MapFrom(src => src.PHeiFangJian))
                .ForMember(src => src.CenterAirCoolWay, det => det.MapFrom(src => src.YAirConditoningControl))
                .ForMember(src => src.CircleLine,
                    det =>
                        det.MapFrom(
                            src => src.ShopProjectMain.FirstOrDefault(p => p.ShopProjectId == src.Id).CircleLineId))
                .ForMember(src => src.CoreTubeLayout, det => det.MapFrom(src => src.PHeXinTong))

                .ForMember(src => src.ElevatorBrand, det => det.MapFrom(src => src.YLiftBrand))
                .ForMember(src => src.ElevatorSpeed, det => det.MapFrom(src => src.YLiftSpeed))
                //.ForMember(src => src.Facade, det => det.MapFrom(src => src.BWaiLiMian))
                .ForMember(src => src.Facade, det => det.MapFrom(src => GetShopAttributeValues(src.BWaiLiMian)))//外立面形式
                .ForMember(src => src.IsPartitionControl,
                    det => det.MapFrom(src => src.YLiftFenQu))
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
                            src => src.ShopProjectMain.FirstOrDefault(p => p.ShopProjectId == src.Id).OpenDate))
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
                                src.ShopProjectMain.FirstOrDefault(p => p.ShopProjectId == src.Id).RentalPrice.Value))
                .ForMember(src => src.SalePrice,
                    det =>
                        det.MapFrom(
                            src => src.ShopProjectMain.FirstOrDefault(p => p.ShopProjectId == src.Id).SalePrice.Value))
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
                //    det =>
                //        det.MapFrom(
                //            src => src.ShopProjectMain.FirstOrDefault(p => p.ShopProjectId == src.Id).TypeOfOperationId))
                .ForMember(src => src.TypeOfOperation, det => det.MapFrom(src => GetShopAttributeValues(src.ShopProjectMain.FirstOrDefault(m=> m.ShopProjectId == src.Id).TypeOfOperationId)))
                .ForMember(src => src.WinIsOpen, det => det.MapFrom(src => src.PWindowOpen))
                .ForMember(src => src.AdressLine, det => det.MapFrom(src => src.AddressLine))
                .ForMember(src=> src.PhotoUrlSmall,det => det.MapFrom(src => GetPhotoUrlSmall(src)))
                ;
        }

        public static string GetPhotoUrlSmall(ShopProject proj)
        {
            // var projMain = proj.ProjectPhoto.Where(m =>m.Recsts == 1).FirstOrDefault();
            if (proj != null)
            {
                return proj.CoverPhoto;
            }
            else
            {
                return "/images/searchlistimg.jpg";
            }
        }

        public static string GetPhotoUrl(ShopProject proj)
        {
            // var projMain = Project.ProjectPhoto.Where(m =>m.Recsts == 1).FirstOrDefault();
            if (proj != null)
            {
                // return projMain.PhotoUrl;
                return proj.CoverPhoto;
            }
            else
            {
                return "/images/searchlistimg.jpg";
            }
        }

        public static string GetShopAttributeValues(string AttrIdValues)
        {
            string val = "";
            // var AttrValues = model.ShopProjectMain.FirstOrDefault(m => m.ShopProjectId == model.Id).TypeOfOperationId;

            var AttributeService = DependencyResolver.Current.GetService<IShopAttributesService>();

            List<int> listId = new List<int>();
            if (!string.IsNullOrEmpty(AttrIdValues))
            {
                string[] array = AttrIdValues.Split(',');

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
                            var atv = AttributeService.GetShopAttributeValuesById(atid);
                            val += atv.ValueStr + ",";
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