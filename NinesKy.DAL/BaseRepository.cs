using NinesKy.IDAL;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace NinesKy.DAL
{
    /// <summary>
    /// 创库基类
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class BaseRepository<T> : InterfaceBaseRepository<T> where T:class
    {
        protected MyDbContext dbContext = ContextFactory.GetCurrentContext();



        public T Add(T entity)
        {
            dbContext.Entry<T>(entity).State = EntityState.Added;
            dbContext.SaveChanges();
            return entity;
        }

        public int Count(System.Linq.Expressions.Expression<Func<T, bool>> predicate)
        {
            return dbContext.Set<T>().Count(predicate);
        }

        public bool Update(T entity)
        {
            dbContext.Set<T>().Attach(entity);
            dbContext.Entry<T>(entity).State = EntityState.Modified;
            return dbContext.SaveChanges() > 0;
        }

        public bool Delete(T entity)
        {
            dbContext.Set<T>().Attach(entity);
            dbContext.Entry<T>(entity).State = EntityState.Deleted;
            return dbContext.SaveChanges() > 0;
        }

        public bool Exist(System.Linq.Expressions.Expression<Func<T, bool>> anyLambda)
        {
            return dbContext.Set<T>().Any(anyLambda);
        }

        public T Find(System.Linq.Expressions.Expression<Func<T, bool>> whereLambda)
        {
            T _entity = dbContext.Set<T>().FirstOrDefault<T>(whereLambda);
            return _entity;
        }

        //public IQueryable<T> FindList<S>(System.Linq.Expressions.Expression<Func<T, bool>> whereLambda, bool isAsc, System.Linq.Expressions.Expression<Func<T, S>> orderLambda)
        //{
        //    var _list = dbContext.Set<T>().Where<T>(whereLambda);
        //    if (isAsc) _list = _list.OrderBy<T, S>(orderLambda);
        //    else _list = _list.OrderByDescending<T, S>(orderLambda);
        //    return _list;
        //}

        //public IQueryable<T> FindPageList<S>(int pageIndex, int pageSize, out int totalRecord, System.Linq.Expressions.Expression<Func<T, bool>> whereLambda, bool isAsc, System.Linq.Expressions.Expression<Func<T, S>> orderLambda)
        //{
        //    var _list = dbContext.Set<T>().Where(whereLambda);
        //    totalRecord = _list.Count();
        //    if (isAsc) _list = _list.OrderBy<T, S>(orderLambda).Skip<T>((pageIndex - 1) * pageSize).Take<T>(pageSize);
        //    else _list = _list.OrderByDescending<T, S>(orderLambda).Skip<T>((pageIndex - 1) * pageSize).Take<T>(pageSize);
        //    return _list;
        //}


        public IQueryable<T> FindList(Expression<Func<T, bool>> whereLambda, string orderName, bool isAsc)
        {
            var _list = dbContext.Set<T>().Where(whereLambda);
            _list = OrderBy(_list, orderName, isAsc);
            return _list;
        }

        public IQueryable<T> FindPageList(int pageIndex, int pageSize, out int totalRecord, Expression<Func<T, bool>> whereLambda, string orderName, bool isAsc)
        {
            var _list = dbContext.Set<T>().Where(whereLambda);
            totalRecord = _list.Count();
            _list = OrderBy(_list, orderName, isAsc).Skip((pageIndex-1)*pageSize).Take(pageSize);
            return _list;
        }
        //排序
        private IQueryable<T> OrderBy(IQueryable<T> source, string propertyName, bool isAsc)
        {
            if (source == null)
            {
                throw new ArgumentNullException("source", "不能为空");
            }
            if (string.IsNullOrEmpty(propertyName)) return source;
            var _parameter = Expression.Parameter(source.ElementType);
            var _property = Expression.Property(_parameter, propertyName);
            if (_property == null)
            {
                throw new ArgumentNullException("propertyName", "属性不存在");
            }
            var _lambda = Expression.Lambda(_property, _parameter);
            var _methodName = isAsc ? "OrderBy" : "OrderByDescending";
            var _resultExpression = Expression.Call(typeof(Queryable), _methodName, new Type[] { source.ElementType, _property.Type }, source.Expression, Expression.Quote(_lambda));
            return source.Provider.CreateQuery<T>(_resultExpression);
        }


    }
}
