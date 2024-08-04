using System.ComponentModel.DataAnnotations;

namespace EFcoreApp.Data
{
    public class KursKayit
    {
        [Key]
        public int KayitId { get; set; }

        [Required]
        public int Ogrencild { get; set; }
        public Ogrenci Ogrenci { get; set; } = null!;

        [Required]
        public int KursId { get; set; }
        public Kurs Kurs { get; set; } = null!;
        public DateTime KayitTarihi { get; set; }

    }
}
