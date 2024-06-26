using BusinessObject.Model;

namespace DataAccess
{
    public class CustomerDAO
    {
        public static List<Customer> GetCustomers()
        {
            List<Customer> listCustomers = new();
            try
            {
                using (MyDBContext context = new())
                {
                    listCustomers = context.Customers.ToList();
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            return listCustomers;
        }

        public static List<Customer> Search(string keyword)
        {
            List<Customer> listCustomers = new();
            try
            {
                using (MyDBContext context = new())
                {
                    listCustomers = context.Customers
                        .Where(c => c.FirstName.Contains(keyword) || c.LastName.Contains(keyword))
                        .ToList();
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            return listCustomers;
        }

        public static void SaveCustomer(Customer customer)
        {
            try
            {
                using (MyDBContext context = new())
                {
                    context.Customers.Add(customer);
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public static Customer FindCustomerById(string customerId)
        {
            Customer? customer = new();
            try
            {
                using (MyDBContext context = new())
                {
                    customer = context.Customers.SingleOrDefault(c => c.Id == customerId);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return customer;
        }

        public static Customer FindCustomerByEmail(string email)
        {
            Customer? customer = new();
            try
            {
                using (MyDBContext context = new())
                {
                    customer = context.Customers.FirstOrDefault(c => c.Email == email);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return customer;
        }

        public static void UpdateCustomer(Customer customer)
        {
            try
            {
                using (MyDBContext context = new())
                {
                    context.Entry(customer).State =
                        Microsoft.EntityFrameworkCore.EntityState.Modified;
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public static void DeleteCustomer(Customer customer)
        {
            try
            {
                using (MyDBContext context = new())
                {
                    Customer? customerToDelete = context
                        .Customers
                        .SingleOrDefault(c => c.Id == customer.Id);
                    context.Customers.Remove(customerToDelete);
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