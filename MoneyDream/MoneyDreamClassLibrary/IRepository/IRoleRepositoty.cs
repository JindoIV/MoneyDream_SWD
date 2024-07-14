using MoneyDreamClassLibrary.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoneyDreamClassLibrary.IRepository
{
    public interface IRoleRepositoty
    {
        IEnumerable<Role> GetAllRoles();
        Role GetRole(int id);
    }
}
