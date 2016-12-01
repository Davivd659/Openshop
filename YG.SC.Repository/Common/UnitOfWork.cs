
namespace YG.SC.Repository
{
    using System.Data.Entity;
    using System.Transactions;
    using System;

    /// <summary>
    /// 类名称：UnitOfWork
    /// 命名空间：YG.SC.IRepository
    /// 类功能：
    /// </summary>
    /// 创建者：孟祺宙
    /// 创建日期：2014/9/11 11:25
    /// 修改者：
    /// 修改时间：
    /// ----------------------------------------------------------------------------------------
    public class UnitOfWork : IUnitOfWork
    {
        /// <summary>
        /// 字段_dbContext
        /// </summary>
        /// 创建者：孟祺宙
        /// 创建日期：2014/9/15 15:05
        /// 修改者：
        /// 修改时间：
        /// ----------------------------------------------------------------------------------------
        private readonly DbContext _dbContext;

        /// <summary>
        /// 字段transaction
        /// </summary>
        /// 创建者：孟祺宙
        /// 创建日期：2014/9/11 11:25
        /// 修改者：
        /// 修改时间：
        /// ----------------------------------------------------------------------------------------
        private TransactionScope _transaction;

        /// <summary>
        /// Initializes a new instance of the <see cref="UnitOfWork"/> class.
        /// </summary>
        /// <param name="dbContext">The dbContext</param>
        /// 创建者：孟祺宙
        /// 创建日期：2014/9/15 15:05
        /// 修改者：
        /// 修改时间：
        /// ----------------------------------------------------------------------------------------
        public UnitOfWork(DbContext dbContext)
        {
            _dbContext = dbContext;
        }

        /// <summary>
        /// 事物开始
        /// </summary>
        /// 创建者：孟祺宙
        /// 创建日期：2014/9/11 11:21
        /// 修改者：
        /// 修改时间：
        /// ----------------------------------------------------------------------------------------
        public void StartTransaction()
        {
            _transaction = new TransactionScope();
        }

        /// <summary>
        /// 提交事物
        /// </summary>
        /// 创建者：孟祺宙
        /// 创建日期：2014/9/11 11:21
        /// 修改者：
        /// 修改时间：
        /// ----------------------------------------------------------------------------------------
        public void CommitTransaction()
        {
            _dbContext.SaveChanges();
            _transaction.Complete();
        }

        /// <summary>
        /// 字段disposed
        /// </summary>
        /// 创建者：孟祺宙 创建日期：2014/5/28 15:28
        /// 修改者：
        /// 修改时间：
        /// ----------------------------------------------------------------------------------------
        private bool _disposed;

        /// <summary>
        /// The Void
        /// </summary>
        /// <param name="disposing"><c>true</c> to release both managed and unmanaged resources; <c>false</c> to release only unmanaged resources.</param>
        /// 创建者：孟祺宙 创建日期：2014/5/28 15:28
        /// 修改者：
        /// 修改时间：
        /// ----------------------------------------------------------------------------------------
        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed && disposing)
            {
                _dbContext.Dispose();
            }
            _disposed = true;
        }

        /// <summary>
        /// 执行与释放或重置非托管资源相关的应用程序定义的任务。
        /// </summary>
        /// 创建者：孟祺宙 创建日期：2014/5/28 15:28
        /// 修改者：
        /// 修改时间：
        /// ----------------------------------------------------------------------------------------
        public void Dispose()
        {
            if (_transaction != null) _transaction.Dispose();
            Dispose(true);
            GC.SuppressFinalize(this);
        }

    }
}
