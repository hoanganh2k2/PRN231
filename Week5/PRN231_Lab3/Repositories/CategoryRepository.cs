using BusinessObject.Models;
using DataAccess;

namespace Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        public List<Category> GetCategories()
        {
            return CategoryDAO.GetCategories();
        }

        public Category GetCategoryById(int categoryId)
        {
            return CategoryDAO.GetCategories().SingleOrDefault(c => c.CategoryId == categoryId);
        }

        public void AddCategory(Category category)
        {
            try
            {
                CategoryDAO.SaveCategory(category);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void UpdateCategory(Category category)
        {
            try
            {
                CategoryDAO.UpdateCategory(category);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void DeleteCategory(int categoryId)
        {
            try
            {
                Category category = GetCategoryById(categoryId);
                if (category != null)
                {
                    CategoryDAO.DeleteCategory(category);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
