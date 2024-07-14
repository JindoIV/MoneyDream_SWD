using InventoryManagement.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace InventoryManagement
{
    /// <summary>
    /// Interaction logic for ExportDetailWindow.xaml
    /// </summary>
    public partial class ExportDetailWindow : Window
    {
        public int productId { get; set; }
        public ExportDetailViewModel ViewModel { get; set; }

        public ExportDetailWindow(int productId)
        {
            InitializeComponent();
            this.productId = productId;

            this.DataContext = ViewModel = new ExportDetailViewModel(productId);
        }
    }
}
