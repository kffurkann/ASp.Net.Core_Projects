using EFcoreApp.Data;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace EFcoreApp.Models
{
    public class KursViewModel
    {
        public int KursId { get; set; }

        [Required]
        [Display(Name="Kurs Başlığı")]//formdaki yeri labelı kaldırman lazım yoksa ezer
        public string? Baslik { get; set; }

        [Required]
        public int OgretmenId { get; set; }
        public ICollection<KursKayit> KursKayitlari { get; set; } = new List<KursKayit>();

    }
}
