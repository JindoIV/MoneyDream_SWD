using MoneyDreamClassLibrary.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoneyDreamClassLibrary
{
    public class RoleDAO
    {
        private static readonly object InstanceLock = new object();
        private static RoleDAO instance = null;

        public static RoleDAO Instance
        {
            get
            {
                lock (InstanceLock)
                {
                    if (instance == null)
                    {
                        instance = new RoleDAO();
                    }
                    return instance;
                }
            }
        }

        public IEnumerable<Role> GetAllRoles()
        {
            List<Role> roles;

            try
            {

                var context = new MoneyDreamContext();
                roles = context.Roles.ToList();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return roles;
        }

        public  Role GetRole(int id)
        {
            Role role;

            try
            {

                var context = new MoneyDreamContext();
                role = context.Roles.SingleOrDefault(r => r.RoleId == id );

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return role;
        }


    }
}

