using MoneyDreamClassLibrary.DataAccess;
using MoneyDreamClassLibrary.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoneyDreamClassLibrary.Repository
{
    public class RoleRepository : IRoleRepositoty
    {
        public IEnumerable<Role> GetAllRoles() => RoleDAO.Instance.GetAllRoles();
        public Role GetRole(int id) => RoleDAO.Instance.GetRole(id);

    }
}
