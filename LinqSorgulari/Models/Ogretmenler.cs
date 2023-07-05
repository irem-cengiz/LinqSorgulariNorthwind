using System;
using System.Collections.Generic;

namespace LinqSorgulari.Models
{
    public partial class Ogretmenler
    {
        public int OgretmenId { get; set; }
        public string OgretmenAd { get; set; } = null!;
        public string OgretmenSoyAd { get; set; } = null!;
    }
}
