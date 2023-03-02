
using AppCore.DataAccess.EntityFramework.Bases;
using DataAccess.Contexts;
using DataAccess.Entities;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Repositories
{
	public abstract class UserRepoBase : RepoBase<User>
	{
		protected UserRepoBase(LibraryContext dbContext) : base(dbContext)
		{
		}
	}

	public class UserRepo : UserRepoBase
	{
		public UserRepo(LibraryContext dbContext) : base(dbContext)
		{
		}
	}
}
