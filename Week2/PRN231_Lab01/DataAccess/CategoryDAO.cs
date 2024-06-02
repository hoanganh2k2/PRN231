using BusinessObjects;

namespace DataAccess
{
    public class CategoryDAO
    {
        public static List<Category> GetCategories()
        {
            List<Category> listCategories = new();
            try
            {
                using (MyDbContext context = new())
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
    }
}
