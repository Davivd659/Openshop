//using System;
//using System.Linq;
//using NUnit.Framework;
//using YG.SC.Common;
//using YG.SC.DataAccess;

//namespace YG.SC.Repository.Test
//{
//    [TestFixture]
//    public class SkuGoodsRepository_Test
//    {
//        TestContext _databaseContext;
//        private IRepository<SctSkuGoods> _sctSkuGoodsRepository;



//        [SetUp]
//        public void SetUp()
//        {
//            _databaseContext = new TestContext();
//            _sctSkuGoodsRepository = new EfRepository<SctSkuGoods>(_databaseContext);
//        }

//        [Test]
//        public void GenerateGoodsNamePinYin()
//        {
//            var array = this._sctSkuGoodsRepository.Get().ToArray();
//            foreach (var item in array)
//            {
//                var pinyin = LanguageTransformation.GetPinYin(item.GoodsName);
//                item.GoodsPinYin = pinyin;
//                this._sctSkuGoodsRepository.Update(item);
//                Console.WriteLine(pinyin);
//            }
//            this._sctSkuGoodsRepository.SaveChanges();
//        }

//    }
//}
