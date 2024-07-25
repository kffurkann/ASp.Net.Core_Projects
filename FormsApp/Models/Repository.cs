namespace FormsApp.Models
{
    public class Repository
    {
        private static readonly List<Product> _products = new();
        private static readonly List<Category> _categories = new();

        static Repository()
        {
            _categories.Add(new Category{ CategoryId = 1, Name="Phone"});
            _categories.Add(new Category{ CategoryId=2, Name="Computer"});

            _products.Add(new Product { ProductId = 1, Name = "ipad 15", Price = 45000, IsActive = true, Image = "1.jpg", CategoryId = 1 });
            _products.Add(new Product { ProductId = 2, Name = "ipad 16", Price = 45000, IsActive = false, Image = "2.jpg", CategoryId = 1 });
            _products.Add(new Product { ProductId = 3, Name = "ipad 17", Price = 45000, IsActive = true, Image = "3.jpg", CategoryId = 1 });
            _products.Add(new Product { ProductId = 4, Name = "ipad 18", Price = 45000, IsActive = false, Image = "4.jpg", CategoryId = 1 });

            _products.Add(new Product { ProductId = 5, Name = "mac air", Price = 45000, IsActive = true, Image = "5.jpg", CategoryId = 2 });
            _products.Add(new Product { ProductId = 6, Name = "mac pro", Price = 45000, IsActive = false, Image = "6.jpg", CategoryId = 2 });
        }

        public static List<Product> Products 
        { 
            get
            {
                return _products;
            }
        }

        public static void CreateProduct(Product entity)
        {
            _products.Add(entity);
        }

        public static void EditProduct(Product updatedProduct)
        {
            var entity = _products.FirstOrDefault(p=>p.ProductId== updatedProduct.ProductId);

            if(entity != null)
            {
                entity.Name= updatedProduct.Name;
                entity.Price= updatedProduct.Price;
                entity.IsActive= updatedProduct.IsActive;
                entity.Image= updatedProduct.Image;
                entity.CategoryId= updatedProduct.CategoryId;
            }
        }

        public static void EditIsActive(Product updatedProduct)
        {
            var entity = _products.FirstOrDefault(p => p.ProductId == updatedProduct.ProductId);

            if (entity != null)
            {
                entity.IsActive = updatedProduct.IsActive;               
            }
        }

        public static void DeleteProduct(Product deletedProduct)
        {
            var entity=_products.FirstOrDefault(p=>p.ProductId== deletedProduct.ProductId);
            if(entity != null)
            {
                _products.Remove(entity);

            }
        }

        public static List<Category> Categories
        {
            get
            {
                return _categories;
            }
        }
    }
}
