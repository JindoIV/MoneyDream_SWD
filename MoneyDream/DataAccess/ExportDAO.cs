using BusinessObject.Models ;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class ExportDAO
    {
        private ExportDAO() { }
        private static ExportDAO? instance = null;
        private static readonly object instanceLock = new object();

        public static ExportDAO Instance
        {
            get
            {
                lock (instanceLock)
                {
                    if (instance == null)
                    {
                        instance = new ExportDAO();
                    }
                    return instance;
                }
            }
        }

        public List<ExportInfo> GetListExport()
        {
            List<ExportInfo> export = new List<ExportInfo>();
            try
            {
                using (var DbContext = new MoneyDreamContext())
                {
                    export = DbContext.ExportInfos.Include(x => x.Account)
                                                  .Include(x => x.Import)
                                                  .Include(x => x.Product)
                                                  .Include(x => x.Product.Supplier)
                                                  .Include(x => x.Product.Category)
                                                  .Include(x => x.Product.Size).ToList();
                }
            }
            catch (Exception)
            {
                throw new Exception("Get list export fail!");
            }
            return export;
        }

        public List<ExportInfo> GetListExportByProductId(int id)
        {
            List<ExportInfo> export = new List<ExportInfo>();
            try
            {
                using (var DbContext = new MoneyDreamContext())
                {
                    export = DbContext.ExportInfos.Where(x => x.ProductId == id).ToList();
                }
            }
            catch (Exception)
            {
                throw new Exception("Get export by product id fail!");
            }
            return export;
        }

        public List<ExportInfo> GetListExportByImportId(int? id)
        {
            List<ExportInfo> allListExport = new List<ExportInfo>();
            List<ExportInfo> export = new List<ExportInfo>();
            try
            {
                using (var DbContext = new MoneyDreamContext())
                {
                    allListExport = DbContext.ExportInfos.Include(x => x.Account)
                                                         .Include(x => x.Product)
                                                         .Include(x => x.Product.Supplier)
                                                         .Include(x => x.Product.Category)
                                                         .Include(x => x.Product.Size)
                                                         .Include(x => x.Import).ToList();
                    export = allListExport.Where(x => x.ImportId == id).ToList();
                }
            }
            catch (Exception)
            {
                throw new Exception("Get export by import id fail!");
            }
            return export;
        }

        public void CreateExport(ExportInfo export)
        {
            try
            {
                using (var DbContext = new MoneyDreamContext())
                {
                    DbContext.ExportInfos.Add(export);
                    DbContext.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
                throw new Exception("Create export fail!");
            }
        }

        public void UpdateExport(ExportInfo export)
        {
            try
            {
                using (var DbContext = new MoneyDreamContext())
                {
                    DbContext.Entry<ExportInfo>(export).State = EntityState.Modified;
                    DbContext.SaveChanges();
                }
            }
            catch (Exception)
            {
                throw new Exception("Update export fail!");
            }
        }

        public void DeleteExport(int id)
        {
            try
            {
                using (var DbContext = new MoneyDreamContext())
                {
                    ExportInfo? export = new ExportInfo();
                    export = DbContext.ExportInfos.SingleOrDefault(x => x.ExportId == id);
                    DbContext.ExportInfos.Remove(export!);
                    DbContext.SaveChanges();
                }
            }
            catch (Exception)
            {
                throw new Exception("Delete export fail!");
            }
        }
    }
}
