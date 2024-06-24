using BusinessObject;
using BusinessObject.Models;

namespace DataAccess
{
    public class CategoryDAO
    {
        public static List<Category> GetCategories()
        {
            using (MyDbContext context = new())
            {
                return context.Category.ToList();
            }
        }

        public static Category GetCategoryById(int id)
        {
            using (MyDbContext context = new())
            {
                return context.Category.Find(id);
            }
        }

        public static void SaveCategory(Category category)
        {
            try
            {
                using (MyDbContext context = new())
                {
                    context.Category.Add(category);
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
                using (MyDbContext context = new())
                {
                    BusinessObject.Models.Category existingCategory = context.Category.Find(category.CategoryId);
                    if (existingCategory != null)
                    {
                        context.Entry(existingCategory).CurrentValues.SetValues(category);
                        context.SaveChanges();
                    }
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
                using (MyDbContext context = new())
                {
                    BusinessObject.Models.Category existingCategory = context.Category.Find(category.CategoryId);
                    if (existingCategory != null)
                    {
                        context.Category.Remove(existingCategory);
                        context.SaveChanges();
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
