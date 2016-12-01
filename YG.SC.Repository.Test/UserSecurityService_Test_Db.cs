//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Xml;
//using System.Xml.Serialization;
//using NUnit.Framework;
//using YG.SC.Common;
//using YG.SC.DataAccess;
//using YG.SC.Model;
//using System.IO;

//namespace YG.SC.Repository.Test
//{
//    [TestFixture]
//    public class UserSecurityService_Test_Db
//    {
//        TestContext _databaseContext;
//        private IRepository<ScmUser> _scmUserRepository;
//        private IRepository<SccUserInRole> _sccUserInRoleRepository;
//        private IRepository<SccActionInRole> _sccActionInRoleRepository;
//        private IRepository<SccAction> _sccActionRepository;
//        private IRepository<SctPath> _sctPathRepository;
//        private IRepository<SctApplication> _sctApplicationRepository;


//        [SetUp]
//        public void SetUp()
//        {
//            _databaseContext = new TestContext();
//            _scmUserRepository = new EfRepository<ScmUser>(_databaseContext);
//            _sccUserInRoleRepository = new EfRepository<SccUserInRole>(_databaseContext);
//            _sccActionInRoleRepository = new EfRepository<SccActionInRole>(_databaseContext);
//            _sccActionRepository = new EfRepository<SccAction>(_databaseContext);
//            _sctPathRepository = new EfRepository<SctPath>(_databaseContext);
//            _sctApplicationRepository = new EfRepository<SctApplication>(_databaseContext);
//        }

//        /// <summary>
//        /// 生成XML
//        /// </summary>
//        /// 创建者：孟祺宙
//        /// 创建日期：2014/9/18 15:19
//        /// 修改者：
//        /// 修改时间：
//        /// ----------------------------------------------------------------------------------------
//        [Test]
//        public void UserSecurity_XML_01()
//        {
//            G_Role_1();
//            G_Role_2();
//            Console.WriteLine("success");
//        }

//        private void G_Role_1()
//        {
//            const string userName = "test123";
//           var ttt= this._scmUserRepository.Get().ToArray();
//            var userId = this._scmUserRepository.FindSingleByExpression(item => item.LoginName == userName).Id;
//            var roleIds = this._sccUserInRoleRepository.Get(item => item.UserId == userId).Select(item => item.RoleId).ToArray();
//            var actionIds = this._sccActionInRoleRepository.Get(item => roleIds.Contains(item.RoleId)).Select(item => item.ActionId).ToArray();

//            var actionInPaths = this._sccActionRepository.Get(item => actionIds.Contains(item.Id)).ToArray();
//            var paths = actionInPaths.Select(item => item.SctPath).ToArray();
//            var appIds = paths.Select(item => item.ApplicationId).ToArray();
//            var apps = this._sctApplicationRepository.Get(item => appIds.Contains(item.Id)).ToArray();

//            var appParentIds = apps.Select(item => item.ParentId).ToArray();
//            var appParents = this._sctApplicationRepository.Get(item => appParentIds.Contains(item.Id)).Distinct().OrderBy(item => item.OrdSeq).ToArray();

//            var q = from p in appParents
//                    select new ScRoleNormalFirstMenu
//                    {
//                        Id = p.Id.ToString(),
//                        Name = p.ApplicationName,
//                        Menu = apps.Where(item => item.ParentId == p.Id).Select(item => new ScRoleNormalFirstMenuMenu
//                        {
//                            Id = item.Id.ToString(),
//                            Name = item.ApplicationName,
//                            Page = paths.Where(k => k.ApplicationId == item.Id).Select(i => new ScRoleNormalFirstMenuMenuPage
//                            {
//                                Id = i.Id.ToString(),
//                                AId = actionInPaths.First(k => k.PathId == i.Id).Id.ToString(),
//                                Url = i.PathUrl,
//                                Name = i.PathName
//                            }).ToArray()
//                        }).ToArray()
//                    };
//            var scRoleNormal = new RoleSecurityEntity { Items = q.ToArray() };

//            using (var fsWriter = new FileStream(@"c:\SENIOR_1.xml", FileMode.Create, FileAccess.Write))
//            {

//                var xs = new XmlSerializer(typeof(RoleSecurityEntity));
//                xs.Serialize(fsWriter, scRoleNormal);
//            }

//        }
//        private void G_Role_2()
//        {
//            const string userName = "admin123";
//            var userId = this._scmUserRepository.FindSingleByExpression(item => item.LoginName == userName).Id;
//            var roleIds = this._sccUserInRoleRepository.Get(item => item.UserId == userId).Select(item => item.RoleId).ToArray();
//            var actionIds = this._sccActionInRoleRepository.Get(item => roleIds.Contains(item.RoleId)).Select(item => item.ActionId).ToArray();

//            var actionInPaths = this._sccActionRepository.Get(item => actionIds.Contains(item.Id)).ToArray();
//            var paths = actionInPaths.Select(item => item.SctPath).ToArray();
//            var appIds = paths.Select(item => item.ApplicationId).ToArray();
//            var apps = this._sctApplicationRepository.Get(item => appIds.Contains(item.Id)).ToArray();

//            var appParentIds = apps.Select(item => item.ParentId).ToArray();
//            var appParents = this._sctApplicationRepository.Get(item => appParentIds.Contains(item.Id)).Distinct().OrderBy(item => item.OrdSeq).ToArray();

//            var q = from p in appParents
//                    select new ScRoleNormalFirstMenu
//                    {
//                        Id = p.Id.ToString(),
//                        Name = p.ApplicationName,
//                        Menu = apps.Where(item => item.ParentId == p.Id).Select(item => new ScRoleNormalFirstMenuMenu
//                        {
//                            Id = item.Id.ToString(),
//                            Name = item.ApplicationName,
//                            Page = paths.Where(k => k.ApplicationId == item.Id).Select(i => new ScRoleNormalFirstMenuMenuPage
//                            {
//                                Id = i.Id.ToString(),
//                                AId = actionInPaths.First(k => k.PathId == i.Id).Id.ToString(),
//                                Url = i.PathUrl,
//                                Name = i.PathName
//                            }).ToArray()
//                        }).ToArray()
//                    };
//            var scRoleNormal = new RoleSecurityEntity { Items = q.ToArray() };

//            using (var fsWriter = new FileStream(@"c:\SENIOR_2.xml", FileMode.Create, FileAccess.Write))
//            {

//                var xs = new XmlSerializer(typeof(RoleSecurityEntity));
//                xs.Serialize(fsWriter, scRoleNormal);
//            }

//        }
//    }
//}
