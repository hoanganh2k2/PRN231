using BusinessObject.Models;

namespace Repositories
{
    public interface ICategoryRepository
    {
        List<Category> GetCategories();
        Category GetCategoryById(int categoryId);
        void AddCategory(Category category);
        void UpdateCategory(Category category);
        void DeleteCategory(int categoryId);
    }
}
