using BusinessObject.Models;
using DataAccess.Repository;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;

namespace InventoryManagement.ViewModel
{
    public class ImportDetailViewModel : BaseViewModel
    {
        public ImportDetailViewModel()
        {
            AddCommand = new RelayCommand<object>((p) => { return true; }, (p) => { });
            EditCommand = new RelayCommand<object>((p) => { return true; }, (p) => { });
            DeleteCommand = new RelayCommand<object>((p) => { return true; }, (p) => { });
            SetDefaultCommand = new RelayCommand<object>((p) => { return true; }, (p) => { });
        }

        public IProductRepository productRepository = new ProductRepository();
        public IExportRepository exportRepository = new ExportRepository();
        public IImportRepository importRepository = new ImportRepository();
        public ICustomerRepository customerRepository = new CustomerRepository();


        private ObservableCollection<ExportInfo>? _ListExportDetail;
        public ObservableCollection<ExportInfo>? ListExportDetail { get => _ListExportDetail; set { _ListExportDetail = value; OnPropertyChanged(); } }


        private ObservableCollection<ImportInfo>? _ListImportDetail;
        public ObservableCollection<ImportInfo>? ListImportDetail { get => _ListImportDetail; set { _ListImportDetail = value; OnPropertyChanged(); } }


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
                    ListExportDetail = new ObservableCollection<ExportInfo>(exportRepository.GetListExportByImportId(SelectedItem?.ImportId));

                    ImportDateInfo = SelectedItem?.DateImport?.ToString().Split(" ")[0];
                    StatusInfo = SelectedItem?.Status.ToString();

                    ImportIdInfo = SelectedItem?.ImportId.ToString();
                    ImportAmountInfo = SelectedItem?.Count.ToString();
                    InStockInfo = (SelectedItem?.Count - (int)ListExportDetail?.Sum(x => x.Count)!).ToString();
                    ImportPriceInfo = SelectedItem?.ImportPrice.ToString();
                    ExportPriceInfo = SelectedItem?.ExportPrice.ToString();
                }
            }
        }


        private int _ProductId;
        public int ProductId
        {
            get => _ProductId;
            set
            {
                _ProductId = value;
                OnPropertyChanged();
            }

        }

        private Product? _GetProduct;
        public Product? GetProduct
        {
            get => _GetProduct;
            set
            {
                _GetProduct = value;
                OnPropertyChanged();
            }

        }

        //Information

        private string? _ImportDefaultInfo;
        public string? ImportDefaultInfo { get => _ImportDefaultInfo; set { _ImportDefaultInfo = value; OnPropertyChanged(); } }

        private string? _ProductNameInfo;
        public string? ProductNameInfo { get => _ProductNameInfo; set { _ProductNameInfo = value; OnPropertyChanged(); } }

        private string? _ProductSupplierInfo;
        public string? ProductSupplierInfo { get => _ProductSupplierInfo; set { _ProductSupplierInfo = value; OnPropertyChanged(); } }

        private string? _ProductCateInfo;
        public string? ProductCateInfo { get => _ProductCateInfo; set { _ProductCateInfo = value; OnPropertyChanged(); } }

        private string? _ProductSizeInfo;
        public string? ProductSizeInfo { get => _ProductSizeInfo; set { _ProductSizeInfo = value; OnPropertyChanged(); } }


        private string? _ImportIdInfo;
        public string? ImportIdInfo { get => _ImportIdInfo; set { _ImportIdInfo = value; OnPropertyChanged(); } }


        private string? _ImportAmountInfo;
        public string? ImportAmountInfo { get => _ImportAmountInfo; set { _ImportAmountInfo = value; OnPropertyChanged(); } }


        private string? _InStockInfo;
        public string? InStockInfo { get => _InStockInfo; set { _InStockInfo = value; OnPropertyChanged(); } }


        private string? _ImportPriceInfo;
        public string? ImportPriceInfo { get => _ImportPriceInfo; set { _ImportPriceInfo = value; OnPropertyChanged(); } }


        private string? _ExportPriceInfo;
        public string? ExportPriceInfo { get => _ExportPriceInfo; set { _ExportPriceInfo = value; OnPropertyChanged(); } }


        private string? _ImportDateInfo;
        public string? ImportDateInfo { get => _ImportDateInfo; set { _ImportDateInfo = value; OnPropertyChanged(); } }


        private string? _StatusInfo;
        public string? StatusInfo { get => _StatusInfo; set { _StatusInfo = value; OnPropertyChanged(); } }


        // Edit form


        private string? _ImportAmountInput;
        public string? ImportAmountInput { get => _ImportAmountInput; set { _ImportAmountInput = value; OnPropertyChanged(); } }


        private string? _ImportPriceInput;
        public string? ImportPriceInput { get => _ImportPriceInput; set { _ImportPriceInput = value; OnPropertyChanged(); } }


        private string? _ExportPriceInput;
        public string? ExportPriceInput { get => _ExportPriceInput; set { _ExportPriceInput = value; OnPropertyChanged(); } }


        private DateTime? _ImportDateInput;
        public DateTime? ImportDateInput { get => _ImportDateInput; set { _ImportDateInput = value; OnPropertyChanged(); } }


        public ICommand AddCommand { get; set; }
        public ICommand EditCommand { get; set; }
        public ICommand DeleteCommand { get; set; }
        public ICommand SetDefaultCommand { get; set; }


        public ImportDetailViewModel(int productId)
        {
            ImportInfo? GetImport = new ImportInfo();

            ProductId = productId;
            GetProduct = productRepository.GetProductById(productId);
            GetImport = importRepository.GetImportById(GetProduct.ImportId);
            ListImportDetail = new ObservableCollection<ImportInfo>(importRepository.GetListImportByProductId(GetProduct?.ProductId));
            ListExportDetail = new ObservableCollection<ExportInfo>(exportRepository.GetListExportByImportId(GetProduct?.ImportId));


            if (GetImport != null && GetImport?.Count - (int)ListExportDetail?.Sum(x => x.Count)! == 0)
            {
                GetImport!.Status = "Hết hàng";
                importRepository.UpdateImport(GetImport);
                ListImportDetail = new ObservableCollection<ImportInfo>(importRepository.GetListImportByProductId(GetProduct?.ProductId));
            } else if (GetImport != null && GetImport?.Count - (int)ListExportDetail?.Sum(x => x.Count)! > 0)
            {
                GetImport!.Status = "Còn hàng";
                importRepository.UpdateImport(GetImport);
                ListImportDetail = new ObservableCollection<ImportInfo>(importRepository.GetListImportByProductId(GetProduct?.ProductId));
            }

            ImportDefaultInfo = GetProduct?.ImportId.ToString();
            ProductNameInfo = GetProduct?.Name;
            ProductSupplierInfo = GetProduct?.Supplier.Name;
            ProductCateInfo = GetProduct?.Category.Name;
            ProductSizeInfo = GetProduct?.Size.Name;

            void ResetInfo()
            {
                ImportDateInfo = string.Empty;
                StatusInfo = string.Empty;

                ImportIdInfo = string.Empty;
                ImportAmountInfo = string.Empty;
                InStockInfo = string.Empty;
                ImportPriceInfo = string.Empty;
                ExportPriceInfo = string.Empty;
            }

            void ResetInput()
            {
                ImportAmountInput = string.Empty;
                ImportPriceInput = string.Empty;
                ExportPriceInput = string.Empty;
                ImportDateInput = null;
            }

            AddCommand = new RelayCommand<object>((p) =>
            {
                if (string.IsNullOrEmpty(ImportAmountInput?.Trim()) ||
                    string.IsNullOrEmpty(ImportPriceInput?.Trim()) ||
                    string.IsNullOrEmpty(ExportPriceInput?.Trim()) ||
                    ImportDateInput == null)
                {
                    return false;
                }

                return true;

            }, (p) =>
            {
                if (!int.TryParse(ImportAmountInput, out int boolExportAmountInput) || int.Parse(ImportAmountInput) <= 0)
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

                ImportInfo importInfo = new ImportInfo()
                {
                    ProductId = GetProduct!.ProductId,
                    Count = int.Parse(ImportAmountInput.Trim()),
                    ImportPrice = int.Parse(ImportPriceInput.Trim()),
                    ExportPrice = int.Parse(ExportPriceInput.Trim()),
                    DateImport = ImportDateInput,
                    Status = int.Parse(ImportAmountInput.Trim()) > 0 ? "Còn hàng" : "Hết hàng",
                };

                importRepository.CreateImport(importInfo);

                MessageBox.Show($"Create success!!!", "Notice", MessageBoxButton.OK, MessageBoxImage.Information);
                ListImportDetail = new ObservableCollection<ImportInfo>(importRepository.GetListImportByProductId(GetProduct?.ProductId));

                ResetInput();
                ResetInfo();
            });

            EditCommand = new RelayCommand<object>((p) =>
            {
                if (string.IsNullOrEmpty(ImportAmountInput?.Trim()) &&
                    string.IsNullOrEmpty(ImportPriceInput?.Trim()) &&
                    string.IsNullOrEmpty(ExportPriceInput?.Trim()) &&
                    ImportDateInput == null)
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

                if (!string.IsNullOrEmpty(ImportAmountInput?.Trim()) && (!int.TryParse(ImportAmountInput, out int boolExportAmountInput) || int.Parse(ImportAmountInput) <= 0))
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

                int amount = !string.IsNullOrEmpty(ImportAmountInput?.Trim()) ? int.Parse(ImportAmountInput.Trim()) : SelectedItem.Count;

                ImportInfo importInfo = new ImportInfo()
                {
                    ImportId = SelectedItem.ImportId,
                    ProductId = SelectedItem.ProductId,
                    Count = !string.IsNullOrEmpty(ImportAmountInput?.Trim()) ? int.Parse(ImportAmountInput.Trim()) : SelectedItem.Count,
                    ImportPrice = !string.IsNullOrEmpty(ImportPriceInput?.Trim()) ? int.Parse(ImportPriceInput.Trim()) : SelectedItem.ImportPrice,
                    ExportPrice = !string.IsNullOrEmpty(ExportPriceInput?.Trim()) ? int.Parse(ExportPriceInput.Trim()) : SelectedItem.ExportPrice,
                    Status = amount > 0 ? "Còn hàng" : "Hết hàng",
                    DateImport = ImportDateInput != null ? ImportDateInput : SelectedItem.DateImport,
                };

                importRepository.UpdateImport(importInfo);

                MessageBox.Show($"Update success!!!", "Notice", MessageBoxButton.OK, MessageBoxImage.Information);
                ListImportDetail = new ObservableCollection<ImportInfo>(importRepository.GetListImportByProductId(GetProduct?.ProductId));

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

                if (ImportDefaultInfo != null && int.Parse(ImportDefaultInfo) == product.ImportId)
                {
                    product.ImportId = null;
                }

                productRepository.UpdateProduct(product);

                importRepository.DeleteImport(SelectedItem!.ImportId);

                MessageBox.Show($"Delete success!!!", "Notice", MessageBoxButton.OK, MessageBoxImage.Information);
                ImportDefaultInfo = product.ImportId.ToString();
                ListImportDetail = new ObservableCollection<ImportInfo>(importRepository.GetListImportByProductId(GetProduct?.ProductId));

                ResetInput();
                ResetInfo();
            });

            SetDefaultCommand = new RelayCommand<object>((p) =>
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
                ImportDefaultInfo = product.ImportId.ToString();
                ListImportDetail = new ObservableCollection<ImportInfo>(importRepository.GetListImportByProductId(GetProduct?.ProductId));

                ResetInput();
                ResetInfo();
            });
        }
    }
}
