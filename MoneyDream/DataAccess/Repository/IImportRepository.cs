using BusinessObject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public interface IImportRepository
    {
        public List<ImportInfo> GetListImportByProductId(int? id);
        public List<ImportInfo> GetListImport();
        public ImportInfo? GetImportById(int? id);
        public void CreateImport(ImportInfo import);
        public void UpdateImport(ImportInfo import);
        public void DeleteImport(int id);
    }
}
