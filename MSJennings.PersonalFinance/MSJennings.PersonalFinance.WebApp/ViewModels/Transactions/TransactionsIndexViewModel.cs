using System.Collections.Generic;
using MSJennings.PersonalFinance.Data.Models;

namespace MSJennings.PersonalFinance.WebApp.ViewModels.Transactions
{
    public class TransactionsIndexViewModel
    {
        #region Public Constructors

        public TransactionsIndexViewModel()
        {
            TransactionsFilter = new TransactionsFilterViewModel();
            TransactionsList = new List<Transaction>();
        }

        #endregion Public Constructors

        #region Public Properties

        public TransactionsFilterViewModel TransactionsFilter { get; private set; }

        public IList<Transaction> TransactionsList { get; private set; }

        #endregion Public Properties
    }
}
