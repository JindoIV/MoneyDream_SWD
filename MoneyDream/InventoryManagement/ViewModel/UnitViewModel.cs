using BusinessObject.Models;
using DataAccess.Repository;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace InventoryManagement.ViewModel
{
    public class UnitViewModel : BaseViewModel
    {
        public IUnitRepository unitRepository = new UnitRepository();
        private ObservableCollection<Unit>? _List;
        public ObservableCollection<Unit>? List { get => _List; set { _List = value; OnPropertyChanged(); } }

        private Unit? _SelectedItem;
        public Unit? SelectedItem
        {
            get => _SelectedItem;
            set
            {
                _SelectedItem = value;
                OnPropertyChanged();
                if (SelectedItem != null)
                {
                    IdInfo = SelectedItem.UnitId.ToString();
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

        public UnitViewModel()
        {
            List = new ObservableCollection<Unit>(unitRepository.GetListUnit());

            void ResetInfo()
            {
                IdInfo = string.Empty;
                NameInfo = string.Empty;
                StatusInfo = string.Empty;
                DescriptionInfo = string.Empty;
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
                    return false;

                return true;

            }, (p) =>
            {
                var displayUnit = unitRepository.GetUnitByName(NameInput!);

                if (displayUnit != null)
                {
                    MessageBox.Show($"Unit already exists!!!", "Notice", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                var unit = new Unit() 
                { 
                    Name = NameInput,
                    Status = StatusInput!.ToString().Split(": ")[1],
                    Description = DescriptionInput,
                };

                unitRepository.CreateUnit(unit);

                MessageBox.Show($"Create success!!!", "Notice", MessageBoxButton.OK, MessageBoxImage.Information);
                List = new ObservableCollection<Unit>(unitRepository.GetListUnit());

                ResetInfo();
                ResetInput();
            });

            EditCommand = new RelayCommand<object>((p) =>
            {
                if (string.IsNullOrEmpty(NameInput) &&
                string.IsNullOrEmpty(StatusInput) &&
                string.IsNullOrEmpty(DescriptionInput))
                    return false;

                return true;

            }, (p) =>
            {
                if (SelectedItem == null)
                {
                    MessageBox.Show($"Please select the unit to update!!!", "Notice", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                };

                var displayUnit = unitRepository.GetUnitByName(NameInput!);

                if (displayUnit != null)
                {
                    MessageBox.Show($"Unit already exists!!!", "Notice", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                Unit unit = new Unit() 
                { 
                    UnitId = SelectedItem.UnitId, 
                    Name = !string.IsNullOrEmpty(NameInput) ? NameInput : SelectedItem.Name,
                    Status = !string.IsNullOrEmpty(StatusInput) ? StatusInput!.ToString().Split(": ")[1] : SelectedItem.Status,
                    Description = !string.IsNullOrEmpty(DescriptionInput) ? DescriptionInput : SelectedItem.Description,
                };
                
                unitRepository.UpdateUnit(unit);

                MessageBox.Show($"Update success!!!", "Notice", MessageBoxButton.OK, MessageBoxImage.Information);
                List = new ObservableCollection<Unit>(unitRepository.GetListUnit());

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
                    unitRepository.DeleteUnit(SelectedItem!.UnitId);
                }
                catch
                {
                    MessageBox.Show($"Cannot delete this unit!!!", "Notice", MessageBoxButton.OK, MessageBoxImage.Information);
                    return;
                }

                MessageBox.Show($"Delete success!!!", "Notice", MessageBoxButton.OK, MessageBoxImage.Information);
                List = new ObservableCollection<Unit>(unitRepository.GetListUnit());

                ResetInfo();
                ResetInput();
            });
        }
    }
}
