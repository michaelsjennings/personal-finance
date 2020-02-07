using System;

namespace MSJennings.PersonalFinance.Data.Models
{
    public class Transaction
    {
        #region Public Properties

        public decimal Amount { get; set; }

        public virtual Category Category { get; set; }

        public int CategoryId { get; set; }

        public DateTime Date { get; set; }

        public int Id { get; set; }

        public bool IsCredit { get; set; }

        public string Memo { get; set; }

        #endregion Public Properties
    }
}
