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
            var mockCategoriesDataService = new Mock<ICategoriesDataService>();
            var mockLogger = new Mock<ILogger<TransactionsController>>();
            var mockTransactionsDataService = new Mock<ITransactionsDataService>();

            var categories = new List<Category>
            {
                new Category { Id = 101, Name = "Category One" },
                new Category { Id = 102, Name = "Category Two" },
                new Category { Id = 103, Name = "Category Three" }
            };

            mockCategoriesDataService.Setup(x => x.RetrieveCategoriesAsync()).ReturnsAsync(categories);

            using var controller = new TransactionsController(
                mockCategoriesDataService.Object,
                mockLogger.Object,
                mockTransactionsDataService.Object);

            // Act
            var result = await controller.Add();

            // Assert
            Assert.IsNotNull(result, FailureMessages.ResultIsNull);
            Assert.IsInstanceOfType(result, typeof(ViewResult), FailureMessages.ResultNotExpectedType);

            var viewResult = result as ViewResult;
            Assert.IsNotNull(viewResult.Model, FailureMessages.ViewModelIsNull);
            Assert.IsInstanceOfType(viewResult.Model, typeof(AddTransactionViewModel), FailureMessages.ViewModelNotExpectedType);

            var viewModel = viewResult.Model as AddTransactionViewModel;
            Assert.AreEqual(DateTime.Today, viewModel.Date);
            Assert.AreEqual(categories.Count, viewModel.CategoriesList.Count());
        }

        [TestMethod]
        public async Task Add_WithGetRequest_ShouldReturnViewWithViewModel_2()
        {
            // Arrange
            var categories = new List<Category>
            {
                new Category { Id = 101, Name = "Category One" },
                new Category { Id = 102, Name = "Category Two" },
                new Category { Id = 103, Name = "Category Three" }
            };

            //var controllerBuilder = new ControllerBuilder<TransactionsController>();
            //controllerBuilder.Mocks<ITransactionsDataService>().Setup(x => x.RetrieveCategoriesAsync()).ReturnsAsync(categories);

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
            Assert.AreEqual(DateTime.Today, viewModel.Date);
            Assert.AreEqual(categories.Count, viewModel.CategoriesList.Count());
        }

        /*
        [TestMethod]
        public async Task Add_WithGetRequest_ShouldReturnViewWithViewModel_3()
        {
            // Arrange
            var categories = new List<Category>
            {
                new Category { Id = 101, Name = "Category One" },
                new Category { Id = 102, Name = "Category Two" },
                new Category { Id = 103, Name = "Category Three" }
            };

            //var controllerBuilder = new ControllerBuilder<TransactionsController>();
            //controllerBuilder.Mocks<ITransactionsDataService>().Setup(x => x.RetrieveCategoriesAsync()).ReturnsAsync(categories);

            var controllerBuilder = new ControllerBuilder<TransactionsController>();
            controllerBuilder.Mocks<ICategoriesDataService>().Setup(x => x.RetrieveCategoriesAsync()).ReturnsAsync(categories);
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
            Assert.AreEqual(DateTime.Today, viewModel.Date);
            Assert.AreEqual(categories.Count, viewModel.CategoriesList.Count());
        }
        */

        [TestMethod]
        public async Task Add_WithInvalidViewModel_ShouldReturnViewWithSameViewModel()
        {
        }

        [TestMethod]
        public async Task Add_WithNullViewModel_ShouldThrowException()
        {
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

            //public const string ExceptionNotExpectedType = "Exception is not the expected type";
            //public const string ExceptionNotThrown = "Exception was not thrown";
            //public const string IncorrectItemCount = "Sequence does not contain the expected number of items";
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

            #endregion Public Fields
        }

        /*
        private class ControllerBuilder<T> where T : Controller
        {
            private readonly IDictionary<Type, Mock> _mocks;

            public ControllerBuilder()
            {
                _mocks = new Dictionary<Type, Mock>();

                var typeOfT = typeof(T);
                var constructor = typeOfT.GetConstructors().Single();

                var parameters = constructor.GetParameters();

                foreach (var parameter in parameters)
                {
                    var mock = (Mock)Activator.CreateInstance(parameter.GetType());

                    _mocks.Add(parameter.GetType(), mock);
                }
            }

            public Mock<TMock> Mocks<TMock>() where TMock : class
            {
                var mockType = typeof(TMock);
                if (_mocks.TryGetValue(mockType, out var mock))
                {
                    return mock as Mock<TMock>;
                }

                throw new ArgumentException();
            }

            public T Build()
            {
                return new T()
            }
        }
        */

        private class TransactionsControllerBuilder
        {
            #region Public Constructors

            public TransactionsControllerBuilder()
            {
                MockCategoriesDataService = new Mock<ICategoriesDataService>();
                MockLogger = new Mock<ILogger<TransactionsController>>();
                MockTransactionsDataService = new Mock<ITransactionsDataService>();
            }

            #endregion Public Constructors

            #region Public Properties

            public Mock<ICategoriesDataService> MockCategoriesDataService { get; private set; }

            public Mock<ILogger<TransactionsController>> MockLogger { get; private set; }

            public Mock<ITransactionsDataService> MockTransactionsDataService { get; private set; }

            #endregion Public Properties

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
