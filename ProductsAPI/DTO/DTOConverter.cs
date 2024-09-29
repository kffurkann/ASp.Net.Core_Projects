using ProductsAPI.Models;

namespace ProductsAPI.DTO
{
    public class DTOConverter
    {
        public static ProductDTO ProductToDTO(Product p)
        {
            var entity = new ProductDTO();//boş
            if (p != null)
            {
                entity.ProductId = p.ProductId;
                entity.ProductName = p.ProductName;
                entity.Price = p.Price;
            }
            return entity;
        }
    }
}
