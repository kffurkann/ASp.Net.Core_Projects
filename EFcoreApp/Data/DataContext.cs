using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System;

namespace EFcoreApp.Data
{
    public class DataContext:DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }

        public DbSet<Kurs> Kurslar => Set<Kurs>(); // =null!; onun yerine initialize ettik.

        public DbSet<Ogrenci> Ogrenciler => Set<Ogrenci>();

        public DbSet<KursKayit> KursKayitlari => Set<KursKayit>();
        public DbSet<Ogretmen> Ogretmenler => Set<Ogretmen>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // KursKayit ile Ogrenci arasındaki ilişki
            modelBuilder.Entity<KursKayit>()
                .HasOne(kk => kk.Ogrenci)
                .WithMany(o => o.KursKayitlari)
                .HasForeignKey(kk => kk.Ogrencild)
                .OnDelete(DeleteBehavior.Restrict);

            // KursKayit ile Kurs arasındaki ilişki
            modelBuilder.Entity<KursKayit>()
                .HasOne(kk => kk.Kurs)
                .WithMany(k => k.KursKayitlari)
                .HasForeignKey(kk => kk.KursId)
                .OnDelete(DeleteBehavior.Restrict);

            // Kurs ile Ogretmen arasındaki ilişki
            modelBuilder.Entity<Kurs>()
                .HasOne(k => k.Ogretmen)
                .WithMany(o => o.Kurslar)
                .HasForeignKey(k => k.OgretmenId)
                .OnDelete(DeleteBehavior.Restrict); // Ogretmen silindiğinde Kurs OgretmenId'yi null yapar
        }
    }
}
