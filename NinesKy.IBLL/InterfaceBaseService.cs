using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NinesKy.IBLL
{
    /// <summary>
    /// 义务层的公共接口
    /// </summary>
    /// <typeparam name="T"></typeparam>
     public interface InterfaceBaseService<T> where T:class
    {
         T Add(T entity);

         bool Update(T entity);

         bool Delete(T entity);
    }
}
