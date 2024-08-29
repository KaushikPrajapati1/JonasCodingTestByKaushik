using System.Collections.Generic;
using System.Threading.Tasks;
using BusinessLayer.Model.Models;

namespace BusinessLayer.Model.Interfaces
{
    public interface ICompanyService
    {
        IEnumerable<CompanyInfo> GetAllCompanies();
        Task<IEnumerable<CompanyInfo>> GetAllCompaniesAsync();
        CompanyInfo GetCompanyByCode(string companyCode);
        Task<CompanyInfo> GetCompanyByCodeAsync(string companyCode);
        bool AddCompany(CompanyInfo company);
        Task<bool> AddCompanyAsync(CompanyInfo company);
        bool UpdateCompany(CompanyInfo company, string companyCode);
        Task<bool> UpdateCompanyAsync(CompanyInfo company, string companyCode);
        bool DeleteCompany(string companyCode);
        Task<bool> DeleteCompanyAsync(string companyCode);
    }
}
