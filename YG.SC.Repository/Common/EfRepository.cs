
namespace YG.SC.Repository
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;
    using System.Linq.Expressions;

    /// <summary>
    /// 类名称：EfRepository
    /// 命名空间：YG.SC.IRepository
    /// 类功能：
    /// </summary>
    /// <typeparam name="TEntity">The type of the entity.</typeparam>
    /// 创建者：孟祺宙
    /// 创建日期：2014/9/10 16:45
    /// 修改者：
    /// 修改时间：
    /// ----------------------------------------------------------------------------------------
    public class EfRepository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        /// <summary>
        /// 字段_context
        /// </summary>
        /// 创建者：孟祺宙
        /// 创建日期：2014/9/10 17:15
        /// 修改者：
        /// 修改时间：
        /// ----------------------------------------------------------------------------------------
        private readonly DbContext _context;

        /// <summary>
        /// 字段_dbSet
        /// </summary>
        /// 创建者：孟祺宙
        /// 创建日期：2014/9/10 17:02
        /// 修改者：
        /// 修改时间：
        /// ----------------------------------------------------------------------------------------
        private readonly DbSet<TEntity> _dbSet;

        /// <summary>
        /// Initializes a new instance of the <see cref="EfRepository{T}" /> class.
        /// </summary>
        /// <param name="dbContext">The dbContext</param>
        /// 创建者：孟祺宙
        /// 创建日期：2014/9/10 17:02
        /// 修改者：
        /// 修改时间：
        /// ----------------------------------------------------------------------------------------
        public EfRepository(DbContext dbContext)
        {
            _context = dbContext;
            _dbSet = _context.Set<TEntity>();
        }

        /// <summary>
        /// Executes the SQL query.
        /// </summary>
        /// <param name="sql">The sql</param>
        /// <param name="parameters">The parameters</param>
        /// <returns>
        /// IEnumerable{`0}
        /// </returns>
        /// 创建者：孟祺宙
        /// 创建日期：2014/9/11 13:22
        /// 修改者：
        /// 修改时间：
        /// ----------------------------------------------------------------------------------------
        public IEnumerable<TEntity> ExecuteSqlQuery(string sql, params object[] parameters)
        {
            return _dbSet.SqlQuery(sql, parameters);
        }

        /// <summary>
        /// Gets the by identifier.
        /// </summary>
        /// <param name="id">The id</param>
        /// <returns>
        /// `0
        /// </returns>
        /// 创建者：孟祺宙
        /// 创建日期：2014/9/10 17:09
        /// 修改者：
        /// 修改时间：
        /// ----------------------------------------------------------------------------------------
        public TEntity GetById(object id)
        {
            return _dbSet.Find(id);
        }

        /// <summary>
        /// The method will 
        /// </summary>
        /// <param name="entity">The entity</param>
        /// 创建者：孟祺宙
        /// 创建日期：2014/9/10 17:19
        /// 修改者：
        /// 修改时间：
        /// ----------------------------------------------------------------------------------------
        public void Insert(TEntity entity)
        {
            _dbSet.Add(entity);
        }

        /// <summary>
        /// Inserts the range.
        /// </summary>
        /// <param name="entities">The entities</param>
        /// 创建者：孟祺宙
        /// 创建日期：2014/10/30 11:26
        /// 修改者：
        /// 修改时间：
        /// ----------------------------------------------------------------------------------------
        public void InsertRange(IEnumerable<TEntity> entities)
        {
            _dbSet.AddRange(entities);
        }

        /// <summary>
        /// The method will 
        /// </summary>
        /// <param name="entity">The entity</param>
        /// 创建者：孟祺宙
        /// 创建日期：2014/9/10 17:20
        /// 修改者：
        /// 修改时间：
        /// ----------------------------------------------------------------------------------------
        public void Update(TEntity entity)
        {
            _dbSet.Attach(entity);
            _context.Entry(entity).State = EntityState.Modified;
        }

        /// <summary>
        /// The method will 
        /// </summary>
        /// <param name="id">The id</param>
        /// 创建者：孟祺宙
        /// 创建日期：2014/9/10 17:20
        /// 修改者：
        /// 修改时间：
        /// ----------------------------------------------------------------------------------------
        public void Delete(object id)
        {
            var entity = _dbSet.Find(id);
            Delete(entity);
        }

        /// <summary>
        /// The method will 
        /// </summary>
        /// <param name="entity">The entity</param>
        /// 创建者：孟祺宙
        /// 创建日期：2014/9/10 17:20
        /// 修改者：
        /// 修改时间：
        /// ----------------------------------------------------------------------------------------
        public void Delete(TEntity entity)
        {
            if (_context.Entry(entity).State == EntityState.Detached)
            {
                _dbSet.Attach(entity);
            }
            _dbSet.Remove(entity);
        }

        /// <summary>
        /// Deletes the range.
        /// </summary>
        /// <param name="entities">The entities</param>
        /// 创建者：孟祺宙
        /// 创建日期：2014/10/30 11:30
        /// 修改者：
        /// 修改时间：
        /// ----------------------------------------------------------------------------------------
        public void DeleteRange(IEnumerable<TEntity> entities)
        {
            var enumerable = entities as TEntity[] ?? entities.ToArray();
            foreach (var entity in enumerable.Where(entity => _context.Entry(entity).State == EntityState.Detached))
            {
                _dbSet.Attach(entity);
            }
            _dbSet.RemoveRange(enumerable);
        }



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
        public TEntity FindSingleByExpression(Expression<Func<TEntity, bool>> expression)
        {
            return _dbSet.FirstOrDefault(expression);
        }

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
        /// 创建日期：2014/9/10 17:24
        /// 修改者：
        /// 修改时间：
        /// ----------------------------------------------------------------------------------------
        public IEnumerable<TEntity> Get(Expression<Func<TEntity, bool>> filter = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, string includeProperties = "")
        {
            IQueryable<TEntity> query = _dbSet;

            if (filter != null)
            {
                query = query.Where(filter);
            }

            query = includeProperties.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries).Aggregate(query, (current, includeProperty) => current.Include(includeProperty));
           
            return orderBy != null ? orderBy(query).ToList() : query.ToList();
        }

        /// <summary>
        /// Gets the table.
        /// </summary>
        /// <value>
        /// The table.
        /// </value>
        /// 创建者：孟祺宙
        /// 创建日期：2014/9/10 17:21
        /// 修改者：
        /// 修改时间：
        /// ----------------------------------------------------------------------------------------
        public IQueryable<TEntity> Table
        {
            get { return _dbSet; }
        }


        /// <summary>
        /// Saves the changes.
        /// </summary>
        /// 创建者：孟祺宙
        /// 创建日期：2014/9/11 11:22
        /// 修改者：
        /// 修改时间：
        /// ----------------------------------------------------------------------------------------
        public void SaveChanges()
        {
            _context.SaveChanges();
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
                _context.Dispose();
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
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
