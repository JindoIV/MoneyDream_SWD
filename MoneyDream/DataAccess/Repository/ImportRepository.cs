using BusinessObject.Models ;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public class ImportRepository : IImportRepository
    {
        public List<ImportInfo> GetListImportByProductId(int? id) => ImportDAO.Instance.GetListImportByProductId(id);
        public List<ImportInfo> GetListImport() => ImportDAO.Instance.GetListImport();
        public ImportInfo? GetImportById(int? id) => ImportDAO.Instance.GetImportById(id);
        public void CreateImport(ImportInfo import) => ImportDAO.Instance.CreateImport(import);
        public void UpdateImport(ImportInfo import) => ImportDAO.Instance.UpdateImport(import);
        public void DeleteImport(int id) => ImportDAO.Instance.DeleteImport(id);
    }
}
