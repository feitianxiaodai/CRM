﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crm.Services
{
    using Crm.IRepository;
    using System.Linq.Expressions;

    public class BaseServices<TEntity> : Crm.IServices.IBaseServices<TEntity> where TEntity : class
    {
        /// <summary>
        /// 在这里由于无法提供具体的类型，因此可以通过依赖注入的方式，将数据仓储的子类对象赋值给dal  通过子类（因为子类可以获得具体的类型）
        /// </summary>
        protected IBaseRepository<TEntity> dal = null;

        #region 1.0 增

        public virtual void Add(TEntity model)
        {
            dal.Add(model);
        }


        #endregion

        #region 2.0 删

        public virtual void Delete(TEntity model, bool isAddedDbContext)
        {
            dal.Delete(model, isAddedDbContext);
        }


        #endregion

        #region 3.0 改

        /// <summary>
        /// 编辑，约定model 是一个自定义的实体，没有追加到EF容器中的
        /// </summary>
        /// <param name="model"></param>
        public virtual void Edit(TEntity model, string[] propertyNames)
        {
            dal.Edit(model, propertyNames);
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
            return dal.QueryWhere(where);
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
            return dal.QueryOrderByWhere<TKey>(where, keySelector);
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
            return dal.QueryOrderByDescWhere<TKey>(where, keySelector);
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
            return dal.QueryJoinWhere(where, tableNames);
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
            return dal.GetListByPage<TKey>(pageindex, pagesize, out totalcount, where, keySelector);
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
            return dal.QueryByProc<TEntity1>(procName, ps);
        }


        #endregion

        #region 6.0 统一保存

        /// <summary>
        /// 统一将EF容器对象中的所有代理类生成相应的sql语句发给db服务器执行
        /// </summary>
        /// <returns></returns>
        public virtual int SaveChanges()
        {
            return dal.SaveChanges();
        }

        #endregion
    }
}
