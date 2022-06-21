
using Hermodus.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hermodus.Service
{
  public interface ICompanyRepository
    {
        bool Save(Company watch);
        IEnumerable<Company> CompanyIEnum { get; }
        IQueryable<Company> CompanyList { get; }
        Company Delete(int? Id);
        Company Details(int? Id);

    }
}
