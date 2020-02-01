using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using MSJennings.PersonalFinance.Data.Models;

namespace MSJennings.PersonalFinance.Data.Services
{
    public interface ICategoriesDataService
    {
        #region Public Methods

        public int CreateCategory(Category category);

        public Task<int> CreateCategoryAsync(Category category);

        public bool DeleteCategory(int categoryId);

        public Task<bool> DeleteCategoryAsync(int categoryId);

        public IList<Category> RetrieveCategories();

        public IList<Category> RetrieveCategories(Expression<Func<Category, bool>> predicate);

        public Task<IList<Category>> RetrieveCategoriesAsync();

        public Task<IList<Category>> RetrieveCategoriesAsync(Expression<Func<Category, bool>> predicate);

        public IQueryable<Category> RetrieveCategoriesQuery();

        public Category RetrieveCategory(int categoryId);

        public Task<Category> RetrieveCategoryAsync(int categoryId);

        public bool UpdateCategory(Category category);

        public Task<bool> UpdateCategoryAsync(Category category);

        #endregion Public Methods
    }
}
