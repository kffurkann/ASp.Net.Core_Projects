using System.ComponentModel.DataAnnotations;

namespace EFcoreApp.Data
{
    public class Ogrenci
    {
        [Key]
        public int Ogrencild { get; set; }
        public string? OgrenciAd { get; set; }
        public string? OgrenciSoyad { get; set; }
        public string AdSoyad 
        { get  
            { return this.OgrenciAd + " " + this.OgrenciSoyad; } 
        }
        public string? Eposta { get; set; }
        public string? Telefon { get; set; }
        public ICollection<KursKayit> KursKayitlari { get; set; } = new List<KursKayit>();
    }
}
