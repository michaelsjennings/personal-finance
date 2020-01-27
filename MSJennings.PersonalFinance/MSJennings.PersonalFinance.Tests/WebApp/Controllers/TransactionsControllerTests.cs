using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MSJennings.PersonalFinance.Tests.WebApp.Controllers
{
    [TestClass]
    public class TransactionsControllerTests
    {
        #region Public Methods

        [TestMethod]
        public async Task Add_WithGetRequest_ShouldReturnViewWithViewModel()
        {
        }

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
    }
}
