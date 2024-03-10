using System.Windows;
using WORKSHOP.Warehouse;

namespace WORKSHOP
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Order(object sender, RoutedEventArgs e)
        {
            OrderClient OrderClient = new();
            OrderClient.Show();
            Close();
        }

        private void Button_Warehouse(object sender, RoutedEventArgs e)
        {
            WarehouseList WarehouseList = new();
            WarehouseList.Show();
            Close();
        }

        private void Button_Product(object sender, RoutedEventArgs e)
        {
            ProductCategory ProductCategory = new();
            ProductCategory.Show();
            Close();
        }

        private void Button_Clients(object sender, RoutedEventArgs e)
        {
            ClientsList ClientsList = new();
            ClientsList.Show();
            Close();
        }

        private void Button_Supplier(object sender, RoutedEventArgs e)
        {
            SupplierList SupplierList = new();
            SupplierList.Show();
            Close();
        }

        private void Button_About(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Pedals and mods are handmade\r\nby Priscilla Custom Effects in Russia.\nGitHub: https://github.com/masajinobe-ef/pfx-workshop\n©Priscilla Fx", "Информация", MessageBoxButton.OK, MessageBoxImage.Information);
        }
    }
}
