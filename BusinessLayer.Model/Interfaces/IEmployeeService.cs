using System.Collections.Generic;
using System.Threading.Tasks;
using BusinessLayer.Model.Models;

namespace BusinessLayer.Model.Interfaces
{
    public interface IEmployeeService
    {
        IEnumerable<EmployeeInfo> GetAllemployees();
        Task<IEnumerable<EmployeeInfo>> GetAllemployeesAsync();
        EmployeeInfo GetEmployeeByCode(string employeeCode);
        Task<EmployeeInfo> GetEmployeeByCodeAsync(string employeeCode);
        bool AddEmployee(EmployeeInfo employee);
        Task<bool> AddEmployeeAsync(EmployeeInfo employee);
        bool UpdateEmployee(EmployeeInfo employee, string employeeCode);
        Task<bool> UpdateEmployeeAsync(EmployeeInfo employee, string employeeCode);
        bool DeleteEmployee(string employeeCode);
        Task<bool> DeleteEmployeeAsync(string employeeCode);
    }
}
