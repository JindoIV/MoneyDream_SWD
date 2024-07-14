using BusinessObject.Models;
using DataAccess.Repository;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace InventoryManagement.ViewModel
{
    public class LoginViewModel : BaseViewModel
    {
        public ICustomerRepository customerRepository = new CustomerRepository();

        public bool IsLogin { get; set; }
        private string? _UserName;
        public string? UserName { get => _UserName; set { _UserName = value; OnPropertyChanged(); } }
        private string? _Password;
        public string? Password { get => _Password; set { _Password = value; OnPropertyChanged(); } }

        public ICommand CloseCommand { get; set; }
        public ICommand LoginCommand { get; set; }
        public ICommand PasswordChangedCommand { get; set; }

        public LoginViewModel()
        {
            IsLogin = false;
            Password = "";
            UserName = "";
            LoginCommand = new RelayCommand<Window>((p) => { return true; }, (p) => { Login(p); });
            CloseCommand = new RelayCommand<Window>((p) => { return true; }, (p) => { p.Close(); });
            PasswordChangedCommand = new RelayCommand<PasswordBox>((p) => { return true; }, (p) => { Password = p.Password; });
        }

        void Login(Window p)
        {
            if (p == null)
                return;

            if (string.IsNullOrWhiteSpace(UserName))
            {
                MessageBox.Show($"Please enter username!!!", "Notice", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (string.IsNullOrWhiteSpace(Password))
            {
                MessageBox.Show($"Please enter password!!!", "Notice", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            Account? accountLogin = customerRepository.GetCustomerByUsername(UserName);

            if ((accountLogin != null && accountLogin.UserName != UserName) || accountLogin == null)
            {
                MessageBox.Show($"Username does not exist!!!", "Notice", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (!CheckHashed(Password, accountLogin.Password))
            {
                MessageBox.Show($"Incorrect password!!!", "Notice", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (accountLogin!.Role.RoleName != "ADMIN")
            {
                MessageBox.Show($"Account access denied!!!", "Notice", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            IsLogin = true;
            p.Close();
        }

        private string Hashing(string password)
        {
            return BCrypt.Net.BCrypt.HashPassword(password, 13);
            //string salt = BCrypt.Net.BCrypt.GenerateSalt();
            //string hashedPassword = BCrypt.Net.BCrypt.HashPassword(password, salt);
            //return hashedPassword;
        }

        private bool CheckHashed(string origin, string hash)
        {
            return BCrypt.Net.BCrypt.Verify(origin, hash);
        }
    }
}
