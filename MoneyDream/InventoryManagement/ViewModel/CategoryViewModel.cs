using BusinessObject.Models;
using DataAccess.Repository;
using System.Collections.ObjectModel;
using System.IO;
using System.Runtime;
using System.Windows;
using System.Windows.Input;

namespace InventoryManagement.ViewModel
{
    public class CategoryViewModel : BaseViewModel
    {
        public ICategoryRepository categoryRepository = new CategoryRepository();

        private ObservableCollection<Category>? _List;
        public ObservableCollection<Category>? List { get => _List; set { _List = value; OnPropertyChanged(); } }

        private Category? _SelectedItem;
        public Category? SelectedItem
        {
            get => _SelectedItem;
            set
            {
                _SelectedItem = value;
                OnPropertyChanged();
                if (SelectedItem != null)
                {
                    IdInfo = SelectedItem.CategoryId.ToString();
                    NameInfo = SelectedItem.Name;
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

        private string? _StatusInfo;
        public string? StatusInfo { get => _StatusInfo; set { _StatusInfo = value; OnPropertyChanged(); } }

        private string? _DescriptionInfo;
        public string? DescriptionInfo { get => _DescriptionInfo; set { _DescriptionInfo = value; OnPropertyChanged(); } }

        // Edit 

        private string? _NameInput;
        public string? NameInput { get => _NameInput; set { _NameInput = value; OnPropertyChanged(); } }

        private string? _StatusInput;
        public string? StatusInput { get => _StatusInput; set { _StatusInput = value; OnPropertyChanged(); } }

        private string? _DescriptionInput;
        public string? DescriptionInput { get => _DescriptionInput; set { _DescriptionInput = value; OnPropertyChanged(); } }


        public ICommand AddCommand { get; set; }
        public ICommand EditCommand { get; set; }
        public ICommand DeleteCommand { get; set; }

        public CategoryViewModel()
        {
            List = new ObservableCollection<Category>(categoryRepository.GetListCategory());

            string? baseDirectory = AppDomain.CurrentDomain.BaseDirectory;

            if (baseDirectory != null )
            {
                while (!baseDirectory!.EndsWith("InventoryManagement"))
                {
                    baseDirectory = Directory.GetParent(baseDirectory)?.Parent?.FullName;
                }
            }

            void ResetInfo()
            {
                IdInfo = string.Empty;
                NameInfo = string.Empty;
                StatusInfo = string.Empty;
                DescriptionInput = string.Empty;
            }

            void ResetInput()
            {
                NameInput = string.Empty;
                StatusInput = null;
                DescriptionInput = string.Empty;
            }

            AddCommand = new RelayCommand<object>((p) =>
            {
                if (string.IsNullOrEmpty(NameInput) ||
                    string.IsNullOrEmpty(StatusInput))
                {
                    return false;
                }

                return true;

            }, (p) =>
            {
                var displayCategory = categoryRepository.GetCategoryByName(NameInput!);

                if (displayCategory != null)
                {
                    MessageBox.Show($"Category already exists!!!", "Notice", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                Category category = new Category()
                {
                    Name = NameInput!,
                    Status = StatusInput!.ToString().Split(": ")[1],
                    Description = DescriptionInput
                };

                categoryRepository.CreateCategory(category);


                string folderName = $"Images/{NameInput}";
                string path = Path.Combine(baseDirectory!, folderName);

                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }

                MessageBox.Show($"Create success!!!", "Notice", MessageBoxButton.OK, MessageBoxImage.Information);
                List = new ObservableCollection<Category>(categoryRepository.GetListCategory());

                ResetInfo();
                ResetInput();
            });

            EditCommand = new RelayCommand<object>((p) =>
            {
                if (string.IsNullOrEmpty(NameInput) &&
                    string.IsNullOrEmpty(StatusInput) &&
                    string.IsNullOrEmpty(DescriptionInput))
                {
                    return false;
                }

                return true;

            }, (p) =>
            {
                if (SelectedItem == null)
                {
                    MessageBox.Show($"Please select the category to update!!!", "Notice", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                };

                var displayCategory = categoryRepository.GetCategoryByName(NameInput!);

                if (displayCategory != null)
                {
                    MessageBox.Show($"Category already exists!!!", "Notice", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                string oldFolderName = $"Images/{SelectedItem.Name}";
                string oldPath = Path.Combine(baseDirectory!, oldFolderName);

                string newFolderName = $"Images/{NameInput}";
                string newPath = Path.Combine(baseDirectory!, newFolderName);

                if (Directory.Exists(oldPath))
                {
                    if (!Directory.Exists(newPath))
                    {
                        Directory.Move(oldPath, newPath);
                    }
                }

                Category category = new Category()
                {
                    CategoryId = SelectedItem.CategoryId,
                    Name = !string.IsNullOrEmpty(NameInput) ? NameInput : SelectedItem.Name,
                    Status = !string.IsNullOrEmpty(StatusInput) ? StatusInput!.ToString().Split(": ")[1] : SelectedItem.Status,
                    Description = !string.IsNullOrEmpty(DescriptionInput) ? DescriptionInput : SelectedItem.Description,
                };

                categoryRepository.UpdateCategory(category);

                MessageBox.Show($"Update success!!!", "Notice", MessageBoxButton.OK, MessageBoxImage.Information);
                List = new ObservableCollection<Category>(categoryRepository.GetListCategory());

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
                    categoryRepository.DeleteCategory(SelectedItem!.CategoryId);
                }
                catch
                {
                    MessageBox.Show($"Cannot delete this category!!!", "Notice", MessageBoxButton.OK, MessageBoxImage.Information);
                    return;
                }

                string folderName = $"Images/{SelectedItem.Name}";
                string path = Path.Combine(baseDirectory!, folderName);

                if (Directory.Exists(path))
                {
                    Directory.Delete(path);
                }

                MessageBox.Show($"Delete success!!!", "Notice", MessageBoxButton.OK, MessageBoxImage.Information);
                List = new ObservableCollection<Category>(categoryRepository.GetListCategory());

                ResetInfo();
                ResetInput();
            });
        }
    }
}
