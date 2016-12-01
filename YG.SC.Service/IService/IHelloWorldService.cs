
namespace YG.SC.Service
{
    /// <summary>
    /// 接口名称：IHelloWorldService
    /// 命名空间：YG.SC.IService
    /// 接口功能：测试服务接口
    /// </summary>
    /// 创建者：孟祺宙
    /// 创建日期：2014/9/10 16:31
    /// 修改者：
    /// 修改时间：
    /// ----------------------------------------------------------------------------------------
    public interface IHelloWorldService : IYgService
    {
        /// <summary>
        /// 根据查询id返回UserName
        /// </summary>
        /// <param name="id">The id</param>
        /// <returns>
        /// The String
        /// </returns>
        /// 创建者：孟祺宙
        /// 创建日期：2014/9/10 16:34
        /// 修改者：
        /// 修改时间：
        /// ----------------------------------------------------------------------------------------
        string Hello(int id);
    }

}