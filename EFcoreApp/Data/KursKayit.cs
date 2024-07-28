using System.ComponentModel.DataAnnotations;

namespace EFcoreApp.Data
{
    public class KursKayit
    {
        [Key]
        public int KayitId { get; set; }
        public int Ogrencild { get; set; }
        public int KursId { get; set; }
        public DateTime KayitTarihi { get; set; }
    }
}
