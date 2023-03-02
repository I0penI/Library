using AppCore.Business.Services.Bases;
using AppCore.Results;
using AppCore.Results.Bases;
using Business.Models;
using DataAccess.Entities;
using DataAccess.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Business.Services
{
    public interface ICategoryService : IService<CategoryModel>
    {
        Task<List<CategoryModel>> GetListAsync();
    }



    public class CategoryService :ICategoryService
    {
        private readonly CategoryRepoBase _categoryRepo;

        public CategoryService(CategoryRepoBase categoryRepo)
        {
            _categoryRepo = categoryRepo;
        }

        public Result Add(CategoryModel model)
        {
            if (_categoryRepo.Exists(c => c.Name.ToLower() == model.Name.ToLower().Trim()))
                return new ErrorResult("Category with same name exists!");
            Category entity = new Category()
            {
                Name = model.Name.Trim(),
                Description = model.Description?.Trim(),
            };
            _categoryRepo.Add(entity);
            return new SuccessResult("Category Added Successfully.");


        }

        public Result Delete(int id)
        {
            CategoryModel existingCategory = Query().SingleOrDefault(c => c.Id == id);
            if (existingCategory == null)
            {
                return new ErrorResult("Category Not Found!");
            }
            if (existingCategory.BookCountDisplay > 0)
            {
                return new ErrorResult("Category Cannot Be Deleted Because Category Contain Books!");
            }
            _categoryRepo.Delete(id);
            return new SuccessResult("Category Deleted Successfully.");

        }

        public void Dispose()
        {
            _categoryRepo.Dispose();
        }

        public async Task<List<CategoryModel>> GetListAsync()
        {
            List<CategoryModel> categories = await Query().ToListAsync();
            return categories;
        }

        public IQueryable<CategoryModel> Query()
        {
            return _categoryRepo.Query().OrderBy(c => c.Name).Select(c => new CategoryModel()
            {
                Description = c.Description,
                Id = c.Id,
                Name = c.Name,
                BookCountDisplay = c.Books.Count
                
                
            });
        }

        public Result Update(CategoryModel model)
        {
            if (_categoryRepo.Exists(c => c.Name.ToLower() == model.Name.ToLower().Trim() && c.Id != model.Id))
                return new ErrorResult("Category With Same Name Exists");
            Category entity = new Category()
            {
                Id = model.Id,
                Name = model.Name.Trim(),
                Description = model.Description?.Trim(),

            };
            _categoryRepo.Update(entity);
            return new SuccessResult("Category Updated Successfully.");
        }
    }
}
