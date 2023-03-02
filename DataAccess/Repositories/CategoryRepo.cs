using AppCore.DataAccess.EntityFramework.Bases;
using DataAccess.Contexts;
using DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories
{
    public abstract class CategoryRepoBase : RepoBase<Category>
    {
        protected CategoryRepoBase(LibraryContext dbContext) : base(dbContext)
        {
        }
    }
    public class CategoryRepo : CategoryRepoBase
    {
        public CategoryRepo(LibraryContext dbContext) : base (dbContext)
        {
        }
    }
}
