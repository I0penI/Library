using AppCore.DataAccess.EntityFramework.Bases;
using DataAccess.Contexts;
using DataAccess.Entities;

namespace DataAccess.Repositories
{
    public abstract class BookRepoBase : RepoBase<Book>
    {
        protected BookRepoBase(LibraryContext dbContext) : base(dbContext) 
        { 
        }
    }
    public class BookRepo : BookRepoBase
    {
        public BookRepo(LibraryContext dbContext) : base(dbContext) 
        {
        }
    }
}
