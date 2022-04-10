using System.Collections.Generic;
using System.Diagnostics;
using System.Net;
using System.Net.Mail;
using Microsoft.AspNetCore.Mvc;
using PET_GIDA.Models;
using PET_GIDA.Models.DB;

namespace PET_GIDA.Controllers;
public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly BITIRMEContext _context;

    public HomeController(ILogger<HomeController> logger, BITIRMEContext context)
    {
        _logger = logger;
        _context = context;
    }

    public IActionResult Index()
    {
        var TumUrunler = _context.Urunlers.Where(o => o.AnaSayfa == true).ToList();
        return View(TumUrunler);
    }

    [HttpPost]
    public IActionResult UrunFiltre([FromBody] FiltreModel model)
    {
        if (model == null)
        {
            return Json("0");  //HATA
        }
        var urunler = _context.Urunlers.Where(x => x.KategoriId == model.KategoriId).ToList();
        return Json(urunler);
    }

    public IActionResult Login()
    {
        return View();
    }

    [HttpPost]
    public IActionResult KaydolAjax([FromBody] Kullanici register)
    {
        if (register == null) return Json("0");
        _context.Kullanicis.Add(register);
        _context.SaveChanges();

        return Json("1");
    }

    [HttpPost]
    public IActionResult Login(LoginModel login)
    {
        if (login == null) return RedirectToAction("Login");
        var seciliKullanici = _context.Kullanicis.FirstOrDefault(x => x.KullaniciAdi == login.KullaniciAdi && x.Sifre == login.Sifre);
        if (seciliKullanici != null)
        {
            ViewBag.AdminMessage = "Girilen Bilgiler Doğru";
            HttpContext.Session.SetString("loginId", seciliKullanici.Id.ToString());
            return RedirectToAction("Urunler", "Home");
        }
        else
        {
            ViewBag.KullaniciMessage = "Kullanıcı adı ve şifrenizi kontrol edin";
        }
        return View();

    }

    public IActionResult AdminLogin()
    {
        return View();
    }

    [HttpPost]
    public IActionResult AdminLogin(LoginModel login)
    {
        if (login == null) return RedirectToAction("AdminLogin");
        var admin = _context.Admins.FirstOrDefault(a => a.KullaniciAdi == login.KullaniciAdi && a.Sifre == login.Sifre);
        if (admin != null)
        {
            ViewBag.AdminMessage = "Giriş başarılı, Hoşgeldiniz";
            HttpContext.Session.SetString("adminloginId", admin.Id.ToString());
            return RedirectToAction("Index", "Admin");
        }
        else
        {
            ViewBag.AdminMessage = "Kimsin sen çık dışarı!";
        }
        return View();
    }

    public IActionResult Fnish()
    {
        var kullaniciId = HttpContext.Session.GetString("loginId");
        var kullaniciIdInt = Convert.ToInt32(kullaniciId);
        return View();
    }
    [HttpPost]
        public IActionResult Fnish(SiparisListesi siparis)
    {
        var kullaniciId = HttpContext.Session.GetString("loginId");
        var kullaniciIdInt = Convert.ToInt32(kullaniciId);
        return View();
    }
    public IActionResult UrunDetay(int? id)
    {
        var urun = _context.Urunlers.FirstOrDefault(x => x.UrunId == id);
        return View(urun);
    }
    public IActionResult Urunler()
    {
        var TumUrunler = _context.Urunlers.ToList();
        return View(TumUrunler);
    }
    public IActionResult Siparislerim()
    {
        return View();
    }
    public IActionResult Sepet()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Siparis([FromBody] SiparisListesi[] dizi)
    {
        if (dizi == null) return Json("0");
            Random Sayi = new Random();
            int rSayi = Sayi.Next(9999, 100000);

        foreach (var item in dizi)
        {
            if (item.Mail == null || item.Tell == null) return Json("2");
            var guncellenecekUrun = _context.Urunlers.FirstOrDefault(x => x.UrunId == item.UrunId);
            guncellenecekUrun.UrunStok = guncellenecekUrun.UrunStok - item.UrunAdet;
            _context.SaveChanges();

            item.SiparisDurumu = "Ödeme Alındı";
            item.SiparisNo = rSayi;
            
            DateTime time = DateTime.Now;
            item.SiparisTarihi = time.ToString("MM/dd/yyyy");
             _context.SiparisListesis.Add(item);
             _context.SaveChanges();
        }

        return Json(rSayi);
    }
    
    [HttpPost]
    public IActionResult SiparisDurumum([FromBody] int No)
    {
        var Siparisler = _context.SiparisListesis.Where(o => o.SiparisNo == No);
        return Json("1");
    }

    public IActionResult Odeme()
    {
        return View();
    }

    public IActionResult Contact()
    {
        return View();
    }




    BITIRMEContext BITIRME = new BITIRMEContext();
    public IActionResult EmailAjax(string email)
    {
        if (email != null)
        {
            var user = BITIRME.Kullanicis.Where(k => k.Email == email).FirstOrDefault();
            if (user == null)
            {
                
                ViewBag.Error = "HATA";
                return View();
            }
            Random rastgele = new Random();
            string harfler = "9A8S7D6F5G4H3J2K1L";
            string password = "";
            for (int i = 0; i < 6; i++)
            {
                password += harfler[rastgele.Next(harfler.Length)];
            }
            // var md5pass = Crypto.Hash(password.Trim(), "MD5").ToLower();
            // user.Sifre = md5pass;
            BITIRME.SaveChanges();
            SendForgetMail(user.KullaniciAdi, user.Email, password);
            return RedirectToAction("Index", "Login");

            _context.Kullanicis.Add(user);

        }
        return View();
    }
    public static void SendForgetMail(string KullaniciAdi, string Email, string password)
    {
        string mailbody;
        try
        {




            mailbody = "<table style='background-color:#fff;padding:10px;width:620px;text-align:left;border-top:10px solid #333333;" +
                 "border-bottom:10px solid #333333;border-left:10px solid #333333;border-right:10px solid #333333'" +
                 " width='630' cellspacing='0' cellpadding='0'><tbody>" +
                 "<tr><td>" +
                 "<table style='background-color:#ffffff;' width='100%' cellspacing='0' cellpadding = '0'>  <tbody><tr><td style = 'padding: 10px;'>" +
                 "<a href = 'http://www.fixedsoft.com/' target = '_blank'  > " +
                 "<img src = 'http://fixedsoft.com/Content/images/logo/logo.png' alt = '' width = '215' height = '55' border = '0' /></a></td>" +
                 "<td style = 'color: #1a2640; font-family: Arial; font-size: 13px;margin-left:50px;' align = 'right' > (0542) 245 71 68 " +
                 "<span style = 'color: #a5b9c5; font-size: 24px;' >|</span> " +
                 "<a style = 'text-decoration: none; color: #1a2640;' href = 'http://www.fixedsoft.com/' target = '_blank' data - saferedirecturl = '#'>" +
                 "www.fixedsoft.com </a> &nbsp; &nbsp; &nbsp;</td></tr>" +
                 "<tr><td colspan = '2' ><hr style='border: 1px dashed black;'/> </td></tr><tr><td style = 'padding: 10px; font-size: 12px; font-family: Arial;' colspan = '2' >" +
                 "<p style = 'margin: 0 0 10px 0;' > Gönderen: <strong>" + KullaniciAdi + "</strong>,</p>" +
                 "<p style = 'margin: 0 0 10px 0;' > Email: <strong>" + Email + "</strong>,</p>" +
                 "<p style = 'margin: 0 0 10px 0;' > Konu: <strong>" + "mesaj.Subject" + "</strong>,</p>" +
                 "<p style = 'margin: 0 0 10px 0;' > Mesaj: <strong>" + "mesaj.From" + "</strong>,</p> <br /> <br />" +
                 "<p style = 'margin: 0 0 0 0;' >" +
                 "Her türlü sorunuzda bize " +
                 "<a style = 'color: #000000;' href = 'mailto:'" + "SAVT EMAİL" + "target = '_blank'> " + "SAVT EMAİLLLL" + " </a> " +
                 "adresinden ulaşabilir veya " +
                 "<a href = 'tel:(542) 245 71 68' target = '_blank' > (0542) 245 71 68 </a> nolu telefondan IK Birimi ile görüşebilirsiniz.</p>" +
                 "<p style = 'margin: 20px 0 0 0;' > Saygılarımızla,</p><p style = 'margin: 5px 0 0 0;' > " +
                 "Fixed Soft | Yazılım Evi</p></td></tr>" +
                 "<tr><td colspan = '2' ><hr style='border: 1px dashed black;'/></td></tr><tr><td style = 'padding: 10px; color: #808080; font-size: 12px;' colspan = '2'>" +
                 "<p style = 'margin: 0 0 0 0; font-family: Arial;' >" +
                 " Copyright &copy; 2019 FixedSoft | Tüm hakları saklıdır.</p></td></tr></tbody></table></td></tr></tbody></table> ";

            MailMessage mesaj = new MailMessage();
            mesaj.From = new MailAddress("alirizasahin5757@hotmail.com", "Şifre Değiştirme Bildirimi");
            mesaj.CC.Add(Email); //name@firmaAdi.com
            mesaj.To.Add(Email);
            if (Email != null)
            {
                mesaj.To.Add(Email);
            }
            mesaj.Subject = "Intranet | Şifre Değiştirme Bildirimi";
            mesaj.IsBodyHtml = true;


            SmtpClient client = new SmtpClient("text.alirizasahin5757@hotmail.com", 25);
            client.Credentials = new NetworkCredential("alirizasahin5757@hotmail.com", "aysemelike1");
            client.Host = "smtp.live.com";  //Gmail için --- smtp.gmail.com
            client.EnableSsl = true; //Gmail'de kesin olması gerekir.

            mesaj.Body = mailbody;
            client.Send(mesaj);

        }
        catch (Exception)
        {

        }
    }






    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
    public IActionResult Cikis()
    {
        HttpContext.Session.Clear();
        return RedirectToAction("Index", "Home");
        return View();
    }
}
