using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WebApplicationProject.Models;

//Veritabanında EF Tablo oluşturması için ilgili model sınıflarını Utulity>AppDbContext ekleriz.

namespace WebApplicationProject.Utility
{
    public class AppDbContext : IdentityDbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<BookType> BookTypes { get; set; } //Tablonun adını belirledik.
        public DbSet<Book> Books { get; set; }
        public DbSet<Hire> Hires { get; set; }
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
    }
}
