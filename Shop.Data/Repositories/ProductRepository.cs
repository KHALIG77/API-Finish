using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shop.Core.Entities;
using Shop.Core.Repositories;

namespace Shop.Data.Repositories
{
    public class ProductRepository:Repository<Product>,IProductRepository
    {
        public ProductRepository(ShopDbContext context):base(context)
        {
            
        }
    }
}
