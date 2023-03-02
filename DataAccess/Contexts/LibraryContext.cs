using DataAccess.Entities;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Contexts
{
    public class LibraryContext :DbContext
    {
        public DbSet<Book> Books { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Writer> Writers { get; set; }
        public DbSet<BookWriter> BookWriters { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<UserDetail> UserDetails { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<City> Cities { get; set; }

        public LibraryContext(DbContextOptions options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<BookWriter>().HasKey(ps => new { ps.BookId, ps.WriterId });

            modelBuilder.Entity<BookWriter>().HasOne(ps => ps.Book).WithMany(p => p.BookWriters).HasForeignKey(ps => ps.BookId).OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<BookWriter>().HasOne(ps => ps.Writer).WithMany(s => s.BookWriter).HasForeignKey(ps => ps.WriterId).OnDelete(DeleteBehavior.NoAction);


            modelBuilder.Entity<Book>()
                .HasOne(c => c.Category)
                .WithMany(b => b.Books)
                .HasForeignKey(b => b.CategoryId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<City>()
                .HasOne(co => co.Country)
                .WithMany(ci => ci.Cities)
                .HasForeignKey(ci => ci.CountryId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<UserDetail>()
                .HasOne(ud => ud.Country)
                .WithMany(co => co.UserDetails)
                .HasForeignKey(ud => ud.CountryId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<UserDetail>()
                .HasOne(ud => ud.City)
                .WithMany(ci => ci.UserDetails)
                .HasForeignKey(ud => ud.CityId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<UserDetail>()
                .HasOne(u => u.User)
                .WithOne(ud => ud.UserDetail)
                .HasForeignKey<UserDetail>(ud => ud.UserId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<UserDetail>()
                .HasIndex(ud => ud.Email).IsUnique(true);

            modelBuilder.Entity<Book>()
                .HasIndex(p => p.Name);
        }
    }
}
