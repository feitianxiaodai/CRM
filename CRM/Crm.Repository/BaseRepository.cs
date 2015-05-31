using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crm.Repository
{
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Linq.Expressions;

    public class BaseRepository<TEntity> :Crm.IRepository.IBaseRepository<TEntity> where TEntity : class
    {
        //1.0  实例化EF上下文
        BaseDbContext db = new BaseDbContext();

        //2.0 定义DbSet<T> 对象
        DbSet<TEntity> _dbset;

        //3.0 在构造函数的初始化_dbset
        public BaseRepository()
        {
            _dbset = db.Set<TEntity>();
        }

        /// <summary>
        ///会影响框架的灵活性
        /// </summary>
        public DbSet<TEntity> DbSet
        {
            get
            {
                return _dbset;
            }
        }

        #region 1.0 增

        public virtual void Add(TEntity model)
        {
            //1.0 参数合法性验证
            if (model == null)
            {
                throw new Exception("BaseRepository泛型类中，新增操作的实体不能为空");
            }


            //2.0 进行新增操作
            _dbset.Add(model);
        }


        #endregion

        #region 2.0 删

        public virtual void Delete(TEntity model, bool isAddedDbContext)
        {
            //1.0 参数合法性验证
            if (model == null)
            {
                throw new Exception("BaseRepository泛型类中，删除操作的实体不能为空");
            }

            //2.0 进行删除逻辑处理
            if (!isAddedDbContext)
            {
                _dbset.Attach(model);
            }

            _dbset.Remove(model);
        }


        #endregion

        #region 3.0 改

        /// <summary>
        /// 编辑，约定model 是一个自定义的实体，没有追加到EF容器中的
        /// </summary>
        /// <param name="model"></param>
        public virtual void Edit(TEntity model, string[] propertyNames)
        {
            //0.0 关闭EF的实体属性合法性检查
            db.Configuration.ValidateOnSaveEnabled = false;

            //1.0 参数合法性验证
            if (model == null)
            {
                throw new Exception("BaseRepository泛型类中，编辑操作的实体不能为空");
            }

            if (propertyNames == null || propertyNames.Length == 0)
            {
                throw new Exception("BaseRepository泛型类中，编辑操作的属性数组必须至少有一个值");
            }

            //2.0 将model追加到EF容器中的
            DbEntityEntry entry = db.Entry(model);
            entry.State = System.Data.EntityState.Unchanged;

            foreach (var item in propertyNames)
            {
                entry.Property(item).IsModified = true;
            }
        }

        #endregion

        #region 4.0 查

        #region 4.0.1 根据条件查询

        /// <summary>
        /// 带条件查询
        /// </summary>
        /// <param name="where"></param>
        /// <returns></returns>
        public virtual List<TEntity> QueryWhere(Expression<Func<TEntity, bool>> where)
        {
            return _dbset.Where(where).ToList();
        }

        #endregion

        #region 4.0.2 排序查询

        /// <summary>
        /// 正序排序
        /// </summary>
        /// <typeparam name="TKey"></typeparam>
        /// <param name="where"></param>
        /// <param name="keySelector"></param>
        /// <returns></returns>
        public virtual List<TEntity> QueryOrderByWhere<TKey>(Expression<Func<TEntity, bool>> where, Expression<Func<TEntity, TKey>> keySelector)
        {
            return QueryOrderByWhere(where, keySelector, false);
        }

        /// <summary>
        /// 倒序排序
        /// </summary>
        /// <typeparam name="TKey"></typeparam>
        /// <param name="where"></param>
        /// <param name="keySelector"></param>
        /// <returns></returns>
        public List<TEntity> QueryOrderByDescWhere<TKey>(Expression<Func<TEntity, bool>> where, Expression<Func<TEntity, TKey>> keySelector)
        {
            return QueryOrderByWhere(where, keySelector, true);
        }

        /// <summary>
        /// 正倒序排序
        /// </summary>
        /// <typeparam name="TKey"></typeparam>
        /// <param name="where"></param>
        /// <param name="keySelector"></param>
        /// <returns></returns>
        List<TEntity> QueryOrderByWhere<TKey>(Expression<Func<TEntity, bool>> where, Expression<Func<TEntity, TKey>> keySelector, bool isdesc)
        {
            if (isdesc)
            {
                return _dbset.Where(where).OrderByDescending(keySelector).ToList();
            }
            else
            {
                return _dbset.Where(where).OrderBy(keySelector).ToList();
            }
        }

        #endregion

        #region 4.0.3 连表查询

        /// <summary>
        /// 连表操作
        /// </summary>
        /// <param name="where">查询条件</param>
        /// <param name="tableNames">要连表的数组</param>
        /// <returns></returns>
        public virtual List<TEntity> QueryJoinWhere(Expression<Func<TEntity, bool>> where, string[] tableNames)
        {
            if (tableNames == null || tableNames.Length == 0)
            {
                throw new Exception("连表的表名数组至少要有一个");
            }

            //将DbSet<TEntity>对象_dbset赋值给父类DbQuery<TEntity> query
            DbQuery<TEntity> query = _dbset;

            //将query通过遍历产生出多个表的链接对象，赋值给query
            foreach (var tablename in tableNames)
            {
                query = query.Include(tablename);
            }

            //利用query进行条件查询
            return query.Where(where).ToList();
        }


        #endregion

        #region 4.0.4 分页查询

        /// <summary>
        /// 分页查询
        /// </summary>
        /// <typeparam name="TKey">表示要排序TEntity中的字段</typeparam>
        /// <param name="pageindex">分页的页码</param>
        /// <param name="pagesize">页容量</param>
        /// <param name="totalcount">总条数</param>
        /// <param name="where">查询条件</param>
        /// <param name="keySelector">要排序的lambda表达式</param>
        /// <returns></returns>
        public virtual List<TEntity> GetListByPage<TKey>(int pageindex, int pagesize, out int totalcount, Expression<Func<TEntity, bool>> where, Expression<Func<TEntity, TKey>> keySelector)
        {
            int ipageindex = (pageindex - 1) * pagesize;

            //查询出满足条件的分页数据的总条数
            totalcount = _dbset.Count(where);

            //查询出满足条件的分页数据
            return _dbset
                 .Where(where)
                .OrderByDescending(keySelector)
                .Skip(ipageindex)
                .Take(pagesize)
                .ToList();
        }


        #endregion

        #endregion

        #region 5.0 调用存储过程

        /// <summary>
        /// 调用存储过程的方法
        /// </summary>
        /// <typeparam name="TEntity1">代表的是存储过程查询结果实体</typeparam>
        /// <param name="procName">存储过程的名称</param>
        /// <param name="ps">存储过程的参数，system.data.sqlclient.SqlParameter[]</param>
        /// <returns></returns>
        public virtual List<TEntity1> QueryByProc<TEntity1>(string procName, params object[] ps)
        {
            return db.Database.SqlQuery<TEntity1>(procName, ps).ToList();
        }


        #endregion

        #region 6.0 统一保存

        /// <summary>
        /// 统一将EF容器对象中的所有代理类生成相应的sql语句发给db服务器执行
        /// </summary>
        /// <returns></returns>
        public virtual int SaveChanges()
        {
            return db.SaveChanges();
        }

        #endregion

    }
}
