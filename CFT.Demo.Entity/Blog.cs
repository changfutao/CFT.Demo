using System;
using System.Collections.Generic;
using System.Text;

namespace CFT.Demo.Entity
{
    /// <summary>
    /// 博客类
    /// </summary>
    public class Blog
    {
        /// <summary>
        /// Id
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// 博客名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 博客地址
        /// </summary>
        public string Url { get; set; }
    }
}
