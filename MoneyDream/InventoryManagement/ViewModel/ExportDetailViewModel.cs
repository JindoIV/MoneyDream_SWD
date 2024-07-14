using BusinessObject.Models;
using DataAccess.Repository;
using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;

namespace InventoryManagement.ViewModel
{
    public class ExportDetailViewModel : BaseViewModel
    {
        public ExportDetailViewModel() 
        {
            AddCommand = new RelayCommand<object>((p) => { return true; }, (p) => { });
            EditCommand = new RelayCommand<object>((p) => { return true; }, (p) => { });
            DeleteCommand = new RelayCommand<object>((p) => { return true; }, (p) => { });
        }

        public IProductRepository productRepository = new ProductRepository();
        public IExportRepository exportRepository = new ExportRepository();
        public IImportRepository importRepository = new ImportRepository();
        public ICustomerRepository customerRepository = new CustomerRepository();


        private ObservableCollection<ExportInfo>? _ListExportDetail;
        public ObservableCollection<ExportInfo>? ListExportDetail { get => _ListExportDetail; set { _ListExportDetail = value; OnPropertyChanged(); } }


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
                    ExportIdInfo = SelectedItem.ExportId.ToString();
                    ExportAmountInfo = SelectedItem.Count.ToString();
                    AccountIdInfo = SelectedItem.AccountId.ToString();
                    AccountUsernameInfo = SelectedItem.Account.UserName.ToString();
                    ExportDateInfo = SelectedItem.DateExport?.ToString().Split(" ")[0];
                    StatusInfo = SelectedItem.Status.ToString();
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

        // Information

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


        private string? _ProductPriceInfo;
        public string? ProductPriceInfo { get => _ProductPriceInfo; set { _ProductPriceInfo = value; OnPropertyChanged(); } }

        private string? _ExportIdInfo;
        public string? ExportIdInfo { get => _ExportIdInfo; set { _ExportIdInfo = value; OnPropertyChanged(); } }


        private string? _ExportAmountInfo;
        public string? ExportAmountInfo { get => _ExportAmountInfo; set { _ExportAmountInfo = value; OnPropertyChanged(); } }


        private string? _AccountIdInfo;
        public string? AccountIdInfo { get => _AccountIdInfo; set { _AccountIdInfo = value; OnPropertyChanged(); } }


        private string? _AccountUsernameInfo;
        public string? AccountUsernameInfo { get => _AccountUsernameInfo; set { _AccountUsernameInfo = value; OnPropertyChanged(); } }


        private string? _ExportDateInfo;
        public string? ExportDateInfo { get => _ExportDateInfo; set { _ExportDateInfo = value; OnPropertyChanged(); } }


        private string? _StatusInfo;
        public string? StatusInfo { get => _StatusInfo; set { _StatusInfo = value; OnPropertyChanged(); } }


        // Edit form


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

        public ExportDetailViewModel(int productId)
        {
            ImportInfo? GetImport = new ImportInfo();

            ProductId = productId;
            GetProduct = productRepository.GetProductById(productId);
            GetImport = importRepository.GetImportById(GetProduct.ImportId);
            ListExportDetail = new ObservableCollection<ExportInfo>(exportRepository.GetListExportByImportId(GetProduct?.ImportId));

            ProductNameInfo = GetProduct?.Name;
            ProductSupplierInfo = GetProduct?.Supplier.Name;
            ProductCateInfo = GetProduct?.Category.Name;
            ProductSizeInfo = GetProduct?.Size.Name;

            ImportIdInfo = GetImport?.ImportId.ToString();
            ImportAmountInfo = GetImport?.Count.ToString();
            InStockInfo = (GetImport?.Count - (int)ListExportDetail?.Sum(x => x.Count)!).ToString();
            ProductPriceInfo = GetImport?.ExportPrice.ToString();

            void ResetInfo()
            {
                ExportIdInfo = string.Empty;
                ExportAmountInfo = string.Empty;
                AccountIdInfo = string.Empty;
                AccountUsernameInfo = string.Empty;
                ExportDateInfo = string.Empty;
                StatusInfo = string.Empty;
            }

            void ResetInput()
            {
                ExportAmountInput = string.Empty;
                UsernameInput = string.Empty;
                ExportDateInput = null;
                StatusInput = null;
            }

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

                if (!int.TryParse(ExportAmountInput, out int boolExportAmountInput) || int.Parse(ExportAmountInput) <= 0)
                {
                    MessageBox.Show($"Quantity must be a positive number!!!", "Notice", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                if (int.Parse(ExportAmountInput) > int.Parse(InStockInfo!))
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
                    ProductId = GetProduct!.ProductId,
                    ImportId = GetImport!.ImportId,
                    AccountId = account.AccountId,
                    Count = int.Parse(ExportAmountInput.Trim()),
                    DateExport = ExportDateInput,
                    Status = StatusInput!.ToString().Split(": ")[1],
                };

                exportRepository.CreateExport(exportInfo);

                MessageBox.Show($"Create success!!!", "Notice", MessageBoxButton.OK, MessageBoxImage.Information);
                ListExportDetail = new ObservableCollection<ExportInfo>(exportRepository.GetListExportByImportId(GetProduct?.ImportId));

                InStockInfo = (GetImport?.Count - (int)ListExportDetail?.Sum(x => x.Count)!).ToString();

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

                if (!string.IsNullOrEmpty(InStockInfo?.Trim()) && (int.Parse(ExportAmountInput) > int.Parse(InStockInfo!)))
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
                    ProductId = GetProduct!.ProductId,
                    ImportId = GetImport!.ImportId,
                    AccountId = account != null ? account.AccountId : SelectedItem.AccountId,
                    Count = !string.IsNullOrEmpty(ExportAmountInput?.Trim()) ? int.Parse(ExportAmountInput.Trim()) : SelectedItem.Count,
                    DateExport = ExportDateInput != null ? ExportDateInput : SelectedItem.DateExport,
                    Status = !string.IsNullOrEmpty(StatusInput?.Trim()) ? StatusInput.ToString().Split(": ")[1] : StatusInfo!,
                };

                exportRepository.UpdateExport(exportInfo);

                MessageBox.Show($"Update success!!!", "Notice", MessageBoxButton.OK, MessageBoxImage.Information);
                ListExportDetail = new ObservableCollection<ExportInfo>(exportRepository.GetListExportByImportId(GetProduct?.ImportId));

                InStockInfo = (GetImport?.Count - (int)ListExportDetail?.Sum(x => x.Count)!).ToString();

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
                ListExportDetail = new ObservableCollection<ExportInfo>(exportRepository.GetListExportByImportId(GetProduct?.ImportId));

                InStockInfo = (GetImport?.Count - (int)ListExportDetail?.Sum(x => x.Count)!).ToString();

                ResetInput();
                ResetInfo();
            });
        }
    }
}
