using BusinessObject.Models;
using DataAccess.Repository;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;

namespace InventoryManagement.ViewModel
{
    public class SizeViewModel : BaseViewModel
    {
        public ISizeRepository sizeRepository = new SizeRepository();

        private ObservableCollection<BusinessObject.Models.Size>? _List;
        public ObservableCollection<BusinessObject.Models.Size>? List { get => _List; set { _List = value; OnPropertyChanged(); } }

        private BusinessObject.Models.Size? _SelectedItem;
        public BusinessObject.Models.Size? SelectedItem
        {
            get => _SelectedItem;
            set
            {
                _SelectedItem = value;
                OnPropertyChanged();
                if (SelectedItem != null)
                {
                    IdInfo = SelectedItem.SizeId.ToString();
                    NameInfo = SelectedItem.Name;
                    ProductWidthInfo = SelectedItem.ProductWidth;
                    ProductHeightInfo = SelectedItem.ProductHeight;
                    SampleHeightInfo = SelectedItem.SampleHeight;
                    SampleWeightInfo = SelectedItem.SampleWeight;
                    StatusInfo = SelectedItem.Status;
                    DescriptionInfo = SelectedItem.Description;
                }
            }
        }

        // Information

        private string? _IdInfo;
        public string? IdInfo { get => _IdInfo; set { _IdInfo = value; OnPropertyChanged(); } }

        private string? _NameInfo;
        public string? NameInfo { get => _NameInfo; set { _NameInfo = value; OnPropertyChanged(); } }

        private string? _ProductWidthInfo;
        public string? ProductWidthInfo { get => _ProductWidthInfo; set { _ProductWidthInfo = value; OnPropertyChanged(); } }

        private string? _ProductHeightInfo;
        public string? ProductHeightInfo { get => _ProductHeightInfo; set { _ProductHeightInfo = value; OnPropertyChanged(); } }

        private string? _SampleHeightInfo;
        public string? SampleHeightInfo { get => _SampleHeightInfo; set { _SampleHeightInfo = value; OnPropertyChanged(); } }

        private string? _SampleWeightInfo;
        public string? SampleWeightInfo { get => _SampleWeightInfo; set { _SampleWeightInfo = value; OnPropertyChanged(); } }

        private string? _StatusInfo;
        public string? StatusInfo { get => _StatusInfo; set { _StatusInfo = value; OnPropertyChanged(); } }

        private string? _DescriptionInfo;
        public string? DescriptionInfo { get => _DescriptionInfo; set { _DescriptionInfo = value; OnPropertyChanged(); } }

        // Edit 

        private string? _NameInput;
        public string? NameInput { get => _NameInput; set { _NameInput = value; OnPropertyChanged(); } }

        private string? _ProductWidthInput;
        public string? ProductWidthInput { get => _ProductWidthInput; set { _ProductWidthInput = value; OnPropertyChanged(); } }

        private string? _ProductHeightInput;
        public string? ProductHeightInput { get => _ProductHeightInput; set { _ProductHeightInput = value; OnPropertyChanged(); } }

        private string? _SampleHeightInput;
        public string? SampleHeightInput { get => _SampleHeightInput; set { _SampleHeightInput = value; OnPropertyChanged(); } }

        private string? _SampleWeightInput;
        public string? SampleWeightInput { get => _SampleWeightInput; set { _SampleWeightInput = value; OnPropertyChanged(); } }

        private string? _StatusInput;
        public string? StatusInput { get => _StatusInput; set { _StatusInput = value; OnPropertyChanged(); } }

        private string? _DescriptionInput;
        public string? DescriptionInput { get => _DescriptionInput; set { _DescriptionInput = value; OnPropertyChanged(); } }

        public ICommand AddCommand { get; set; }
        public ICommand EditCommand { get; set; }
        public ICommand DeleteCommand { get; set; }

        public SizeViewModel()
        {
            List = new ObservableCollection<BusinessObject.Models.Size>(sizeRepository.GetListSize());

            void ResetInfo()
            {
                IdInfo = string.Empty;
                NameInfo = string.Empty;
                ProductWidthInfo = string.Empty;
                ProductHeightInfo = string.Empty;
                SampleHeightInfo = string.Empty;
                SampleWeightInfo = string.Empty;
                StatusInfo = string.Empty;
                DescriptionInfo = string.Empty;
            }

            void ResetInput()
            {
                NameInput = string.Empty;
                ProductWidthInput = string.Empty;
                ProductHeightInput = string.Empty;
                SampleHeightInput = string.Empty;
                SampleWeightInput = string.Empty;
                StatusInput = null;
                DescriptionInput = string.Empty;
            }

            AddCommand = new RelayCommand<object>((p) =>
            {
                if (string.IsNullOrEmpty(NameInput) ||
                string.IsNullOrEmpty(ProductWidthInput) ||
                string.IsNullOrEmpty(ProductHeightInput) ||
                string.IsNullOrEmpty(SampleHeightInput) ||
                string.IsNullOrEmpty(SampleWeightInput) ||
                string.IsNullOrEmpty(StatusInput))
                    return false;

                return true;

            }, (p) =>
            {
                var displaySize = sizeRepository.GetSizeByName(NameInput!);

                if (displaySize != null)
                {
                    MessageBox.Show($"Size already exists!!!", "Notice", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                BusinessObject.Models.Size size = new BusinessObject.Models.Size()
                {
                    Name = NameInput!,
                    ProductWidth = ProductWidthInput!,
                    ProductHeight = ProductHeightInput!, 
                    SampleHeight = SampleHeightInput!, 
                    SampleWeight = SampleWeightInput!,
                    Status = StatusInput!.ToString().Split(": ")[1],
                    Description = DescriptionInput!
                };

                sizeRepository.CreateSize(size);

                MessageBox.Show($"Create success!!!", "Notice", MessageBoxButton.OK, MessageBoxImage.Information);
                List = new ObservableCollection<BusinessObject.Models.Size>(sizeRepository.GetListSize());

                ResetInfo();
                ResetInput();
            });

            EditCommand = new RelayCommand<object>((p) =>
            {
                if (string.IsNullOrEmpty(NameInput) &&
                string.IsNullOrEmpty(ProductWidthInput) &&
                string.IsNullOrEmpty(ProductHeightInput) &&
                string.IsNullOrEmpty(SampleHeightInput) &&
                string.IsNullOrEmpty(SampleWeightInput) &&
                string.IsNullOrEmpty(StatusInput) &&
                string.IsNullOrEmpty(DescriptionInput))
                    return false;

                return true;

            }, (p) =>
            {
                if (SelectedItem == null)
                {
                    MessageBox.Show($"Please select the size to update!!!", "Notice", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                };

                var displaySize = sizeRepository.GetSizeByName(NameInput!);

                if (displaySize != null)
                {
                    MessageBox.Show($"Size already exists!!!", "Notice", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                BusinessObject.Models.Size size = new BusinessObject.Models.Size()
                {
                    SizeId = SelectedItem.SizeId,
                    Name = !string.IsNullOrEmpty(NameInput) ? NameInput : SelectedItem.Name,
                    ProductWidth = !string.IsNullOrEmpty(ProductWidthInput) ? ProductWidthInput : SelectedItem.ProductWidth,
                    ProductHeight = !string.IsNullOrEmpty(ProductHeightInput) ? ProductHeightInput : SelectedItem.ProductHeight,
                    SampleHeight = !string.IsNullOrEmpty(SampleHeightInput) ? SampleHeightInput : SelectedItem.SampleHeight,
                    SampleWeight = !string.IsNullOrEmpty(SampleWeightInput) ? SampleWeightInput : SelectedItem.SampleWeight,
                    Status = !string.IsNullOrEmpty(StatusInput) ? StatusInput.ToString().Split(": ")[1] : SelectedItem.Status,
                    Description = !string.IsNullOrEmpty(DescriptionInput) ? DescriptionInput : SelectedItem.Description,
                };

                sizeRepository.UpdateSize(size);

                MessageBox.Show($"Update success!!!", "Notice", MessageBoxButton.OK, MessageBoxImage.Information);
                List = new ObservableCollection<BusinessObject.Models.Size>(sizeRepository.GetListSize());

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
                    sizeRepository.DeleteSize(SelectedItem!.SizeId);
                }
                catch
                {
                    MessageBox.Show($"Cannot delete this size!!!", "Notice", MessageBoxButton.OK, MessageBoxImage.Information);
                    return;
                }

                MessageBox.Show($"Delete success!!!", "Notice", MessageBoxButton.OK, MessageBoxImage.Information);
                List = new ObservableCollection<BusinessObject.Models.Size>(sizeRepository.GetListSize());

                ResetInfo();
                ResetInput();
            });
        }
    }
}
