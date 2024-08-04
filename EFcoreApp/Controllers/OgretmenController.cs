using EFcoreApp.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EFcoreApp.Controllers
{
    public class OgretmenController : Controller
    {
        private readonly DataContext _context;//injection yöntemi

        public OgretmenController(DataContext context)
        {
            _context = context;
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Ogretmen model)
        {
            _context.Ogretmenler.Add(model);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");//sayfa, controller  burada aynı sayfaya aktarılır
        }

        public async Task<IActionResult> Index()
        {
            return View(await _context.Ogretmenler.ToListAsync());
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ogrm = await _context
                                .Ogretmenler
                                .FirstOrDefaultAsync(o => o.OgretmenId == id);

            //var ogr = await _context.Ogrenciler.FindAsync(id);

            // sadece id'ye göre arama yapar
            //var ogr = await _context.Ogrenciler.FirstOrDefaultAsync(o=>o.Ogrencild==id);//bulduğu ilk herhangi bir kritere göre değeri döndürür.
            // illa id istemez
            if (ogrm == null)
            {
                return NotFound();
            }

            return View(ogrm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]//güvenlik önlemi sunucu ile istemcinin aynı kişi olmasını sağlıyor.
        public async Task<IActionResult> Edit(int id, Ogretmen model)
        {
            if (id != model.OgretmenId)
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
                    if (!_context.Ogretmenler.Any(o => o.OgretmenId == model.OgretmenId))
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

            var ogretmen = await _context.Ogretmenler.FindAsync(id);
            if (ogretmen == null) { return NotFound(); }
            return View(ogretmen);//return View("Delete", ogretmen);
        }

        [HttpPost]
        public async Task<IActionResult> Delete([FromForm] int id)
        {

            var ogretmen = await _context.Ogretmenler.FindAsync(id);
            if (ogretmen == null) { return NotFound(); }
            _context.Ogretmenler.Remove(ogretmen);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }
    }
}
