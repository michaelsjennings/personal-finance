using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using MSJennings.PersonalFinance.Data.Models;
using MSJennings.PersonalFinance.Data.Services;
using MSJennings.PersonalFinance.WebApp.Controllers;
using MSJennings.PersonalFinance.WebApp.ViewModels.Transactions;

namespace MSJennings.PersonalFinance.Tests.WebApp.Controllers
{
    [TestClass]
    public class TransactionsControllerTests
    {
        #region Public Methods

        [TestMethod]
        public async Task Add_WithGetRequest_ShouldReturnViewWithViewModel()
        {
            // Arrange
            var categories = new List<Category>
            {
                new Category { Id = 101, Name = "Category One" },
                new Category { Id = 102, Name = "Category Two" },
                new Category { Id = 103, Name = "Category Three" }
            };

            var controllerBuilder = new TransactionsControllerBuilder();
            controllerBuilder.MockCategoriesDataService.Setup(x => x.RetrieveCategoriesAsync()).ReturnsAsync(categories);

            using var controller = controllerBuilder.Build();

            // Act
            var result = await controller.Add();

            // Assert
            Assert.IsNotNull(result, FailureMessages.ResultIsNull);
            Assert.IsInstanceOfType(result, typeof(ViewResult), FailureMessages.ResultNotExpectedType);

            var viewResult = result as ViewResult;
            Assert.IsNotNull(viewResult.Model, FailureMessages.ViewModelIsNull);
            Assert.IsInstanceOfType(viewResult.Model, typeof(AddTransactionViewModel), FailureMessages.ViewModelNotExpectedType);

            var viewModel = viewResult.Model as AddTransactionViewModel;
            Assert.AreEqual(DateTime.Today, viewModel.Date, FailureMessages.ViewModelPropertyNotExpectedValue);
            Assert.AreEqual(categories.Count, viewModel.CategoriesList.Count(), FailureMessages.IncorrectItemCount);
        }

        [TestMethod]
        public async Task Add_WithInvalidViewModel_ShouldReturnViewWithSameViewModel()
        {
            // Arrange
            var controllerBuilder = new TransactionsControllerBuilder();
            using var controller = controllerBuilder.Build();

            controller.ModelState.AddModelError("SomeKey", "SomeErrorMessage");

            var postedViewModel = new AddTransactionViewModel
            {
                Date = new DateTime(2008, 11, 17),
                CategoryId = 101,
                Memo = "Something",
                Amount = 123.45m,
                IsCredit = false
            };

            // Act
            var result = await controller.Add(postedViewModel);

            // Assert
            Assert.IsNotNull(result, FailureMessages.ResultIsNull);
            Assert.IsInstanceOfType(result, typeof(ViewResult), FailureMessages.ResultNotExpectedType);

            var viewResult = result as ViewResult;
            Assert.IsNotNull(viewResult.Model, FailureMessages.ViewModelIsNull);
            Assert.IsInstanceOfType(viewResult.Model, typeof(AddTransactionViewModel), FailureMessages.ViewModelNotExpectedType);

            var resultViewModel = viewResult.Model as AddTransactionViewModel;
            Assert.AreEqual(postedViewModel.Date, resultViewModel.Date, FailureMessages.ViewModelPropertyNotExpectedValue);
            Assert.AreEqual(postedViewModel.CategoryId, resultViewModel.CategoryId, FailureMessages.ViewModelPropertyNotExpectedValue);
            Assert.AreEqual(postedViewModel.Memo, resultViewModel.Memo, FailureMessages.ViewModelPropertyNotExpectedValue);
            Assert.AreEqual(postedViewModel.Amount, resultViewModel.Amount, FailureMessages.ViewModelPropertyNotExpectedValue);
            Assert.AreEqual(postedViewModel.IsCredit, resultViewModel.IsCredit, FailureMessages.ViewModelPropertyNotExpectedValue);
        }

        [TestMethod]
        public async Task Add_WithNullViewModel_ShouldThrowException()
        {
            // Arrange
            var controllerBuilder = new TransactionsControllerBuilder();
            using var controller = controllerBuilder.Build();

            Exception caughtException = null;

            // Act
            try
            {
                _ = await controller.Add(null);
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
        public async Task Add_WithValidViewModel_ShouldCreateAndRedirect()
        {
        }

        [TestMethod]
        public async Task Delete_WithId_ShouldDeleteAndRedirect()
        {
        }

        [TestMethod]
        public async Task Details_WithInvalidId_ShouldReturnNotFound()
        {
        }

        [TestMethod]
        public async Task Details_WithValidId_ShouldReturnViewWithViewModel()
        {
        }

        [TestMethod]
        public async Task Edit_WithInvalidId_ShouldReturnNotFound()
        {
        }

        [TestMethod]
        public async Task Edit_WithInvalidViewModel_ShouldReturnViewWithSameViewModel()
        {
        }

        [TestMethod]
        public async Task Edit_WithNullViewModel_ShouldThrowException()
        {
        }

        [TestMethod]
        public async Task Edit_WithValidId_ShouldReturnViewWithViewModel()
        {
        }

        [TestMethod]
        public async Task Edit_WithValidViewModel_ShouldUpdateAndRedirect()
        {
        }

        [TestMethod]
        public async Task Index_WithGetRequest_ShouldReturnViewWithViewModel()
        {
        }

        #endregion Public Methods

        #region Private Classes

        private static class FailureMessages
        {
            #region Public Fields

            public const string ExceptionNotExpectedType = "Exception is not the expected type";
            public const string ExceptionNotThrown = "Exception was not thrown";
            public const string IncorrectItemCount = "Sequence does not contain the expected number of items";

            //public const string MissingExpectedItem = "Sequence does not contain an expected item";
            //public const string ResultIsNotNull = "Result is not null";
            public const string ResultIsNull = "Result is null";

            //public const string ResultNotExpectedValue = "Result is not the expected value";
            public const string ResultNotExpectedType = "Result is not the expected type";

            //public const string UnexpectedItem = "Sequence contains an unexpected item";
            //public const string ValueNotExpected = "Value is not the expected value";
            //public const string ValueNotSet = "Value has not been set";
            public const string ViewModelIsNull = "View model is null";

            public const string ViewModelNotExpectedType = "View model is not the expected type";
            public const string ViewModelPropertyNotExpectedValue = "View model property is not the expected value";

            #endregion Public Fields
        }

        private class TransactionsControllerBuilder
        {
            #region Public Fields

            public readonly Mock<ICategoriesDataService> MockCategoriesDataService = new Mock<ICategoriesDataService>();

            public readonly Mock<ILogger<TransactionsController>> MockLogger = new Mock<ILogger<TransactionsController>>();

            public readonly Mock<ITransactionsDataService> MockTransactionsDataService = new Mock<ITransactionsDataService>();

            #endregion Public Fields

            #region Public Methods

            public TransactionsController Build()
            {
                return new TransactionsController(
                    MockCategoriesDataService.Object,
                    MockLogger.Object,
                    MockTransactionsDataService.Object);
            }

            #endregion Public Methods
        }

        #endregion Private Classes
    }
}
