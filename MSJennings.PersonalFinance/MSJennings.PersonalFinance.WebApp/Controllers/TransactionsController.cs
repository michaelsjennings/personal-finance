using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MSJennings.PersonalFinance.Data.Services;
using MSJennings.PersonalFinance.WebApp.ViewModels.Transactions;

namespace MSJennings.PersonalFinance.WebApp.Controllers
{
    [Route("[controller]")]
    public class TransactionsController : Controller
    {
        #region Private Fields

        private readonly ILogger<TransactionsController> _logger;
        private readonly ITransactionsDataService _transactionsDataService;

        #endregion Private Fields

        #region Public Constructors

        public TransactionsController(
            ITransactionsDataService transactionsDataService,
            ILogger<TransactionsController> logger)
        {
            _transactionsDataService = transactionsDataService;
            _logger = logger;
        }

        #endregion Public Constructors

        #region Public Methods

        [HttpGet]
        public async Task<IActionResult> Add()
        {
            var result = Content("// todo: GET Add");
            return await Task.FromResult(result).ConfigureAwait(false);
        }

        [HttpPost]
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
            var result = Content("// todo: GET Details");
            return await Task.FromResult(result).ConfigureAwait(false);
        }

        [HttpGet("{id}/[action]")]
        public async Task<IActionResult> Edit(int id)
        {
            var result = Content("// todo: GET Edit");
            return await Task.FromResult(result).ConfigureAwait(false);
        }

        [HttpPost("{id}/[action]")]
        public async Task<IActionResult> Edit(EditTransactionViewModel viewModel)
        {
            var result = Content("// todo: POST Edit");
            return await Task.FromResult(result).ConfigureAwait(false);
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
