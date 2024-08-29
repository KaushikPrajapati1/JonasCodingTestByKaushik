using BusinessLayer.Model.Interfaces;
using System.Collections.Generic;
using AutoMapper;
using BusinessLayer.Model.Models;
using DataAccessLayer.Model.Interfaces;
using DataAccessLayer.Model.Models;
using System.Globalization;
using System;
using System.Threading.Tasks;

namespace BusinessLayer.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IMapper _mapper;

        public EmployeeService(IEmployeeRepository employeeRepository, IMapper mapper)
        {
            _employeeRepository = employeeRepository;
            _mapper = mapper;
        }
        public IEnumerable<EmployeeInfo> GetAllemployees()
        {
            var res = _employeeRepository.GetAll();
            return _mapper.Map<IEnumerable<EmployeeInfo>>(res);
        }
        public async Task<IEnumerable<EmployeeInfo>> GetAllemployeesAsync()
        {
            var res = await _employeeRepository.GetAllAsync();
            return  _mapper.Map<IEnumerable<EmployeeInfo>>(res);
        }
        public EmployeeInfo GetEmployeeByCode(string employeeCode)
        {
            var result = _employeeRepository.GetByCode(employeeCode);
            return _mapper.Map<EmployeeInfo>(result);
        }
        public async Task<EmployeeInfo> GetEmployeeByCodeAsync(string employeeCode)
        {
            var result = await _employeeRepository.GetByCodeAsync(employeeCode);
            return _mapper.Map<EmployeeInfo>(result);
        }
        public bool AddEmployee(EmployeeInfo company)
        {
            var Comp = _mapper.Map<Employee>(company);
            return _employeeRepository.SaveEmployee(Comp);

        }
        public async Task<bool> AddEmployeeAsync(EmployeeInfo company)
        {
            var Comp = _mapper.Map<Employee>(company);
            return await _employeeRepository.SaveEmployeeAsync(Comp);

        }
        public bool UpdateEmployee(EmployeeInfo company, string employeeCode)
        {
            var ExistingCompany = GetEmployeeByCode(employeeCode);
            if (ExistingCompany == null)
            {
                throw new Exception("Employee ID doesn't exist");
            }
            else
            {
                var comp = _mapper.Map<Employee>(company);
                return _employeeRepository.SaveEmployee(comp);
            }
        }
        public async Task<bool> UpdateEmployeeAsync(EmployeeInfo employee, string employeeCode)
        {
            var ExistingEmployee = await GetEmployeeByCodeAsync(employeeCode);
            if (ExistingEmployee == null)
            {
                throw new Exception("Employee ID doesn't exist");
            }
            else
            {
                var empl = _mapper.Map<Employee>(employee);
                return await _employeeRepository.SaveEmployeeAsync(empl);
            }
        }
        public bool DeleteEmployee(string employeeCode) {
           return _employeeRepository.DeleteEmployee(employeeCode);  
        }
        public async Task<bool> DeleteEmployeeAsync(string employeeCode)
        {
            return await _employeeRepository.DeleteEmployeeAsync(employeeCode);
        }
    }
}
