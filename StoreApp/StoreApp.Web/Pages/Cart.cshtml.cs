using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using StoreApp.Data.Abstract;
using StoreApp.Web.Helpers;
using StoreApp.Web.Models;

namespace StoreApp.Web.Pages
{
    public class CartModel : PageModel
    {
        private IStoreRepository _repository;
        public CartModel(IStoreRepository repository, Cart cartService)
        {
            _repository = repository;
            Cart = cartService;//SessionCart'da getCart çaðrýlýr.
        }
        public Cart? Cart { get; set; }
        public void OnGet()
        {
        }

        public IActionResult OnPost(int Id)
        {
            var product = _repository.Products.FirstOrDefault(i => i.Id == Id);

            if (product != null)
            {
                Cart?.AddItem(product, 1);//ÖNCE OVERRIDE ÇALIÞIR SONRA BASE'E GÝDER
            }

            return RedirectToPage("/cart");
        }

        public IActionResult OnPostRemove(int Id)
        {
            Cart?.RemoveItem(Cart.Items.First(p => p.Product.Id == Id).Product);
            return RedirectToPage("/Cart");
        }
    }
}
