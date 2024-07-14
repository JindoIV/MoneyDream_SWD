using DataAccess.Repository;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using BusinessObject.Models;
using System.Windows.Documents;

namespace InventoryManagement.ViewModel
{
    public class MainViewModel : BaseViewModel
    {
        private ObservableCollection<InStock>? _inStockList;
        public ObservableCollection<InStock>? inStockList { get => _inStockList; set { _inStockList = value; OnPropertyChanged(); } }

        private ObservableCollection<BusinessObject.Models.Size>? _Size;
        public ObservableCollection<BusinessObject.Models.Size>? Size { get => _Size; set { _Size = value; OnPropertyChanged(); } }

        private ObservableCollection<Category>? _Category;
        public ObservableCollection<Category>? Category { get => _Category; set { _Category = value; OnPropertyChanged(); } }

        private ObservableCollection<Supplier>? _Supplier;
        public ObservableCollection<Supplier>? Supplier { get => _Supplier; set { _Supplier = value; OnPropertyChanged(); } }

        public IProductRepository productRepository = new ProductRepository();
        public IImportRepository importRepository = new ImportRepository();
        public IExportRepository exportRepository = new ExportRepository();
        public ISupplierRepository supplierRepository = new SupplierRepository();
        public ICategoryRepository categoryRepository = new CategoryRepository();
        public ISizeRepository sizeRepository = new SizeRepository();

        private InStock? _SelectedItem = null;
        public InStock? SelectedItem
        {
            get => _SelectedItem;
            set
            {
                _SelectedItem = value;
                OnPropertyChanged();

                if (SelectedItem != null)
                {
                    SelectedNoInfo = SelectedItem.Id.ToString();
                    SelectedProductInfo = SelectedItem?.Product?.Name;
                    SelectedSupplierInfo = SelectedItem?.Product?.Supplier?.Name;
                    SelectedCategoryInfo = SelectedItem?.Product?.Category?.Name;
                    SelectedSizeInfo = SelectedItem?.Product?.Size?.Name;
                    SumImportListInfo = SelectedItem?.SumImportList.ToString();
                    SumExportListInfo = SelectedItem?.SumExportList.ToString();
                    SumCountInfo = SelectedItem?.Count.ToString();

                    if (SelectedItem?.Product?.ImportId == null)
                    {
                        MessageBox.Show($"Select the type of imported goods for sale!!!", "Notice", MessageBoxButton.OK, MessageBoxImage.Warning);
                    }
                }
            }
        }

        //Info product

        private string? _SelectedNoInfo;
        public string? SelectedNoInfo
        {
            get => _SelectedNoInfo;
            set
            {
                _SelectedNoInfo = value;
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

        // Display stock

        private string? _SumImportListInfo = "0";
        public string? SumImportListInfo
        {
            get => _SumImportListInfo;
            set
            {
                _SumImportListInfo = value;
                OnPropertyChanged();
            }

        }

        private string? _SumExportListInfo = "0";
        public string? SumExportListInfo
        {
            get => _SumExportListInfo;
            set
            {
                _SumExportListInfo = value;
                OnPropertyChanged();
            }

        }

        private string? _SumCountInfo = "0";
        public string? SumCountInfo
        {
            get => _SumCountInfo;
            set
            {
                _SumCountInfo = value;
                OnPropertyChanged();
            }

        }

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

        public bool Isloaded = false;
        public ICommand LoadedWindowCommand { get; set; }
        public ICommand UnitCommand { get; set; }
        public ICommand SuppierCommand { get; set; }
        public ICommand CustomerCommand { get; set; }
        public ICommand ProductCommand { get; set; }
        public ICommand SizeCommand { get; set; }
        public ICommand ImportCommand { get; set; }
        public ICommand ExportCommand { get; set; }
        public ICommand CategoryCommand { get; set; }

        public ICommand ExportDetailCommand { get; set; }
        public ICommand ImportDetailCommand { get; set; }
        public ICommand RefreshCommand { get; set; }

        public ICommand SearchCommand { get; set; }

        public MainViewModel()
        {
            Supplier = new ObservableCollection<Supplier>(supplierRepository.GetListSupplier().Where(x => x.Status == "Enable"));
            Category = new ObservableCollection<Category>(categoryRepository.GetListCategory().Where(x => x.Status == "Enable"));
            Size = new ObservableCollection<BusinessObject.Models.Size>(sizeRepository.GetListSize().Where(x => x.Status == "Enable"));

            List<Product> listPorduct = productRepository.GetListProduct().Where(x => x.Status == "Enable").ToList();

            var ProductNameUnique = listPorduct.GroupBy(p => p.Name)
                                      .Select(g => g.First())
                                      .Where(x => x.Status == "Enable")
                                      .ToList();

            SelectedProductNameUnique = new ObservableCollection<Product>(ProductNameUnique);

            LoadedWindowCommand = new RelayCommand<Window>((p) => { return true; }, (p) =>
            {
                Isloaded = true;
                if (p == null)
                    return;
                p.Hide();
                LoginWindow loginWindow = new LoginWindow();
                loginWindow.ShowDialog();

                if (loginWindow.DataContext == null)
                    return;
                var loginVM = loginWindow.DataContext as LoginViewModel;

                if (loginVM!.IsLogin)
                {
                    p.Show();
                    LoadInStockData();
                }
                else
                {
                    p.Close();
                }
            });

            UnitCommand = new RelayCommand<object>((p) => { return true; }, (p) => { UnitWindow wd = new UnitWindow(); wd.ShowDialog(); });
            SuppierCommand = new RelayCommand<object>((p) => { return true; }, (p) => { SupplierWindow wd = new SupplierWindow(); wd.ShowDialog(); });
            CustomerCommand = new RelayCommand<object>((p) => { return true; }, (p) => { CustomerWindow wd = new CustomerWindow(); wd.ShowDialog(); });
            ProductCommand = new RelayCommand<object>((p) => { return true; }, (p) => { ProductWindow wd = new ProductWindow(); wd.ShowDialog(); });
            ImportCommand = new RelayCommand<object>((p) => { return true; }, (p) => { ImportWindow wd = new ImportWindow(); wd.ShowDialog(); });
            SizeCommand = new RelayCommand<object>((p) => { return true; }, (p) => { SizeWindow wd = new SizeWindow(); wd.ShowDialog(); });
            CategoryCommand = new RelayCommand<object>((p) => { return true; }, (p) => { CategoryWindow wd = new CategoryWindow(); wd.ShowDialog(); });
            ExportCommand = new RelayCommand<object>((p) => { return true; }, (p) => { ExportWindow wd = new ExportWindow(); wd.ShowDialog(); });


            ExportDetailCommand = new RelayCommand<object>((p) =>
            {
                if (SelectedItem == null) return false;

                return true;
            }, (p) =>
            {
                ExportDetailWindow wd = new ExportDetailWindow(SelectedItem!.Product!.ProductId);
                wd.ShowDialog();
            });

            ImportDetailCommand = new RelayCommand<object>((p) =>
            {
                if (SelectedItem == null) return false;

                return true;
            }, (p) =>
            {
                ImportDetailWindow wd = new ImportDetailWindow(SelectedItem!.Product!.ProductId);
                wd.ShowDialog();
            });

            RefreshCommand = new RelayCommand<object>((p) =>
            {
                return true;
            }, (p) =>
            {
                SelectedNoInfo = string.Empty;
                SelectedProductInfo = null;
                SelectedSupplierInfo = null;
                SelectedCategoryInfo = null;
                SelectedSizeInfo = null;
                SelectedProduct = null;
                SelectedSupplier = null;
                SelectedCategory = null;
                SelectedSize = null;
                LoadInStockData();
            });

            SearchCommand = new RelayCommand<object>((p) =>
            {
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
                displayUniqueProduct = productRepository.GetUniqueProduct(SelectedProduct!.Name, SelectedSupplier!.SupplierId, SelectedCategory!.CategoryId, SelectedSize!.SizeId);


                if (displayUniqueProduct == null)
                {
                    MessageBox.Show($"Product does not exist!!!", "Notice", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                int productIdFiler = 0;

                if (displayUniqueProduct != null)
                {
                    productIdFiler = displayUniqueProduct.ProductId;
                }

                inStockList = new ObservableCollection<InStock>();

                var importList = importRepository.GetListImportByProductId(displayUniqueProduct!.ProductId);
                var exportList = exportRepository.GetListExportByProductId(displayUniqueProduct.ProductId);
                string imagePath = $"pack://application:,,,/Images/{displayUniqueProduct.Category.Name}/{displayUniqueProduct.ImageUrl}";

                int sumImportList = 0;
                int sumExportList = 0;

                if (importList != null && importList.Count() > 0)
                {
                    sumImportList = (int)importList.Sum(p => p.Count);
                }
                if (exportList != null && exportList.Count() > 0)
                {
                    sumExportList = (int)exportList.Sum(p => p.Count);
                }

                InStock stock = new InStock();
                stock.Id = displayUniqueProduct.ProductId;
                stock.Count = sumImportList - sumExportList;
                displayUniqueProduct.ImageUrl = imagePath;
                stock.Product = displayUniqueProduct;
                stock.SumImportList = sumImportList;
                stock.SumExportList = sumExportList;

                inStockList.Add(stock);

                SelectedNoInfo = string.Empty;
                SelectedProductInfo = null;
                SelectedSupplierInfo = null;
                SelectedCategoryInfo = null;
                SelectedSizeInfo = null;
            });
        }

        void LoadInStockData()
        {
            inStockList = new ObservableCollection<InStock>();

            var productList = productRepository.GetListProduct();

            foreach (var item in productList)
            {
                var importList = importRepository.GetListImportByProductId(item.ProductId);
                var exportList = exportRepository.GetListExportByProductId(item.ProductId);
                string imagePath = $"pack://application:,,,/Images/{item.Category.Name}/{item.ImageUrl}";

                int sumImportList = 0;
                int sumExportList = 0;

                if (importList != null && importList.Count() > 0)
                {
                    sumImportList = (int)importList.Sum(p => p.Count);
                }
                if (exportList != null && exportList.Count() > 0)
                {
                    sumExportList = (int)exportList.Sum(p => p.Count);
                }

                InStock stock = new InStock();
                stock.Id = item.ProductId;
                stock.Count = sumImportList - sumExportList;
                item.ImageUrl = imagePath;
                stock.Product = item;
                stock.SumImportList = sumImportList;
                stock.SumExportList = sumExportList;

                inStockList.Add(stock);
            }
        }
    }
}
