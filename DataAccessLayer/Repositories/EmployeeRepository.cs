using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataAccessLayer.Model.Interfaces;
using DataAccessLayer.Model.Models;

namespace DataAccessLayer.Repositories
{
    public class EmployeeRepository : IEmployeeRepository
    {
	    private readonly IDbWrapper<Employee> _companyDbWrapper;

	    public EmployeeRepository(IDbWrapper<Employee> companyDbWrapper)
	    {
		    _companyDbWrapper = companyDbWrapper;
        }

        public IEnumerable<Employee> GetAll()
        {
            return _companyDbWrapper.FindAll();
        }
        public async Task<IEnumerable<Employee>> GetAllAsync()
        {
            return await _companyDbWrapper.FindAllAsync();
        }
        public Employee GetByCode(string employeeCode)
        {
            return _companyDbWrapper.Find(t => t.EmployeeCode.Equals(employeeCode))?.FirstOrDefault();
        }
        public async Task<Employee> GetByCodeAsync(string employeeCode)
        {
            var comp =  await _companyDbWrapper.FindAsync(t => t.EmployeeCode.Equals(employeeCode));
            return  comp.FirstOrDefault();       
        }
        public bool SaveEmployee(Employee employee)
        {
            var itemRepo = _companyDbWrapper.Find(t =>
                t.SiteId.Equals(employee.SiteId) && t.EmployeeCode.Equals(employee.EmployeeCode))?.FirstOrDefault();
            if (itemRepo !=null)
            {
               
                itemRepo.EmployeeName = employee.EmployeeName;
                itemRepo.CompanyName = employee.CompanyName;
                itemRepo.Occupation = employee.Occupation;
                itemRepo.EmployeeStatus = employee.EmployeeStatus;
                itemRepo.EmailAddress = employee.EmailAddress;
                itemRepo.Phone = employee.Phone;
                itemRepo.LastModified = employee.LastModified;
                return _companyDbWrapper.Update(itemRepo);
            }

            return _companyDbWrapper.Insert(employee);
        }
        public async Task<bool> SaveEmployeeAsync(Employee employee)
        {
            var itemRepo = _companyDbWrapper.Find(t =>
                t.SiteId.Equals(employee.SiteId) && t.EmployeeCode.Equals(employee.EmployeeCode))?.FirstOrDefault();
            if (itemRepo != null)
            {
              
                itemRepo.EmployeeName = employee.EmployeeName;
                itemRepo.CompanyName = employee.CompanyName;
                itemRepo.Occupation = employee.Occupation;
                itemRepo.EmployeeStatus = employee.EmployeeStatus;
                itemRepo.EmailAddress = employee.EmailAddress;
                itemRepo.Phone = employee.Phone;
                itemRepo.LastModified = employee.LastModified;
                return await _companyDbWrapper.UpdateAsync(itemRepo);
            }
            return await _companyDbWrapper.InsertAsync(employee);
        }
        public bool DeleteEmployee(string employeeCode)
        {
            return _companyDbWrapper.Delete(t => t.EmployeeCode.Equals(employeeCode));
        }
        public async Task<bool> DeleteEmployeeAsync(string employeeCode)
        {
            return await _companyDbWrapper.DeleteAsync(t => t.EmployeeCode.Equals(employeeCode));
        }
    }
}
