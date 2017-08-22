using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;

namespace NinesKy.DAL
{
    /// <summary>
    /// 数据上下文简单工厂
    /// </summary>
    public class ContextFactory
    {
        /// <summary>
        /// 获取当前数据上下文
        /// </summary>
        /// <returns></returns>
        public static MyDbContext GetCurrentContext()
        {
            //CallContext提供对每个逻辑执行线程都唯一的数据槽，而在WEB程序里，
            //每一个请求恰巧就是一个逻辑线程所以可以使用CallContext来实现单个请求之内的DbContext单例
            MyDbContext dbContext = CallContext.GetData("MyDbContext") as MyDbContext;
            if (dbContext == null)
            {
                dbContext = new MyDbContext();
                CallContext.SetData("MyDbContext", dbContext);
            }
            return dbContext;
        }
    }
}
