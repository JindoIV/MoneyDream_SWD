using BusinessObject.Models ;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public class UnitRepository : IUnitRepository
    {
        public List<Unit> GetListUnit() => UnitDAO.Instance.GetListUnit();
        public Unit GetUnitByName(string name) => UnitDAO.Instance.GetUnitByName(name)!;
        public void CreateUnit(Unit unit) => UnitDAO.Instance.CreateUnit(unit);
        public void UpdateUnit(Unit unit) => UnitDAO.Instance.UpdateUnit(unit);
        public Unit GetUnitById(int id) => UnitDAO.Instance.GetUnitById(id)!;
        public void DeleteUnit(int id) => UnitDAO.Instance.DeleteUnit(id);
    }
}
