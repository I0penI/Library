using AppCore.Business.Services.Bases;
using AppCore.Records.Bases;
using AppCore.Results;
using AppCore.Results.Bases;
using Business.Models;
using DataAccess.Entities;
using DataAccess.Repositories;

namespace Business.Services
{
    public interface IWriterService : IService<WriterModel>
    {

    }
    public class WriterService : IWriterService
    {

        private readonly WriterRepoBase _writerRepo;

        public WriterService(WriterRepoBase writerRepo)
        {
            _writerRepo = writerRepo;
        }
        public Result Add(WriterModel model)
        {
            if (_writerRepo.Exists(w => w.Name.ToLower() == model.Name.Trim()))
                return new ErrorResult("Writer with same name exists!");          
            Writer entity = new Writer()
            {
               Id= model.Id,
               Name = model.Name.Trim(),
               Surname = model.Surname.Trim(),
            };
            _writerRepo.Add(entity);
            return new SuccessResult("Writer added successfully.");
        }

        public Result Delete(int id)
        {
            WriterModel existingWriter = Query().SingleOrDefault(w => w.Id == id);
            if (existingWriter == null)
            {
                return new ErrorResult("Writer Not Found!");
            }
            if (existingWriter.BookCountDisplay > 0)
            {
                return new ErrorResult("Writer Cannot Be Deleted Because Writer Contain Products!");
            }
            _writerRepo.Delete(id);
            return new SuccessResult("Writer Deleted Successfully.");
        }

        public void Dispose()
        {
            _writerRepo.Dispose();
        }

        public IQueryable<WriterModel> Query()
        {
            return _writerRepo.Query().OrderBy(w => w.Name).Select(w => new WriterModel()
            {
                Id= w.Id,
                Name= w.Name,
                Surname= w.Surname,
                NameSurnameDisplay=w.Name + " " +w.Surname

            });
        }

        public Result Update(WriterModel model)
        {
            if (_writerRepo.Exists(p => p.Name.ToLower() == model.Name.ToLower().Trim() && p.Id != model.Id))
                return new ErrorResult("Writer with same name exists!");
            Writer entity = new     Writer()
            {
                Id = model.Id,                             
                Name = model.Name.Trim(),
                Surname = model.Surname,
              
              
            };
            _writerRepo.Update(entity);
            return new SuccessResult("Writer updated successfully");
        }
    }
}
