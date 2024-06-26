using BusinessObject.Model;

namespace DataAccess
{
    public class CategoryDAO
    {
        public static List<Category> GetCategories()
        {
            List<Category> listCategories = new();
            try
            {
                using (MyDBContext context = new())
                {
                    listCategories = context.Categories.ToList();
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            return listCategories;
        }

        public static Category FindCategoryById(int categoryID)
        {
            Category? category = new();
            try
            {
                using (MyDBContext context = new())
                {
                    category = context.Categories.SingleOrDefault(c => c.CategoryID == categoryID);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return category;
        }

        public static void SaveCategory(Category category)
        {
            try
            {
                using (MyDBContext context = new())
                {
                    context.Categories.Add(category);
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public static void UpdateCategory(Category category)
        {
            try
            {
                using (MyDBContext context = new())
                {
                    context.Entry(category).State =
                        Microsoft.EntityFrameworkCore.EntityState.Modified;
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public static void DeleteCategory(Category category)
        {
            try
            {
                using (MyDBContext context = new())
                {
                    Category? categoryToDelete = context
                        .Categories
                        .SingleOrDefault(c => c.CategoryID == category.CategoryID);
                    context.Categories.Remove(categoryToDelete);
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}