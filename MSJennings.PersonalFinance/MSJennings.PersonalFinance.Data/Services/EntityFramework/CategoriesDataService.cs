using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MSJennings.PersonalFinance.Data.Models;

namespace MSJennings.PersonalFinance.Data.Services.EntityFramework
{
    public class CategoriesDataService : ICategoriesDataService
    {
        #region Private Fields

        private readonly AppDbContext _dbContext;

        #endregion Private Fields

        #region Public Constructors

        public CategoriesDataService(AppDbContext dbContext)
        {
            if (dbContext is null)
            {
                throw new ArgumentNullException(nameof(dbContext));
            }

            _dbContext = dbContext;
        }

        #endregion Public Constructors

        #region Public Methods

        public int CreateCategory(Category category)
        {
            if (category is null)
            {
                throw new ArgumentNullException(nameof(category));
            }

            _dbContext.Add(category);
            _dbContext.SaveChanges();

            return category.Id;
        }

        public async Task<int> CreateCategoryAsync(Category category)
        {
            if (category is null)
            {
                throw new ArgumentNullException(nameof(category));
            }

            _dbContext.Add(category);
            await _dbContext.SaveChangesAsync().ConfigureAwait(false);

            return category.Id;
        }

        public bool DeleteCategory(int categoryId)
        {
            var category = _dbContext.Categories.SingleOrDefault(x => x.Id == categoryId);

            if (category is null)
            {
                return false;
            }

            _dbContext.Remove(category);
            _dbContext.SaveChanges();

            return true;
        }

        public async Task<bool> DeleteCategoryAsync(int categoryId)
        {
            var category = await _dbContext.Categories.SingleOrDefaultAsync(x => x.Id == categoryId).ConfigureAwait(false);

            if (category is null)
            {
                return false;
            }

            _dbContext.Remove(category);
            await _dbContext.SaveChangesAsync().ConfigureAwait(false);

            return true;
        }

        public IList<Category> RetrieveCategories()
        {
            return _dbContext.Categories.AsNoTracking().ToList();
        }

        public IList<Category> RetrieveCategories(Expression<Func<Category, bool>> predicate)
        {
            return _dbContext.Categories.AsNoTracking().Where(predicate).ToList();
        }

        public async Task<IList<Category>> RetrieveCategoriesAsync()
        {
            return await _dbContext.Categories.AsNoTracking().ToListAsync().ConfigureAwait(false);
        }

        public async Task<IList<Category>> RetrieveCategoriesAsync(Expression<Func<Category, bool>> predicate)
        {
            return await _dbContext.Categories.AsNoTracking().Where(predicate).ToListAsync().ConfigureAwait(false);
        }

        public IQueryable<Category> RetrieveCategoriesQuery()
        {
            return _dbContext.Categories.AsNoTracking();
        }

        public Category RetrieveCategory(int categoryId)
        {
            return _dbContext.Categories.AsNoTracking().SingleOrDefault(x => x.Id == categoryId);
        }

        public async Task<Category> RetrieveCategoryAsync(int categoryId)
        {
            return await _dbContext.Categories.AsNoTracking().SingleOrDefaultAsync(x => x.Id == categoryId).ConfigureAwait(false);
        }

        public bool UpdateCategory(Category category)
        {
            if (category is null)
            {
                throw new ArgumentNullException(nameof(category));
            }

            if (category.Id == default)
            {
                throw new InvalidOperationException($"Cannot update {nameof(Category)} with no {nameof(Category.Id)}");
            }

            var categoryToUpdate = _dbContext.Categories.SingleOrDefault(x => x.Id == category.Id);
            if (categoryToUpdate == null)
            {
                return false;
            }

            _dbContext.Entry(categoryToUpdate).CurrentValues.SetValues(category);
            return _dbContext.SaveChanges() > 0;
        }

        public async Task<bool> UpdateCategoryAsync(Category category)
        {
            if (category is null)
            {
                throw new ArgumentNullException(nameof(category));
            }

            if (category.Id == default)
            {
                throw new InvalidOperationException($"Cannot update {nameof(Category)} with no {nameof(Category.Id)}");
            }

            var categoryToUpdate = await _dbContext.Categories.SingleOrDefaultAsync(x => x.Id == category.Id).ConfigureAwait(false);
            if (categoryToUpdate == null)
            {
                return false;
            }

            _dbContext.Entry(categoryToUpdate).CurrentValues.SetValues(category);
            return (await _dbContext.SaveChangesAsync().ConfigureAwait(false)) > 0;
        }

        #endregion Public Methods
    }
}
