using AppCore.DataAccess.EntityFramework.Bases;
using DataAccess.Contexts;
using DataAccess.Entities;

namespace DataAccess.Repositories
{
    public abstract class WriterRepoBase : RepoBase<Writer>
    {
        protected WriterRepoBase(LibraryContext dbContext) : base(dbContext)
        {

        }
    }
    public class WriterRepo : WriterRepoBase
    {
        public WriterRepo(LibraryContext dbContext) : base(dbContext)
        {
        }
    }
}
