using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Contracts
{
    public interface IRepositroryManager
    {
        IEmployeeRepository Employee { get; }
        ICompanyRepository Company { get; }

        Task SaveAsync();
    }
}
