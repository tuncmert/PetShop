
using Microsoft.AspNetCore.Mvc;
using PET_GIDA.Models.DB;

namespace PET_GIDA.Components
{
    [ViewComponent]
    public class KullaniciViewComponent : ViewComponent
    {
        private readonly BITIRMEContext _context;
        public KullaniciViewComponent(BITIRMEContext context)
        {
            _context = context;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var loginId = HttpContext.Session.GetString("loginId");
            try
            {
                var loginIdInt = Convert.ToInt32(loginId);
                var Kullanici = _context.Kullanicis.FirstOrDefault(a => a.Id == loginIdInt);
                return View(Kullanici);
            }
            catch (System.Exception)
            {
                #if DEBUG
                throw;
                #endif
            }
        }
    }
}