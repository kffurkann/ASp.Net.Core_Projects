using EFcoreApp.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EFcoreApp.Controllers
{
    public class OgrenciController : Controller
    {
        private readonly DataContext _context;//injection yöntemi

        public OgrenciController(DataContext context)
        {
            _context=context;
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Ogrenci model)
        {
            _context.Ogrenciler.Add(model);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");//sayfa, controller  burada aynı sayfaya aktarılır
        }

        public async Task<IActionResult> Index()
        {
            return View(await _context.Ogrenciler.ToListAsync());
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if(id==null)
            {
                return NotFound();
            }

            var ogr = await _context.Ogrenciler.FindAsync(id);// sadece id'ye göre arama yapar
            //var ogr = await _context.Ogrenciler.FirstOrDefaultAsync(o=>o.Ogrencild==id);//bulduğu ilk herhangi bir kritere göre değeri döndürür.
                                                                                          // illa id istemez
            if (ogr== null)
            {
                return NotFound();
            }

            return View(ogr);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]//güvenlik önlemi sunucu ile istemcinin aynı kişi olmasını sağlıyor.
        public async Task<IActionResult> Edit(int id, Ogrenci model)
        {
            if(id != model.Ogrencild)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(model);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_context.Ogrenciler.Any(o => o.Ogrencild == model.Ogrencild))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }                   
                }
                return RedirectToAction("Index");
            }
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ogrenci = await _context.Ogrenciler.FindAsync(id);
            if(ogrenci == null) { return NotFound(); }
            return View(ogrenci);//return View("Delete", ogrenci);
        }

        [HttpPost]
        public async Task<IActionResult> Delete([FromForm]int id)
        {

            var ogrenci = await _context.Ogrenciler.FindAsync(id);
            if (ogrenci == null) { return NotFound(); }
            _context.Ogrenciler.Remove(ogrenci);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }
    }
}
