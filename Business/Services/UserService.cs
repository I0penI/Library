
using AppCore.Business.Services.Bases;
using AppCore.Results;
using AppCore.Results.Bases;
using Business.Models;
using DataAccess.Entities;
using DataAccess.Repositories;

namespace Business.Services
{

	public interface IUserService : IService<UserModel>
	{

	}
	public class UserService : IUserService
	{
		private readonly UserRepoBase _userRepo;

		public UserService(UserRepoBase userRepo)
		{
			_userRepo = userRepo;
		}

		public Result Add(UserModel model)
		{
			if (_userRepo.Exists(u => u.UserName.ToLower() == model.UserName.ToLower().Trim()))
				return new ErrorResult("The user with same name ezists!");
			User entity = new User
			{
				UserName = model.UserName,
				Password = model.Password,
				RoleId = model.RoleId,
				IsActive = model.IsActive,
				UserDetail=new UserDetail()
				{
                    Address = model.UserDetail.Address.Trim(),
                    CityId = model.UserDetail.CityId ?? 0,
                    CountryId = model.UserDetail.CountryId.Value,
                    Email = model.UserDetail.Email.Trim(),
                    Sex = model.UserDetail.Sex
                }
			};
			_userRepo.Add(entity);
			return new SuccessResult();
		}

		public Result Delete(int id)
		{
			throw new NotImplementedException();
		}

		public void Dispose()
		{
			_userRepo.Dispose();
		}

		public IQueryable<UserModel> Query()
		{
			return _userRepo.Query(u => u.Role).Select(u => new UserModel()
			{
				Id = u.Id,
				IsActive = u.IsActive,
				Password = u.Password,
				RoleId = u.RoleId,
				UserName = u.UserName,
				RoleName = u.Role.Name
			});
		}

		public Result Update(UserModel model)
		{
			throw new NotImplementedException();
		}
	}
}
