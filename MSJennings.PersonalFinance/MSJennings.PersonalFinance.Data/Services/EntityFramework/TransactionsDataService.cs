using Microsoft.EntityFrameworkCore;
using MSJennings.PersonalFinance.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace MSJennings.PersonalFinance.Data.Services.EntityFramework
{
    public class TransactionsDataService : ITransactionsDataService
    {
        #region Private Fields

        private readonly AppDbContext _dbContext;

        #endregion Private Fields

        #region Public Constructors

        public TransactionsDataService(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        #endregion Public Constructors

        #region Public Methods

        public int CreateTransaction(Transaction transaction)
        {
            if (transaction is null)
            {
                throw new ArgumentNullException(nameof(transaction));
            }

            _dbContext.Add(transaction);
            _dbContext.SaveChanges();

            return transaction.Id;
        }

        public async Task<int> CreateTransactionAsync(Transaction transaction)
        {
            if (transaction is null)
            {
                throw new ArgumentNullException(nameof(transaction));
            }

            _dbContext.Add(transaction);
            await _dbContext.SaveChangesAsync().ConfigureAwait(false);

            return transaction.Id;
        }

        public bool DeleteTransaction(int transactionId)
        {
            var transaction = _dbContext.Transactions.SingleOrDefault(x => x.Id == transactionId);

            if (transaction is null)
            {
                return false;
            }

            _dbContext.Remove(transaction);
            _dbContext.SaveChanges();

            return true;
        }

        public async Task<bool> DeleteTransactionAsync(int transactionId)
        {
            var transaction = await _dbContext.Transactions.SingleOrDefaultAsync(x => x.Id == transactionId).ConfigureAwait(false);

            if (transaction is null)
            {
                return false;
            }

            _dbContext.Remove(transaction);
            await _dbContext.SaveChangesAsync().ConfigureAwait(false);

            return true;
        }

        public Transaction RetrieveTransaction(int transactionId)
        {
            return _dbContext.Transactions
                .AsNoTracking()
                .Include(x => x.Category)
                .SingleOrDefault(x => x.Id == transactionId);
        }

        public async Task<Transaction> RetrieveTransactionAsync(int transactionId)
        {
            return await _dbContext.Transactions
                .AsNoTracking()
                .Include(x => x.Category)
                .SingleOrDefaultAsync(x => x.Id == transactionId)
                .ConfigureAwait(false);
        }

        public IList<Transaction> RetrieveTransactions()
        {
            return _dbContext.Transactions
                .AsNoTracking()
                .Include(x => x.Category)
                .ToList();
        }

        public IList<Transaction> RetrieveTransactions(Expression<Func<Transaction, bool>> predicate)
        {
            return _dbContext.Transactions
                .AsNoTracking()
                .Include(x => x.Category)
                .Where(predicate)
                .ToList();
        }

        public async Task<IList<Transaction>> RetrieveTransactionsAsync()
        {
            return await _dbContext.Transactions
                .AsNoTracking()
                .Include(x => x.Category)
                .ToListAsync()
                .ConfigureAwait(false);
        }

        public async Task<IList<Transaction>> RetrieveTransactionsAsync(Expression<Func<Transaction, bool>> predicate)
        {
            return await _dbContext.Transactions
                .AsNoTracking()
                .Include(x => x.Category)
                .Where(predicate)
                .ToListAsync()
                .ConfigureAwait(false);
        }

        public IQueryable<Transaction> RetrieveTransactionsQuery()
        {
            return _dbContext.Transactions.Include(x => x.Category).AsNoTracking();
        }

        public bool UpdateTransaction(Transaction transaction)
        {
            if (transaction is null)
            {
                throw new ArgumentNullException(nameof(transaction));
            }

            if (transaction.Id == default)
            {
                throw new InvalidOperationException($"Cannot update {nameof(Transaction)} with no {nameof(Transaction.Id)}");
            }

            var transactionToUpdate = _dbContext.Transactions.SingleOrDefault(x => x.Id == transaction.Id);
            if (transactionToUpdate == null)
            {
                return false;
            }

            _dbContext.Entry(transactionToUpdate).CurrentValues.SetValues(transaction);
            return _dbContext.SaveChanges() > 0;
        }

        public async Task<bool> UpdateTransactionAsync(Transaction transaction)
        {
            if (transaction is null)
            {
                throw new ArgumentNullException(nameof(transaction));
            }

            if (transaction.Id == default)
            {
                throw new InvalidOperationException($"Cannot update {nameof(Transaction)} with no {nameof(Transaction.Id)}");
            }

            var transactionToUpdate = await _dbContext.Transactions.SingleOrDefaultAsync(x => x.Id == transaction.Id).ConfigureAwait(false);
            if (transactionToUpdate == null)
            {
                return false;
            }

            _dbContext.Entry(transactionToUpdate).CurrentValues.SetValues(transaction);
            return (await _dbContext.SaveChangesAsync().ConfigureAwait(false)) > 0;
        }

        #endregion Public Methods
    }
}
