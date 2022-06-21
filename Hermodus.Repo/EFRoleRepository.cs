
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Hermodus.Data;
using Hermodus.Repo;
using Hermodus.Service;

namespace Hermodus.Repo
{
    public class EFRoleRepository : IRoleRepository
    {
        EFDbContext context = new EFDbContext();

        public IEnumerable<Role> RoleIEnum
        {
            get { return context.Roles; }
        }
    }
}
