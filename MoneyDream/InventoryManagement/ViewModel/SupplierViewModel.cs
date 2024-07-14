using BusinessObject.Models ;
using DataAccess.Repository;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace InventoryManagement.ViewModel
{
    public class SupplierViewModel : BaseViewModel
    {
        public ISupplierRepository supplierRepository = new SupplierRepository();

        private ObservableCollection<Supplier>? _List;
        public ObservableCollection<Supplier>? List { get => _List; set { _List = value; OnPropertyChanged(); } }

        private Supplier? _SelectedItem;
        public Supplier? SelectedItem
        {
            get => _SelectedItem;
            set
            {
                _SelectedItem = value;
                OnPropertyChanged();
                if (SelectedItem != null)
                {
                    IdInfo = SelectedItem.SupplierId.ToString();
                    NameInfo = SelectedItem.Name;
                    PhoneInfo = SelectedItem.Phone.ToString();
                    AddressInfo = SelectedItem.Address;
                    EmailInfo = SelectedItem.Email;
                    StatusInfo = SelectedItem.Status;
                    DateInfo = SelectedItem.ContractDate.ToString();
                    MoreInfo = SelectedItem.MoreInfo;
                }
            }
        }

        // Information

        private string? _IdInfo;
        public string? IdInfo { get => _IdInfo; set { _IdInfo = value; OnPropertyChanged(); } }

        private string? _NameInfo;
        public string? NameInfo { get => _NameInfo; set { _NameInfo = value; OnPropertyChanged(); } }


        private string? _PhoneInfo;
        public string? PhoneInfo { get => _PhoneInfo; set { _PhoneInfo = value; OnPropertyChanged(); } }


        private string? _AddressInfo;
        public string? AddressInfo { get => _AddressInfo; set { _AddressInfo = value; OnPropertyChanged(); } }


        private string? _EmailInfo;
        public string? EmailInfo { get => _EmailInfo; set { _EmailInfo = value; OnPropertyChanged(); } }


        private string? _StatusInfo;
        public string? StatusInfo { get => _StatusInfo; set { _StatusInfo = value; OnPropertyChanged(); } }


        private string? _DateInfo;
        public string? DateInfo { get => _DateInfo; set { _DateInfo = value; OnPropertyChanged(); } }


        private string? _MoreInfo;
        public string? MoreInfo { get => _MoreInfo; set { _MoreInfo = value; OnPropertyChanged(); } }


        // Edit

        private string? _NameInput;
        public string? NameInput { get => _NameInput; set { _NameInput = value; OnPropertyChanged(); } }


        private string? _PhoneInput;
        public string? PhoneInput { get => _PhoneInput; set { _PhoneInput = value; OnPropertyChanged(); } }


        private string? _AddressInput;
        public string? AddressInput { get => _AddressInput; set { _AddressInput = value; OnPropertyChanged(); } }


        private string? _EmailInput;
        public string? EmailInput { get => _EmailInput; set { _EmailInput = value; OnPropertyChanged(); } }


        private string? _StatusInput;
        public string? StatusInput { get => _StatusInput; set { _StatusInput = value; OnPropertyChanged(); } }


        private string? _MoreInput;
        public string? MoreInput { get => _MoreInput; set { _MoreInput = value; OnPropertyChanged(); } }


        public ICommand AddCommand { get; set; }
        public ICommand EditCommand { get; set; }
        public ICommand DeleteCommand { get; set; }

        public SupplierViewModel()
        {
            List = new ObservableCollection<Supplier>(supplierRepository.GetListSupplier());

            void ResetInfo()
            {
                IdInfo = string.Empty;
                NameInfo = string.Empty;
                PhoneInfo = string.Empty;
                AddressInfo = string.Empty;
                EmailInfo = string.Empty;
                StatusInfo = string.Empty;
                DateInfo = string.Empty;
                MoreInfo = string.Empty;
            }

            void ResetInput()
            {
                NameInput = string.Empty;
                PhoneInput = string.Empty;
                AddressInput = string.Empty;
                EmailInput = string.Empty;
                StatusInput = null;
                MoreInput = string.Empty;
            }

            AddCommand = new RelayCommand<object>((p) =>
            {
                if (string.IsNullOrEmpty(NameInput) ||
                string.IsNullOrEmpty(PhoneInput) ||
                string.IsNullOrEmpty(AddressInput) ||
                string.IsNullOrEmpty(EmailInput) ||
                string.IsNullOrEmpty(StatusInput))
                    return false;

                return true;

            }, (p) =>
            {
                var displaySupplier = supplierRepository.GetSupplierByName(NameInput!);

                if (displaySupplier != null)
                {
                    MessageBox.Show($"Supplier already exists!!!", "Notice", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                bool IsPhoneNumberValid(string phoneNumber)
                {
                    string pattern = @"^\+?\d{10,11}$";
                    Regex regex = new Regex(pattern);
                    return regex.IsMatch(phoneNumber);
                }

                if (!string.IsNullOrEmpty(PhoneInput) && !IsPhoneNumberValid(PhoneInput))
                {
                    MessageBox.Show($"Phone number must contain 10 or 11 digits!!!", "Notice", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                var supplier = new Supplier()
                {
                    Name = NameInput!,
                    Phone = PhoneInput!,
                    Address = AddressInput!,
                    Email = EmailInput!,
                    Status = StatusInput!.ToString().Split(": ")[1],
                    ContractDate = DateTime.Now,
                    MoreInfo = MoreInput
                };

                supplierRepository.CreateSupplier(supplier);

                MessageBox.Show($"Create success!!!", "Notice", MessageBoxButton.OK, MessageBoxImage.Information);
                List = new ObservableCollection<Supplier>(supplierRepository.GetListSupplier());

                ResetInfo();
                ResetInput();
            });

            EditCommand = new RelayCommand<object>((p) =>
            {
                if (string.IsNullOrEmpty(NameInput) &&
                string.IsNullOrEmpty(PhoneInput) &&
                string.IsNullOrEmpty(AddressInput) &&
                string.IsNullOrEmpty(EmailInput) &&
                string.IsNullOrEmpty(StatusInput) &&
                string.IsNullOrEmpty(MoreInput))
                    return false;

                return true;

            }, (p) =>
            {
                if (SelectedItem == null)
                {
                    MessageBox.Show($"Please select the supplier to update!!!", "Notice", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                };

                var displaySupplier = supplierRepository.GetSupplierByName(NameInput!);

                if (displaySupplier != null)
                {
                    MessageBox.Show($"Supplier already exists!!!", "Notice", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                bool IsPhoneNumberValid(string phoneNumber)
                {
                    string pattern = @"^\+?\d{10,11}$";
                    Regex regex = new Regex(pattern);
                    return regex.IsMatch(phoneNumber);
                }

                if (!string.IsNullOrEmpty(PhoneInput) && !IsPhoneNumberValid(PhoneInput))
                {
                    MessageBox.Show($"Phone number must contain 10 or 11 digits!!!", "Notice", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                Supplier supplier = new Supplier()
                {
                    SupplierId = SelectedItem.SupplierId,
                    Name = !string.IsNullOrEmpty(NameInput) ? NameInput : SelectedItem.Name,
                    Phone = !string.IsNullOrEmpty(PhoneInput) ? PhoneInput : SelectedItem.Phone,
                    Address = !string.IsNullOrEmpty(AddressInput) ? AddressInput : SelectedItem.Address,
                    Email = !string.IsNullOrEmpty(EmailInput) ? EmailInput : SelectedItem.Email,
                    Status = !string.IsNullOrEmpty(StatusInput) ? StatusInput.ToString().Split(": ")[1] : SelectedItem.Status,
                    ContractDate = SelectedItem.ContractDate,
                    MoreInfo = !string.IsNullOrEmpty(MoreInput) ? MoreInput : SelectedItem.MoreInfo,
                };

                supplierRepository.UpdateSupplier(supplier);

                MessageBox.Show($"Update success!!!", "Notice", MessageBoxButton.OK, MessageBoxImage.Information);
                List = new ObservableCollection<Supplier>(supplierRepository.GetListSupplier());

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
                    supplierRepository.DeleteSupplier(SelectedItem!.SupplierId);
                }
                catch
                {
                    MessageBox.Show($"Cannot delete this supplier!!!", "Notice", MessageBoxButton.OK, MessageBoxImage.Information);
                    return;
                }

                MessageBox.Show($"Delete success!!!", "Notice", MessageBoxButton.OK, MessageBoxImage.Information);
                List = new ObservableCollection<Supplier>(supplierRepository.GetListSupplier());

                ResetInfo();
                ResetInput();
            });
        }
    }
}
