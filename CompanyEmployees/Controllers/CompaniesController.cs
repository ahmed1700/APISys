using System;
using Contracts;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using System.Collections.Generic;
using Entities.DataTransferObjects;
using Entities.Models;
using System.Linq;
using CompanyEmployees.ModelBinders;
using System.Threading.Tasks;

namespace CompanyEmployees.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompaniesController : ControllerBase
    {
        private ILoggerManager _logger;
        private readonly IRepositroryManager _repositrory;
        private readonly IMapper _mapper;
        public CompaniesController(ILoggerManager logger, IRepositroryManager repositrory , IMapper mapper)
        {
            this._logger = logger;
            this._repositrory = repositrory;
            this._mapper = mapper;
        }
        [HttpGet]
        public async Task<IActionResult> GetCompanies()
        {
                var companies =await _repositrory.Company.GeAlltCompaniesAsync(trackChanges: false);
                var companiesDto = _mapper.Map<IEnumerable<CompanyDto>>(companies);
                return Ok(companiesDto);
        }
        [HttpGet("{id}" , Name ="CompanyByID")]
        public IActionResult GetCompany(Guid id)
        {
            var company = _repositrory.Company.GetCompanyAsync(id, trackChanges: false);
            if (company == null)
            {
                _logger.LogInfo($"Company Wiht Id : {id} doesn't exist in the database");
                return NotFound();
            }
            else
            {
                var companyDto = _mapper.Map<CompanyDto>(company);
                return Ok(companyDto);
            }
        }
        [HttpGet("collection/({ids})" , Name ="CompanyCollection")]
        public async Task<IActionResult> GetCompanyCollection(   [ModelBinder(BinderType = typeof(ArrayModelBinder))] IEnumerable<Guid> ids)
        {
            if (ids ==null)
            {
                _logger.LogError("Parameter ids is null");
                return BadRequest("Parameter ids is null");
            }
            var companyEntites =await _repositrory.Company.GetByIdsAsync(ids, trackChanges: false);
            // Id valid + id not valid ==> Collection ? 
            if (ids.Count() != companyEntites.Count())
            {
                _logger.LogError("Parameter ids is null");
                return NotFound();
            }
            var companyToReturn = _mapper.Map<IEnumerable<CompanyDto>>(companyEntites);
            return Ok(companyToReturn);
        }
        [HttpPost("collection")]
        public async Task<IActionResult> CreateCompanyCollection([FromBody]IEnumerable<CompanyForCreateDto> companyCollection)
        {
            if (companyCollection == null)
            {
                _logger.LogError("Company Collection Object sent from client is null");
                return BadRequest("Company Collection Object is null");
            }
            var companyEntites = _mapper.Map<IEnumerable<Company>>(companyCollection);
            foreach (var company in companyEntites)
            {
                _repositrory.Company.CreateCompany(company);
            }
           await _repositrory.SaveAsync();
            // return result ==> Saved 
            var companyCollectiontoReturn = _mapper.Map<IEnumerable<CompanyDto>>(companyEntites);
            var ids = String.Join(",", companyCollectiontoReturn.Select(c => c.Id));
            return CreatedAtRoute("CompanyCollection", new { ids }, companyCollectiontoReturn);
        }
        [HttpPost]
        public  IActionResult CreateCompany([FromBody]CompanyForCreateDto company)
        {
            if (company ==null)
            {
                _logger.LogError("CompanyForCreateDto Object sent from client is null");
                return BadRequest("CompanyForCreateDto Object is null");
            }
            var companyEntity = _mapper.Map<Company>(company);
            _repositrory.Company.CreateCompany(companyEntity);
            _repositrory.SaveAsync();
            // return result ==> Saved 
            var companytoReturn= _mapper.Map<CompanyDto>(companyEntity);
            return CreatedAtRoute("CompanyByID", new { id = companytoReturn.Id }, companytoReturn);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCompany(Guid id)
        {
            var company =await _repositrory.Company.GetCompanyAsync(id, trackChanges: false);
            if (company == null)
            {
                _logger.LogInfo($"Company Wiht Id : {id} doesn't exist in the database");
                return NotFound();
            }
            _repositrory.Company.DeleteCompany(company);
            await _repositrory.SaveAsync();
            return NoContent();
        }
        [HttpPut("{id}")]
         public async Task<IActionResult> UpdateCompany(Guid id ,   [FromBody]CompanyForUpdateDto company)
        {
            if (company==null)
            {
                _logger.LogError("CompanyForUpdateDto object sent from client is  null");
                return BadRequest("CompanyForUpdateDto object is  null");
            }
            var companyEntity = await _repositrory.Company.GetCompanyAsync(id, trackChanges: true);
            if (companyEntity == null)
            {
                _logger.LogInfo($"Company Wiht Id : {id} doesn't exist in the database");
                return NotFound();
            }
            _mapper.Map(company, companyEntity);
            await _repositrory.SaveAsync();
            return NoContent();
        }


    }
}