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
    public class EmployeeController : ApiController
    {
        private readonly IEmployeeService _employeeService;
        private readonly IMapper _mapper;
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();
        public EmployeeController(IEmployeeService employeeService, IMapper mapper)
        {
            _employeeService = employeeService;
            _mapper = mapper;
        }


        [HttpGet]
        public async Task<IEnumerable<EmployeeDto>> GetAllAsync()
        {
            Logger.Info("Employee Controller GetAllAsync Request Received");
            var items = await _employeeService.GetAllemployeesAsync();
            return _mapper.Map<IEnumerable<EmployeeDto>>(items);
            
        }

        // GET api/<controller>/5
        [HttpGet]
        public async Task<IHttpActionResult> Get(string employeeCode)
        {
            Logger.Info("Employee Controller Get Request Received");
            var item = await _employeeService.GetEmployeeByCodeAsync(employeeCode);
            if (item != null)
            {
                return Content(System.Net.HttpStatusCode.OK, _mapper.Map<EmployeeDto>(item));
            }
            else {
                return Content(System.Net.HttpStatusCode.NotFound, "Company with Code " + employeeCode + " not found"); 
            }
        }
        // POST api/<controller>
        [HttpPost]
        public async Task<IHttpActionResult> Post([FromBody] EmployeeDto value)
        {
            try
            {
                Logger.Info("Employee Controller Post Request Received");
                var company = _mapper.Map<EmployeeInfo>(value);
                await _employeeService.AddEmployeeAsync(company);
                var messsage = Content(System.Net.HttpStatusCode.Created, value);

                return messsage;
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        // PUT api/<controller>/5
        [HttpPut]
        public async Task<IHttpActionResult> Put(string id, [FromBody] EmployeeDto value)
        {
            Logger.Info("Employee Controller Put Request Received" + id.ToString());
            var company = _mapper.Map<EmployeeInfo>(value);
            if (company != null)
            {
                return Ok(await _employeeService.UpdateEmployeeAsync(company, id));
            }
            Logger.Info("Employee Controller Put Request Employee Code Not Found! " + id.ToString());
            return Content(System.Net.HttpStatusCode.NotFound, "Employee with Code " + id + " not found");
        }

        // DELETE api/<controller>/5
        [HttpDelete]
        public async Task<IHttpActionResult> Delete(string id)
        {
            try
            {
                Logger.Info("Employee Controller Delete Request Received" + id.ToString());
                return Ok(await _employeeService.DeleteEmployeeAsync(id));
            }
            catch (Exception ex)
            {
                Logger.Warn("Employee Controller Delete Id Not Found! " + ex.Message.ToString());
                return InternalServerError(ex);
            }
        }
    }
}