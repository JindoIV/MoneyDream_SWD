using BusinessObject.Models ;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public interface IExportRepository
    {
        public List<ExportInfo> GetListExport();
        public List<ExportInfo> GetListExportByProductId(int id);
        public List<ExportInfo> GetListExportByImportId(int? id);
        public void CreateExport(ExportInfo export);
        public void UpdateExport(ExportInfo export);
        public void DeleteExport(int id);
    }
}
