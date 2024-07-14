using BusinessObject.Models;
using DataAccess.Repository;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;

namespace InventoryManagement.ViewModel
{
    public class StockInViewModel : BaseViewModel
    {
        public IProductRepository productRepository = new ProductRepository();
        public IImportRepository importRepository = new ImportRepository();
        public ISupplierRepository supplierRepository = new SupplierRepository();
        public ISizeRepository sizeRepository = new SizeRepository();
        public ICategoryRepository categoryRepository = new CategoryRepository();
        public IExportRepository exportRepository = new ExportRepository();


        private ObservableCollection<ImportInfo>? _List;
        public ObservableCollection<ImportInfo>? List { get => _List; set { _List = value; OnPropertyChanged(); } }

        private ObservableCollection<BusinessObject.Models.Size>? _Size;
        public ObservableCollection<BusinessObject.Models.Size>? Size { get => _Size; set { _Size = value; OnPropertyChanged(); } }

        private ObservableCollection<Category>? _Category;
        public ObservableCollection<Category>? Category { get => _Category; set { _Category = value; OnPropertyChanged(); } }

        private ObservableCollection<Supplier>? _Supplier;
        public ObservableCollection<Supplier>? Supplier { get => _Supplier; set { _Supplier = value; OnPropertyChanged(); } }

        private ImportInfo? _SelectedItem = null;
        public ImportInfo? SelectedItem
        {
            get => _SelectedItem;
            set
            {
                _SelectedItem = value;
                OnPropertyChanged();

                if (SelectedItem != null)
                {
                    ImportInfo? getImport = new ImportInfo();
                    List<ExportInfo>? ListExportDetail = new List<ExportInfo>();

                    getImport = importRepository.GetImportById(SelectedItem.ImportId);
                    ListExportDetail = exportRepository.GetListExportByImportId(SelectedItem.ImportId);

                    ImportDefaultInfo = SelectedItem.Product.ImportId.ToString();
                    ImportIdInfo = SelectedItem.ImportId.ToString();
                    SelectedProductInfo = SelectedItem.Product.Name;
                    SelectedSupplierInfo = SelectedItem.Product.Supplier.Name;
                    SelectedCategoryInfo = SelectedItem.Product.Category.Name;
                    SelectedSizeInfo = SelectedItem.Product.Size.Name;
                    CountInfo = SelectedItem.Count.ToString();
                    InStockInfo = (getImport?.Count - (int)ListExportDetail?.Sum(x => x.Count)!).ToString();
                    ImportPriceInfo = SelectedItem.ImportPrice.ToString();
                    ExportPriceInfo = SelectedItem.ExportPrice.ToString();
                    ImportDateInfo = SelectedItem?.DateImport?.ToString().Split(" ")[0];
                    StatusInfo = SelectedItem?.Status?.ToString();
                }
            }
        }

        //Information 

        private string? _ImportDefaultInfo;
        public string? ImportDefaultInfo
        {
            get => _ImportDefaultInfo;
            set
            {
                _ImportDefaultInfo = value;
                OnPropertyChanged();
            }

        }


        private string? _SelectedProductInfo;
        public string? SelectedProductInfo
        {
            get => _SelectedProductInfo;
            set
            {
                _SelectedProductInfo = value;
                OnPropertyChanged();
            }

        }

        private string? _SelectedSupplierInfo;
        public string? SelectedSupplierInfo
        {
            get => _SelectedSupplierInfo;
            set
            {
                _SelectedSupplierInfo = value;
                OnPropertyChanged();
            }

        }

        private string? _SelectedCategoryInfo;
        public string? SelectedCategoryInfo
        {
            get => _SelectedCategoryInfo;
            set
            {
                _SelectedCategoryInfo = value;
                OnPropertyChanged();
            }

        }

        private string? _SelectedSizeInfo;
        public string? SelectedSizeInfo
        {
            get => _SelectedSizeInfo;
            set
            {
                _SelectedSizeInfo = value;
                OnPropertyChanged();
            }

        }


        private string? _ImportIdInfo;
        public string? ImportIdInfo { get => _ImportIdInfo; set { _ImportIdInfo = value; OnPropertyChanged(); } }


        private string? _CountInfo;
        public string? CountInfo { get => _CountInfo; set { _CountInfo = value; OnPropertyChanged(); } }

        private string? _InStockInfo;
        public string? InStockInfo { get => _InStockInfo; set { _InStockInfo = value; OnPropertyChanged(); } }


        private string? _ImportPriceInfo;
        public string? ImportPriceInfo { get => _ImportPriceInfo; set { _ImportPriceInfo = value; OnPropertyChanged(); } }


        private string? _ExportPriceInfo;
        public string? ExportPriceInfo { get => _ExportPriceInfo; set { _ExportPriceInfo = value; OnPropertyChanged(); } }


        private string? _StatusInfo;
        public string? StatusInfo { get => _StatusInfo; set { _StatusInfo = value; OnPropertyChanged(); } }

        private string? _ImportDateInfo;
        public string? ImportDateInfo { get => _ImportDateInfo; set { _ImportDateInfo = value; OnPropertyChanged(); } }


        // Edit Form

        private Product? _SelectedProduct;
        public Product? SelectedProduct
        {
            get => _SelectedProduct;
            set
            {
                _SelectedProduct = value;
                OnPropertyChanged();
            }

        }

        private Category? _SelectedCategory;
        public Category? SelectedCategory
        {
            get => _SelectedCategory;
            set
            {
                _SelectedCategory = value;
                OnPropertyChanged();
            }
        }

        private BusinessObject.Models.Size? _SelectedSize;
        public BusinessObject.Models.Size? SelectedSize
        {
            get => _SelectedSize;
            set
            {
                _SelectedSize = value;
                OnPropertyChanged();
            }
        }

        private Supplier? _SelectedSupplier;
        public Supplier? SelectedSupplier
        {
            get => _SelectedSupplier;
            set
            {
                _SelectedSupplier = value;
                OnPropertyChanged();
            }
        }


        private ObservableCollection<Product>? _SelectedProductNameUnique;
        public ObservableCollection<Product>? SelectedProductNameUnique
        {
            get => _SelectedProductNameUnique;
            set
            {
                _SelectedProductNameUnique = value;
                OnPropertyChanged();
            }
        }

        private string? _CountInput;
        public string? CountInput { get => _CountInput; set { _CountInput = value; OnPropertyChanged(); } }


        private string? _ImportPriceInput;
        public string? ImportPriceInput { get => _ImportPriceInput; set { _ImportPriceInput = value; OnPropertyChanged(); } }


        private string? _ExportPriceInput;
        public string? ExportPriceInput { get => _ExportPriceInput; set { _ExportPriceInput = value; OnPropertyChanged(); } }


        private DateTime? _ImportDateInput;
        public DateTime? ImportDateInput { get => _ImportDateInput; set { _ImportDateInput = value; OnPropertyChanged(); } }


        public ICommand AddCommand { get; set; }
        public ICommand EditCommand { get; set; }
        public ICommand DeleteCommand { get; set; }
        public ICommand SearchCommand { get; set; }
        public ICommand RefreshCommand { get; set; }
        public ICommand ImportDefaultCommand { get; set; }


        public StockInViewModel()
        {
            List = new ObservableCollection<ImportInfo>(importRepository.GetListImport());
            Supplier = new ObservableCollection<Supplier>(supplierRepository.GetListSupplier().Where(x => x.Status == "Enable"));
            Category = new ObservableCollection<Category>(categoryRepository.GetListCategory().Where(x => x.Status == "Enable"));
            Size = new ObservableCollection<BusinessObject.Models.Size>(sizeRepository.GetListSize().Where(x => x.Status == "Enable"));

            List<Product> listPorduct = productRepository.GetListProduct().Where(x => x.Status == "Enable").ToList();

            var ProductNameUnique = listPorduct.GroupBy(p => p.Name)
                                      .Select(g => g.First())
                                      .Where(x => x.Status == "Enable")
                                      .ToList();

            SelectedProductNameUnique = new ObservableCollection<Product>(ProductNameUnique);


            void ResetInfo()
            {
                ImportDefaultInfo = string.Empty;
                ImportIdInfo = string.Empty;
                SelectedProductInfo = string.Empty;
                SelectedSupplierInfo = string.Empty;
                SelectedCategoryInfo = string.Empty;
                SelectedSizeInfo = string.Empty;
                CountInfo = string.Empty;
                ImportPriceInfo = string.Empty;
                ExportPriceInfo = string.Empty;
                ImportDateInfo = string.Empty;
                StatusInfo = string.Empty;
            }

            void ResetInput()
            {
                SelectedProduct = null;
                SelectedSupplier = null;
                SelectedCategory = null;
                SelectedSize = null;
                CountInput = string.Empty;
                ImportPriceInput = string.Empty;
                ExportPriceInput = string.Empty;
                ImportDateInput = null;
            }

            RefreshCommand = new RelayCommand<object>((p) =>
            {
                return true;
            }, (p) =>
            {
                List<Product> listPorduct = productRepository.GetListProduct().Where(x => x.Status == "Enable").ToList();

                var ProductNameUnique = listPorduct.GroupBy(p => p.Name)
                                          .Select(g => g.First())
                                          .ToList();

                List = new ObservableCollection<ImportInfo>(importRepository.GetListImport());
                SelectedProductNameUnique = new ObservableCollection<Product>(ProductNameUnique);
                Supplier = new ObservableCollection<Supplier>(supplierRepository.GetListSupplier().Where(x => x.Status == "Enable"));
                Category = new ObservableCollection<Category>(categoryRepository.GetListCategory().Where(x => x.Status == "Enable"));
                Size = new ObservableCollection<BusinessObject.Models.Size>(sizeRepository.GetListSize().Where(x => x.Status == "Enable"));

                ResetInput();
                ResetInfo();
            });

            AddCommand = new RelayCommand<object>((p) =>
            {
                if (SelectedProduct == null ||
                SelectedSupplier == null ||
                SelectedCategory == null ||
                SelectedSize == null ||
                string.IsNullOrEmpty(CountInput?.Trim()) ||
                string.IsNullOrEmpty(ImportPriceInput?.Trim()) ||
                string.IsNullOrEmpty(ExportPriceInput?.Trim()) ||
                ImportDateInput == null)
                    return false;

                return true;

            }, (p) =>
            {
                var displayUniqueProduct = productRepository.GetUniqueProduct(SelectedProduct!.Name, SelectedSupplier!.SupplierId, SelectedCategory!.CategoryId, SelectedSize!.SizeId);

                if (displayUniqueProduct == null)
                {
                    MessageBox.Show($"Product does not exist!!!", "Notice", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                if (!int.TryParse(CountInput, out int boolExportAmountInput) || int.Parse(CountInput) <= 0)
                {
                    MessageBox.Show($"Quantity must be a positive number!!!", "Notice", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                if (!int.TryParse(ImportPriceInput, out int boolImportPriceInput) || int.Parse(ImportPriceInput) <= 0)
                {
                    MessageBox.Show($"Import price must be a positive number!!!", "Notice", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                if (!int.TryParse(ExportPriceInput, out int boolExportPriceInput) || int.Parse(ExportPriceInput) <= 0)
                {
                    MessageBox.Show($"Export price must be a positive number!!!", "Notice", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }


                ImportInfo import = new ImportInfo()
                {
                    ProductId = displayUniqueProduct.ProductId,
                    Count = int.Parse(CountInput!.Trim()),
                    ImportPrice = int.Parse(ImportPriceInput!.Trim()),
                    ExportPrice = int.Parse(ExportPriceInput!.Trim()),
                    Status = "Còn hàng",
                    DateImport = ImportDateInput,
                };

                importRepository.CreateImport(import);

                MessageBox.Show($"Create success!!!", "Notice", MessageBoxButton.OK, MessageBoxImage.Information);
                List = new ObservableCollection<ImportInfo>(importRepository.GetListImport());

                ResetInput();
                ResetInfo();
            });

            EditCommand = new RelayCommand<object>((p) =>
            {
                if (SelectedProduct == null &&
                    string.IsNullOrEmpty(CountInput?.Trim()) &&
                    string.IsNullOrEmpty(ImportPriceInput?.Trim()) &&
                    string.IsNullOrEmpty(ExportPriceInput?.Trim()) &&
                    ImportDateInput == null &&
                    SelectedSupplier == null &&
                    SelectedSize == null &&
                    SelectedCategory == null)
                {
                    return false;
                }

                return true;

            }, (p) =>
            {
                if (SelectedItem == null)
                {
                    MessageBox.Show($"Please select the import to update!!!", "Notice", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                var displayImportById = importRepository.GetImportById(SelectedItem?.ImportId);

                string productName = SelectedProduct != null ? SelectedProduct.Name : displayImportById!.Product.Name;
                int supplierId = SelectedSupplier != null ? SelectedSupplier.SupplierId : displayImportById!.Product.Supplier.SupplierId;
                int categoryId = SelectedCategory != null ? SelectedCategory.CategoryId : displayImportById!.Product.Category.CategoryId;
                int sizeId = SelectedSize != null ? SelectedSize.SizeId : displayImportById!.Product.Size.SizeId;

                var displayUniqueProduct = productRepository.GetUniqueProduct(productName, supplierId, categoryId, sizeId);

                if (displayUniqueProduct == null)
                {
                    MessageBox.Show($"Product does not exist!!!", "Notice", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                if (!string.IsNullOrEmpty(CountInput?.Trim()) && (!int.TryParse(CountInput, out int boolExportAmountInput) || int.Parse(CountInput) <= 0))
                {
                    MessageBox.Show($"Quantity must be a positive number!!!", "Notice", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                if (!string.IsNullOrEmpty(ImportPriceInput?.Trim()) && (!int.TryParse(ImportPriceInput, out int boolImportPriceInput) || int.Parse(ImportPriceInput) <= 0))
                {
                    MessageBox.Show($"Import price must be a positive number!!!", "Notice", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                if (!string.IsNullOrEmpty(ExportPriceInput?.Trim()) && (!int.TryParse(ExportPriceInput, out int boolExportPriceInput) || int.Parse(ExportPriceInput) <= 0))
                {
                    MessageBox.Show($"Export price must be a positive number!!!", "Notice", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                ImportInfo import = new ImportInfo()
                {
                    ImportId = SelectedItem!.ImportId,
                    ProductId = displayUniqueProduct != null ? displayUniqueProduct.ProductId : displayImportById!.Product.ProductId,
                    Count = !string.IsNullOrEmpty(CountInput?.Trim()) ? int.Parse(CountInput!.Trim()) : SelectedItem.Count,
                    ImportPrice = !string.IsNullOrEmpty(ImportPriceInput?.Trim()) ? int.Parse(ImportPriceInput!.Trim()) : SelectedItem.ImportPrice,
                    ExportPrice = !string.IsNullOrEmpty(ExportPriceInput?.Trim()) ? int.Parse(ExportPriceInput!.Trim()) : SelectedItem.ExportPrice,
                    Status = SelectedItem.Status,
                    DateImport = ImportDateInput != null ? ImportDateInput : SelectedItem.DateImport,
                };

                importRepository.UpdateImport(import);

                MessageBox.Show($"Update success!!!", "Notice", MessageBoxButton.OK, MessageBoxImage.Information);
                List = new ObservableCollection<ImportInfo>(importRepository.GetListImport());

                ResetInput();
                ResetInfo();
            });

            DeleteCommand = new RelayCommand<object>((p) =>
            {
                if (SelectedItem == null)
                    return false;

                return true;
            }, (p) =>
            {
                List<ExportInfo> listExport = new List<ExportInfo>();
                listExport = exportRepository.GetListExportByImportId(SelectedItem!.ImportId);

                if (listExport.Count > 0)
                {
                    foreach (ExportInfo item in listExport)
                    {
                        exportRepository.DeleteExport(item.ExportId);
                    }
                }

                Product product = new Product();

                product = productRepository.GetProductById(SelectedItem!.ProductId);

                if (ImportDefaultInfo != null && (product.ImportId == null || int.Parse(ImportDefaultInfo) == product.ImportId))
                {
                    product.ImportId = null;
                }

                productRepository.UpdateProduct(product);

                importRepository.DeleteImport(SelectedItem!.ImportId);

                MessageBox.Show($"Delete success!!!", "Notice", MessageBoxButton.OK, MessageBoxImage.Information);
                List = new ObservableCollection<ImportInfo>(importRepository.GetListImport());

                ResetInput();
                ResetInfo();
            });

            SearchCommand = new RelayCommand<object>((p) =>
            {
                if (SelectedItem != null) return true;

                if (SelectedSupplier != null &&
                 SelectedSize != null &&
                 SelectedCategory != null &&
                 SelectedProduct != null)
                {
                    return true;
                }

                return false;
            }, (p) =>
            {
                Product? displayUniqueProduct = null;

                if (SelectedSupplier != null &&
                 SelectedSize != null &&
                 SelectedCategory != null &&
                 SelectedProduct != null)
                {
                    displayUniqueProduct = productRepository.GetUniqueProduct(SelectedProduct.Name, SelectedSupplier.SupplierId, SelectedCategory.CategoryId, SelectedSize.SizeId);
                }              

                int productIdFiler = 0;

                if (displayUniqueProduct != null && SelectedItem != null ||
                        displayUniqueProduct != null && SelectedItem == null)
                {
                    productIdFiler = displayUniqueProduct.ProductId;
                }
                else if (displayUniqueProduct == null && SelectedItem != null)
                {
                    productIdFiler = SelectedItem.ProductId;
                }

                if (displayUniqueProduct == null && SelectedItem == null)
                {
                    MessageBox.Show($"Product does not exist!!!", "Notice", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                List = new ObservableCollection<ImportInfo>(importRepository.GetListImportByProductId(productIdFiler));

                ResetInput();
                ResetInfo();
            });

            ImportDefaultCommand = new RelayCommand<object>((p) =>
            {
                if (SelectedItem != null && SelectedItem.Product.ImportId == SelectedItem.ImportId)
                {
                    return false;
                }

                if (SelectedItem != null) return true;

                return false;
            }, (p) =>
            {
                Product product = new Product();

                product = productRepository.GetProductById(SelectedItem!.ProductId);

                product.ImportId = SelectedItem.ImportId;

                productRepository.UpdateProduct(product);

                MessageBox.Show($"Select the type of imported goods success!!!", "Notice", MessageBoxButton.OK, MessageBoxImage.Information);
                List = new ObservableCollection<ImportInfo>(importRepository.GetListImport());

                ResetInput();
                ResetInfo();
            });
        }
    }
}
