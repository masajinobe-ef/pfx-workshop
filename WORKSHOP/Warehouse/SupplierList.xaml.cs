using Npgsql;
using System.Data;
using System.Windows;
using WORKSHOP.Warehouse.Actions;

namespace WORKSHOP.Warehouse
{
    public partial class SupplierList : Window
    {
        private readonly DataSet ds = new();
        private readonly DataTable dt = new();

        readonly string conString = "Host=127.0.0.1;Port=5432;Database=postgres;Username=postgres;Password=1076;Include Error Detail=true;";
        readonly string sql = ("SELECT * FROM public.\"Supplier\";");
        public SupplierList()
        {
            InitializeComponent();
            var con = new NpgsqlConnection(conString);
            con.Open();

            NpgsqlDataAdapter adapter = new(sql, con);
            ds.Reset();
            adapter.Fill(ds);
            dt = ds.Tables[0];
            Supplier_DG.ItemsSource = dt.DefaultView;

            con.Close();
        }

        private void Button_Add(object sender, RoutedEventArgs e)
        {
            AddSupplier AddSupplier = new();
            AddSupplier.Show();
            Close();
        }

        private void Button_Delete(object sender, RoutedEventArgs e)
        {
            DeleteSupplier DeleteSupplier = new();
            DeleteSupplier.Show();
            Close();
        }

        private void Button_Edit(object sender, RoutedEventArgs e)
        {
            EditSupplier EditSupplier = new();
            EditSupplier.Show();
            Close();
        }

        private void Button_Close(object sender, RoutedEventArgs e)
        {
            WarehouseList WarehouseList = new();
            WarehouseList.Show();
            Close();
        }
    }
}
