using StoreApp.Data.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreApp.Data.Abstract
{
    public interface IStoreRepository
    {
        IQueryable<Product> Products { get; }
        void CreateProduct(Product entity);
    }
}
