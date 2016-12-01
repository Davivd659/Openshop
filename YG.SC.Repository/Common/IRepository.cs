
namespace YG.SC.Repository
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;

    /// <summary>
    /// 接口名称：Repository
    /// 命名空间：YG.SC.Repository
    /// 接口功能：
    /// </summary>
    /// <typeparam name="TEntity">The type of the entity.</typeparam>
    /// 创建者：孟祺宙
    /// 创建日期：2014/9/10 16:44
    /// 修改者：
    /// 修改时间：
    /// ----------------------------------------------------------------------------------------
    public interface IRepository<TEntity> : IDisposable where TEntity : class
    {
        /// <summary>
        /// Gets the by identifier.
        /// </summary>
        /// <param name="id">The id</param>
        /// <returns>
        /// `0
        /// </returns>
        /// 创建者：孟祺宙
        /// 创建日期：2014/9/10 16:44
        /// 修改者：
        /// 修改时间：
        /// ----------------------------------------------------------------------------------------
        TEntity GetById(object id);

        /// <summary>
        /// Finds the single by expression.
        /// </summary>
        /// <param name="expression">The expression</param>
        /// <returns>
        /// `0
        /// </returns>
        /// 创建者：孟祺宙
        /// 创建日期：2014/9/10 17:26
        /// 修改者：
        /// 修改时间：
        /// ----------------------------------------------------------------------------------------
        TEntity FindSingleByExpression(Expression<Func<TEntity, bool>> expression);

        /// <summary>
        /// The method will
        /// </summary>
        /// <param name="entity">The entity</param>
        /// 创建者：孟祺宙
        /// 创建日期：2014/9/10 16:44
        /// 修改者：
        /// 修改时间：
        /// ----------------------------------------------------------------------------------------
        void Insert(TEntity entity);

        /// <summary>
        /// The method will
        /// </summary>
        /// <param name="entity">The entity</param>
        /// 创建者：孟祺宙
        /// 创建日期：2014/9/10 16:44
        /// 修改者：
        /// 修改时间：
        /// ----------------------------------------------------------------------------------------
        void Update(TEntity entity);

        /// <summary>
        /// The method will 
        /// </summary>
        /// <param name="id">The id</param>
        /// 创建者：孟祺宙
        /// 创建日期：2014/9/10 17:19
        /// 修改者：
        /// 修改时间：
        /// ----------------------------------------------------------------------------------------
        void Delete(object id);

        /// <summary>
        /// The method will
        /// </summary>
        /// <param name="entity">The entity</param>
        /// 创建者：孟祺宙
        /// 创建日期：2014/9/10 16:44
        /// 修改者：
        /// 修改时间：
        /// ----------------------------------------------------------------------------------------
        void Delete(TEntity entity);

        /// <summary>
        /// Gets the table.
        /// </summary>
        /// <value>
        /// The table.
        /// </value>
        /// 创建者：孟祺宙
        /// 创建日期：2014/9/10 16:44
        /// 修改者：
        /// 修改时间：
        /// ----------------------------------------------------------------------------------------
        IQueryable<TEntity> Table { get; }

        /// <summary>
        /// The method will 
        /// </summary>
        /// <param name="filter">The filter</param>
        /// <param name="orderBy">The orderBy</param>
        /// <param name="includeProperties">The includeProperties</param>
        /// <returns>
        /// The IQueryable{`0}
        /// </returns>
        /// 创建者：孟祺宙
        /// 创建日期：2014/9/10 17:23
        /// 修改者：
        /// 修改时间：
        /// ----------------------------------------------------------------------------------------
        IEnumerable<TEntity> Get(Expression<Func<TEntity, bool>> filter = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, string includeProperties = "");

        /// <summary>
        /// Executes the SQL query.
        /// </summary>
        /// <param name="sql">The sql</param>
        /// <param name="parameters">The parameters</param>
        /// <returns>
        /// IEnumerable{`0}
        /// </returns>
        /// 创建者：孟祺宙
        /// 创建日期：2014/9/11 13:18
        /// 修改者：
        /// 修改时间：
        /// ----------------------------------------------------------------------------------------
        IEnumerable<TEntity> ExecuteSqlQuery(string sql, params object[] parameters);

        /// <summary>
        /// Saves the changes.
        /// </summary>
        /// 创建者：孟祺宙
        /// 创建日期：2014/9/11 11:22
        /// 修改者：
        /// 修改时间：
        /// ----------------------------------------------------------------------------------------
        void SaveChanges();
    }
}