using System;
using System.Collections.Generic;
#nullable disable
namespace PET_GIDA.Models.DB
{
    public partial class SiparisListesi
    {
        public int Id { get; set; }
        public int? Tell { get; set; }
        public int? SiparisNo { get; set; }
        public string Mail { get; set; }
        public string? SiparisTarihi { get; set; }
        public string? SiparisDurumu { get; set; }
        public int UrunId { get; set; }
        public int? UrunAdet { get; set; }

        public virtual Urunler Urun { get; set; }
    }
}
