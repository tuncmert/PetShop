using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PET_GIDA.Models;
using PET_GIDA.Models.DB;

namespace PET_GIDA.Controllers;

[LoginControlAdminFilter]
public class AdminController : Controller
{
    private readonly BITIRMEContext _context;

    public AdminController(BITIRMEContext context)
    {
        _context = context;
    }

    public IActionResult Index()
    {
        return View();
    }
    public IActionResult KediMamalari()
    {
        var kediMamalari = _context.Urunlers.Where(o => o.KategoriId == 1).ToList();
        return View(kediMamalari);
    }
    public IActionResult KediEvleri()
    {
        var kediEvleri = _context.Urunlers.Where(r => r.KategoriId == 3).ToList();
        return View(kediEvleri);
    }
    public IActionResult KopekOyuncaklari()
    {
        var kopekOyuncaklari = _context.Urunlers.Where(e => e.KategoriId == 4).ToList();
        return View(kopekOyuncaklari);
    }

    [HttpPost]
    public async Task<IActionResult> UrunkEkleAsync()
    {
        var file1 = HttpContext.Request.Form.Files["dosya1"];
        var file2 = HttpContext.Request.Form.Files["dosya2"];
        var file3 = HttpContext.Request.Form.Files["dosya3"];
        var file4 = HttpContext.Request.Form.Files["dosya4"];
        var eklenecekUrun = new Urunler();
        eklenecekUrun.KategoriId = Convert.ToInt32(HttpContext.Request.Form["KategoriId"]);
        eklenecekUrun.UrunAd = HttpContext.Request.Form["Ad"].ToString();
        eklenecekUrun.UrunFiyat = Convert.ToDecimal(HttpContext.Request.Form["Fiyat"]);
        eklenecekUrun.UrunStok = Convert.ToInt32(HttpContext.Request.Form["Stok"]);
        eklenecekUrun.UrunKisaAciklama = HttpContext.Request.Form["KisaAciklama"].ToString();
        eklenecekUrun.UrunUzunAciklama = HttpContext.Request.Form["UzunAciklama"].ToString();
        eklenecekUrun.AnaSayfa = Convert.ToBoolean(HttpContext.Request.Form["AnaSayfa"]);
        _context.Urunlers.Add(eklenecekUrun);
        _context.SaveChanges();

        if (file1 != null)
        {
            using (var ms = new MemoryStream())
            {
                await file1.CopyToAsync(ms);
                var fileBytes = ms.ToArray();
                eklenecekUrun.UrunResim1 = fileBytes;
                _context.SaveChanges();
                var dosyaAdi = file1.FileName;
                System.IO.File.WriteAllBytes(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", dosyaAdi), fileBytes);
            }
        }
        if (file2 != null)
        {
            using (var ms = new MemoryStream())
            {
                await file2.CopyToAsync(ms);
                var fileBytes = ms.ToArray();
                eklenecekUrun.UrunResim2 = fileBytes;
                _context.SaveChanges();
                var dosyaAdi = file2.FileName;
                System.IO.File.WriteAllBytes(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", dosyaAdi), fileBytes);
            }
        }
        if (file3 != null)
        {
            using (var ms = new MemoryStream())
            {
                await file3.CopyToAsync(ms);
                var fileBytes = ms.ToArray();
                eklenecekUrun.UrunResim3 = fileBytes;
                _context.SaveChanges();
                var dosyaAdi = file3.FileName;
                System.IO.File.WriteAllBytes(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", dosyaAdi), fileBytes);
            }
        }
        if (file4 != null)
        {
            using (var ms = new MemoryStream())
            {
                await file4.CopyToAsync(ms);
                var fileBytes = ms.ToArray();
                eklenecekUrun.UrunResim4 = fileBytes;
                _context.SaveChanges();
                var dosyaAdi = file4.FileName;
                System.IO.File.WriteAllBytes(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", dosyaAdi), fileBytes);
            }
        }
        return Json("1");
    }

    [HttpPost]
    public IActionResult UrunSil([FromBody] Urunler urunler)
    {
        if (urunler == null) return Json("0");
        var silinecekUrun = _context.Urunlers.FirstOrDefault(o => o.UrunId == urunler.UrunId);
        if (silinecekUrun == null) return Json("0");
        _context.SaveChanges();
        _context.Urunlers.Remove(silinecekUrun);
        _context.SaveChanges();
        return Json("1");
    }


    [HttpPost]
    public async Task<IActionResult> UrunDuzenleAsync()
    {
        var file1 = HttpContext.Request.Form.Files["dosya1"];
        var file2 = HttpContext.Request.Form.Files["dosya2"];
        var file3 = HttpContext.Request.Form.Files["dosya3"];
        var file4 = HttpContext.Request.Form.Files["dosya4"];
        var UrunId = Convert.ToInt32(HttpContext.Request.Form["Id"]);
        var GuncellenecekUrun = _context.Urunlers.FirstOrDefault(p => p.UrunId == UrunId);
        GuncellenecekUrun.UrunAd = HttpContext.Request.Form["Ad"].ToString();
        GuncellenecekUrun.UrunFiyat = Convert.ToDecimal(HttpContext.Request.Form["Fiyat"]);
        GuncellenecekUrun.UrunKisaAciklama = HttpContext.Request.Form["KAciklama"].ToString();
        GuncellenecekUrun.UrunUzunAciklama = HttpContext.Request.Form["UAciklama"].ToString();
        GuncellenecekUrun.UrunStok = Convert.ToInt32(HttpContext.Request.Form["Stok"]);
        GuncellenecekUrun.AnaSayfa = Convert.ToBoolean(HttpContext.Request.Form["AnaSayfa"]);
        _context.SaveChanges();

        if (file1 != null)
        {
            using (var ms = new MemoryStream())
            {
                await file1.CopyToAsync(ms);
                var fileBytes = ms.ToArray();
                GuncellenecekUrun.UrunResim1 = fileBytes;
                _context.SaveChanges();
                var dosyaAdi = file1.FileName;
                System.IO.File.WriteAllBytes(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", dosyaAdi), fileBytes);
            }
        }
        if (file2 != null)
        {
            using (var ms = new MemoryStream())
            {
                await file2.CopyToAsync(ms);
                var fileBytes = ms.ToArray();
                GuncellenecekUrun.UrunResim2 = fileBytes;
                _context.SaveChanges();
                var dosyaAdi = file2.FileName;
                System.IO.File.WriteAllBytes(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", dosyaAdi), fileBytes);
            }
        }
        if (file3 != null)
        {
            using (var ms = new MemoryStream())
            {
                await file3.CopyToAsync(ms);
                var fileBytes = ms.ToArray();
                GuncellenecekUrun.UrunResim3 = fileBytes;
                _context.SaveChanges();
                var dosyaAdi = file3.FileName;
                System.IO.File.WriteAllBytes(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", dosyaAdi), fileBytes);
            }
        }
        if (file4 != null)
        {
            using (var ms = new MemoryStream())
            {
                await file4.CopyToAsync(ms);
                var fileBytes = ms.ToArray();
                GuncellenecekUrun.UrunResim4 = fileBytes;
                _context.SaveChanges();
                var dosyaAdi = file4.FileName;
                System.IO.File.WriteAllBytes(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", dosyaAdi), fileBytes);
            }
        }
        return Json("1");
    }
    public IActionResult MamaDetay(int? id)
    {
        if (id == null) return RedirectToAction("Index");
        var seciliMama = _context.Urunlers.FirstOrDefault(m => m.UrunId == id);
        return View(seciliMama);
    }


    public IActionResult KopekMamalari()
    {
        var KopekMamalari = _context.Urunlers.Where(p => p.KategoriId == 2).ToList();
        return View(KopekMamalari);
    }
    public IActionResult KopekMamalariDetay(int? id)
    {
        if (id == null) return RedirectToAction("Index");
        var seciliMama = _context.Urunlers.FirstOrDefault(k => k.UrunId == id);
        return View(seciliMama);
    }

    public IActionResult Satilanlar()
    {
        var SiparistekiUrunler = _context.SiparisListesis.Include(x => x.Urun).ToList();
        return View(SiparistekiUrunler);
    }
    [HttpPost]
    public IActionResult SiparisDurumu([FromBody] UrunModel model)
    {
        if (model == null)
        {
            return Json("0");
        }
        var GuncellenecekSiparis = _context.SiparisListesis.FirstOrDefault(x => x.Id == model.UrunIdd);
        if (GuncellenecekSiparis == null) return Json("0");
        GuncellenecekSiparis.SiparisDurumu = model.SiparisD;
        _context.SaveChanges();
        return Json("1");
    }
    public IActionResult Cikis()
    {
        HttpContext.Session.Clear();
        return RedirectToAction("AdminLogin", "Home");
    }
}