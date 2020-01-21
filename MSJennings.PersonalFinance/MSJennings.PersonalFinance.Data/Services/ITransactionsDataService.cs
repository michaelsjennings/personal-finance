using MSJennings.PersonalFinance.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace MSJennings.PersonalFinance.Data.Services
{
    public interface ITransactionsDataService
    {
        #region Public Methods

        public int CreateTransaction(Transaction transaction);

        public Task<int> CreateTransactionAsync(Transaction transaction);

        public bool DeleteTransaction(int transactionId);

        public Task<bool> DeleteTransactionAsync(int transactionId);

        public Transaction RetrieveTransaction(int transactionId);

        public Task<Transaction> RetrieveTransactionAsync(int transactionId);

        public IList<Transaction> RetrieveTransactions();

        public IList<Transaction> RetrieveTransactions(Expression<Func<Transaction, bool>> predicate);

        public Task<IList<Transaction>> RetrieveTransactionsAsync();

        public Task<IList<Transaction>> RetrieveTransactionsAsync(Expression<Func<Transaction, bool>> predicate);

        public IQueryable<Transaction> RetrieveTransactionsQuery();

        public bool UpdateTransaction(Transaction transaction);

        public Task<bool> UpdateTransactionAsync(Transaction transaction);

        #endregion Public Methods
    }
}