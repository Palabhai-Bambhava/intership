using EmployeePortal.Data;
using EmployeePortal.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace EmployeePortal.Repository
{
    public class EmployeeRepository
    {
        private readonly AppDbContext db;

        public EmployeeRepository(AppDbContext dbContext) 
        {
            this.db = dbContext;
;        }

        public async Task<List<Employee>> GetEmployees()
        {
            return await db.Employees.ToListAsync();
        }

        public async Task SaveEmployee(Employee emp)
        {
            await db.Employees.AddAsync(emp);
            await db.SaveChangesAsync();
        }
        public async Task updateEmployee(int id, Employee obj)
        {
            var employee = await db.Employees.FindAsync(id);
            if (employee == null)
            {
                throw new Exception("Employee Not Found");
            }
            employee.Name = obj.Name;
            employee.Email = obj.Email;
            employee.Salary = obj.Salary;
            employee.Status = obj.Status;
            employee.Age = obj.Age;
            employee.Mobile = obj.Mobile;

            await db.SaveChangesAsync();
        }
        public async Task DeleteEmployee(int id)
        {
            var employee = await db.Employees.FindAsync(id);
            if (employee == null)
            {
                throw new Exception("Employee Not Found");
            }
            db.Employees.Remove(employee);
            await db.SaveChangesAsync();
        }



    }
}
