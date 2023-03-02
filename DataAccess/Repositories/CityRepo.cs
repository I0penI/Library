using AppCore.DataAccess.EntityFramework.Bases;
using DataAccess.Contexts;
using DataAccess.Entities;


namespace DataAccess.Repositories
{
    public abstract class CityRepoBase : RepoBase<City>
    {
        protected CityRepoBase(LibraryContext dbContext) : base(dbContext)
        {
        }
    }
    public class CityRepo : CityRepoBase
    {
        public CityRepo(LibraryContext dbContext) : base(dbContext)
        {
        }
    }
}
