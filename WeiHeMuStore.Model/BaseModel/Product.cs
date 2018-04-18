using System;
using System.Collections.Generic;
using System.Text;

namespace WeiHeMuStore.Model.BaseModel
{
    public class Product
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        /// <summary>
        /// 主图
        /// </summary>
        public string ProductImage { get; set; }
        /// <summary>
        /// 介绍
        /// </summary>
        public string ProductIntroduce { get; set; }
        public decimal ProductPrice { get; set; }
        public int IsHot { get; set; }
        public virtual ProductCate ProductCates { get; set; }
    }
}
