using Grpc.Core;
using GrpcServiceDemo.DataAccess;

namespace GrpcServiceDemo.Services
{
    public class EmployeeCRUDService : EmployeeCRUD.EmployeeCRUDBase
    {
        private readonly ILogger<EmployeeCRUDService> _logger;
        private readonly AppDbContext db = null;

        public EmployeeCRUDService(ILogger<EmployeeCRUDService> logger, AppDbContext db)
        {
            _logger = logger;
            this.db = db;
        }

        public override Task<Employees> SelectAll(Empty request, ServerCallContext context)
        {
            Employees responseData = new();
            List<Employee> query = db.Employees
                                       .Select(emp => new Employee
                                       {
                                           EmployeeID = emp.employeeId,
                                           FirstName = emp.firstName,
                                           LastName = emp.lastName
                                       })
                                       .ToList();
            responseData.Items.AddRange(query);

            return Task.FromResult(responseData);
        }

        public override Task<Employee> SelectByID(EmployeeFilter request, ServerCallContext context)
        {
            DataAccess.Employee? data = db.Employees.Find(request.EmployeeID);
            Employee emp = new()
            {
                EmployeeID = data.employeeId,
                FirstName = data.firstName,
                LastName = data.lastName
            };

            return Task.FromResult(emp);
        }

        public override Task<Empty> Insert(Employee request, ServerCallContext context)
        {
            db.Employees.Add(new DataAccess.Employee()
            {
                employeeId = request.EmployeeID,
                firstName = request.FirstName,
                lastName = request.LastName
            });
            db.SaveChanges();

            return Task.FromResult(new Empty());
        }

        public override Task<Empty> Update(Employee request, ServerCallContext context)
        {
            db.Employees.Update(new DataAccess.Employee()
            {
                employeeId = request.EmployeeID,
                firstName = request.FirstName,
                lastName = request.LastName
            });
            db.SaveChanges();

            return Task.FromResult(new Empty());
        }

        public override Task<Empty> Delete(EmployeeFilter request, ServerCallContext context)
        {
            DataAccess.Employee? data = db.Employees.Find(request.EmployeeID);
            db.Employees.Remove(new DataAccess.Employee()
            {
                employeeId = data.employeeId,
                firstName = data.firstName,
                lastName = data.lastName
            });
            db.SaveChanges();

            return Task.FromResult(new Empty());
        }
    }
}
