using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Microsoft.AspNetCore.Mvc.Rendering;
using MSJennings.PersonalFinance.Data.Models;

namespace MSJennings.PersonalFinance.WebApp.ViewModels.Transactions
{
    public class TransactionsFilterViewModel
    {
        #region Public Properties

        [Display(Name = "Amount To")]
        public decimal? AmountRangeEnd { get; set; }

        [Display(Name = "Amount From")]
        public decimal? AmountRangeStart { get; set; }

        public SelectList CategoriesList { get; set; }

        [Display(Name = "Category")]
        public int? CategoryId { get; set; }

        [Display(Name = "Date To")]
        public DateTime? DateRangeEnd { get; set; }

        [Display(Name = "Date From")]
        public DateTime? DateRangeStart { get; set; }

        [Display(Name = "Is Credit")]
        public bool? IsCredit { get; set; }

        public SelectList IsCreditList { get; set; }

        [Display(Name = "Memo")]
        public string Memo { get; set; }

        public int PageIndex { get; set; } = 0;

        [Display(Name = "Page Size")]
        public int PageSize { get; set; } = 10;

        public SelectList PageSizesList { get; set; }

        public bool SortDescending { get; set; } = false;

        public string SortName { get; set; } = nameof(Transaction.Date);

        #endregion Public Properties

        #region Public Methods

        public IQueryable<Transaction> ApplyFilteringSortingAndPaging(IQueryable<Transaction> query)
        {
            query = ApplyFiltering(query);
            query = ApplySorting(query);
            query = ApplyPaging(query);

            return query;
        }

        #endregion Public Methods

        #region Private Methods

        private IQueryable<Transaction> ApplyFiltering(IQueryable<Transaction> query)
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
                query = query.Where(x => x.Memo.Contains(Memo));
            }

            return query;
        }

        private IQueryable<Transaction> ApplyPaging(IQueryable<Transaction> query)
        {
            if (PageSize > 0)
            {
                query = query
                    .Skip(PageIndex * PageSize)
                    .Take(PageSize);
            }

            return query;
        }

        private IQueryable<Transaction> ApplySorting(IQueryable<Transaction> query)
        {
            if (!string.IsNullOrWhiteSpace(SortName))
            {
                if (SortName.Equals(nameof(Transaction.Amount), StringComparison.OrdinalIgnoreCase))
                {
                    query = SortDescending ? query.OrderByDescending(x => x.Amount) : query.OrderBy(x => x.Amount);
                }
                else if (SortName.Equals(nameof(Category), StringComparison.OrdinalIgnoreCase))
                {
                    query = SortDescending ? query.OrderByDescending(x => x.Category.Name) : query.OrderBy(x => x.Category.Name);
                }
                else if (SortName.Equals(nameof(Transaction.Date), StringComparison.OrdinalIgnoreCase))
                {
                    query = SortDescending ? query.OrderByDescending(x => x.Date) : query.OrderBy(x => x.Date);
                }
                else if (SortName.Equals(nameof(Transaction.IsCredit), StringComparison.OrdinalIgnoreCase))
                {
                    query = SortDescending ? query.OrderByDescending(x => x.IsCredit) : query.OrderBy(x => x.IsCredit);
                }
                else if (SortName.Equals(nameof(Transaction.Memo), StringComparison.OrdinalIgnoreCase))
                {
                    query = SortDescending ? query.OrderByDescending(x => x.Memo) : query.OrderBy(x => x.Memo);
                }
                else
                {
                    throw new ArgumentOutOfRangeException(SortName);
                }
            }

            return query;
        }

        #endregion Private Methods
    }
}
