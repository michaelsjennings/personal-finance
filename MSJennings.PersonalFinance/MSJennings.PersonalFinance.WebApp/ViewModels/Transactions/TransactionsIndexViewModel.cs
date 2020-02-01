using System.Collections.Generic;
using MSJennings.PersonalFinance.Data.Models;

namespace MSJennings.PersonalFinance.WebApp.ViewModels.Transactions
{
    public class TransactionsIndexViewModel
    {
        #region Public Constructors

        public TransactionsIndexViewModel()
        {
            TransactionsList = new List<Transaction>();
        }

        #endregion Public Constructors

        #region Public Properties

        public IList<Transaction> TransactionsList { get; private set; }

        #endregion Public Properties
    }
}
