using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Results;
using AutoMapper;
using BusinessLayer.Model.Interfaces;
using BusinessLayer.Model.Models;
using DataAccessLayer.Model.Models;
using WebApi.Models;
using NLog;

namespace WebApi.Controllers
{
    public class CompanyController : ApiController
    {
        private readonly ICompanyService _companyService;
        private readonly IMapper _mapper;
     
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();
        public CompanyController(ICompanyService companyService, IMapper mapper)
        {
            _companyService = companyService;
            _mapper = mapper;
           
        }
        
        [HttpGet]
        public async Task<IEnumerable<CompanyDto>> GetAllAsync()
        {
            Logger.Info("Company Controller GetAllAsync Request Received");
            var items = await _companyService.GetAllCompaniesAsync();

            return _mapper.Map<IEnumerable<CompanyDto>>(items);
        }
        

        // GET api/<controller>/5
        [HttpGet]
        public async Task<IHttpActionResult> Get(string companyCode)
        {
            Logger.Info("Company Controller Get Request Received");
            var item = await _companyService.GetCompanyByCodeAsync(companyCode);
            if (item != null)
            {
                return Content(System.Net.HttpStatusCode.OK,_mapper.Map<CompanyDto>(item));
            }
            return Content(System.Net.HttpStatusCode.NotFound, "Company with Code " + companyCode + " not found");
        }
        // POST api/<controller>
        [HttpPost]
        public async Task<IHttpActionResult> Post([FromBody] CompanyDto value)
        {
            try
            {
                Logger.Info("Company Controller Post Request Received");
                var company = _mapper.Map<CompanyInfo>(value);
               await _companyService.AddCompanyAsync(company);
                var messsage = Content(System.Net.HttpStatusCode.Created, value);
               
                return messsage;
            }
            catch (Exception ex)
            {
                Logger.Warn("Company Controller Post Request Not Worked!" + ex.Message.ToString());
                return InternalServerError(ex);
            }
        }

        // PUT api/<controller>/5
        [HttpPut]
        public async Task<IHttpActionResult> Put(string id, [FromBody] CompanyDto value)
        {
            try
            {
                Logger.Info("Company Controller Put Request Received");
                var company = _mapper.Map<CompanyInfo>(value);
            return Ok(await _companyService.UpdateCompanyAsync(company, id));
            }
            catch (Exception ex)
            {
                Logger.Warn("Company Controller Put Request Not Worked!" + ex.Message.ToString());
                return InternalServerError(ex);
            }
        }

        // DELETE api/<controller>/5
        [HttpDelete]
        public async Task<IHttpActionResult> Delete(string id)
        {
            try
            {
                Logger.Info("Company Controller Delete Request Received");
                return Ok(await _companyService.DeleteCompanyAsync(id));
            }
            catch (Exception ex)
            {
                Logger.Warn("Company Controller Delete Request Not Worked!" + ex.Message.ToString());
                return InternalServerError(ex);
            }
        }
    }
}