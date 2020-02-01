using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc.Rendering;
using MSJennings.PersonalFinance.Data.Models;

namespace MSJennings.PersonalFinance.WebApp.ViewModels.Transactions
{
    public class TransactionsIndexFilterViewModel
    {
        public int PageSize { get; set; } = 10;

        public int PageIndex { get; set; } = 0;

        public string SortName { get; set; }

        public bool SortDescending { get; set; } = false;

        public decimal? AmountRangeStart { get; set; }

        public decimal? AmountRangeEnd { get; set; }

        public int? CategoryId { get; set; }

        public SelectList CategoriesList { get; set; }

        public DateTime? DateRangeStart { get; set; }

        public DateTime? DateRangeEnd { get; set; }

        public bool? IsCredit { get; set; }

        public string Memo { get; set; }

        public IQueryable<Transaction> ApplyFilteringSortingAndPating(IQueryable<Transaction> query)
        {
            query = ApplyFilter(query);
            query = ApplySort(query);
            query = ApplyPage(query);

            return query;
        }

        private IQueryable<Transaction> ApplyFilter(IQueryable<Transaction> query)
        {
            if (AmountRangeStart.HasValue)
            {
                query = query.Where(x => x.Amount >= AmountRangeStart.Value);
            }

            if (AmountRangeEnd.HasValue)
            {
                query = query.Where(x => x.Amount <= AmountRangeEnd.Value);
            }

            if (CategoryId.HasValue)
            {
                query = query.Where(x => x.CategoryId == CategoryId.Value);
            }

            if (DateRangeStart.HasValue)
            {
                query = query.Where(x => x.Date >= DateRangeStart.Value.Date);
            }

            if (DateRangeEnd.HasValue)
            {
                query = query.Where(x => x.Date < DateRangeEnd.Value.Date.AddDays(1));
            }

            if (IsCredit.HasValue)
            {
                query = query.Where(x => x.IsCredit == IsCredit.Value);
            }

            if (!string.IsNullOrWhiteSpace(Memo))
            {
                query = query.Where(x => x.Memo.Contains(Memo, StringComparison.OrdinalIgnoreCase));
            }

            return query;
        }

        private IQueryable<Transaction> ApplySort(IQueryable<Transaction> query)
        {
            if (!string.IsNullOrWhiteSpace(SortName))
            {
                Func<Transaction, dynamic> orderByFunc = (x) =>
                {
                    if (SortName.Equals(nameof(Transaction.Amount), StringComparison.OrdinalIgnoreCase))
                    {
                        return x.Amount;
                    }
                    else if (SortName.Equals(nameof(Category), StringComparison.OrdinalIgnoreCase))
                    {
                        return x.Category.Name;
                    }
                    else if (SortName.Equals(nameof(Transaction.Date), StringComparison.OrdinalIgnoreCase))
                    {
                        return x.Date;
                    }
                    else if (SortName.Equals(nameof(Transaction.IsCredit), StringComparison.OrdinalIgnoreCase))
                    {
                        return x.IsCredit;
                    }
                    else if (SortName.Equals(nameof(Transaction.Memo), StringComparison.OrdinalIgnoreCase))
                    {
                        return x.Memo;
                    }

                    throw new ArgumentOutOfRangeException(SortName);
                };

                query = (SortDescending ? query.OrderByDescending(orderByFunc) : query.OrderBy(orderByFunc)) as IOrderedQueryable<Transaction>;
            }

            return query;
        }

        private IQueryable<Transaction> ApplyPage(IQueryable<Transaction> query)
        {
            if (PageSize > 0)
            {
                query = query
                    .Skip(PageIndex * PageSize)
                    .Take(PageSize);
            }

            return query;
        }
    }
}
