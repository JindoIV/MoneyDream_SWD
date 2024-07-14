using BusinessObject.Models;
using DataAccess.Repository;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows;

namespace InventoryManagement.ViewModel
{
    public class StockOutViewModel : BaseViewModel
    {
        public IProductRepository productRepository = new ProductRepository();
        public IImportRepository importRepository = new ImportRepository();
        public ISupplierRepository supplierRepository = new SupplierRepository();
        public ISizeRepository sizeRepository = new SizeRepository();
        public ICategoryRepository categoryRepository = new CategoryRepository();
        public IExportRepository exportRepository = new ExportRepository();
        public ICustomerRepository customerRepository = new CustomerRepository();


        private ObservableCollection<ExportInfo>? _List;
        public ObservableCollection<ExportInfo>? List { get => _List; set { _List = value; OnPropertyChanged(); } }

        private ObservableCollection<Product>? _Product;
        public ObservableCollection<Product>? Product { get => _Product; set { _Product = value; OnPropertyChanged(); } }

        private ObservableCollection<BusinessObject.Models.Size>? _Size;
        public ObservableCollection<BusinessObject.Models.Size>? Size { get => _Size; set { _Size = value; OnPropertyChanged(); } }

        private ObservableCollection<Category>? _Category;
        public ObservableCollection<Category>? Category { get => _Category; set { _Category = value; OnPropertyChanged(); } }

        private ObservableCollection<Supplier>? _Supplier;
        public ObservableCollection<Supplier>? Supplier { get => _Supplier; set { _Supplier = value; OnPropertyChanged(); } }

        private ExportInfo? _SelectedItem = null;
        public ExportInfo? SelectedItem
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

                    getImport = importRepository.GetImportById(SelectedItem.Product.ImportId);
                    ListExportDetail = exportRepository.GetListExportByImportId(SelectedItem.Product.ImportId);

                    ImportDefaultInfo = SelectedItem.Product.ImportId.ToString();
                    SelectedProductInfo = SelectedItem.Product.Name;
                    SelectedSupplierInfo = SelectedItem.Product.Supplier.Name;
                    SelectedCategoryInfo = SelectedItem.Product.Category.Name;
                    SelectedSizeInfo = SelectedItem.Product.Size.Name;
                    ImportAmountInfo = getImport?.Count.ToString();
                    InStockInfo = (getImport?.Count - (int)ListExportDetail?.Sum(x => x.Count)!).ToString();
                    ExportIdInfo = SelectedItem.ExportId.ToString();
                    ExportAmountInfo = SelectedItem.Count.ToString();
                    ImportPriceInfo = getImport?.ImportPrice.ToString();
                    ExportPriceInfo = getImport?.ExportPrice.ToString();
                    UsernameInfo = SelectedItem.Account.UserName;
                    ExportDateInfo = SelectedItem?.DateExport?.ToString().Split(" ")[0];
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


        private string? _ImportAmountInfo;
        public string? ImportAmountInfo { get => _ImportAmountInfo; set { _ImportAmountInfo = value; OnPropertyChanged(); } }


        private string? _InStockInfo;
        public string? InStockInfo { get => _InStockInfo; set { _InStockInfo = value; OnPropertyChanged(); } }


        private string? _ExportIdInfo;
        public string? ExportIdInfo { get => _ExportIdInfo; set { _ExportIdInfo = value; OnPropertyChanged(); } }


        private string? _ExportAmountInfo;
        public string? ExportAmountInfo { get => _ExportAmountInfo; set { _ExportAmountInfo = value; OnPropertyChanged(); } }


        private string? _ImportPriceInfo;
        public string? ImportPriceInfo { get => _ImportPriceInfo; set { _ImportPriceInfo = value; OnPropertyChanged(); } }


        private string? _ExportPriceInfo;
        public string? ExportPriceInfo { get => _ExportPriceInfo; set { _ExportPriceInfo = value; OnPropertyChanged(); } }


        private string? _UsernameInfo;
        public string? UsernameInfo { get => _UsernameInfo; set { _UsernameInfo = value; OnPropertyChanged(); } }


        private string? _ExportDateInfo;
        public string? ExportDateInfo { get => _ExportDateInfo; set { _ExportDateInfo = value; OnPropertyChanged(); } }


        private string? _StatusInfo;
        public string? StatusInfo { get => _StatusInfo; set { _StatusInfo = value; OnPropertyChanged(); } }


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

        private string? _ExportAmountInput;
        public string? ExportAmountInput { get => _ExportAmountInput; set { _ExportAmountInput = value; OnPropertyChanged(); } }


        private string? _UsernameInput;
        public string? UsernameInput { get => _UsernameInput; set { _UsernameInput = value; OnPropertyChanged(); } }


        private DateTime? _ExportDateInput;
        public DateTime? ExportDateInput { get => _ExportDateInput; set { _ExportDateInput = value; OnPropertyChanged(); } }


        private string? _StatusInput;
        public string? StatusInput { get => _StatusInput; set { _StatusInput = value; OnPropertyChanged(); } }


        public ICommand AddCommand { get; set; }
        public ICommand EditCommand { get; set; }
        public ICommand DeleteCommand { get; set; }
        public ICommand SearchCommand { get; set; }
        public ICommand RefreshCommand { get; set; }


        public StockOutViewModel()
        {
            List = new ObservableCollection<ExportInfo>(exportRepository.GetListExport());
            Supplier = new ObservableCollection<Supplier>(supplierRepository.GetListSupplier().Where(x => x.Status == "Enable"));
            Category = new ObservableCollection<Category>(categoryRepository.GetListCategory().Where(x => x.Status == "Enable"));
            Size = new ObservableCollection<BusinessObject.Models.Size>(sizeRepository.GetListSize().Where(x => x.Status == "Enable"));

            List<Product> listPorduct = productRepository.GetListProduct().Where(x => x.Status == "Enable").ToList();

            var ProductNameUnique = listPorduct.GroupBy(p => p.Name)
                                      .Select(g => g.First())
                                      .ToList();

            SelectedProductNameUnique = new ObservableCollection<Product>(ProductNameUnique);


            void ResetInfo()
            {
                ImportDefaultInfo = string.Empty;
                SelectedProductInfo = null;
                SelectedSupplierInfo = null;
                SelectedCategoryInfo = null;
                SelectedSizeInfo = null;
                ImportAmountInfo = string.Empty;
                InStockInfo = string.Empty;
                ExportIdInfo = string.Empty;
                ExportAmountInfo = string.Empty;
                ImportPriceInfo = string.Empty;
                ExportPriceInfo = string.Empty;
                UsernameInfo = string.Empty;
                ExportDateInfo = string.Empty;
                StatusInfo = string.Empty;
            }

            void ResetInput()
            {
                SelectedProduct = null;
                SelectedSupplier = null;
                SelectedCategory = null;
                SelectedSize = null;
                ExportAmountInput = string.Empty;
                UsernameInput = string.Empty;
                StatusInput = null;
                ExportDateInput = null;
            }

            RefreshCommand = new RelayCommand<object>((p) =>
            {
                return true;
            }, (p) =>
            {
                List<Product> listPorduct = productRepository.GetListProduct();

                var ProductNameUnique = listPorduct.GroupBy(p => p.Name)
                                          .Select(g => g.First())
                                          .ToList();

                SelectedProductNameUnique = new ObservableCollection<Product>(ProductNameUnique.Where(x => x.Status == "Enable"));
                List = new ObservableCollection<ExportInfo>(exportRepository.GetListExport());
                Supplier = new ObservableCollection<Supplier>(supplierRepository.GetListSupplier().Where(x => x.Status == "Enable"));
                Category = new ObservableCollection<Category>(categoryRepository.GetListCategory().Where(x => x.Status == "Enable"));
                Size = new ObservableCollection<BusinessObject.Models.Size>(sizeRepository.GetListSize().Where(x => x.Status == "Enable"));

                ResetInput();
                ResetInfo();
            });

            AddCommand = new RelayCommand<object>((p) =>
            {
                if (string.IsNullOrEmpty(ExportAmountInput?.Trim()) ||
                    string.IsNullOrEmpty(UsernameInput?.Trim()) ||
                    string.IsNullOrEmpty(StatusInput?.Trim()) ||
                    ExportDateInput == null)
                {
                    return false;
                }

                return true;

            }, (p) =>
            {
                Account? account = new Account();
                account = customerRepository.GetCustomerByUsername(UsernameInput);

                var displayUniqueProduct = productRepository.GetUniqueProduct(SelectedProduct!.Name, SelectedSupplier!.SupplierId, SelectedCategory!.CategoryId, SelectedSize!.SizeId);

                if (displayUniqueProduct == null)
                {
                    MessageBox.Show($"Product does not exist!!!", "Notice", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                if (!int.TryParse(ExportAmountInput, out int boolExportAmountInput) || int.Parse(ExportAmountInput) <= 0)
                {
                    MessageBox.Show($"Quantity must be a positive number!!!", "Notice", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                ImportInfo? getImport = new ImportInfo();
                List<ExportInfo>? ListExportDetail = new List<ExportInfo>();

                getImport = importRepository.GetImportById(displayUniqueProduct.ImportId);
                ListExportDetail = exportRepository.GetListExportByImportId(displayUniqueProduct.ImportId);

                int instock = getImport!.Count - (int)ListExportDetail.Sum(x => x.Count);

                if (int.Parse(ExportAmountInput) > instock)
                {
                    MessageBox.Show($"Insufficient stock quantity to proceed!!!", "Notice", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                if (account == null)
                {
                    MessageBox.Show($"Username does not exist!!!", "Notice", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                ExportInfo exportInfo = new ExportInfo()
                {
                    ProductId = displayUniqueProduct.ProductId,
                    ImportId = (int)displayUniqueProduct.ImportId!,
                    AccountId = account.AccountId,
                    Count = int.Parse(ExportAmountInput.Trim()),
                    DateExport = ExportDateInput,
                    Status = StatusInput!.ToString().Split(": ")[1],
                };

                exportRepository.CreateExport(exportInfo);

                MessageBox.Show($"Create success!!!", "Notice", MessageBoxButton.OK, MessageBoxImage.Information);
                List = new ObservableCollection<ExportInfo>(exportRepository.GetListExport());

                ResetInput();
                ResetInfo();
            });

            EditCommand = new RelayCommand<object>((p) =>
            {
                if (string.IsNullOrEmpty(ExportAmountInput?.Trim()) &&
                    string.IsNullOrEmpty(UsernameInput?.Trim()) &&
                    string.IsNullOrEmpty(StatusInput?.Trim()) &&
                    ExportDateInput == null)
                {
                    return false;
                }

                return true;

            }, (p) =>
            {
                Account? account = new Account();
                account = customerRepository.GetCustomerByUsername(UsernameInput);

                var displayUniqueProduct = productRepository.GetUniqueProduct(SelectedProduct!.Name, SelectedSupplier!.SupplierId, SelectedCategory!.CategoryId, SelectedSize!.SizeId);

                if (displayUniqueProduct == null)
                {
                    MessageBox.Show($"Product does not exist!!!", "Notice", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                if (SelectedItem == null)
                {
                    MessageBox.Show($"Please select the export to update!!!", "Notice", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                if (!string.IsNullOrEmpty(ExportAmountInput?.Trim()) && (!int.TryParse(ExportAmountInput, out int boolExportAmountInput) || int.Parse(ExportAmountInput) <= 0))
                {
                    MessageBox.Show($"Quantity must be a positive number!!!", "Notice", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                ImportInfo? getImport = new ImportInfo();
                List<ExportInfo>? ListExportDetail = new List<ExportInfo>();

                getImport = importRepository.GetImportById(displayUniqueProduct.ImportId);
                ListExportDetail = exportRepository.GetListExportByImportId(displayUniqueProduct.ImportId);

                int instock = getImport!.Count - (int)ListExportDetail.Sum(x => x.Count);

                if (!string.IsNullOrEmpty(InStockInfo?.Trim()) && (int.Parse(ExportAmountInput!) > instock))
                {
                    MessageBox.Show($"Insufficient stock quantity to proceed!!!", "Notice", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                if (!string.IsNullOrEmpty(UsernameInput?.Trim()) && account == null)
                {
                    MessageBox.Show($"Username does not exist!!!", "Notice", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                ExportInfo exportInfo = new ExportInfo()
                {
                    ExportId = SelectedItem.ExportId,
                    ProductId = displayUniqueProduct.ProductId,
                    ImportId = SelectedItem!.ImportId,
                    AccountId = account != null ? account.AccountId : SelectedItem.AccountId,
                    Count = !string.IsNullOrEmpty(ExportAmountInput?.Trim()) ? int.Parse(ExportAmountInput.Trim()) : SelectedItem.Count,
                    DateExport = ExportDateInput != null ? ExportDateInput : SelectedItem.DateExport,
                    Status = !string.IsNullOrEmpty(StatusInput?.Trim()) ? StatusInput.ToString().Split(": ")[1] : StatusInfo!,
                };

                exportRepository.UpdateExport(exportInfo);

                MessageBox.Show($"Update success!!!", "Notice", MessageBoxButton.OK, MessageBoxImage.Information);
                List = new ObservableCollection<ExportInfo>(exportRepository.GetListExport());

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
                exportRepository.DeleteExport(SelectedItem!.ExportId);

                MessageBox.Show($"Delete success!!!", "Notice", MessageBoxButton.OK, MessageBoxImage.Information);
                List = new ObservableCollection<ExportInfo>(exportRepository.GetListExport());

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

                Product product = new Product();

                product = productRepository.GetProductById(productIdFiler);
                List = new ObservableCollection<ExportInfo>(exportRepository.GetListExportByImportId(product.ImportId));

                ResetInput();
                ResetInfo();
            });        
        }
    }
}
