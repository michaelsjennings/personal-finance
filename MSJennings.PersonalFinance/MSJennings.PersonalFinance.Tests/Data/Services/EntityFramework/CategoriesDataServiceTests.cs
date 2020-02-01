using System;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MSJennings.PersonalFinance.Data.Models;
using MSJennings.PersonalFinance.Data.Services.EntityFramework;

namespace MSJennings.PersonalFinance.Tests.Data.Services.EntityFramework
{
    [TestClass]
    public class CategoriesDataServiceTests
    {
        #region Public Methods

        [TestMethod]
        public void CreateCategory_WithNullCategory_ShouldThrowException()
        {
            // Arrange
            using var dbContext = GetDbContext();

            var service = new CategoriesDataService(dbContext);

            Exception caughtException = null;

            // Act
            try
            {
                service.CreateCategory(null);
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
        public void CreateCategory_WithValidCategory_ShouldSaveAndReturnId()
        {
            // Arrange
            var category = new Category
            {
                Name = "Test Category"
            };

            using var dbContext = GetDbContext();

            var service = new CategoriesDataService(dbContext);

            // Act
            var result = service.CreateCategory(category);

            // Assert
            Assert.AreNotEqual(default, result, FailureMessages.ResultNotExpectedValue);
            Assert.AreEqual(1, dbContext.Categories.Count(), FailureMessages.IncorrectItemCount);

            var createdCategory = dbContext.Categories.Single(x => x.Id == result);
            Assert.AreEqual(category.Name, createdCategory.Name, FailureMessages.ValueNotExpected);
        }

        [TestMethod]
        public async Task CreateCategoryAsync_WithNullCategory_ShouldThrowException()
        {
            // Arrange
            using var dbContext = GetDbContext();

            var service = new CategoriesDataService(dbContext);

            Exception caughtException = null;

            // Act
            try
            {
                await service.CreateCategoryAsync(null);
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
        public async Task CreateCategoryAsync_WithValidCategory_ShouldSaveAndReturnId()
        {
            // Arrange
            var category = new Category
            {
                Name = "Test Category"
            };

            using var dbContext = GetDbContext();

            var service = new CategoriesDataService(dbContext);

            // Act
            var result = await service.CreateCategoryAsync(category);

            // Assert
            Assert.AreNotEqual(default, result, FailureMessages.ResultNotExpectedValue);
            Assert.AreEqual(1, dbContext.Categories.Count(), FailureMessages.IncorrectItemCount);

            var createdCategory = dbContext.Categories.Single(x => x.Id == result);
            Assert.AreEqual(category.Name, createdCategory.Name, FailureMessages.ValueNotExpected);
        }

        [TestMethod]
        public void DeleteCategory_WithInvalidCategoryId_ShouldReturnFalse()
        {
            // Arrange
            using var dbContext = GetDbContext();

            var service = new CategoriesDataService(dbContext);

            // Act
            var result = service.DeleteCategory(101);

            // Assert
            Assert.IsFalse(result, FailureMessages.ResultNotExpectedValue);
        }

        [TestMethod]
        public void DeleteCategory_WithValidCategoryId_ShouldRemoveAndReturnTrue()
        {
            // Arrange
            var category = new Category
            {
                Id = 101,
                Name = "Test Category"
            };

            using var dbContext = GetDbContext();

            dbContext.Add(category);
            dbContext.SaveChanges();

            var service = new CategoriesDataService(dbContext);

            // Pre-Assert
            Assert.AreEqual(1, dbContext.Categories.Count(), FailureMessages.IncorrectItemCount);

            // Act
            var result = service.DeleteCategory(101);

            // Assert
            Assert.IsTrue(result, FailureMessages.ResultNotExpectedValue);
            Assert.AreEqual(0, dbContext.Categories.Count(), FailureMessages.IncorrectItemCount);
        }

        [TestMethod]
        public async Task DeleteCategoryAsync_WithInvalidCategoryId_ShouldReturnFalse()
        {
            // Arrange
            using var dbContext = GetDbContext();

            var service = new CategoriesDataService(dbContext);

            // Act
            var result = await service.DeleteCategoryAsync(101);

            // Assert
            Assert.IsFalse(result, FailureMessages.ResultNotExpectedValue);
        }

        [TestMethod]
        public async Task DeleteCategoryAsync_WithValidCategoryId_ShouldRemoveAndReturnTrue()
        {
            // Arrange
            var category = new Category
            {
                Id = 101,
                Name = "Test Category"
            };

            using var dbContext = GetDbContext();

            dbContext.Add(category);
            dbContext.SaveChanges();

            var service = new CategoriesDataService(dbContext);

            // Pre-Assert
            Assert.AreEqual(1, dbContext.Categories.Count(), FailureMessages.IncorrectItemCount);

            // Act
            var result = await service.DeleteCategoryAsync(101);

            // Assert
            Assert.IsTrue(result, FailureMessages.ResultNotExpectedValue);
            Assert.AreEqual(0, dbContext.Categories.Count(), FailureMessages.IncorrectItemCount);
        }

        [TestMethod]
        public void RetrieveCategories_WithNoPredicate_ShouldReturnAllCategories()
        {
            // Arrange
            var categories = new[]
            {
                new Category
                {
                    Id = 101,
                    Name = "Test Category"
                },
                new Category
                {
                    Id = 102,
                    Name = "Another Test Category"
                },
                new Category
                {
                    Id = 103,
                    Name = "Yet Another Test Category"
                }
            };

            using var dbContext = GetDbContext();

            dbContext.AddRange(categories);
            dbContext.SaveChanges();

            var service = new CategoriesDataService(dbContext);

            // Pre-Assert
            Assert.AreEqual(3, dbContext.Categories.Count(), FailureMessages.IncorrectItemCount);

            // Act
            var result = service.RetrieveCategories();

            // Assert
            Assert.IsNotNull(result, FailureMessages.ResultIsNull);
            Assert.AreEqual(3, result.Count, FailureMessages.IncorrectItemCount);
            Assert.IsTrue(result.Any(x => x.Id == 101), FailureMessages.MissingExpectedItem);
            Assert.IsTrue(result.Any(x => x.Id == 102), FailureMessages.MissingExpectedItem);
            Assert.IsTrue(result.Any(x => x.Id == 103), FailureMessages.MissingExpectedItem);
        }

        [TestMethod]
        public void RetrieveCategories_WithPredicate_ShouldReturnExpectedCategories()
        {
            // Arrange
            var categories = new[]
            {
                new Category
                {
                    Id = 101,
                    Name = "Test Category"
                },
                new Category
                {
                    Id = 102,
                    Name = "Another Test Category"
                },
                new Category
                {
                    Id = 103,
                    Name = "Yet Another Test Category"
                }
            };

            using var dbContext = GetDbContext();

            dbContext.AddRange(categories);
            dbContext.SaveChanges();

            var service = new CategoriesDataService(dbContext);

            // Pre-Assert
            Assert.AreEqual(3, dbContext.Categories.Count(), FailureMessages.IncorrectItemCount);

            // Act
            var result = service.RetrieveCategories(x => x.Id < 103);

            // Assert
            Assert.IsNotNull(result, FailureMessages.ResultIsNull);
            Assert.AreEqual(2, result.Count, FailureMessages.IncorrectItemCount);
            Assert.IsTrue(result.Any(x => x.Id == 101), FailureMessages.MissingExpectedItem);
            Assert.IsTrue(result.Any(x => x.Id == 102), FailureMessages.MissingExpectedItem);
            Assert.IsFalse(result.Any(x => x.Id == 103), FailureMessages.UnexpectedItem);
        }

        [TestMethod]
        public async Task RetrieveCategoriesAsync_WithNoPredicate_ShouldReturnAllCategories()
        {
            // Arrange
            var categories = new[]
            {
                new Category
                {
                    Id = 101,
                    Name = "Test Category"
                },
                new Category
                {
                    Id = 102,
                    Name = "Another Test Category"
                },
                new Category
                {
                    Id = 103,
                    Name = "Yet Another Test Category"
                }
            };

            using var dbContext = GetDbContext();

            dbContext.AddRange(categories);
            dbContext.SaveChanges();

            var service = new CategoriesDataService(dbContext);

            // Pre-Assert
            Assert.AreEqual(3, dbContext.Categories.Count(), FailureMessages.IncorrectItemCount);

            // Act
            var result = await service.RetrieveCategoriesAsync();

            // Assert
            Assert.IsNotNull(result, FailureMessages.ResultIsNull);
            Assert.AreEqual(3, result.Count, FailureMessages.IncorrectItemCount);
            Assert.IsTrue(result.Any(x => x.Id == 101), FailureMessages.MissingExpectedItem);
            Assert.IsTrue(result.Any(x => x.Id == 102), FailureMessages.MissingExpectedItem);
            Assert.IsTrue(result.Any(x => x.Id == 103), FailureMessages.MissingExpectedItem);
        }

        [TestMethod]
        public async Task RetrieveCategoriesAsync_WithPredicate_ShouldReturnExpectedCategories()
        {
            // Arrange
            var categories = new[]
            {
                new Category
                {
                    Id = 101,
                    Name = "Test Category"
                },
                new Category
                {
                    Id = 102,
                    Name = "Another Test Category"
                },
                new Category
                {
                    Id = 103,
                    Name = "Yet Another Test Category"
                }
            };

            using var dbContext = GetDbContext();

            dbContext.AddRange(categories);
            dbContext.SaveChanges();

            var service = new CategoriesDataService(dbContext);

            // Pre-Assert
            Assert.AreEqual(3, dbContext.Categories.Count(), FailureMessages.IncorrectItemCount);

            // Act
            var result = await service.RetrieveCategoriesAsync(x => x.Id < 103);

            // Assert
            Assert.IsNotNull(result, FailureMessages.ResultIsNull);
            Assert.AreEqual(2, result.Count, FailureMessages.IncorrectItemCount);
            Assert.IsTrue(result.Any(x => x.Id == 101), FailureMessages.MissingExpectedItem);
            Assert.IsTrue(result.Any(x => x.Id == 102), FailureMessages.MissingExpectedItem);
            Assert.IsFalse(result.Any(x => x.Id == 103), FailureMessages.UnexpectedItem);
        }

        [TestMethod]
        public void RetrieveCategoriesQuery_ShouldReturnCategoriesQuery()
        {
            // Arrange
            using var dbContext = GetDbContext();

            var service = new CategoriesDataService(dbContext);

            // Act
            var result = service.RetrieveCategoriesQuery();

            // Assert
            Assert.IsNotNull(result, FailureMessages.ResultIsNull);
        }

        [TestMethod]
        public void RetrieveCategory_WithInvalidCategoryId_ShouldReturnNull()
        {
            // Arrange
            using var dbContext = GetDbContext();

            var service = new CategoriesDataService(dbContext);

            // Act
            var result = service.RetrieveCategory(101);

            // Assert
            Assert.IsNull(result, FailureMessages.ResultIsNotNull);
        }

        [TestMethod]
        public void RetrieveCategory_WithValidCategoryId_ShouldReturnCategory()
        {
            // Arrange
            var category = new Category
            {
                Id = 101,
                Name = "Test Category"
            };

            using var dbContext = GetDbContext();

            dbContext.Add(category);
            dbContext.SaveChanges();

            var service = new CategoriesDataService(dbContext);

            // Act
            var result = service.RetrieveCategory(101);

            // Assert
            Assert.IsNotNull(result, FailureMessages.ResultIsNull);
            Assert.AreEqual(category.Name, result.Name, FailureMessages.ValueNotExpected);
        }

        [TestMethod]
        public async Task RetrieveCategoryAsync_WithInvalidCategoryId_ShouldReturnNull()
        {
            // Arrange
            using var dbContext = GetDbContext();

            var service = new CategoriesDataService(dbContext);

            // Act
            var result = await service.RetrieveCategoryAsync(101);

            // Assert
            Assert.IsNull(result, FailureMessages.ResultIsNotNull);
        }

        [TestMethod]
        public async Task RetrieveCategoryAsync_WithValidCategoryId_ShouldReturnCategory()
        {
            // Arrange
            var category = new Category
            {
                Id = 101,
                Name = "Test Category"
            };

            using var dbContext = GetDbContext();

            dbContext.Add(category);
            dbContext.SaveChanges();

            var service = new CategoriesDataService(dbContext);

            // Act
            var result = await service.RetrieveCategoryAsync(101);

            // Assert
            Assert.IsNotNull(result, FailureMessages.ResultIsNull);
            Assert.AreEqual(category.Name, result.Name, FailureMessages.ValueNotExpected);
        }

        [TestMethod]
        public void UpdateCategory_WithNoCategoryId_ShouldThrowException()
        {
            // Arrange
            var category = new Category
            {
                Name = "Test Category"
            };

            using var dbContext = GetDbContext();

            var service = new CategoriesDataService(dbContext);

            Exception caughtException = null;

            // Act
            try
            {
                service.UpdateCategory(category);
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
        public void UpdateCategory_WithNullCategory_ShouldThrowException()
        {
            // Arrange
            using var dbContext = GetDbContext();

            var service = new CategoriesDataService(dbContext);

            Exception caughtException = null;

            // Act
            try
            {
                service.UpdateCategory(null);
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
        public void UpdateCategory_WithValidCategory_ShouldSaveAndReturnTrue()
        {
            // Arrange
            var category = new Category
            {
                Id = 101,
                Name = "Test Category"
            };

            using var dbContext = GetDbContext();

            dbContext.Add(category);
            dbContext.SaveChanges();

            var categoryToUpdate = new Category
            {
                Id = 101,
                Name = "Another Test Category"
            };

            var service = new CategoriesDataService(dbContext);

            // Act
            var result = service.UpdateCategory(categoryToUpdate);

            // Assert
            Assert.IsTrue(result, FailureMessages.ResultNotExpectedValue);

            var updatedCategory = dbContext.Categories.Single(x => x.Id == category.Id);
            Assert.AreEqual(categoryToUpdate.Name, updatedCategory.Name, FailureMessages.ValueNotExpected);
        }

        [TestMethod]
        public async Task UpdateCategoryAsync_WithNoCategoryId_ShouldThrowException()
        {
            // Arrange
            var category = new Category
            {
                Name = "Test Category"
            };

            using var dbContext = GetDbContext();

            var service = new CategoriesDataService(dbContext);

            Exception caughtException = null;

            // Act
            try
            {
                await service.UpdateCategoryAsync(category);
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
        public async Task UpdateCategoryAsync_WithNullCategory_ShouldThrowException()
        {
            // Arrange
            using var dbContext = GetDbContext();

            var service = new CategoriesDataService(dbContext);

            Exception caughtException = null;

            // Act
            try
            {
                await service.UpdateCategoryAsync(null);
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
        public async Task UpdateCategoryAsync_WithValidCategory_ShouldSaveAndReturnTrue()
        {
            // Arrange
            var category = new Category
            {
                Id = 101,
                Name = "Test Category"
            };

            using var dbContext = GetDbContext();

            dbContext.Add(category);
            dbContext.SaveChanges();

            var categoryToUpdate = new Category
            {
                Id = 101,
                Name = "Another Test Category"
            };

            var service = new CategoriesDataService(dbContext);

            // Act
            var result = await service.UpdateCategoryAsync(categoryToUpdate);

            // Assert
            Assert.IsTrue(result, FailureMessages.ResultNotExpectedValue);

            var updatedCategory = dbContext.Categories.Single(x => x.Id == category.Id);
            Assert.AreEqual(categoryToUpdate.Name, updatedCategory.Name, FailureMessages.ValueNotExpected);
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
