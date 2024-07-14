using BusinessObject.Models;
using Microsoft.EntityFrameworkCore;

namespace DataAccess
{
    public class ImportDAO
    {
        private ImportDAO() { }
        private static ImportDAO? instance = null;
        private static readonly object instanceLock = new object();

        public static ImportDAO Instance
        {
            get
            {
                lock (instanceLock)
                {
                    if (instance == null)
                    {
                        instance = new ImportDAO();
                    }
                    return instance;
                }
            }
        }

        public List<ImportInfo> GetListImport()
        {
            List<ImportInfo> listImport = new List<ImportInfo>();
            try
            {
                using (var DbContext = new MoneyDreamContext())
                {
                    listImport = DbContext.ImportInfos.Include(x => x.Product)
                        .Include(x => x.Product.Size)
                        .Include(x => x.Product.Category)
                        .Include(x => x.Product.Supplier)
                        .ToList();
                }
            }
            catch (Exception)
            {
                throw new Exception("Get list import fail!");
            }
            return listImport;
        }

        public List<ImportInfo> GetListImportByProductId(int? id)
        {
            List<ImportInfo> import = new List<ImportInfo>();
            try
            {
                using (var DbContext = new MoneyDreamContext())
                {
                    import = DbContext.ImportInfos.Include(x => x.Product)
                        .Include(x => x.Product.Supplier)
                        .Include(x => x.Product.Category)
                        .Include(x => x.Product.Size)
                        .Where(x => x.ProductId == id).ToList();
                }
            }
            catch (Exception)
            {
                throw new Exception("Get import by product id fail!");
            }
            return import;
        }

        public ImportInfo? GetImportById(int? id)
        {
            ImportInfo? import = new ImportInfo();
            try
            {
                using (var DbContext = new MoneyDreamContext())
                {
                    import = DbContext.ImportInfos.Include(x => x.Product)
                                                    .Include(x => x.Product.Supplier)
                                                    .Include (x => x.Product.Size)
                                                    .Include(x => x.Product.Category)
                                                    .SingleOrDefault(x => x.ImportId == id);
                }
            }
            catch (Exception)
            {
                throw new Exception("Get import by id fail!");
            }
            return import;
        }

        public void CreateImport(ImportInfo import)
        {
            try
            {
                using (var DbContext = new MoneyDreamContext())
                {
                    DbContext.ImportInfos.Add(import);
                    DbContext.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
                throw new Exception("Create import fail!");
            }
        }

        public void UpdateImport(ImportInfo import)
        {
            try
            {
                using (var DbContext = new MoneyDreamContext())
                {
                    DbContext.Entry<ImportInfo>(import).State = EntityState.Modified;
                    DbContext.SaveChanges();
                }
            }
            catch (Exception)
            {
                throw new Exception("Update import fail!");
            }
        }

        public void DeleteImport(int id)
        {
            try
            {
                using (var DbContext = new MoneyDreamContext())
                {
                    ImportInfo? import = new ImportInfo();
                    import = DbContext.ImportInfos.SingleOrDefault(x => x.ImportId == id);
                    DbContext.ImportInfos.Remove(import!);
                    DbContext.SaveChanges();
                }
            }
            catch (Exception)
            {
                throw new Exception("Delete import fail!");
            }
        }
    }
}
