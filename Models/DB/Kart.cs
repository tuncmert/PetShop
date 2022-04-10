using System;
using System.Collections.Generic;
#nullable disable
namespace PET_GIDA.Models.DB
{
    public partial class Kart
    {
        public int Id { get; set; }
        public string KartSahibi { get; set; }
        public int? KartNumarasi { get; set; }
        public int? Ay { get; set; }
        public int? Yil { get; set; }
        public int? Cvv { get; set; }
    }
}
