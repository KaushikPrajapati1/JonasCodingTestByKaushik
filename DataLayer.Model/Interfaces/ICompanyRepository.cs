using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer.Model.Models;

namespace DataAccessLayer.Model.Interfaces
{
    public interface ICompanyRepository
    {
        IEnumerable<Company> GetAll();
        Task<IEnumerable<Company>> GetAllAsync();
        Company GetByCode(string companyCode);
        Task<Company> GetByCodeAsync(string companyCode);
        bool SaveCompany(Company company);
        Task<bool> SaveCompanyAsync(Company company);
        bool DeleteCompany(string companyCode);
        Task<bool> DeleteCompanyAsync(string companyCode);
    }
}
