
namespace YG.SC.Repository
{
    using YG.SC.DataAccess;

    /// <summary>
    /// 接口名称：IHelloWorldRepository
    /// 命名空间：YG.SC.Repository
    /// 接口功能：
    /// </summary>
    /// 创建者：孟祺宙
    /// 创建日期：2014/9/15 15:16
    /// 修改者：
    /// 修改时间：
    /// ----------------------------------------------------------------------------------------
    public interface IHelloWorldRepository : IDisposableRepository
    {
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
        HelloWorld Get(int id);

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
        int InsertEntity(HelloWorld helloWorld);
    }
}
