using BusinessObject.Models ;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public class SizeRepository : ISizeRepository
    {
        public List<Size> GetListSize() => SizeDAO.Instance.GetListSize();
        public Size GetSizeByName(string name) => SizeDAO.Instance.GetSizeByName(name)!;
        public void CreateSize(Size size) => SizeDAO.Instance.CreateSize(size);
        public void UpdateSize(Size size) => SizeDAO.Instance.UpdateSize(size);
        public Size GetSizeById(int id) => SizeDAO.Instance.GetSizeById(id)!;
        public void DeleteSize(int id) => SizeDAO.Instance.DeleteSize(id);
    }
}
