using Microsoft.EntityFrameworkCore;
using NeveraPortal.BLL.Services.Interfaces;
using NeveraPortal.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeveraPortal.BLL.Services.Repositories
{
    public class CompanyRepository : GenericRepository<Company>, ICompanyRepository
    {
        public List<Company> GetCompanies()
        {
            return dbSet.Include(c => c.Country).Where(q => q.IsDeleted == false).ToList();
        }
    }
}
