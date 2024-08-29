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
    public class CompanyService : ICompanyService
    {
        private readonly ICompanyRepository _companyRepository;
        private readonly IMapper _mapper;

        public CompanyService(ICompanyRepository companyRepository, IMapper mapper)
        {
            _companyRepository = companyRepository;
            _mapper = mapper;
        }
        public IEnumerable<CompanyInfo> GetAllCompanies()
        {
            var res = _companyRepository.GetAll();
            return _mapper.Map<IEnumerable<CompanyInfo>>(res);
        }
        public async Task<IEnumerable<CompanyInfo>> GetAllCompaniesAsync()
        {
            var res = await _companyRepository.GetAllAsync();
            return  _mapper.Map<IEnumerable<CompanyInfo>>(res);
        }
        public CompanyInfo GetCompanyByCode(string companyCode)
        {
            var result = _companyRepository.GetByCode(companyCode);
            return _mapper.Map<CompanyInfo>(result);
        }
        public async Task<CompanyInfo> GetCompanyByCodeAsync(string companyCode)
        {
            var result = await _companyRepository.GetByCodeAsync(companyCode);
            return _mapper.Map<CompanyInfo>(result);
        }
        public bool AddCompany(CompanyInfo company)
        {
            var Comp = _mapper.Map<Company>(company);
            return _companyRepository.SaveCompany(Comp);

        }
        public async Task<bool> AddCompanyAsync(CompanyInfo company)
        {
            var Comp = _mapper.Map<Company>(company);
            return await _companyRepository.SaveCompanyAsync(Comp);

        }
        public bool UpdateCompany(CompanyInfo company, string companyCode)
        {
            var ExistingCompany = GetCompanyByCode(companyCode);
            if (ExistingCompany == null)
            {
                throw new Exception("Company ID doesn't exist");
            }
            else
            {
                var comp = _mapper.Map<Company>(company);
                return _companyRepository.SaveCompany(comp);
            }
        }
        public async Task<bool> UpdateCompanyAsync(CompanyInfo company, string companyCode)
        {
            var ExistingCompany = await GetCompanyByCodeAsync(companyCode);
            if (ExistingCompany == null)
            {
                throw new Exception("Company ID doesn't exist");
            }
            else
            {
                var comp = _mapper.Map<Company>(company);
                return await _companyRepository.SaveCompanyAsync(comp);
            }
        }
        public bool DeleteCompany(string companyCode) {
           return _companyRepository.DeleteCompany(companyCode);  
        }
        public async Task<bool> DeleteCompanyAsync(string companyCode)
        {
            return await _companyRepository.DeleteCompanyAsync(companyCode);
        }
    }
}
