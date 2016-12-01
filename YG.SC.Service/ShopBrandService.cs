using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using YG.SC.Common;
using YG.SC.DataAccess;
using YG.SC.Model;
using YG.SC.Repository;
using YG.SC.Service.IService;

namespace YG.SC.Service
{
    public class ShopBrandService : IShopBrandService
    {

        private readonly IRepository<ShopBrand> _ShopBrandRepository;
        private readonly IRepository<ShopBrandBranch> _ShopBrandBranchRepository;
        private readonly IRepository<ShopBrandPhoto> _ShopBrandPhotoRepository;
        private readonly IRepository<ShopBrandAttributeValues> _ShopBrandAttributeValuesRepository;
        private readonly IRepository<ApplyBrand> _ApplyBrandRepository;
        private readonly IRepository<Customer> _CustomerRepository;
        public ShopBrandService(IRepository<ShopBrand> shopBrandRepository, IRepository<ShopBrandBranch> shopBrandBranchRepository, IRepository<ShopBrandPhoto> shopBrandPhotoRepository, IRepository<ShopBrandAttributeValues> shopBrandAttributeValuesRepository, IRepository<ApplyBrand> applyBrandRepository, IRepository<Customer> customerRepository)
        {
            _ShopBrandRepository = shopBrandRepository;
            _ShopBrandBranchRepository = shopBrandBranchRepository;
            _ShopBrandPhotoRepository = shopBrandPhotoRepository;
            _ShopBrandAttributeValuesRepository = shopBrandAttributeValuesRepository;
            _ApplyBrandRepository = applyBrandRepository;
            _CustomerRepository = customerRepository;
        }

        public void Dispose()
        {
            this._ShopBrandRepository.Dispose();
            this._ShopBrandBranchRepository.Dispose();
            this._ShopBrandPhotoRepository.Dispose();
            this._ShopBrandAttributeValuesRepository.Dispose();
        }
        public Tuple<DataAccess.ShopBrand[], Model.PagerEntity> GetEntitsByName(int pg, int ShopBrandId, string Name)
        {
            const int top = 10;
            var idx = (pg - 1) < 0 ? 0 : (pg - 1);

            Expression<Func<ShopBrand, bool>> expressionFilter = (entity) => (ShopBrandId == 0 || entity.Id == ShopBrandId) && (string.IsNullOrEmpty(Name) || entity.Name.Contains(Name)) && (entity.Recsts != -1);
            var total = this._ShopBrandRepository.Get(expressionFilter).Count();
            var array = this._ShopBrandRepository.Get(expressionFilter, orderBy: item => item.OrderByDescending(p => p.Id)).Skip(top * idx).Take(top).ToArray();
            return Tuple.Create(array, new PagerEntity { Total = total, PageIndex = idx + 1, Top = top });

        }
        /// <summary>
        /// 前端页面
        /// </summary>
        /// <param name="pg"></param>
        /// <param name="AttributeId"></param>
        /// <returns></returns>
        public Tuple<ShopBrand[], PagerEntity> GetEntits(int pg, int AttributeValuesId, int JionIn)
        {
            const int top = 10;
            var idx = (pg - 1) < 0 ? 0 : (pg - 1);
            #region// 两个 AttributeValuesId情况
            //List<ShopBrand> lists = new List<ShopBrand>();
            // List<ShopBrandAttributeValues> listatt = new List<ShopBrandAttributeValues>();
            //foreach (var i in AttributeValuesIds)
            //{
            //    if (lists.Count<=0)
            //    {
            //        this._ShopBrandAttributeValuesRepository.Get(item => item.AttributeValuesId == i)
            //            .OrderByDescending(item => item.BrandId)
            //            .ToList()
            //            .ForEach(item => listatt.Add(item));
            //    }
            //    else
            //    {
            //        listatt.ForEach(item =>
            //        {
            //            if (item.AttributeValuesId!=i)
            //            {
            //                listatt.Remove(item);
            //            }
            //        })
            //    }
            //}
            #endregion

            int total;
            ShopBrand[] array;
            if (AttributeValuesId == 0)
            {
                Expression<Func<ShopBrand, bool>> expressionFilter = (entity) => (entity.Recsts != -1);
                total = this._ShopBrandRepository.Get(expressionFilter).Count();
                array = this._ShopBrandRepository.Get(expressionFilter, orderBy: item => item.OrderByDescending(p => p.Id)).Skip(top * idx).Take(top).ToArray();
                //return Tuple.Create(array, new PagerEntity { Total = total, PageIndex = idx + 1, Top = top });
            }
            else
            {
                var model = this._ShopBrandAttributeValuesRepository.Get(item => item.AttributeValuesId == AttributeValuesId).OrderByDescending(item => item.Id).ToList();
                List<ShopBrand> list = new List<ShopBrand>();
                foreach (var item in model)
                {
                    list.Add(item.ShopBrand);
                }
                total = list.Count();
                array = list.Skip(top * idx).Take(top).ToArray();
                //return Tuple.Create(array, new PagerEntity { Total = total, PageIndex = idx + 1, Top = top });

            }
            if (JionIn != -1)
            {
                bool b = JionIn == 0 ? true : false;

                array = array.Where(item => item.JoinIn == b).ToArray();
                total = array.Length;
            }
            return Tuple.Create(array, new PagerEntity { Total = total, PageIndex = idx + 1, Top = top });

        }


        public Tuple<ShopBrand[], PagerEntity> GetEntits(int pg, int AttributeValuesId, string Name)
        {
            const int top = 10;
            var idx = (pg - 1) < 0 ? 0 : (pg - 1);

            int total;
            ShopBrand[] array;
            if (AttributeValuesId == 0)
            {
                var model = this._ShopBrandRepository.Table.Where(item => item.Recsts != -1);

                if (!string.IsNullOrEmpty(Name))
                {
                    model = model.Where(m => m.Name == Name);
                }

                total = model.Count();
                array = model.OrderByDescending(m => m.Id).Skip(top * idx).Take(top).ToArray();
            }
            else
            {
                var model = this._ShopBrandAttributeValuesRepository.Get(item => item.AttributeValuesId == AttributeValuesId).OrderByDescending(item => item.Id).ToList();
                List<ShopBrand> list = new List<ShopBrand>();
                foreach (var item in model)
                {
                    list.Add(item.ShopBrand);
                }
                total = list.Count();
                array = list.Skip(top * idx).Take(top).ToArray();

            }

            return Tuple.Create(array, new PagerEntity { Total = total, PageIndex = idx + 1, Top = top });

        }


        public DataAccess.ShopBrand GetById(int id)
        {
            return this._ShopBrandRepository.GetById(id);
        }

        public void Update(DataAccess.ShopBrand sb)
        {
            this._ShopBrandRepository.Update(sb);
            this._ShopBrandRepository.SaveChanges();
        }

        public void Insert(DataAccess.ShopBrand sb)
        {
            this._ShopBrandRepository.Insert(sb);
            this._ShopBrandRepository.SaveChanges();
        }


        /// <summary>
        /// 获取品牌下的分店列表
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Tuple<DataAccess.ShopBrandBranch[], Model.PagerEntity> GetBranchByBrandId(int pg, int id, string name)
        {
            //return this._ShopBrandBranchRepository.Get(item => item.BrandId == id && item.Recsts == 1).ToList();
            const int top = 10;
            var idx = (pg - 1) < 0 ? 0 : (pg - 1);

            Expression<Func<ShopBrandBranch, bool>> expressionFilter = (entity) => (id == 0 || entity.BrandId == id) && (string.IsNullOrEmpty(name) || entity.BranchName.Contains(name)) && (entity.Recsts != -1);
            var total = this._ShopBrandBranchRepository.Get(expressionFilter).Count();
            var array = this._ShopBrandBranchRepository.Get(expressionFilter, orderBy: item => item.OrderByDescending(p => p.Id)).Skip(top * idx).Take(top).ToArray();
            return Tuple.Create(array, new PagerEntity { Total = total, PageIndex = idx + 1, Top = top });
        }

        public ShopBrandBranch GetBranchById(int id)
        {
            return this._ShopBrandBranchRepository.GetById(id);
        }

        public void BranchUpdate(ShopBrandBranch sb)
        {
            this._ShopBrandBranchRepository.Update(sb);
            this._ShopBrandBranchRepository.SaveChanges();
        }

        public void BranchInsert(ShopBrandBranch sb)
        {
            this._ShopBrandBranchRepository.Insert(sb);
            this._ShopBrandBranchRepository.SaveChanges();
        }


        public Tuple<ShopBrandPhoto[], PagerEntity> GetPhotoByBrandId(int pg, int id, string name)
        {
            const int top = 10;
            var idx = (pg - 1) < 0 ? 0 : (pg - 1);

            Expression<Func<ShopBrandPhoto, bool>> expressionFilter = (entity) => (id == 0 || entity.BrandId == id) && (entity.Recsts != -1);
            var total = this._ShopBrandPhotoRepository.Get(expressionFilter).Count();
            var array = this._ShopBrandPhotoRepository.Get(expressionFilter, orderBy: item => item.OrderByDescending(p => p.Id)).Skip(top * idx).Take(top).ToArray();
            return Tuple.Create(array, new PagerEntity { Total = total, PageIndex = idx + 1, Top = top });
        }

        public ShopBrandPhoto GetPhotoById(int id)
        {
            return this._ShopBrandPhotoRepository.GetById(id);
        }

        public void UpdatePhoto(ShopBrandPhoto sb)
        {
            this._ShopBrandPhotoRepository.Update(sb);
            this._ShopBrandPhotoRepository.SaveChanges();
        }

        public void InsertPhoto(ShopBrandPhoto sb)
        {
            this._ShopBrandPhotoRepository.Insert(sb);
            this._ShopBrandPhotoRepository.SaveChanges();
        }

        /// <summary>
        /// 获取申请状态。
        /// </summary>
        /// <returns></returns>
        public Dictionary<int, string> GetApplyStatusList()
        {
            return CommonEnum.GetDictionaryFromEnum(typeof(CommonEnum.StatusOfBrandApply));
        }

        /// <summary>
        /// 获取申请列表。
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        public Tuple<ApplyBrand[], PagerEntity> GetApplyBrandList(BrandApplyFilter filter)
        {
            DateTime? right = filter.ApplyTimeRight.HasValue ? (DateTime?)filter.ApplyTimeRight.Value.AddDays(1) : null;
            var query = (from a in _ApplyBrandRepository.Table
                         from b in _ShopBrandRepository.Table
                         where a.BrandId == b.Id
                         && a.ApplyUser == filter.ApplyUserId
                         && (filter.ApplyTimeLeft.HasValue ? a.ApplyTime >= filter.ApplyTimeLeft : true)
                         && (right.HasValue ? a.ApplyTime < right : true)
                         && (filter.Status.HasValue && filter.Status.Value >= 0 ? (CommonEnum.StatusOfBrandApply)a.Status == filter.Status : true)
                         && (string.IsNullOrEmpty(filter.BrandName) ? true : b.Name.Contains(filter.BrandName))
                         select a);
            query = query.OrderByDescending(m => m.Id);
            return filter.GetPagerData(query);
        }

        /// <summary>
        /// 获取审批列表。
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        public Tuple<ApplyBrand[], PagerEntity> GetAuditBrandList(BrandApplyFilter filter)
        {
            DateTime? right = filter.ApplyTimeRight.HasValue ? (DateTime?)filter.ApplyTimeRight.Value.AddDays(1) : null;
            var query = (from a in _ApplyBrandRepository.Table
                         from b in _ShopBrandRepository.Table
                         from c in _CustomerRepository.Table
                         where a.BrandId == b.Id
                         && b.Id == c.Companyid
                         && c.Id == filter.AuditUserId
                         && (filter.ApplyTimeLeft.HasValue ? a.ApplyTime >= filter.ApplyTimeLeft : true)
                         && (right.HasValue ? a.ApplyTime < right : true)
                         && (filter.Status.HasValue && filter.Status.Value >= 0 ? (CommonEnum.StatusOfBrandApply)a.Status == filter.Status : true)
                         && (string.IsNullOrEmpty(filter.Key) ? true : (a.Contract.Contains(filter.Key) || a.Phone.Contains(filter.Key)))
                         select a);
            query = query.OrderByDescending(m => m.Id);
            return filter.GetPagerData(query);
        }


        public void PassApply(int passApplyId)
        {
            ApplyBrand ab = _ApplyBrandRepository.GetById(passApplyId);
            ab.Status = (int)CommonEnum.StatusOfBrandApply.Pass;
            ab.AuditTime = DateTime.Now;
            _ApplyBrandRepository.Update(ab);
            _ApplyBrandRepository.SaveChanges();
        }

        public void RejectApply(int rejectApplyId, string rejectReason)
        {
            ApplyBrand ab = _ApplyBrandRepository.GetById(rejectApplyId);
            ab.Status = (int)CommonEnum.StatusOfBrandApply.Reject;
            ab.RejectReason = rejectReason;
            ab.AuditTime = DateTime.Now;
            _ApplyBrandRepository.Update(ab);
            _ApplyBrandRepository.SaveChanges();
        }


        public bool GetEntitsByName(string name)
        {
            bool result = false;
            if (_ShopBrandRepository.Table.Where(m => m.Name == name).Count() > 0)
            {
                result = true;
            }
            return result;
        }


        public bool GetEntitsByAbbreviation(string Abbreviation)
        {
            bool result = false;
            if (_ShopBrandRepository.Table.Where(m => m.Abbreviation == Abbreviation).Count() > 0)
            {
                result = true;
            }
            return result;
        }
    }
}
