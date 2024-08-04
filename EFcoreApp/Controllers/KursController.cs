using EFcoreApp.Data;
using EFcoreApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
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

        public async Task<IActionResult> Index()
        {
            return View(await _context.Kurslar.Include(k=>k.Ogretmen).ToListAsync());
        }

        public async Task<IActionResult> Create()
        {
            ViewBag.Ogretmenler= new SelectList(await _context.Ogretmenler.ToListAsync(),"OgretmenId","AdSoyad");//id ve görünür texti
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(KursViewModel model)
        {
            if (ModelState.IsValid)
            {
                _context.Kurslar.Add(new Kurs() { KursId = model.KursId, Baslik = model.Baslik, OgretmenId = model.OgretmenId });
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");//sayfa, controller  burada aynı sayfaya aktarılır
            }

            ViewBag.Ogretmenler = new SelectList(await _context.Ogretmenler.ToListAsync(), "OgretmenId", "AdSoyad");//id ve görünür texti
            return View(model);
            
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var krs = await _context
                                .Kurslar
                                .Include(k=>k.KursKayitlari)
                                .ThenInclude(k=>k.Ogrenci)
                                .Select(k=> new KursViewModel
                                {
                                    KursId = k.KursId,
                                    Baslik = k.Baslik,
                                    OgretmenId = k.OgretmenId,
                                    KursKayitlari=k.KursKayitlari
                                })
                                .FirstOrDefaultAsync(k=>k.KursId== id);
            
            
            
            
            // sadece id'ye göre arama yapar
                                                              //var ogr = await _context.Ogrenciler.FirstOrDefaultAsync(o=>o.Ogrencild==id);//bulduğu ilk herhangi bir kritere göre değeri döndürür.
                                                              // illa id istemez
            if (krs == null)
            {
                return NotFound();
            }

            ViewBag.Ogretmenler = new SelectList(await _context.Ogretmenler.ToListAsync(), "OgretmenId", "AdSoyad");//id ve görünür texti
            return View(krs);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]//güvenlik önlemi sunucu ile istemcinin aynı kişi olmasını sağlıyor.
        public async Task<IActionResult> Edit(int id, KursViewModel model)
        {
            if (id != model.KursId)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(new Kurs() { KursId=model.KursId, Baslik=model.Baslik, OgretmenId=model.OgretmenId});
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

            ViewBag.Ogretmenler = new SelectList(await _context.Ogretmenler.ToListAsync(), "OgretmenId", "AdSoyad");//id ve görünür texti
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
