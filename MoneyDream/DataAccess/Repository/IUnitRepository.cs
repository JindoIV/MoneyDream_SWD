using BusinessObject.Models ;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public interface IUnitRepository
    {
        public List<Unit> GetListUnit();
        public Unit GetUnitByName(string name);
        public Unit GetUnitById(int id);
        public void CreateUnit(Unit unit);
        public void UpdateUnit(Unit unit);
        public void DeleteUnit(int id);
    }
}
