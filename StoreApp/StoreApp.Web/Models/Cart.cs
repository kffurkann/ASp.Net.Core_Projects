using StoreApp.Data.Concrete;

namespace StoreApp.Web.Models
{
    public class Cart
    {
        public List<CartItem> Items { get; set; } = new List<CartItem>();

        public virtual void AddItem(Product product, int quantity)
        {
            var item = Items.Where(p => p.Product.Id == product.Id).FirstOrDefault();

            if (item == null)//sıfırdan ekleme
            {
                Items.Add(new CartItem { Product = product, Quantity = quantity });
            }
            else//içinde var ise
            {
                item.Quantity += quantity;
            }
        }

        public virtual void RemoveItem(Product product)
        {
            Items.RemoveAll(i => i.Product.Id == product.Id);
        }

        public decimal CalculateTotal()
        {
            return Items.Sum(i => i.Product.Price * i.Quantity);
        }

        public virtual void Clear()//sepeti temizler
        {
            Items.Clear();
        }
    }

    public class CartItem
    {
        public int CartItemId { get; set; }
        public Product Product { get; set; } = new();
        public int Quantity { get; set; }
    }
}
