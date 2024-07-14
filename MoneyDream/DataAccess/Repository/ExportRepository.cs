using BusinessObject.Models ;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public class ExportRepository : IExportRepository
    {
        public List<ExportInfo> GetListExport() => ExportDAO.Instance.GetListExport();
        public List<ExportInfo> GetListExportByProductId(int id) => ExportDAO.Instance.GetListExportByProductId(id);
        public List<ExportInfo> GetListExportByImportId(int? id) => ExportDAO.Instance.GetListExportByImportId(id);
        public void CreateExport(ExportInfo export) => ExportDAO.Instance.CreateExport(export);
        public void UpdateExport(ExportInfo export) => ExportDAO.Instance.UpdateExport(export);
        public void DeleteExport(int id) => ExportDAO.Instance.DeleteExport(id);
    }
}
