using System;
using System.Collections.Generic;
using System.Text;

namespace WeiHeMuStore.Model.BaseModel
{
    public class ProductCate
    {
        public ProductCate()
        {
            Products = new HashSet<Product>();
        }
        public int ProductCateId { get; set; }

        public string ProductCateName { get; set; }

        public virtual ICollection<Product> Products { get; set; }
    }
}
