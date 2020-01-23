using System;
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
            _categoriesDataService = categoriesDataService;
            _logger = logger;
            _transactionsDataService = transactionsDataService;
        }

        #endregion Public Constructors

        #region Public Methods

        [HttpGet("[action]")]
        public async Task<IActionResult> Add()
        {
            var result = Content("// todo: GET Add");
            return await Task.FromResult(result).ConfigureAwait(false);
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> Add(AddTransactionViewModel viewModel)
        {
            var result = Content("// todo: POST Add");
            return await Task.FromResult(result).ConfigureAwait(false);
        }

        [HttpPost("{id}/[action]")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = Content("// todo: POST Delete");
            return await Task.FromResult(result).ConfigureAwait(false);
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

            var transactions = await _transactionsDataService.RetrieveTransactionsAsync().ConfigureAwait(false);
            foreach (var transaction in transactions)
            {
                viewModel.TransactionsList.Add(transaction);
            }

            return View(viewModel);
        }

        #endregion Public Methods
    }
}
