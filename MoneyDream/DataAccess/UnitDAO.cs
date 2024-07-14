using BusinessObject.Models ;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class UnitDAO
    {
        private UnitDAO() { }
        private static UnitDAO? instance = null;
        private static readonly object instanceLock = new object();

        public static UnitDAO Instance
        {
            get
            {
                lock (instanceLock)
                {
                    if (instance == null)
                    {
                        instance = new UnitDAO();
                    }
                    return instance;
                }
            }
        }

        public List<Unit> GetListUnit()
        {
            List<Unit> listUnit = new List<Unit>();
            try
            {
                using (var DbContext = new MoneyDreamContext())
                {
                    listUnit = DbContext.Units.ToList();
                }
            }
            catch (Exception)
            {
                throw new Exception("Get list units fail!");
            }
            return listUnit;
        }

        public Unit? GetUnitByName(string name)
        {
            Unit? unit = new Unit();
            try
            {
                using (var DbContext = new MoneyDreamContext())
                {
                    unit = DbContext.Units.SingleOrDefault(x => x.Name == name);
                }
            }
            catch (Exception)
            {
                throw new Exception("Get unit by name fail!");
            }
            return unit;
        }

        public Unit? GetUnitById(int id)
        {
            Unit? unit = new Unit();
            try
            {
                using (var DbContext = new MoneyDreamContext())
                {
                    unit = DbContext.Units.SingleOrDefault(x => x.UnitId == id);
                }
            }
            catch (Exception)
            {
                throw new Exception("Get unit by id fail!");
            }
            return unit;
        }

        public void CreateUnit(Unit unit)
        {
            try
            {
                using (var DbContext = new MoneyDreamContext())
                {
                    DbContext.Units.Add(unit);
                    DbContext.SaveChanges();
                }
            }
            catch (Exception)
            {
                throw new Exception("Create unit fail!");
            }
        }

        public void UpdateUnit(Unit unit)
        {
            try
            {
                using (var DbContext = new MoneyDreamContext())
                {
                    DbContext.Entry<Unit>(unit).State = EntityState.Modified;
                    DbContext.SaveChanges();
                }
            }
            catch (Exception)
            {
                throw new Exception("Update unit fail!");
            }
        }

        public void DeleteUnit(int id)
        {
            try
            {
                using (var DbContext = new MoneyDreamContext())
                {
                    Unit? unit = new Unit();
                    unit = DbContext.Units.SingleOrDefault(x => x.UnitId == id);
                    DbContext.Units.Remove(unit!);
                    DbContext.SaveChanges();
                }
            }
            catch (Exception)
            {
                throw new Exception("Delete unit fail!");
            }
        }
    }
}
