using AppCore.DataAccess.EntityFramework.Bases;
using DataAccess.Contexts;
using DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories
{

    public abstract class CountryRepoBase : RepoBase<Country>
    {
        protected CountryRepoBase(LibraryContext dbContext) : base(dbContext)
        {
        }
    }

    public class CountryRepo : CountryRepoBase
    {
        public CountryRepo(LibraryContext dbContext) : base(dbContext)
        {
        }
    }
}
