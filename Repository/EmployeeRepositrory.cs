using System;
using System.Collections.Generic;
using Contracts;
using Entities.Data;
using Entities.Models;
using System.Linq;

namespace Repository
{
    class EmployeeRepositrory : RepositoryBase<Employee>, IEmployeeRepository
    {
        public EmployeeRepositrory(RepositryContext _repositryContext) : base(_repositryContext)
        {

        }

        public void CreateEmployeeForCompany(Guid companyId, Employee employee)
        {
            employee.CompanyId = companyId;
            Create(employee);
        }

        public void DeleteEmployee(Employee employee) => Delete(employee);

        public Employee GetEmployee(Guid companyId, Guid employeeId, bool trackChanges)
        => FindByCondtion(e => e.CompanyId.Equals(companyId) && e.Id.Equals(employeeId),
            trackChanges).OrderBy(e => e.Name).SingleOrDefault();

        public IEnumerable<Employee> GetEmployees(Guid companyId, bool trackChanges)
            => FindByCondtion(e => e.CompanyId.Equals(companyId), trackChanges).OrderBy(e => e.Name);
    }
}
