using StoreApp.Data.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreApp.Data.Concrete
{
    public class EfStoreRepository : IStoreRepository
    {
        private StoreDbContext _context;
        public EfStoreRepository(StoreDbContext context)
        {
            _context = context;
        }

        public IQueryable<Product> Products => _context.Products;//DbContext Products

        public void CreateProduct(Product entity)
        {
            throw new NotImplementedException();
        }
    }
}
