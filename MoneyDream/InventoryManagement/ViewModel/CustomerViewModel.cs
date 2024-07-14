using BusinessObject.Models;
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
    public class CustomerViewModel : BaseViewModel
    {
        public ICustomerRepository customerRepository = new CustomerRepository();

        private ObservableCollection<Account>? _List;
        public ObservableCollection<Account>? List { get => _List; set { _List = value; OnPropertyChanged(); } }

        private Account? _SelectedItem;
        public Account? SelectedItem
        {
            get => _SelectedItem;
            set
            {
                _SelectedItem = value;
                OnPropertyChanged();
                if (SelectedItem != null)
                {
                    IdInfo = SelectedItem.AccountId.ToString();
                    UsernameInfo = SelectedItem.UserName;
                    RoleInfo = SelectedItem.Role.RoleName;
                    FullnameInfo = SelectedItem.FullName;
                    PhoneInfo = SelectedItem.PhoneNumber;
                    CityInfo = SelectedItem.City;
                    EmailInfo = SelectedItem.Email;
                    GenderInfo = SelectedItem.Gender;
                    GenderInfo = SelectedItem.Gender;
                    AgeInfo = SelectedItem.Age.ToString();
                    DateofbirthInfo = SelectedItem.DateofBirth.ToString()?.Split(" ")[0];
                    StatusInfo = SelectedItem.Status;
                }
            }
        }

        // Information

        private string? _IdInfo;
        public string? IdInfo { get => _IdInfo; set { _IdInfo = value; OnPropertyChanged(); } }


        private string? _UsernameInfo;
        public string? UsernameInfo { get => _UsernameInfo; set { _UsernameInfo = value; OnPropertyChanged(); } }


        private string? _RoleInfo;
        public string? RoleInfo { get => _RoleInfo; set { _RoleInfo = value; OnPropertyChanged(); } }


        private string? _FullnameInfo;
        public string? FullnameInfo { get => _FullnameInfo; set { _FullnameInfo = value; OnPropertyChanged(); } }


        private string? _PhoneInfo;
        public string? PhoneInfo { get => _PhoneInfo; set { _PhoneInfo = value; OnPropertyChanged(); } }


        private string? _CityInfo;
        public string? CityInfo { get => _CityInfo; set { _CityInfo = value; OnPropertyChanged(); } }


        private string? _EmailInfo;
        public string? EmailInfo { get => _EmailInfo; set { _EmailInfo = value; OnPropertyChanged(); } }


        private string? _GenderInfo;
        public string? GenderInfo { get => _GenderInfo; set { _GenderInfo = value; OnPropertyChanged(); } }


        private string? _AgeInfo;
        public string? AgeInfo { get => _AgeInfo; set { _AgeInfo = value; OnPropertyChanged(); } }


        private string? _DateofbirthInfo;
        public string? DateofbirthInfo { get => _DateofbirthInfo; set { _DateofbirthInfo = value; OnPropertyChanged(); } }


        private string? _StatusInfo;
        public string? StatusInfo { get => _StatusInfo; set { _StatusInfo = value; OnPropertyChanged(); } }

        // Edit

        private string? _UsernameInput;
        public string? UsernameInput { get => _UsernameInput; set { _UsernameInput = value; OnPropertyChanged(); } }


        private string? _PhoneInput;
        public string? PhoneInput { get => _PhoneInput; set { _PhoneInput = value; OnPropertyChanged(); } }


        public ICommand SearchCommand { get; set; }
        public ICommand RefreshCommand { get; set; }

        public CustomerViewModel()
        {
            List = new ObservableCollection<Account>(customerRepository.GetListCustomer());

            void ResetField()
            {
                IdInfo = string.Empty;
                UsernameInfo = string.Empty;
                RoleInfo = string.Empty;
                FullnameInfo = string.Empty;
                PhoneInfo = string.Empty;
                CityInfo = string.Empty;
                EmailInfo = string.Empty;
                GenderInfo = string.Empty;
                GenderInfo = string.Empty;
                AgeInfo = string.Empty;
                DateofbirthInfo = string.Empty;
                StatusInfo = string.Empty;
                UsernameInput = string.Empty;
                PhoneInput = string.Empty;
            }

            SearchCommand = new RelayCommand<object>((p) =>
            {
                if (!string.IsNullOrEmpty(UsernameInput) ||
                !string.IsNullOrEmpty(PhoneInput))
                    return true;

                return false;
            }, (p) =>
            {
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

                Account? account = new Account();


                if (!string.IsNullOrEmpty(UsernameInput) && !string.IsNullOrEmpty(PhoneInput))
                {
                    account = customerRepository.GetCustomerByUsernameAndPhone(UsernameInput, PhoneInput);
                }

                if (!string.IsNullOrEmpty(UsernameInput) && !string.IsNullOrEmpty(PhoneInput) && account == null)
                {
                    MessageBox.Show($"Account does not exist!!!", "Notice", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                if (!string.IsNullOrEmpty(UsernameInput))
                {
                    account = customerRepository.GetCustomerByUsername(UsernameInput);
                }

                if (!string.IsNullOrEmpty(UsernameInput) && account == null)
                {
                    MessageBox.Show($"Username does not exist!!!", "Notice", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                if (!string.IsNullOrEmpty(PhoneInput))
                {
                    account = customerRepository.GetCustomerByPhone(PhoneInput);
                }

                if (!string.IsNullOrEmpty(PhoneInput) && account == null)
                {
                    MessageBox.Show($"Phone number does not exist!!!", "Notice", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                List<Account> accounts = new List<Account>();
                accounts.Add(account!);
                List = new ObservableCollection<Account>(accounts);
            });

            RefreshCommand = new RelayCommand<object>((p) =>
            {
                return true;
            }, (p) =>
            {
                List = new ObservableCollection<Account>(customerRepository.GetListCustomer());
                ResetField();
            });
        }
    }
}
