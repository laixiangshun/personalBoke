using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NinesKy.Models.Config
{
    /// <summary>
    /// 元素集合
    /// </summary>
    [ConfigurationCollection(typeof(KeyValueElement))] 
    public class KeyValueElementCollection:ConfigurationElementCollection
    {
        //忽略大小写
        public KeyValueElementCollection():base(StringComparer.OrdinalIgnoreCase){}

        /// <summary>
        /// 集合中指定键的配置元素
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        new public KeyValueElement this[string name]
        {
            get { return (KeyValueElement)base.BaseGet(name); }
            set
            {
                if (base.Properties.Contains(name)) base[name] = value;
                else base.BaseAdd(value);
            }
        }

        /// <summary>
        /// 创建新元素
        /// </summary>
        /// <returns></returns>
        protected override ConfigurationElement CreateNewElement()
        {
            return new KeyValueElement();
        }

        /// <summary>
        /// 获取指定元素的键
        /// </summary>
        /// <param name="element"></param>
        /// <returns></returns>
        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((KeyValueElement)element).Key;
        }
    }
}
