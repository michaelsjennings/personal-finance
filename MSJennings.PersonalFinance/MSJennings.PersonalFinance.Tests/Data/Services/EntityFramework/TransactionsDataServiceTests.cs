using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MSJennings.PersonalFinance.Data.Models;
using MSJennings.PersonalFinance.Data.Services.EntityFramework;
using System;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace MSJennings.PersonalFinance.Tests.Data.Services.EntityFramework
{
    [TestClass]
    public class TransactionsDataServiceTests
    {
        #region Public Methods

        [TestMethod]
        public void CreateTransaction_WithNullTransaction_ShouldThrowException()
        {
            // Arrange
            using var dbContext = GetDbContext();

            var service = new TransactionsDataService(dbContext);

            Exception caughtException = null;

            // Act
            try
            {
                service.CreateTransaction(null);
            }
            catch (Exception exception)
            {
                caughtException = exception;
            }

            // Assert
            Assert.IsNotNull(caughtException, FailureMessages.ExceptionNotThrown);
            Assert.IsInstanceOfType(caughtException, typeof(ArgumentNullException), FailureMessages.ExceptionNotExpectedType);
        }

        [TestMethod]
        public void CreateTransaction_WithValidTransaction_ShouldSaveAndReturnId()
        {
            // Arrange
            var transaction = new Transaction
            {
                Amount = 123.45m,
                CategoryId = 101,
                Date = DateTime.Today,
                IsCredit = true,
                Memo = "This is a test"
            };

            using var dbContext = GetDbContext();

            var service = new TransactionsDataService(dbContext);

            // Act
            var result = service.CreateTransaction(transaction);

            // Assert
            Assert.AreNotEqual(default, result, FailureMessages.ResultNotExpectedValue);
            Assert.AreEqual(1, dbContext.Transactions.Count(), FailureMessages.IncorrectItemCount);

            var createdTransaction = dbContext.Transactions.Single(x => x.Id == result);
            Assert.AreEqual(transaction.Amount, createdTransaction.Amount, FailureMessages.ValueNotExpected);
            Assert.AreEqual(transaction.CategoryId, createdTransaction.CategoryId, FailureMessages.ValueNotExpected);
            Assert.AreEqual(transaction.Date, createdTransaction.Date, FailureMessages.ValueNotExpected);
            Assert.AreEqual(transaction.IsCredit, createdTransaction.IsCredit, FailureMessages.ValueNotExpected);
            Assert.AreEqual(transaction.Memo, createdTransaction.Memo, FailureMessages.ValueNotExpected);
        }

        [TestMethod]
        public async Task CreateTransactionAsync_WithNullTransaction_ShouldThrowException()
        {
            // Arrange
            using var dbContext = GetDbContext();

            var service = new TransactionsDataService(dbContext);

            Exception caughtException = null;

            // Act
            try
            {
                await service.CreateTransactionAsync(null);
            }
            catch (Exception exception)
            {
                caughtException = exception;
            }

            // Assert
            Assert.IsNotNull(caughtException, FailureMessages.ExceptionNotThrown);
            Assert.IsInstanceOfType(caughtException, typeof(ArgumentNullException), FailureMessages.ExceptionNotExpectedType);
        }

        [TestMethod]
        public async Task CreateTransactionAsync_WithValidTransaction_ShouldSaveAndReturnId()
        {
            // Arrange
            var transaction = new Transaction
            {
                Amount = 123.45m,
                CategoryId = 101,
                Date = DateTime.Today,
                IsCredit = true,
                Memo = "This is a test"
            };

            using var dbContext = GetDbContext();

            var service = new TransactionsDataService(dbContext);

            // Act
            var result = await service.CreateTransactionAsync(transaction);

            // Assert
            Assert.AreNotEqual(default, result, FailureMessages.ResultNotExpectedValue);
            Assert.AreEqual(1, dbContext.Transactions.Count(), FailureMessages.IncorrectItemCount);

            var createdTransaction = dbContext.Transactions.Single(x => x.Id == result);
            Assert.AreEqual(transaction.Amount, createdTransaction.Amount, FailureMessages.ValueNotExpected);
            Assert.AreEqual(transaction.CategoryId, createdTransaction.CategoryId, FailureMessages.ValueNotExpected);
            Assert.AreEqual(transaction.Date, createdTransaction.Date, FailureMessages.ValueNotExpected);
            Assert.AreEqual(transaction.IsCredit, createdTransaction.IsCredit, FailureMessages.ValueNotExpected);
            Assert.AreEqual(transaction.Memo, createdTransaction.Memo, FailureMessages.ValueNotExpected);
        }

        [TestMethod]
        public void DeleteTransaction_WithInvalidTransactionId_ShouldReturnFalse()
        {
            // Arrange
            using var dbContext = GetDbContext();

            var service = new TransactionsDataService(dbContext);

            // Act
            var result = service.DeleteTransaction(201);

            // Assert
            Assert.IsFalse(result, FailureMessages.ResultNotExpectedValue);
        }

        [TestMethod]
        public void DeleteTransaction_WithValidTransactionId_ShouldRemoveAndReturnTrue()
        {
            // Arrange
            var transaction = new Transaction
            {
                Amount = 123.45m,
                CategoryId = 101,
                Date = DateTime.Today,
                Id = 201,
                IsCredit = true,
                Memo = "This is a test"
            };

            using var dbContext = GetDbContext();

            dbContext.Add(transaction);
            dbContext.SaveChanges();

            var service = new TransactionsDataService(dbContext);

            // Pre-Assert
            Assert.AreEqual(1, dbContext.Transactions.Count(), FailureMessages.IncorrectItemCount);

            // Act
            var result = service.DeleteTransaction(transaction.Id);

            // Assert
            Assert.IsTrue(result, FailureMessages.ResultNotExpectedValue);
            Assert.AreEqual(0, dbContext.Transactions.Count(), FailureMessages.IncorrectItemCount);
        }

        [TestMethod]
        public async Task DeleteTransactionAsync_WithInvalidTransactionId_ShouldReturnFalse()
        {
            // Arrange
            using var dbContext = GetDbContext();

            var service = new TransactionsDataService(dbContext);

            // Act
            var result = await service.DeleteTransactionAsync(201);

            // Assert
            Assert.IsFalse(result, FailureMessages.ResultNotExpectedValue);
        }

        [TestMethod]
        public async Task DeleteTransactionAsync_WithValidTransactionId_ShouldRemoveAndReturnTrue()
        {
            // Arrange
            var transaction = new Transaction
            {
                Amount = 123.45m,
                CategoryId = 101,
                Date = DateTime.Today,
                Id = 201,
                IsCredit = true,
                Memo = "This is a test"
            };

            using var dbContext = GetDbContext();

            dbContext.Add(transaction);
            dbContext.SaveChanges();

            var service = new TransactionsDataService(dbContext);

            // Pre-Assert
            Assert.AreEqual(1, dbContext.Transactions.Count(), FailureMessages.IncorrectItemCount);

            // Act
            var result = await service.DeleteTransactionAsync(transaction.Id);

            // Assert
            Assert.IsTrue(result, FailureMessages.ResultNotExpectedValue);
            Assert.AreEqual(0, dbContext.Transactions.Count(), FailureMessages.IncorrectItemCount);
        }

        [TestMethod]
        public void RetrieveTransaction_WithInvalidTransactionId_ShouldReturnNull()
        {
            // Arrange
            using var dbContext = GetDbContext();

            var service = new TransactionsDataService(dbContext);

            // Act
            var result = service.RetrieveTransaction(201);

            // Assert
            Assert.IsNull(result, FailureMessages.ResultIsNotNull);
        }

        [TestMethod]
        public void RetrieveTransaction_WithValidTransactionId_ShouldReturnTransaction()
        {
            // Arrange
            var transaction = new Transaction
            {
                Amount = 123.45m,
                CategoryId = 101,
                Date = DateTime.Today,
                Id = 201,
                IsCredit = true,
                Memo = "This is a test"
            };

            using var dbContext = GetDbContext();

            dbContext.Add(transaction);
            dbContext.SaveChanges();

            var service = new TransactionsDataService(dbContext);

            // Act
            var result = service.RetrieveTransaction(transaction.Id);

            // Assert
            Assert.IsNotNull(result, FailureMessages.ResultIsNull);
            Assert.AreEqual(transaction.Amount, result.Amount, FailureMessages.ValueNotExpected);
            Assert.AreEqual(transaction.CategoryId, result.CategoryId, FailureMessages.ValueNotExpected);
            Assert.AreEqual(transaction.Date, result.Date, FailureMessages.ValueNotExpected);
            Assert.AreEqual(transaction.IsCredit, result.IsCredit, FailureMessages.ValueNotExpected);
            Assert.AreEqual(transaction.Memo, result.Memo, FailureMessages.ValueNotExpected);
        }

        [TestMethod]
        public async Task RetrieveTransactionAsync_WithInvalidTransactionId_ShouldReturnNull()
        {
            // Arrange
            using var dbContext = GetDbContext();

            var service = new TransactionsDataService(dbContext);

            // Act
            var result = await service.RetrieveTransactionAsync(201);

            // Assert
            Assert.IsNull(result, FailureMessages.ResultIsNotNull);
        }

        [TestMethod]
        public async Task RetrieveTransactionAsync_WithValidTransactionId_ShouldReturnTransaction()
        {
            // Arrange
            var transaction = new Transaction
            {
                Amount = 123.45m,
                CategoryId = 101,
                Date = DateTime.Today,
                Id = 201,
                IsCredit = true,
                Memo = "This is a test"
            };

            using var dbContext = GetDbContext();

            dbContext.Add(transaction);
            dbContext.SaveChanges();

            var service = new TransactionsDataService(dbContext);

            // Act
            var result = await service.RetrieveTransactionAsync(transaction.Id);

            // Assert
            Assert.IsNotNull(result, FailureMessages.ResultIsNull);
            Assert.AreEqual(transaction.Amount, result.Amount, FailureMessages.ValueNotExpected);
            Assert.AreEqual(transaction.CategoryId, result.CategoryId, FailureMessages.ValueNotExpected);
            Assert.AreEqual(transaction.Date, result.Date, FailureMessages.ValueNotExpected);
            Assert.AreEqual(transaction.IsCredit, result.IsCredit, FailureMessages.ValueNotExpected);
            Assert.AreEqual(transaction.Memo, result.Memo, FailureMessages.ValueNotExpected);
        }

        [TestMethod]
        public void RetrieveTransactions_WithNoPredicate_ShouldReturnAllTransactions()
        {
            // Arrange
            var transactions = new[]
            {
                new Transaction
                {
                    Amount = 123.45m,
                    CategoryId = 101,
                    Date = DateTime.Today,
                    Id = 201,
                    IsCredit = true,
                    Memo = "This is a test"
                },
                new Transaction
                {
                    Amount = 234.56m,
                    CategoryId = 102,
                    Date = DateTime.Today.AddDays(-1),
                    Id = 202,
                    IsCredit = true,
                    Memo = "This is another test"
                },
                new Transaction
                {
                    Amount = 345.67m,
                    CategoryId = 101,
                    Date = DateTime.Today.AddDays(1),
                    Id = 203,
                    IsCredit = false,
                    Memo = "This is yet another test"
                }
            };

            using var dbContext = GetDbContext();

            dbContext.AddRange(transactions);
            dbContext.SaveChanges();

            var service = new TransactionsDataService(dbContext);

            // Pre-Assert
            Assert.AreEqual(3, dbContext.Transactions.Count(), FailureMessages.IncorrectItemCount);

            // Act
            var result = service.RetrieveTransactions();

            // Assert
            Assert.IsNotNull(result, FailureMessages.ResultIsNull);
            Assert.AreEqual(3, result.Count, FailureMessages.IncorrectItemCount);
            Assert.IsTrue(result.Any(x => x.Id == 201), FailureMessages.MissingExpectedItem);
            Assert.IsTrue(result.Any(x => x.Id == 202), FailureMessages.MissingExpectedItem);
            Assert.IsTrue(result.Any(x => x.Id == 203), FailureMessages.MissingExpectedItem);
        }

        [TestMethod]
        public void RetrieveTransactions_WithPredicate_ShouldReturnExpectedTransactions()
        {
            // Arrange
            var transactions = new[]
            {
                new Transaction
                {
                    Amount = 123.45m,
                    CategoryId = 101,
                    Date = DateTime.Today,
                    Id = 201,
                    IsCredit = true,
                    Memo = "This is a test"
                },
                new Transaction
                {
                    Amount = 234.56m,
                    CategoryId = 102,
                    Date = DateTime.Today.AddDays(-1),
                    Id = 202,
                    IsCredit = true,
                    Memo = "This is another test"
                },
                new Transaction
                {
                    Amount = 345.67m,
                    CategoryId = 101,
                    Date = DateTime.Today.AddDays(1),
                    Id = 203,
                    IsCredit = false,
                    Memo = "This is yet another test"
                }
            };

            using var dbContext = GetDbContext();

            dbContext.AddRange(transactions);
            dbContext.SaveChanges();

            var service = new TransactionsDataService(dbContext);

            // Pre-Assert
            Assert.AreEqual(3, dbContext.Transactions.Count(), FailureMessages.IncorrectItemCount);

            // Act
            var result = service.RetrieveTransactions(x => x.CategoryId == 101);

            // Assert
            Assert.IsNotNull(result, FailureMessages.ResultIsNull);
            Assert.AreEqual(2, result.Count, FailureMessages.IncorrectItemCount);
            Assert.IsTrue(result.Any(x => x.Id == 201), FailureMessages.MissingExpectedItem);
            Assert.IsTrue(result.Any(x => x.Id == 203), FailureMessages.MissingExpectedItem);
            Assert.IsFalse(result.Any(x => x.Id == 202), FailureMessages.UnexpectedItem);
        }

        [TestMethod]
        public async Task RetrieveTransactionsAsync_WithNoPredicate_ShouldReturnAllTransactions()
        {
            // Arrange
            var transactions = new[]
            {
                new Transaction
                {
                    Amount = 123.45m,
                    CategoryId = 101,
                    Date = DateTime.Today,
                    Id = 201,
                    IsCredit = true,
                    Memo = "This is a test"
                },
                new Transaction
                {
                    Amount = 234.56m,
                    CategoryId = 102,
                    Date = DateTime.Today.AddDays(-1),
                    Id = 202,
                    IsCredit = true,
                    Memo = "This is another test"
                },
                new Transaction
                {
                    Amount = 345.67m,
                    CategoryId = 101,
                    Date = DateTime.Today.AddDays(1),
                    Id = 203,
                    IsCredit = false,
                    Memo = "This is yet another test"
                }
            };

            using var dbContext = GetDbContext();

            dbContext.AddRange(transactions);
            dbContext.SaveChanges();

            var service = new TransactionsDataService(dbContext);

            // Pre-Assert
            Assert.AreEqual(3, dbContext.Transactions.Count(), FailureMessages.IncorrectItemCount);

            // Act
            var result = await service.RetrieveTransactionsAsync();

            // Assert
            Assert.IsNotNull(result, FailureMessages.ResultIsNull);
            Assert.AreEqual(3, result.Count, FailureMessages.IncorrectItemCount);
            Assert.IsTrue(result.Any(x => x.Id == 201), FailureMessages.MissingExpectedItem);
            Assert.IsTrue(result.Any(x => x.Id == 202), FailureMessages.MissingExpectedItem);
            Assert.IsTrue(result.Any(x => x.Id == 203), FailureMessages.MissingExpectedItem);
        }

        [TestMethod]
        public async Task RetrieveTransactionsAsync_WithPredicate_ShouldReturnExpectedTransactions()
        {
            // Arrange
            var transactions = new[]
            {
                new Transaction
                {
                    Amount = 123.45m,
                    CategoryId = 101,
                    Date = DateTime.Today,
                    Id = 201,
                    IsCredit = true,
                    Memo = "This is a test"
                },
                new Transaction
                {
                    Amount = 234.56m,
                    CategoryId = 102,
                    Date = DateTime.Today.AddDays(-1),
                    Id = 202,
                    IsCredit = true,
                    Memo = "This is another test"
                },
                new Transaction
                {
                    Amount = 345.67m,
                    CategoryId = 101,
                    Date = DateTime.Today.AddDays(1),
                    Id = 203,
                    IsCredit = false,
                    Memo = "This is yet another test"
                }
            };

            using var dbContext = GetDbContext();

            dbContext.AddRange(transactions);
            dbContext.SaveChanges();

            var service = new TransactionsDataService(dbContext);

            // Pre-Assert
            Assert.AreEqual(3, dbContext.Transactions.Count(), FailureMessages.IncorrectItemCount);

            // Act
            var result = await service.RetrieveTransactionsAsync(x => x.CategoryId == 101);

            // Assert
            Assert.IsNotNull(result, FailureMessages.ResultIsNull);
            Assert.AreEqual(2, result.Count, FailureMessages.IncorrectItemCount);
            Assert.IsTrue(result.Any(x => x.Id == 201), FailureMessages.MissingExpectedItem);
            Assert.IsTrue(result.Any(x => x.Id == 203), FailureMessages.MissingExpectedItem);
            Assert.IsFalse(result.Any(x => x.Id == 202), FailureMessages.UnexpectedItem);
        }

        [TestMethod]
        public void RetrieveTransactionsQuery_ShouldReturnTransactionsQuery()
        {
            // Arrange
            using var dbContext = GetDbContext();

            var service = new TransactionsDataService(dbContext);

            // Act
            var result = service.RetrieveTransactionsQuery();

            // Assert
            Assert.IsNotNull(result, FailureMessages.ResultIsNull);
        }

        [TestMethod]
        public void UpdateTransaction_WithNoTransactionId_ShouldThrowException()
        {
            // Arrange
            var transaction = new Transaction
            {
                Amount = 123.45m,
                CategoryId = 101,
                Date = DateTime.Today,
                IsCredit = true,
                Memo = "This is a test"
            };

            using var dbContext = GetDbContext();

            var service = new TransactionsDataService(dbContext);

            Exception caughtException = null;

            // Act
            try
            {
                service.UpdateTransaction(transaction);
            }
            catch (Exception exception)
            {
                caughtException = exception;
            }

            // Assert
            Assert.IsNotNull(caughtException, FailureMessages.ExceptionNotThrown);
            Assert.IsInstanceOfType(caughtException, typeof(InvalidOperationException), FailureMessages.ExceptionNotExpectedType);
        }

        [TestMethod]
        public void UpdateTransaction_WithNullTransaction_ShouldThrowException()
        {
            // Arrange
            using var dbContext = GetDbContext();

            var service = new TransactionsDataService(dbContext);

            Exception caughtException = null;

            // Act
            try
            {
                service.UpdateTransaction(null);
            }
            catch (Exception exception)
            {
                caughtException = exception;
            }

            // Assert
            Assert.IsNotNull(caughtException, FailureMessages.ExceptionNotThrown);
            Assert.IsInstanceOfType(caughtException, typeof(ArgumentNullException), FailureMessages.ExceptionNotExpectedType);
        }

        [TestMethod]
        public void UpdateTransaction_WithValidTransaction_ShouldSaveAndReturnTrue()
        {
            // Arrange
            var transaction = new Transaction
            {
                Amount = 123.45m,
                CategoryId = 101,
                Date = DateTime.Today,
                Id = 201,
                IsCredit = true,
                Memo = "This is a test"
            };

            using var dbContext = GetDbContext();

            dbContext.Add(transaction);
            dbContext.SaveChanges();

            var transactionToUpdate = new Transaction
            {
                Amount = 234.56m,
                CategoryId = 102,
                Date = DateTime.Today.AddDays(1),
                Id = 201,
                IsCredit = false,
                Memo = "This is another test"
            };

            var service = new TransactionsDataService(dbContext);

            // Act
            var result = service.UpdateTransaction(transactionToUpdate);

            // Assert
            Assert.IsTrue(result, FailureMessages.ResultNotExpectedValue);

            var updatedTransaction = dbContext.Transactions.Single(x => x.Id == transaction.Id);
            Assert.AreEqual(transactionToUpdate.Amount, updatedTransaction.Amount, FailureMessages.ValueNotExpected);
            Assert.AreEqual(transactionToUpdate.CategoryId, updatedTransaction.CategoryId, FailureMessages.ValueNotExpected);
            Assert.AreEqual(transactionToUpdate.Date, updatedTransaction.Date, FailureMessages.ValueNotExpected);
            Assert.AreEqual(transactionToUpdate.IsCredit, updatedTransaction.IsCredit, FailureMessages.ValueNotExpected);
            Assert.AreEqual(transactionToUpdate.Memo, updatedTransaction.Memo, FailureMessages.ValueNotExpected);
        }

        [TestMethod]
        public async Task UpdateTransactionAsync_WithNoTransactionId_ShouldThrowException()
        {
            // Arrange
            var transaction = new Transaction
            {
                Amount = 123.45m,
                CategoryId = 101,
                Date = DateTime.Today,
                IsCredit = true,
                Memo = "This is a test"
            };

            using var dbContext = GetDbContext();

            var service = new TransactionsDataService(dbContext);

            Exception caughtException = null;

            // Act
            try
            {
                await service.UpdateTransactionAsync(transaction);
            }
            catch (Exception exception)
            {
                caughtException = exception;
            }

            // Assert
            Assert.IsNotNull(caughtException, FailureMessages.ExceptionNotThrown);
            Assert.IsInstanceOfType(caughtException, typeof(InvalidOperationException), FailureMessages.ExceptionNotExpectedType);
        }

        [TestMethod]
        public async Task UpdateTransactionAsync_WithNullTransaction_ShouldThrowException()
        {
            // Arrange
            using var dbContext = GetDbContext();

            var service = new TransactionsDataService(dbContext);

            Exception caughtException = null;

            // Act
            try
            {
                await service.UpdateTransactionAsync(null);
            }
            catch (Exception exception)
            {
                caughtException = exception;
            }

            // Assert
            Assert.IsNotNull(caughtException, FailureMessages.ExceptionNotThrown);
            Assert.IsInstanceOfType(caughtException, typeof(ArgumentNullException), FailureMessages.ExceptionNotExpectedType);
        }

        [TestMethod]
        public async Task UpdateTransactionAsync_WithValidTransaction_ShouldSaveAndReturnTrue()
        {
            // Arrange
            var transaction = new Transaction
            {
                Amount = 123.45m,
                CategoryId = 101,
                Date = DateTime.Today,
                Id = 201,
                IsCredit = true,
                Memo = "This is a test"
            };

            using var dbContext = GetDbContext();

            dbContext.Add(transaction);
            dbContext.SaveChanges();

            var transactionToUpdate = new Transaction
            {
                Amount = 234.56m,
                CategoryId = 102,
                Date = DateTime.Today.AddDays(1),
                Id = 201,
                IsCredit = false,
                Memo = "This is another test"
            };

            var service = new TransactionsDataService(dbContext);

            // Act
            var result = await service.UpdateTransactionAsync(transactionToUpdate);

            // Assert
            Assert.IsTrue(result, FailureMessages.ResultNotExpectedValue);

            var updatedTransaction = dbContext.Transactions.Single(x => x.Id == transaction.Id);
            Assert.AreEqual(transactionToUpdate.Amount, updatedTransaction.Amount, FailureMessages.ValueNotExpected);
            Assert.AreEqual(transactionToUpdate.CategoryId, updatedTransaction.CategoryId, FailureMessages.ValueNotExpected);
            Assert.AreEqual(transactionToUpdate.Date, updatedTransaction.Date, FailureMessages.ValueNotExpected);
            Assert.AreEqual(transactionToUpdate.IsCredit, updatedTransaction.IsCredit, FailureMessages.ValueNotExpected);
            Assert.AreEqual(transactionToUpdate.Memo, updatedTransaction.Memo, FailureMessages.ValueNotExpected);
        }

        #endregion Public Methods

        #region Private Methods

        private AppDbContext GetDbContext([CallerMemberName] string name = "")
        {
            var dbContextOptions = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(name)
                .Options;

            return new AppDbContext(dbContextOptions);
        }

        #endregion Private Methods

        #region Private Classes

        private static class FailureMessages
        {
            #region Public Fields

            public const string ExceptionNotExpectedType = "Exception is not the expected type";
            public const string ExceptionNotThrown = "Exception was not thrown";
            public const string IncorrectItemCount = "Sequence does not contain the expected number of items";
            public const string MissingExpectedItem = "Sequence does not contain an expected item";
            public const string ResultIsNotNull = "Result is not null";
            public const string ResultIsNull = "Result is null";
            public const string ResultNotExpectedValue = "Result is not the expected value";
            public const string UnexpectedItem = "Sequence contains an unexpected item";
            public const string ValueNotExpected = "Value is not the expected value";
            public const string ValueNotSet = "Value has not been set";

            #endregion Public Fields
        }

        #endregion Private Classes
    }
}
