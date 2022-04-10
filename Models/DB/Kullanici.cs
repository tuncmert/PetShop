using System;
using System.Collections.Generic;

namespace PET_GIDA.Models.DB
{
    public partial class Kullanici
    {
        public int Id { get; set; }
        public string KullaniciAdi { get; set; }
        public string Sifre { get; set; }
        public string Email { get; set; }
    }
}
