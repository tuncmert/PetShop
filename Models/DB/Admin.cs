using System;
using System.Collections.Generic;

namespace PET_GIDA.Models.DB
{
    public partial class Admin
    {
        public int Id { get; set; }
        public string Ad { get; set; }
        public string Soyad { get; set; }
        public string KullaniciAdi { get; set; }
        public string Sifre { get; set; }
    }
}
