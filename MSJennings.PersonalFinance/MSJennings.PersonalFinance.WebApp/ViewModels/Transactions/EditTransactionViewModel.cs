using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace MSJennings.PersonalFinance.WebApp.ViewModels.Transactions
{
    public class EditTransactionViewModel
    {
        #region Public Properties

        [Required]
        [Display(Name = "Amount")]
        public decimal Amount { get; set; }

        public SelectList CategoriesList { get; set; }

        [Required]
        [Display(Name = "Category")]
        public int CategoryId { get; set; }

        [Required]
        [Display(Name = "Date")]
        public DateTime Date { get; set; }

        [Required]
        public int Id { get; set; }

        [Required]
        [Display(Name = "Is Credit")]
        public bool IsCredit { get; set; }

        [Display(Name = "Memo")]
        public string Memo { get; set; }

        #endregion Public Properties
    }
}
