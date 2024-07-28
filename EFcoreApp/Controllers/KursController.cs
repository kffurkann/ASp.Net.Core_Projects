using EFcoreApp.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EFcoreApp.Controllers
{
    public class KursController : Controller
    {
        private readonly DataContext _context;//injection yöntemi

        public KursController(DataContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> IndexAsync()
        {
            return View(await _context.Kurslar.ToListAsync());
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Kurs model)
        {
            _context.Kurslar.Add(model);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");//sayfa, controller  burada aynı sayfaya aktarılır
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var krs = await _context.Kurslar.FindAsync(id);// sadece id'ye göre arama yapar
                                                              //var ogr = await _context.Ogrenciler.FirstOrDefaultAsync(o=>o.Ogrencild==id);//bulduğu ilk herhangi bir kritere göre değeri döndürür.
                                                              // illa id istemez
            if (krs == null)
            {
                return NotFound();
            }

            return View(krs);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]//güvenlik önlemi sunucu ile istemcinin aynı kişi olmasını sağlıyor.
        public async Task<IActionResult> Edit(int id, Kurs model)
        {
            if (id != model.KursId)
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
                catch (DbUpdateException)
                {
                    if (!_context.Kurslar.Any(o => o.KursId == model.KursId))
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
            return View(model);//Edit Formu tekrardan kullanıcı karşısına çıkar
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var kurs = await _context.Kurslar.FindAsync(id);
            if (kurs == null) { return NotFound(); }
            return View(kurs);//return View("Delete", ogrenci);
        }

        [HttpPost]
        public async Task<IActionResult> Delete([FromForm] int id)
        {

            var kurs = await _context.Kurslar.FindAsync(id);
            if (kurs == null) { return NotFound(); }
            _context.Kurslar.Remove(kurs);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }
    }
}
