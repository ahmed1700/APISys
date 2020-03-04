using System.Collections.Generic;
using Contracts;
using Entities.Data;
using Entities.Models;
using System.Linq;
using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Repository
{
    public class CompanyRepositrory : RepositoryBase<Company> , ICompanyRepository
    {
        public CompanyRepositrory(RepositryContext _repositryContext) : base(_repositryContext)
        {

        }
       
        public void CreateCompany(Company company) => Create(company);
        public void DeleteCompany(Company company) => Delete(company);

        public async Task<IEnumerable<Company>> GetByIdsAsync(IEnumerable<Guid> ids, bool trackChanges) =>
           await FindByCondtion(col => ids.Contains(col.Id), trackChanges).ToListAsync();

        public async Task<IEnumerable<Company>> GeAlltCompaniesAsync(bool trackChanges) => 
          await FindAll(trackChanges).OrderBy(c => c.Name).ToListAsync();
        public async Task<Company> GetCompanyAsync(Guid companyId, bool trackChanges) =>
          await FindByCondtion(c => c.Id.Equals(companyId),trackChanges).SingleOrDefaultAsync();
    }
}
