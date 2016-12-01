
namespace YG.SC.Repository
{
    using System.Data.Entity;
    using YG.SC.DataAccess;

    /// <summary>
    /// 类名称：HelloWorldRepository
    /// 命名空间：YG.SC.Repository
    /// 类功能：
    /// </summary>
    /// 创建者：孟祺宙
    /// 创建日期：2014/9/15 15:20
    /// 修改者：
    /// 修改时间：
    /// ----------------------------------------------------------------------------------------
    public class HelloWorldRepository : EfRepository<HelloWorld>, IHelloWorldRepository
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="HelloWorldRepository"/> class.
        /// </summary>
        /// <param name="dbContext">The dbContext</param>
        /// 创建者：孟祺宙
        /// 创建日期：2014/9/15 15:20
        /// 修改者：
        /// 修改时间：
        /// ----------------------------------------------------------------------------------------
        public HelloWorldRepository(DbContext dbContext)
            : base(dbContext)
        {

        }

        /// <summary>
        /// 根据ID获取实体
        /// </summary>
        /// <param name="id">The id</param>
        /// <returns>
        /// The HelloWorld
        /// </returns>
        /// 创建者：孟祺宙
        /// 创建日期：2014/9/15 15:15
        /// 修改者：
        /// 修改时间：
        /// ----------------------------------------------------------------------------------------
        public HelloWorld Get(int id)
        {
            return GetById(id);
        }

        /// <summary>
        /// 插入实体返回id
        /// </summary>
        /// <param name="helloWorld">The helloWorld</param>
        /// <returns>
        /// The Int32
        /// </returns>
        /// 创建者：孟祺宙
        /// 创建日期：2014/9/15 16:48
        /// 修改者：
        /// 修改时间：
        /// ----------------------------------------------------------------------------------------
        public int InsertEntity(HelloWorld helloWorld)
        {
            base.Insert(helloWorld);
            return helloWorld.Id;
        }

        /// <summary>
        /// 释放服务使用中的资源
        /// </summary>
        /// 创建者：孟祺宙
        /// 创建日期：2014/9/15 15:32
        /// 修改者：
        /// 修改时间：
        /// ----------------------------------------------------------------------------------------
        public void DisposeRepository()
        {
            Dispose();
        }


    }
}
