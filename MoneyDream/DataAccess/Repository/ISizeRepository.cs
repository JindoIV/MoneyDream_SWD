using BusinessObject.Models ;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public interface ISizeRepository
    {
        public List<Size> GetListSize();
        public Size GetSizeByName(string name);
        public Size GetSizeById(int id);
        public void CreateSize(Size size);
        public void UpdateSize(Size size);
        public void DeleteSize(int id);
    }
}
