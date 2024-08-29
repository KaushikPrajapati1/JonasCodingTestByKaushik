using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer.Model.Models;

namespace DataAccessLayer.Model.Interfaces
{
    public interface IEmployeeRepository
    {
        IEnumerable<Employee> GetAll();
        Task<IEnumerable<Employee>> GetAllAsync();
        Employee GetByCode(string employeeCode);
        Task<Employee> GetByCodeAsync(string employeeCode);
        bool SaveEmployee(Employee employee);
        Task<bool> SaveEmployeeAsync(Employee employee);
        bool DeleteEmployee(string employeeCode);
        Task<bool> DeleteEmployeeAsync(string employeeCode);
    }
}
