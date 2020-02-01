using System;
using System.ComponentModel.DataAnnotations;

namespace MSJennings.PersonalFinance.WebApp.ViewModels.Transactions
{
    public class TransactionDetailsViewModel
    {
        #region Public Properties

        [Display(Name = "Amount")]
        public decimal Amount { get; set; }

        public int CategoryId { get; set; }

        [Display(Name = "Category")]
        public string CategoryName { get; set; }

        [Display(Name = "Date")]
        public DateTime Date { get; set; }

        public int Id { get; set; }

        [Display(Name = "Is Credit")]
        public bool IsCredit { get; set; }

        [Display(Name = "Memo")]
        public string Memo { get; set; }

        #endregion Public Properties
    }
}
