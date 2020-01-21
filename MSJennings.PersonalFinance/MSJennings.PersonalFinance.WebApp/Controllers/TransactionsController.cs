using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MSJennings.PersonalFinance.Data.Services;
using MSJennings.PersonalFinance.WebApp.ViewModels.Transactions;
using System.Threading.Tasks;

namespace MSJennings.PersonalFinance.WebApp.Controllers
{
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
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddTransactionViewModel viewModel)
        {
        }

        [HttpPost("{id}/[action]")]
        public async Task<IActionResult> Delete(int id)
        {
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Details(int id)
        {
        }

        [HttpGet("{id}/[action]")]
        public async Task<IActionResult> Edit(int id)
        {
        }

        [HttpPost("{id}/[action]")]
        public async Task<IActionResult> Edit(EditTransactionViewModel viewModel)
        {
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
