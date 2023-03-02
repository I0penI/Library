

using AppCore.DataAccess.EntityFramework.Bases;
using Business.Models.Report;
using DataAccess.Entities;
using DataAccess.Repositories;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Business.Services.Report
{
    public interface IReportService
    {
        List<ReportItemModel> GetReport(ReportFilterModel filter, bool useInnerJoin = false);
    }
    public class ReportService : IReportService
    {
        private readonly BookRepoBase _brepo;
        public ReportService(BookRepoBase repo)
        {
            _brepo = repo;
        }


        public List<ReportItemModel> GetReport(ReportFilterModel filter, bool useInnerJoin = false)
        {
            var BookQuery = _brepo.Query();
            var categoryQuery = _brepo.Query<Category>();
            var WriterQuery = _brepo.Query<Writer>();
            var BookWriterQuery = _brepo.Query<BookWriter>();

            IQueryable<ReportItemModel> query = null;
            if (useInnerJoin)
            {
                query = from p in BookQuery
                        join c in categoryQuery
                        on p.CategoryId equals c.Id
                        join ps in BookWriterQuery
                        on p.Id equals ps.BookId
                        join s in WriterQuery
                        on ps.WriterId equals s.Id
                        where p.Id == 5 || (p.StockAmount >= 10 && p.StockAmount <= 20)
                        orderby s.Name descending, c.Name, p.Name descending
                        select new ReportItemModel
                        {
                            CategoryDescription = c.Description,
                            CategoryName = c.Name,
                            BookDescription = p.Description,
                            BookName = p.Name,
                            StockAmount = p.StockAmount + " pieces ",
                            WriterName = s.Name,


                            CategoryId = c.Id,
                            WriterId = s.Id
                        };
            }
            else
            {
                query = from p in BookQuery
                        join c in categoryQuery
                        on p.CategoryId equals c.Id into categoryJoin
                        from category in categoryJoin.DefaultIfEmpty()
                        join ps in BookWriterQuery
                        on p.Id equals ps.BookId into BookWriterJoin
                        from BookWriter in BookWriterJoin.DefaultIfEmpty()
                        join s in WriterQuery
                        on BookWriter.WriterId equals s.Id into WriterJoin
                        from Writer in WriterJoin.DefaultIfEmpty()
                        select new ReportItemModel()
                        {
                            CategoryDescription = category.Description,
                            CategoryName = category.Name,
                            BookDescription = p.Description,
                            BookName = p.Name,
                            StockAmount = $" {p.StockAmount} piece(s)",
                            WriterName = Writer.Name,

                            CategoryId = category.Id,
                            WriterId = Writer.Id
                        };
            }
            query = query.OrderBy(q => q.WriterName).ThenBy(q => q.CategoryName).ThenBy(q => q.BookName);

            if (filter is not null)
            {
                if (filter.CategoryId.HasValue)
                    query = query.Where(q => q.CategoryId == filter.CategoryId);
                if (!string.IsNullOrWhiteSpace(filter.BookName))
                    query = query.Where(q => q.BookName.ToLower().Contains(filter.BookName.ToLower().Trim()));
                if (filter.WriterIds is not null && filter.WriterIds.Count > 0)
                    query = query.Where(q => filter.WriterIds.Contains(q.WriterId ?? 0));
            }
            return query.ToList();
        }
    }
}

