using System;
using System.Collections.Generic;
#nullable disable
namespace PET_GIDA.Models.DB
{
    public partial class Urunler
    {
        public Urunler()
        {
            SiparisListesis = new HashSet<SiparisListesi>();
        }

        public int UrunId { get; set; }
        public string UrunAd { get; set; }
        public decimal UrunFiyat { get; set; }
        public string UrunKisaAciklama { get; set; }
        public string UrunUzunAciklama { get; set; }
        public int? UrunStok { get; set; }
        public byte[] UrunResim1 { get; set; }
        public byte[] UrunResim2 { get; set; }
        public byte[] UrunResim3 { get; set; }
        public byte[] UrunResim4 { get; set; }
        public int? KategoriId { get; set; }
        public bool? AnaSayfa { get; set; }
        public bool? KargoyaVerildi { get; set; }
        public bool? TeslimEdildi { get; set; }

        public virtual ICollection<SiparisListesi> SiparisListesis { get; set; }
    }
}
