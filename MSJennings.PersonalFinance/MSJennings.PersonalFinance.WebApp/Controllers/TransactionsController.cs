using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using MSJennings.PersonalFinance.Data.Models;
using MSJennings.PersonalFinance.Data.Services;
using MSJennings.PersonalFinance.WebApp.ViewModels.Transactions;

namespace MSJennings.PersonalFinance.WebApp.Controllers
{
    [Route("[controller]")]
    public class TransactionsController : Controller
    {
        #region Private Fields

        private readonly ICategoriesDataService _categoriesDataService;
        private readonly ILogger<TransactionsController> _logger;
        private readonly ITransactionsDataService _transactionsDataService;

        #endregion Private Fields

        #region Public Constructors

        public TransactionsController(
            ICategoriesDataService categoriesDataService,
            ILogger<TransactionsController> logger,
            ITransactionsDataService transactionsDataService)
        {
            if (categoriesDataService is null)
            {
                throw new ArgumentNullException(nameof(categoriesDataService));
            }

            if (logger is null)
            {
                throw new ArgumentNullException(nameof(logger));
            }

            if (transactionsDataService is null)
            {
                throw new ArgumentNullException(nameof(transactionsDataService));
            }

            _categoriesDataService = categoriesDataService;
            _logger = logger;
            _transactionsDataService = transactionsDataService;
        }

        #endregion Public Constructors

        #region Public Methods

        [HttpGet("[action]")]
        public async Task<IActionResult> Add()
        {
            var viewModel = new AddTransactionViewModel
            {
                Date = DateTime.Today,
                CategoriesList = new SelectList(
                    await _categoriesDataService.RetrieveCategoriesAsync().ConfigureAwait(false),
                    nameof(Category.Id),
                    nameof(Category.Name))
            };

            return View(viewModel);
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> Add(AddTransactionViewModel viewModel)
        {
            if (viewModel is null)
            {
                throw new ArgumentNullException(nameof(viewModel));
            }

            if (!ModelState.IsValid)
            {
                return View(viewModel);
            }

            _ = await _transactionsDataService.CreateTransactionAsync(new Transaction
            {
                Date = viewModel.Date,
                CategoryId = viewModel.CategoryId,
                Memo = viewModel.Memo,
                Amount = viewModel.Amount,
                IsCredit = viewModel.IsCredit
            }).ConfigureAwait(false);

            return RedirectToAction(nameof(Index));
        }

        [HttpPost("{id}/[action]")]
        public async Task<IActionResult> Delete(int id)
        {
            _ = await _transactionsDataService.DeleteTransactionAsync(id).ConfigureAwait(false);
            return RedirectToAction(nameof(Index));
        }

        [HttpGet("{id}/[action]")]
        public async Task<IActionResult> Details(int id)
        {
            var transaction = await _transactionsDataService.RetrieveTransactionAsync(id).ConfigureAwait(false);
            if (transaction == null)
            {
                return NotFound();
            }

            var viewModel = new TransactionDetailsViewModel
            {
                Id = transaction.Id,
                Date = transaction.Date,
                CategoryId = transaction.CategoryId,
                CategoryName = transaction.Category.Name,
                Memo = transaction.Memo,
                Amount = transaction.Amount,
                IsCredit = transaction.IsCredit
            };

            return View(viewModel);
        }

        [HttpGet("{id}/[action]")]
        public async Task<IActionResult> Edit(int id)
        {
            var transaction = await _transactionsDataService.RetrieveTransactionAsync(id).ConfigureAwait(false);
            if (transaction == null)
            {
                return NotFound();
            }

            var viewModel = new EditTransactionViewModel
            {
                Id = transaction.Id,
                Date = transaction.Date,
                CategoryId = transaction.CategoryId,
                CategoriesList = new SelectList(
                    await _categoriesDataService.RetrieveCategoriesAsync().ConfigureAwait(false),
                    nameof(Category.Id),
                    nameof(Category.Name)),
                Memo = transaction.Memo,
                Amount = transaction.Amount,
                IsCredit = transaction.IsCredit
            };

            return View(viewModel);
        }

        [HttpPost("{id}/[action]")]
        public async Task<IActionResult> Edit(EditTransactionViewModel viewModel)
        {
            if (viewModel is null)
            {
                throw new ArgumentNullException(nameof(viewModel));
            }

            if (!ModelState.IsValid)
            {
                return View(viewModel);
            }

            _ = await _transactionsDataService.UpdateTransactionAsync(new Transaction
            {
                Id = viewModel.Id,
                Date = viewModel.Date,
                CategoryId = viewModel.CategoryId,
                Memo = viewModel.Memo,
                Amount = viewModel.Amount,
                IsCredit = viewModel.IsCredit
            }).ConfigureAwait(false);

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var viewModel = new TransactionsIndexViewModel();

            viewModel.TransactionsFilter.CategoriesList = new SelectList(
                await _categoriesDataService.RetrieveCategoriesAsync().ConfigureAwait(false),
                nameof(Category.Id),
                nameof(Category.Name));

            viewModel.TransactionsFilter.IsCreditList = new SelectList(
                new[]
                {
                    new { Value = true.ToString(CultureInfo.InvariantCulture), Text = "Yes" },
                    new { Value = false.ToString(CultureInfo.InvariantCulture), Text = "No" }
                },
                nameof(SelectListItem.Value),
                nameof(SelectListItem.Text));

            var transactions = await _transactionsDataService.RetrieveTransactionsAsync().ConfigureAwait(false);
            foreach (var transaction in transactions)
            {
                viewModel.TransactionsList.Add(transaction);
            }

            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Index(TransactionsIndexViewModel viewModel)
        {
            if (viewModel is null)
            {
                throw new ArgumentNullException(nameof(viewModel));
            }

            if (viewModel.TransactionsFilter is null)
            {
                throw new InvalidOperationException($"{nameof(viewModel.TransactionsFilter)} is null");
            }

            var query = _transactionsDataService.BuildTransactionsQuery();
            query = viewModel.TransactionsFilter.ApplyFilteringSortingAndPaging(query);

            var transactions = await _transactionsDataService.ExecuteTransactionsQueryAsync(query).ConfigureAwait(false);
            foreach (var transaction in transactions)
            {
                viewModel.TransactionsList.Add(transaction);
            }

            return View(viewModel);
        }

        #endregion Public Methods
    }
}
