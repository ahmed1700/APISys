using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Contracts;
using Entities.DataTransferObjects;
using Entities.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace CompanyEmployees.Controllers
{
    [Route("api/companies/{companyId}/employees")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private ILoggerManager _logger;
        private readonly IRepositroryManager _repositrory;
        private readonly IMapper _mapper;
        public EmployeeController(ILoggerManager logger, IRepositroryManager repositrory, IMapper mapper)
        {
            this._logger = logger;
            this._repositrory = repositrory;
            this._mapper = mapper;
        }
        [HttpGet]
        public IActionResult GetEmployeesForCompany(Guid companyId)
        {
            var company = _repositrory.Company.GetCompanyAsync(companyId, trackChanges: false);
            if (company == null)
            {
                _logger.LogInfo($"Company Wiht Id : {companyId} doesn't exist in the database");
                return NotFound();
            }
            var employees = _repositrory.Employee.GetEmployees(companyId, trackChanges: false);
            var employeesDto = _mapper.Map<IEnumerable<EmployeeDto>>(employees);
            return Ok(employeesDto);
        }
        [HttpGet("{id}", Name = "GetEmployeeForCompany")]
        public IActionResult GetEmployeeForCompany(Guid companyId, Guid id)
        {
            var company = _repositrory.Company.GetCompanyAsync(companyId, trackChanges: false);
            if (company == null)
            {
                _logger.LogInfo($"Company Wiht Id : {companyId} doesn't exist in the database");
                return NotFound();
            }
            var employee = _repositrory.Employee.GetEmployee(companyId, id, trackChanges: false);
            var employeeDto = _mapper.Map<EmployeeDto>(employee);
            return Ok(employeeDto);
        }
        [HttpPost]
        public IActionResult CreateEmployeeForCompany(Guid companyId, [FromBody] EmployeeForCreateDto employee)
        {
            if (employee == null)
            {
                _logger.LogError("EmployeeForCreateDto Object sent from client is null");
                return BadRequest("EmployeeForCreateDto Object is null");
            }
            if (!ModelState.IsValid)
            {
                _logger.LogError("Invalid Model state for the EmployeeForCreateDto object");
                return UnprocessableEntity(ModelState);
            }
            // Sure ? if company id exists ?
            var company = _repositrory.Company.GetCompanyAsync(companyId, trackChanges: false);
            if (company == null)
            {
                _logger.LogInfo($"Company With d : {companyId} does't exist in the database.");
                return NotFound();
            }
            // Everything is Okay emp/id 
            var employeeEntity = _mapper.Map<Employee>(employee);

            _repositrory.Employee.CreateEmployeeForCompany(companyId, employeeEntity);
            _repositrory.SaveAsync();
            var employeeToReturn = _mapper.Map<EmployeeDto>(employeeEntity);
            // save --?url 
            return CreatedAtRoute("GetEmployeeForCompany", new { companyId, id = employeeEntity.Id }, employeeToReturn);
        }
        [HttpDelete("{id}")]
        public IActionResult DeleteEmployeeForCompany(Guid companyId, Guid id)
        {
        var company = _repositrory.Company.GetCompanyAsync(companyId, trackChanges: false);
        if (company == null)
        {
            _logger.LogInfo($"Company Wiht Id : {companyId} doesn't exist in the database");
            return NotFound();
        }
            var employeeForComapny = _repositrory.Employee.GetEmployee(companyId, id, trackChanges: false);
            if (employeeForComapny==null)
            {
                _logger.LogInfo($"Employee Wiht Id : {id} doesn't exist in the database");
                return NotFound();
            }
            _repositrory.Employee.DeleteEmployee(employeeForComapny);
            _repositrory.SaveAsync();
            return NoContent(); // 204
        }

        [HttpPut("{id}")]
        public IActionResult UpdateEmployeeForCompany(Guid companyId, Guid id ,[FromBody] EmployeeForUpdateDto employee)
        {
            if (employee == null)
            {
                _logger.LogError("EmployeeForCreateDto Object sent from client is null");
                return BadRequest("EmployeeForCreateDto Object is null");
            }
            var company = _repositrory.Company.GetCompanyAsync(companyId, trackChanges: false);
            if (company == null)
            {
                _logger.LogInfo($"Company Wiht Id : {companyId} doesn't exist in the database");
                return NotFound();
            }
            if (!ModelState.IsValid)
            {
                _logger.LogError("Invalid model state for the EmployeeForUpdateDto object");
                return UnprocessableEntity(ModelState);
            }
            var employeeEntitiy = _repositrory.Employee.GetEmployee(companyId, id, trackChanges: true);
            if (employeeEntitiy == null)
            {
                _logger.LogInfo($"Employee Wiht Id : {id} doesn't exist in the database");
                return NotFound();
            }
            _mapper.Map(employee, employeeEntitiy);
            _repositrory.SaveAsync();
            return NoContent();
        }
        [HttpPatch("{id}")]
        public IActionResult PariallyUpdateEmployeeForCompany(Guid companyId, Guid id,
            [FromBody] JsonPatchDocument<EmployeeForUpdateDto> pathDoc)
        {
            if (pathDoc == null)
            {
                _logger.LogError("EmployeeForCreateDto Object sent from client is null");
                return BadRequest("EmployeeForCreateDto Object is null");
            }
            var company = _repositrory.Company.GetCompanyAsync(companyId, trackChanges: false);
            if (company == null)
            {
                _logger.LogInfo($"Company Wiht Id : {companyId} doesn't exist in the database");
                return NotFound();
            }
            var employeeEntitiy = _repositrory.Employee.GetEmployee(companyId, id, trackChanges: true);
            if (employeeEntitiy == null)
            {
                _logger.LogInfo($"Employee Wiht Id : {id} doesn't exist in the database");
                return NotFound();
            }
            var employeeToPatch = _mapper.Map<EmployeeForUpdateDto>(employeeEntitiy);
            pathDoc.ApplyTo(employeeToPatch , ModelState);
            TryValidateModel(employeeToPatch);
            if (!ModelState.IsValid)
            {
                _logger.LogError("Invalid model state for the Patch Document");
                return UnprocessableEntity(ModelState);
            }
            _mapper.Map(employeeToPatch, employeeEntitiy);
            _repositrory.SaveAsync();
            return NoContent();
        }
    }
}