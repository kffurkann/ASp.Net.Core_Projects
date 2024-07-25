using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.ComponentModel.DataAnnotations;

namespace FormsApp.Models
{
    //[Bind("Name", "Price")] burada da bind edebilirsin.
    public class Product
    {
        [Display(Name="Urun Id")]
        //[BindNever]
        public int? ProductId { get; set; }

        [Display(Name = "Urun Adı")]
        [Required(ErrorMessage ="Gerekli bir Alan")]
        public string? Name { get; set; }

        [Required]
        [Range(0,100000)]
        [Display(Name = "Fiyat")]
        public decimal Price { get; set; }


        [Display(Name = "Image")]
        public string? Image { get; set; }

        [Display(Name = "Is Active")]
        public bool IsActive { get; set; }

        [Required]
        [Display(Name = "Category Id")]
        public int? CategoryId { get; set; }
    }
}
