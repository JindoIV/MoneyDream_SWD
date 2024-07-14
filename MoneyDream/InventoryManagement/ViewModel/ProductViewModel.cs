using BusinessObject.Models;
using CloudinaryDotNet.Actions;
using CloudinaryDotNet;
using DataAccess.Repository;
using Microsoft.Win32;
using System.Collections.ObjectModel;
using System.IO;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using System.Configuration;
using Newtonsoft.Json;

namespace InventoryManagement.ViewModel
{
    public class ProductViewModel : BaseViewModel
    {
        public IProductRepository productRepository = new ProductRepository();
        public IUnitRepository unitRepository = new UnitRepository();
        public ISupplierRepository supplierRepository = new SupplierRepository();
        public ISizeRepository sizeRepository = new SizeRepository();
        public ICategoryRepository categoryRepository = new CategoryRepository();

        private ObservableCollection<Product>? _List;
        public ObservableCollection<Product>? List { get => _List; set { _List = value; OnPropertyChanged(); } }

        private ObservableCollection<Unit>? _Unit;
        public ObservableCollection<Unit>? Unit { get => _Unit; set { _Unit = value; OnPropertyChanged(); } }

        private ObservableCollection<BusinessObject.Models.Size>? _Size;
        public ObservableCollection<BusinessObject.Models.Size>? Size { get => _Size; set { _Size = value; OnPropertyChanged(); } }

        private ObservableCollection<Category>? _Category;
        public ObservableCollection<Category>? Category { get => _Category; set { _Category = value; OnPropertyChanged(); } }

        private ObservableCollection<Supplier>? _Supplier;
        public ObservableCollection<Supplier>? Supplier { get => _Supplier; set { _Supplier = value; OnPropertyChanged(); } }

        public string GetBaseDirectory()
        {
            string? baseDirectory = AppDomain.CurrentDomain.BaseDirectory;

            if (baseDirectory != null)
            {
                while (!baseDirectory!.EndsWith("InventoryManagement"))
                {
                    baseDirectory = Directory.GetParent(baseDirectory)?.Parent?.FullName;
                }
            }

            return baseDirectory!;
        }

        private Product? _SelectedItem = null;
        public Product? SelectedItem
        {
            get => _SelectedItem;
            set
            {
                _SelectedItem = value;
                OnPropertyChanged();

                if (SelectedItem != null)
                {
                    ProductIdInfo = SelectedItem.ProductId.ToString();
                    NameInfo = SelectedItem.Name;
                    SelectedSupplierInfo = SelectedItem.Supplier.Name;
                    SelectedCategoryInfo = SelectedItem.Category.Name;
                    SelectedSizeInfo = SelectedItem.Size.Name;
                    SelectedUnitInfo = SelectedItem.Unit.Name;
                    OldPriceInfo = SelectedItem.OldPrice.ToString();
                    DiscountInfo = SelectedItem.Discount.ToString().Split('.')[0];
                    StatusInfo = SelectedItem.Status;
                    DescriptionInfo = SelectedItem.Description;
                }
            }
        }

        // Information

        private string? _ProductIdInfo;
        public string? ProductIdInfo { get => _ProductIdInfo; set { _ProductIdInfo = value; OnPropertyChanged(); } }


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

        private string? _SelectedUnitInfo;
        public string? SelectedUnitInfo
        {
            get => _SelectedUnitInfo;
            set
            {
                _SelectedUnitInfo = value;
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

        private string? _NameInfo;
        public string? NameInfo { get => _NameInfo; set { _NameInfo = value; OnPropertyChanged(); } }


        private string? _OldPriceInfo;
        public string? OldPriceInfo { get => _OldPriceInfo; set { _OldPriceInfo = value; OnPropertyChanged(); } }


        private string? _DiscountInfo;
        public string? DiscountInfo { get => _DiscountInfo; set { _DiscountInfo = value; OnPropertyChanged(); } }


        private string? _StatusInfo;
        public string? StatusInfo { get => _StatusInfo; set { _StatusInfo = value; OnPropertyChanged(); } }


        private string? _DescriptionInfo;
        public string? DescriptionInfo { get => _DescriptionInfo; set { _DescriptionInfo = value; OnPropertyChanged(); } }

        // Edit Form

        private Category? _SelectedCategoryInput;
        public Category? SelectedCategoryInput
        {
            get => _SelectedCategoryInput;
            set
            {
                _SelectedCategoryInput = value;
                OnPropertyChanged();
            }
        }

        private Unit? _SelectedUnitInput;
        public Unit? SelectedUnitInput
        {
            get => _SelectedUnitInput;
            set
            {
                _SelectedUnitInput = value;
                OnPropertyChanged();
            }
        }

        private BusinessObject.Models.Size? _SelectedSizeInput;
        public BusinessObject.Models.Size? SelectedSizeInput
        {
            get => _SelectedSizeInput;
            set
            {
                _SelectedSizeInput = value;
                OnPropertyChanged();
            }
        }

        private Supplier? _SelectedSupplierInput;
        public Supplier? SelectedSupplierInput
        {
            get => _SelectedSupplierInput;
            set
            {
                _SelectedSupplierInput = value;
                OnPropertyChanged();
            }
        }

        private string? _NameInput;
        public string? NameInput { get => _NameInput; set { _NameInput = value; OnPropertyChanged(); } }


        private string? _OldPriceInput;
        public string? OldPriceInput { get => _OldPriceInput; set { _OldPriceInput = value; OnPropertyChanged(); } }


        private string? _DiscountInput;
        public string? DiscountInput { get => _DiscountInput; set { _DiscountInput = value; OnPropertyChanged(); } }


        private string? _StatusInput;
        public string? StatusInput { get => _StatusInput; set { _StatusInput = value; OnPropertyChanged(); } }


        private string? _DescriptionInput;
        public string? DescriptionInput { get => _DescriptionInput; set { _DescriptionInput = value; OnPropertyChanged(); } }

        private string? _ImageUrlInput;
        public string? ImageUrlInput { get => _ImageUrlInput; set { _ImageUrlInput = value; OnPropertyChanged(); } }


        private string? _ImageUrlSaveInput;
        public string? ImageUrlSaveInput { get => _ImageUrlSaveInput; set { _ImageUrlSaveInput = value; OnPropertyChanged(); } }


        public ICommand AddCommand { get; set; }
        public ICommand EditCommand { get; set; }
        public ICommand DeleteCommand { get; set; }
        public ICommand UploadImageCommand { get; set; }
        public ICommand RefreshCommand { get; set; }

        List<Product> ReloadImage()
        {
            List<Product> list = new List<Product>();
            List<Product> getProducts = new List<Product>();
            getProducts = productRepository.GetListProduct();

            foreach (var item in getProducts)
            {
                //string selectedFilePath = GetBaseDirectory() + $"/Images/{item.Category.Name}/{item.ImageUrl}";
                //item.ImageUrl = selectedFilePath;
                item.ImageUrl = $"pack://application:,,,/Images/{item.Category.Name}/{item.ImageUrl}";
                list.Add(item);
            }

            return list;
        }

        public ProductViewModel()
        {
            string? baseDirectory = GetBaseDirectory();

            List = new ObservableCollection<Product>(ReloadImage());
            Unit = new ObservableCollection<Unit>(unitRepository.GetListUnit().Where(x => x.Status == "Enable"));
            Supplier = new ObservableCollection<Supplier>(supplierRepository.GetListSupplier().Where(x => x.Status == "Enable"));
            Size = new ObservableCollection<BusinessObject.Models.Size>(sizeRepository.GetListSize().Where(x => x.Status == "Enable"));
            Category = new ObservableCollection<Category>(categoryRepository.GetListCategory().Where(x => x.Status == "Enable"));

            void ResetInput()
            {
                NameInput = string.Empty;
                SelectedSupplierInput = null;
                SelectedCategoryInput = null;
                SelectedSizeInput = null;
                SelectedUnitInput = null;
                OldPriceInput = string.Empty;
                DiscountInput = string.Empty;
                DescriptionInput = string.Empty;
                StatusInput = null;
                ImageUrlInput = string.Empty;
                ImageUrlSaveInput = string.Empty;
            }

            void ResetInfo()
            {
                ProductIdInfo = string.Empty;
                NameInfo = string.Empty;
                SelectedSupplierInfo = null;
                SelectedCategoryInfo = null;
                SelectedSizeInfo = null;
                SelectedUnitInfo = null;
                OldPriceInfo = string.Empty;
                DiscountInfo = string.Empty;
                StatusInfo = string.Empty;
                DescriptionInfo = string.Empty;
            }

            RefreshCommand = new RelayCommand<object>((p) =>
            {
                return true;

            }, (p) =>
            {
                List = new ObservableCollection<Product>(ReloadImage());
                Unit = new ObservableCollection<Unit>(unitRepository.GetListUnit().Where(x => x.Status == "Enable"));
                Supplier = new ObservableCollection<Supplier>(supplierRepository.GetListSupplier().Where(x => x.Status == "Enable"));
                Size = new ObservableCollection<BusinessObject.Models.Size>(sizeRepository.GetListSize().Where(x => x.Status == "Enable"));
                Category = new ObservableCollection<Category>(categoryRepository.GetListCategory().Where(x => x.Status == "Enable"));

                ResetInput();
                ResetInfo();
            });

            AddCommand = new RelayCommand<object>((p) =>
            {
                if (SelectedSupplierInput == null ||
                SelectedUnitInput == null ||
                SelectedCategoryInput == null ||
                SelectedSizeInput == null ||
                string.IsNullOrEmpty(NameInput?.Trim()) ||
                string.IsNullOrEmpty(OldPriceInput?.Trim()) ||
                string.IsNullOrEmpty(StatusInput?.Trim()) ||
                string.IsNullOrEmpty(DiscountInput?.Trim()) ||
                string.IsNullOrEmpty(ImageUrlInput?.Trim()))
                    return false;

                return true;

            }, (p) =>
            {
                var displayUniqueProduct = productRepository.GetUniqueProduct(NameInput!, SelectedSupplierInput!.SupplierId, SelectedCategoryInput!.CategoryId, SelectedSizeInput!.SizeId);

                if (displayUniqueProduct != null)
                {
                    MessageBox.Show($"Product already exists!!!", "Notice", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                var displayProductByName = productRepository.GetProductByName(NameInput!);

                if (displayUniqueProduct == null && displayProductByName != null)
                {
                    MessageBox.Show($"Product name already exists!!!", "Notice", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                if (!int.TryParse(OldPriceInput, out int boolOldPriceInput) || int.Parse(OldPriceInput) <= 0)
                {
                    MessageBox.Show($"Price must be a positive number!!!", "Notice", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                if (!int.TryParse(DiscountInput, out int boolDiscountInput) || int.Parse(DiscountInput) < 0 || int.Parse(DiscountInput) > 100)
                {
                    MessageBox.Show($"Discount must have a value from 0 to 100!!!", "Notice", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                Product product = new Product()
                {
                    Name = NameInput!,
                    SizeId = SelectedSizeInput.SizeId,
                    SupplierId = SelectedSupplierInput.SupplierId,
                    UnitId = SelectedUnitInput!.UnitId,
                    CategoryId = SelectedCategoryInput.CategoryId,
                    OldPrice = int.Parse(OldPriceInput),
                    Discount = int.Parse(DiscountInput),
                    Status = StatusInput!.ToString().Split(": ")[1],
                    Description = DescriptionInput,
                    ImageUrl = "",
                };

                productRepository.CreateProduct(product);

                List<Product> listProduct = new List<Product>();
                listProduct = productRepository.GetListProduct();
                int maxId = listProduct.Max(x => x.ProductId);

                product.ImageUrl = $"{maxId}.jpg";

                productRepository.UpdateProduct(product);

                if (ImageUrlSaveInput != null)
                {
                    BitmapImage bitmapImage = new BitmapImage(new Uri(ImageUrlSaveInput));

                    BitmapSource bitmapSource = bitmapImage;

                    // Create BitmapEncoder to save image
                    BitmapEncoder encoder = new PngBitmapEncoder();
                    encoder.Frames.Add(BitmapFrame.Create(bitmapSource));

                    string getCategory = SelectedCategoryInput != null ? SelectedCategoryInput.Name : SelectedItem!.Category.Name;

                    // declare path to save image
                    string filePath = $"Images/{getCategory}/{maxId}.jpg";
                    string path = System.IO.Path.Combine(baseDirectory, filePath);

                    // Save image
                    using (FileStream fileStream = new FileStream(path, FileMode.Create, FileAccess.Write))
                    {
                        encoder.Save(fileStream);
                    }

                    // Upload Image To Cloud
                    var config = ConfigurationManager.AppSettings;

                    var account = new CloudinaryDotNet.Account(
                        config["Cloudinary:Name"],
                        config["Cloudinary:ApiKey"],
                        config["Cloudinary:ApiSecret"]
                    );

                    var cloudinary = new Cloudinary(account);

                    var uploadParams = new ImageUploadParams()
                    {
                        File = new FileDescription(path)
                    };

                    try
                    {
                        var uploadResult = cloudinary.Upload(uploadParams);
                        product.ImageUrlcloud = JsonConvert.SerializeObject(uploadResult.SecureUrl.ToString());
                        product.PublicId = JsonConvert.SerializeObject((uploadResult.PublicId));
                    }
                    catch
                    {
                        product.ImageUrlcloud = null;
                        product.PublicId = null;
                    }

                    productRepository.UpdateProduct(product);
                }

                MessageBox.Show($"Create success!!!", "Notice", MessageBoxButton.OK, MessageBoxImage.Information);
                List = new ObservableCollection<Product>(ReloadImage());

                ResetInput();
                ResetInfo();
            });

            EditCommand = new RelayCommand<object>((p) =>
            {
                if (SelectedSupplierInput == null &&
                SelectedUnitInput == null &&
                SelectedCategoryInput == null &&
                SelectedSizeInput == null &&
                string.IsNullOrEmpty(NameInput?.Trim()) &&
                string.IsNullOrEmpty(OldPriceInput?.Trim()) &&
                string.IsNullOrEmpty(StatusInput?.Trim()) &&
                string.IsNullOrEmpty(DiscountInput?.Trim()) &&
                string.IsNullOrEmpty(ImageUrlInput?.Trim()) &&
                string.IsNullOrEmpty(DescriptionInput?.Trim()))
                    return false;

                return true;
            }, (p) =>
            {
                if (SelectedItem == null)
                {
                    MessageBox.Show($"Please select the product to update!!!", "Notice", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                };

                int supplierId = SelectedSupplierInput != null ? SelectedSupplierInput.SupplierId : SelectedItem.SupplierId;
                int categoryId = SelectedCategoryInput != null ? SelectedCategoryInput.CategoryId : SelectedItem.CategoryId;
                int sizeId = SelectedSizeInput != null ? SelectedSizeInput.SizeId : SelectedItem.SizeId;

                var displayUniqueProduct = productRepository.GetUniqueProduct(NameInput!, supplierId, categoryId, sizeId);
                var displayProductByName = productRepository.GetProductByName(NameInput!);

                if (displayUniqueProduct != null && displayUniqueProduct != SelectedItem)
                {
                    MessageBox.Show($"Product already exists!!!", "Notice", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                if (displayUniqueProduct == null && displayProductByName != null)
                {
                    MessageBox.Show($"Product name already exists!!!", "Notice", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                if (!string.IsNullOrEmpty(OldPriceInput?.Trim()) && (!int.TryParse(OldPriceInput, out int boolOldPriceInput) || int.Parse(OldPriceInput) <= 0))
                {
                    MessageBox.Show($"Price must be a positive number!!!", "Notice", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                if (!string.IsNullOrEmpty(DiscountInput?.Trim()) && (!int.TryParse(DiscountInput, out int boolDiscountInput) || int.Parse(DiscountInput) < 0 || int.Parse(DiscountInput) > 100))
                {
                    MessageBox.Show($"Discount must have a value from 0 to 100!!!", "Notice", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                Product displayProductDesValueByID = new Product();

                if (SelectedItem != null)
                {
                    displayProductDesValueByID = productRepository.GetProductById(SelectedItem.ProductId);
                }

                Product? product = null;
                if (SelectedItem != null)
                {
                    product = new Product()
                    {
                        ProductId = SelectedItem.ProductId,
                        Name = !string.IsNullOrEmpty(NameInput?.Trim()) ? NameInput : SelectedItem.Name,
                        SizeId = SelectedSizeInput != null ? SelectedSizeInput.SizeId : SelectedItem.SizeId,
                        SupplierId = SelectedSupplierInput != null ? SelectedSupplierInput.SupplierId : SelectedItem.SupplierId,
                        UnitId = SelectedUnitInput != null ? SelectedUnitInput.UnitId : SelectedItem.UnitId,
                        CategoryId = SelectedCategoryInput != null ? SelectedCategoryInput.CategoryId : SelectedItem.CategoryId,
                        OldPrice = !string.IsNullOrEmpty(OldPriceInput?.Trim()) ? int.Parse(OldPriceInput) : SelectedItem.OldPrice,
                        Discount = !string.IsNullOrEmpty(DiscountInput?.Trim()) ? int.Parse(DiscountInput) : SelectedItem.Discount,
                        Status = !string.IsNullOrEmpty(StatusInput?.Trim()) ? StatusInput.ToString().Split(": ")[1] : SelectedItem.Status,
                        Description = !string.IsNullOrEmpty(DescriptionInput?.Trim()) ? DescriptionInput : SelectedItem.Description,
                        ImageUrl = System.IO.Path.GetFileName(SelectedItem.ImageUrl),
                        ImageUrlcloud = SelectedItem.ImageUrlcloud,
                        PublicId = SelectedItem.PublicId,
                    };
                }

                Product getProductImage = new Product();
                getProductImage = productRepository.GetProductById(SelectedItem!.ProductId);

                if (ImageUrlSaveInput != null && ImageUrlSaveInput != "")
                {
                    BitmapImage bitmapImage = new BitmapImage(new Uri(ImageUrlSaveInput));

                    BitmapSource bitmapSource = bitmapImage;

                    // Create BitmapEncoder to save image
                    BitmapEncoder encoder = new PngBitmapEncoder();
                    encoder.Frames.Add(BitmapFrame.Create(bitmapSource));

                    // declare path to save image
                    string filePath = $"Images/{SelectedItem!.Category.Name}/{getProductImage.ImageUrl}";
                    string path = System.IO.Path.Combine(baseDirectory, filePath);

                    // Save image
                    using (FileStream fileStream = new FileStream(path, FileMode.Create, FileAccess.Write))
                    {
                        encoder.Save(fileStream);
                    }

                    //Update Image Cloud

                    if (SelectedItem.PublicId != null)
                    {
                        var config = ConfigurationManager.AppSettings;

                        var account = new CloudinaryDotNet.Account(
                            config["Cloudinary:Name"],
                            config["Cloudinary:ApiKey"],
                            config["Cloudinary:ApiSecret"]
                        );

                        var cloudinary = new Cloudinary(account);

                        var uploadParams = new ImageUploadParams()
                        {
                            File = new FileDescription(path),
                            PublicId = JsonConvert.DeserializeObject(SelectedItem.PublicId)!.ToString(),
                            Overwrite = true
                        };

                        var uploadResult = cloudinary.Upload(uploadParams);
                    } 
                    else
                    {
                        // Upload Image To Cloud
                        var config = ConfigurationManager.AppSettings;

                        var account = new CloudinaryDotNet.Account(
                            config["Cloudinary:Name"],
                            config["Cloudinary:ApiKey"],
                            config["Cloudinary:ApiSecret"]
                        );

                        var cloudinary = new Cloudinary(account);

                        var uploadParams = new ImageUploadParams()
                        {
                            File = new FileDescription(path)
                        };

                        try
                        {
                            var uploadResult = cloudinary.Upload(uploadParams);
                            product.ImageUrlcloud = JsonConvert.SerializeObject(uploadResult.SecureUrl.ToString());
                            product.PublicId = JsonConvert.SerializeObject((uploadResult.PublicId));
                        }
                        catch
                        {
                            product.ImageUrlcloud = null;
                            product.PublicId = null;
                        }
                    }
                }

                productRepository.UpdateProduct(product!);

                MessageBox.Show($"Update success!!!", "Notice", MessageBoxButton.OK, MessageBoxImage.Information);
                List = new ObservableCollection<Product>(ReloadImage());

                ResetInfo();
                ResetInput();
            });

            DeleteCommand = new RelayCommand<object>((p) =>
            {
                if (SelectedItem == null)
                    return false;

                return true;

            }, (p) =>
            {
                try
                {
                    productRepository.DeleteProduct(SelectedItem!.ProductId);
                }
                catch
                {
                    MessageBox.Show($"Cannot delete this product!!!", "Notice", MessageBoxButton.OK, MessageBoxImage.Information);
                    return;
                }

                // declare path to save image
                string filePath = $"Images/{SelectedItem!.Category.Name}/{System.IO.Path.GetFileName(SelectedItem.ImageUrl)}";
                string path = System.IO.Path.Combine(baseDirectory, filePath);

                if (File.Exists(path))
                {
                    File.Delete(path);
                }

                //Delete Image Cloud
                if (SelectedItem.PublicId != null)
                {
                    var config = ConfigurationManager.AppSettings;

                    var account = new CloudinaryDotNet.Account(
                        config["Cloudinary:Name"],
                        config["Cloudinary:ApiKey"],
                        config["Cloudinary:ApiSecret"]
                    );

                    var cloudinary = new Cloudinary(account);

                    var deleteParams = new DeletionParams(JsonConvert.DeserializeObject(SelectedItem.PublicId)!.ToString());

                    var deletionResult = cloudinary.Destroy(deleteParams);

                    //if (deletionResult.Result == "ok")
                    //{
                    //    MessageBox.Show("Image deleted successfully.");
                    //}
                }

                MessageBox.Show($"Delete success!!!", "Notice", MessageBoxButton.OK, MessageBoxImage.Information);
                List = new ObservableCollection<Product>(ReloadImage());

                ResetInput();
                ResetInfo();
            });

            UploadImageCommand = new RelayCommand<object>((p) =>
            {

                return true;

            }, (p) =>
            {
                OpenFileDialog openFileDialog = new OpenFileDialog();
                openFileDialog.Filter = "Image files (*.png;*.jpeg;*.jpg)|*.png;*.jpeg;*.jpg|All files (*.*)|*.*";

                if (openFileDialog.ShowDialog() == true)
                {
                    try
                    {
                        string selectedFilePath = openFileDialog.FileName;

                        BitmapImage bitmap = new BitmapImage();
                        bitmap.BeginInit();
                        bitmap.UriSource = new Uri(selectedFilePath);
                        bitmap.EndInit();

                        ImageUrlInput = System.IO.Path.GetFileName(selectedFilePath);
                        ImageUrlSaveInput = bitmap.ToString();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Error: {ex.Message}");
                    }
                }
            });
        }
    }
}
