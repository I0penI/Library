using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace DataAccess.Contexts
{
    public class LibraryContextFactory : IDesignTimeDbContextFactory<LibraryContext>
    {
        public LibraryContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<LibraryContext>();
            optionsBuilder.UseSqlServer("server=.\\SQLEXPRESS;database=Library;user id=sa;password=sa;multipleactiveresultsets=true;trustservercertificate=true;"
);
            

            return new LibraryContext(optionsBuilder.Options); 
        }
    }
    
    
}
