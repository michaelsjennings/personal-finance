using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace MSJennings.PersonalFinance.WebApp.ViewModels.Transactions
{
    public class AddTransactionViewModel
    {
        #region Public Properties

        [Required]
        public decimal Amount { get; set; }

        public SelectList Category { get; private set; }

        [Required]
        public int CategoryId { get; set; }

        [Required]
        public DateTime Date { get; set; }

        [Required]
        public bool IsCredit { get; set; }

        public string Memo { get; set; }

        #endregion Public Properties
    }
}
