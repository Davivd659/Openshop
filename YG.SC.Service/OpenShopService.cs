using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using YG.SC.DataAccess;
using YG.SC.Model;
using YG.SC.Repository;
using YG.SC.Service.IService;

namespace YG.SC.Service
{
    public class OpenShopService : IOpenShopService
    {

        private readonly IRepository<OpenShop> _openShopRepository;
        private readonly IRepository<OpenShopPhoto> _openShopPhotoRepository;
        private readonly IRepository<OpenShopShopProject> _OpenShopShopProjectRepository;
        private readonly IRepository<ShopProject> _ShopProjectRepository;
        public OpenShopService(IRepository<OpenShop> openShopRepository, IRepository<OpenShopPhoto> openShopPhotoRepository, IRepository<OpenShopShopProject> openShopShopProjectRepository, IRepository<ShopProject> shopProjectRepository)
        {
            _openShopRepository = openShopRepository;
            _openShopPhotoRepository = openShopPhotoRepository;
            _OpenShopShopProjectRepository = openShopShopProjectRepository;
            _ShopProjectRepository = shopProjectRepository;
        }

        public void Dispose()
        {
            this._openShopRepository.Dispose();
            this._openShopPhotoRepository.Dispose();
            this._OpenShopShopProjectRepository.Dispose();
            this._ShopProjectRepository.Dispose();
        }
        public Tuple<DataAccess.OpenShop[], Model.PagerEntity> GetEntitsByName(int pg, int Type, string Name)
        {
            const int top = 10;
            var idx = (pg - 1) < 0 ? 0 : (pg - 1);

            Expression<Func<OpenShop, bool>> expressionFilter = (entity) => (Type == 0 || entity.Type == Type) && (string.IsNullOrEmpty(Name) || entity.Name.Contains(Name)) && (entity.Recsts != -1);
            var total = this._openShopRepository.Get(expressionFilter).Count();
            var array = this._openShopRepository.Get(expressionFilter, orderBy: item => item.OrderByDescending(p => p.Id)).Skip(top * idx).Take(top).ToArray();
            return Tuple.Create(array, new PagerEntity { Total = total, PageIndex = idx + 1, Top = top });

        }
        //OpenShopAttributeValues

        //public void UpdateProjectAttributes(int attributeId, string AttrValues, int OpnShopId)
        //{
        //    /*,[AttrId],[AttrValueId],[IsEnable],[ShopProjectId]*/

        //    List<int> listId = new List<int>();
        //    if (!string.IsNullOrEmpty(AttrValues))
        //    {
        //        string[] array = AttrValues.Split(',');

        //        foreach (var str in array)
        //        {
        //            if (!string.IsNullOrEmpty(str))
        //            {
        //                int atid = 0;
        //                int.TryParse(str, out atid);
        //                if (atid > 0)
        //                {
        //                    listId.Add(Convert.ToInt32(str));
        //                }
        //            }
        //        }
        //    }

        //    var ItemList = _shopProjectAttributesValueRepository.Table.Where(m => m.ShopProjectId == ProjectId
        //        && m.AttrId == attributeId).ToList();
        //    if (ItemList.Count > 0)
        //    {
        //        foreach (var item in ItemList)
        //        {
        //            if (listId.Contains(item.AttrValueId.Value))
        //            {
        //                item.IsEnable = true;
        //            }
        //            else
        //            {
        //                item.IsEnable = false;
        //            }
        //        }
        //    }
        //    // 新增 
        //    int[] oldIdlist = (from m in ItemList select m.AttrValueId.Value).ToArray();
        //    var addList = listId.Except(oldIdlist);
        //    foreach (var toAddId in addList)
        //    {
        //        ShopProjectAttributesValues newMode = new ShopProjectAttributesValues();
        //        newMode.AttrId = attributeId;
        //        newMode.AttrValueId = toAddId;
        //        newMode.IsEnable = true;
        //        newMode.ShopProjectId = ProjectId;
        //        _shopProjectAttributesValueRepository.Insert(newMode);
        //    }
        //    _shopProjectAttributesValueRepository.SaveChanges();
        //}

        //        /// <summary>
        //        /// 前端页面
        //        /// </summary>
        //        /// <param name="pg"></param>
        //        /// <param name="AttributeId"></param>
        //        /// <returns></returns>
        //        public Tuple<OpenShop[], PagerEntity> GetEntitsByName(int pg, int AttributeValuesId)
        //        {
        //            const int top = 10;
        //            var idx = (pg - 1) < 0 ? 0 : (pg - 1);
        //            #region// 两个 AttributeValuesId情况
        //            //List<OpenShop> lists = new List<OpenShop>();
        //            // List<OpenShopAttributeValues> listatt = new List<OpenShopAttributeValues>();
        //            //foreach (var i in AttributeValuesIds)
        //            //{
        //            //    if (lists.Count<=0)
        //            //    {
        //            //        this._OpenShopAttributeValuesRepository.Get(item => item.AttributeValuesId == i)
        //            //            .OrderByDescending(item => item.BrandId)
        //            //            .ToList()
        //            //            .ForEach(item => listatt.Add(item));
        //            //    }
        //            //    else
        //            //    {
        //            //        listatt.ForEach(item =>
        //            //        {
        //            //            if (item.AttributeValuesId!=i)
        //            //            {
        //            //                listatt.Remove(item);
        //            //            }
        //            //        })
        //            //    }
        //            //}
        //#endregion

        //            if (AttributeValuesId==0)
        //            {
        //                Expression<Func<OpenShop, bool>> expressionFilter = (entity) => (entity.Recsts != -1);
        //                var total = this._openShopRepository.Get(expressionFilter).Count();
        //                var array = this._openShopRepository.Get(expressionFilter, orderBy: item => item.OrderByDescending(p => p.Id)).Skip(top * idx).Take(top).ToArray();
        //                return Tuple.Create(array, new PagerEntity { Total = total, PageIndex = idx + 1, Top = top });
        //            }
        //            else
        //            {
        //                var model = this._OpenShopAttributeValuesRepository.Get(item => item.AttributeValuesId == AttributeValuesId).OrderByDescending(item => item.Id).ToList();
        //                List<OpenShop> list = new List<OpenShop>();
        //                foreach (var item in model)
        //                {
        //                    list.Add(item.OpenShop);
        //                }
        //                var total = list.Count();
        //                var array = list.Skip(top * idx).Take(top).ToArray();
        //                return Tuple.Create(array, new PagerEntity { Total = total, PageIndex = idx + 1, Top = top });

        //            }


        //        }


        public DataAccess.OpenShop GetById(int id)
        {
            return this._openShopRepository.GetById(id);
        }

        public void Update(DataAccess.OpenShop sb)
        {
            this._openShopRepository.Update(sb);
            this._openShopRepository.SaveChanges();
        }

        public void Insert(DataAccess.OpenShop sb)
        {
            this._openShopRepository.Insert(sb);
            this._openShopRepository.SaveChanges();
        }






        public Tuple<OpenShopPhoto[], PagerEntity> GetPhotoByBrandId(int pg, int id, string name)
        {
            const int top = 10;
            var idx = (pg - 1) < 0 ? 0 : (pg - 1);

            Expression<Func<OpenShopPhoto, bool>> expressionFilter = (entity) => (id == 0 || entity.OpenShopId == id) && (entity.Recsts != -1);
            var total = this._openShopPhotoRepository.Get(expressionFilter).Count();
            var array = this._openShopPhotoRepository.Get(expressionFilter, orderBy: item => item.OrderByDescending(p => p.Id)).Skip(top * idx).Take(top).ToArray();
            return Tuple.Create(array, new PagerEntity { Total = total, PageIndex = idx + 1, Top = top });
        }

        public OpenShopPhoto GetPhotoById(int id)
        {
            return this._openShopPhotoRepository.GetById(id);
        }

        public void UpdatePhoto(OpenShopPhoto sb)
        {
            this._openShopPhotoRepository.Update(sb);
            this._openShopPhotoRepository.SaveChanges();
        }

        public void InsertPhoto(OpenShopPhoto sb)
        {
            this._openShopPhotoRepository.Insert(sb);
            this._openShopPhotoRepository.SaveChanges();
        }




        public Tuple<OpenShopShopProject[], PagerEntity> GetEntitsByName(int pg, string Name, string selRecsts)
        {
            const int top = 10;
            var idx = (pg - 1) < 0 ? 0 : (pg - 1);

            Expression<Func<OpenShopShopProject, bool>> expressionFilter = (entity) => (string.IsNullOrEmpty(Name) || entity.OpenShop.Name.Contains(Name));
            if (!string.IsNullOrEmpty(selRecsts))
            {
                int Recsts = Convert.ToInt32(selRecsts);
                expressionFilter = (entity) => (entity.Recsts == Recsts);
            }

            var total = this._OpenShopShopProjectRepository.Get(expressionFilter).Count();
            var array = this._OpenShopShopProjectRepository.Get(expressionFilter, orderBy: item => item.OrderByDescending(p => p.Id)).Skip(top * idx).Take(top).ToArray();
            return Tuple.Create(array, new PagerEntity { Total = total, PageIndex = idx + 1, Top = top });
        }

        public OpenShopShopProject GetOpenShopProjectById(int id)
        {
            return this._OpenShopShopProjectRepository.GetById(id);

        }

        public void Update(OpenShopShopProject osp)
        {
            this._OpenShopShopProjectRepository.Update(osp);
            this._OpenShopShopProjectRepository.SaveChanges();

        }

        public void Insert(OpenShopShopProject osp)
        {
            this._OpenShopShopProjectRepository.Insert(osp);
            this._OpenShopShopProjectRepository.SaveChanges();
        }


        public List<OpenShop> GetAllOpenShop()
        {
            return this._openShopRepository.Get().ToList();
        }

        public List<ShopProject> GetAllShopProject()
        {
            return this._ShopProjectRepository.Get().ToList();

        }

        public Tuple<OpenShop[], PagerEntity> SearchOpenShop(OpenShopSearchCriteria criteria)
        {
            int top = 6;
            if (criteria.PageSize > 0)
            { top = criteria.PageSize; }

            var idx = (criteria.PageIndex - 1) < 0 ? 0 : (criteria.PageIndex - 1);

            var query = _openShopRepository.Table.Where(m => m.Recsts == 1);
            if (!string.IsNullOrEmpty(criteria.Keys))
            {
                query = query.Where(m => m.Name.Contains(criteria.Keys));
            }
            // type
            if (criteria.TypeId.HasValue && criteria.TypeId.Value > 0)
            {
                query = query.Where(m => m.Type==criteria.TypeId);
            }
            // area 
            if (criteria.AreaId.HasValue && criteria.AreaId.Value > 0)
            {
                query = query.Where(ｍ => ｍ.RegionId == criteria.AreaId);
            }
            // hot id

            int total = query.Count();
            var array = query.OrderByDescending(m => m.Id).Skip(idx * top).Take(top).ToArray();

            return Tuple.Create(array, new PagerEntity { Total = total, PageIndex = idx + 1, Top = top });

        }

        public bool GetEntitsByName(string Name)
        {
            bool result = false;
            if (_openShopRepository.Table.Where(m => m.Name == Name).Count() > 0)
            {
                result = true;
            }
            return result;
        }


        public bool GetEntitsByAbbreviation(string Abbreviation)
        {
            bool result = false;
            if (_openShopRepository.Table.Where(m => m.Abbreviation == Abbreviation).Count() > 0)
            {
                result = true;
            }
            return result;
        }
    }
}
