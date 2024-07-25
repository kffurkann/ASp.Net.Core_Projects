using FormsApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Diagnostics;
using System.IO;

namespace FormsApp.Controllers
{
    public class HomeController : Controller
    {

        public HomeController()
        {
         
        }

        [HttpGet]
        public IActionResult Index(string searchString, string searchCategory)
        {
            var products = Repository.Products;
            if (!String.IsNullOrEmpty(searchString))
            {
                ViewBag.SearchString = searchString;
                products=products.Where(p => p.Name!.ToLower().Contains(searchString)).ToList();
            }

            if (!String.IsNullOrEmpty(searchCategory) && searchCategory != "0")
            {
                products=products.Where(p=>p.CategoryId==int.Parse(searchCategory)).ToList();
            }

            //ViewBag.Categories = new SelectList(Repository.Categories, "CategoryId", "Name");

            var model = new ProductViewModel
            {
                Products = products,
                Categories = Repository.Categories,
                SelectedCategory = searchCategory
            };
            return View(model);
        }

        [HttpGet]
        public IActionResult Create()
        {
            ViewBag.Categories = new SelectList(Repository.Categories, "CategoryId", "Name");
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Product model, IFormFile imageFile)//istediğin attributeleri de yazabilirsin veya
        {                                                              //bind ile seçebilirsin ([Bind("Name","Price")]Product model)
                                                                       //imageFile modelde de (IFormFile imageFile) oluşabilirdi, böyle de olur

            var extension = "";

            if (imageFile != null)
            {
                var allowedExtensions = new[] { ".jpg", ".jpeg", ".png" };//dağıtamızın sebebi hiç bişey doldurmadan submit yaptığında hata veriyordu.
                extension=Path.GetExtension(imageFile.FileName);

                if (!allowedExtensions.Contains(extension))
                {
                    ModelState.AddModelError("", "Geçerli bir Tür Giriniz");//name or image
                }
            }


            if (ModelState.IsValid) //asp-validation-summary="All"> ile hata mesajını buraya verir.
            {
                
                if (imageFile != null)
                {
                    var randomFileName = string.Format($"{Guid.NewGuid().ToString()}{extension}");
                    var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/img", randomFileName);

                    using (var stream = new FileStream(path, FileMode.Create))
                    {
                        await imageFile.CopyToAsync(stream);
                    }

                    model.Image = randomFileName;
                    model.ProductId = Repository.Products.Count + 1;
                    Repository.CreateProduct(model);
                    return RedirectToAction("Index");
                }
             
            }

            ViewBag.Categories = new SelectList(Repository.Categories, "CategoryId", "Name");
            return View(model);
        }

        [HttpGet]
        public IActionResult Edit(int? id)
        {

            if (id == null)
            {
                return NotFound();
            }

            var entity=Repository.Products.FirstOrDefault(p=>p.ProductId== id);


            if (entity == null)
            {
                return NotFound();
            }

            ViewBag.Categories = new SelectList(Repository.Categories, "CategoryId", "Name");
            return View(entity);
            
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id,Product model,IFormFile? imageFile)//resim güncellenmek istenmeyebilir.
        {
            if(id != model.ProductId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                if (imageFile != null)
                {

                    var allowedExtension = new[] { ".jpeg", ".jpg", ".png" };
                    var extension=Path.GetExtension(imageFile.FileName);
                    var randomFileName=string.Format(Guid.NewGuid().ToString(), extension);
                    var path=Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/img", randomFileName);

                    using (var stream = new FileStream(path, FileMode.Create))
                    {
                        await imageFile.CopyToAsync(stream);
                    }
                    model.Image = randomFileName;
                    
                }
                Repository.EditProduct(model);
                return RedirectToAction("Index");
            }

            ViewBag.Categories = new SelectList(Repository.Categories, "CategoryId", "Name");
            return View();
        }

        [HttpGet]
        public IActionResult Delete(int? id)
        {
            if(id == null){
                
                return NotFound();
            }

            var entity = Repository.Products.FirstOrDefault(p=>p.ProductId==id);

            if(entity == null)
            {
                return NotFound();
            }

            //Repository.DeleteProduct(entity);
            //return RedirectToAction("Index");
            return View("DeleteConfirm", entity);
        }

        [HttpPost]
        public IActionResult Delete(int id, int ProductId)
        {
            if (id != ProductId)
            {

                return NotFound();
            }

            var entity = Repository.Products.FirstOrDefault(p => p.ProductId == ProductId);

            if (entity == null)
            {
                return NotFound();
            }

            Repository.DeleteProduct(entity);
            return RedirectToAction("Index");
        }

        public IActionResult EditProducts(List<Product> Products)
        {
            foreach (var product in Products)
            {
                Repository.EditIsActive(product);
            }
            return RedirectToAction("Index");
        }
    }
}
