using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using MSJennings.PersonalFinance.Data.Models;

namespace MSJennings.PersonalFinance.Data.Services
{
    public interface ITransactionsDataService
    {
        #region Public Methods

        public IQueryable<Transaction> BuildTransactionsQuery();

        public int CreateTransaction(Transaction transaction);

        public Task<int> CreateTransactionAsync(Transaction transaction);

        public bool DeleteTransaction(int transactionId);

        public Task<bool> DeleteTransactionAsync(int transactionId);

        public IList<Transaction> ExecuteTransactionsQuery(IQueryable<Transaction> query);

        public Task<IList<Transaction>> ExecuteTransactionsQueryAsync(IQueryable<Transaction> query);

        public Transaction RetrieveTransaction(int transactionId);

        public Task<Transaction> RetrieveTransactionAsync(int transactionId);

        public IList<Transaction> RetrieveTransactions();

        public IList<Transaction> RetrieveTransactions(Expression<Func<Transaction, bool>> predicate);

        public Task<IList<Transaction>> RetrieveTransactionsAsync();

        public Task<IList<Transaction>> RetrieveTransactionsAsync(Expression<Func<Transaction, bool>> predicate);

        public bool UpdateTransaction(Transaction transaction);

        public Task<bool> UpdateTransactionAsync(Transaction transaction);

        #endregion Public Methods
    }
}
