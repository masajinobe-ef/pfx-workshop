using Npgsql;
using System.Data;
using System.Windows;

namespace WORKSHOP
{
    public partial class ProductCategory : Window
    {
        private readonly DataSet ds = new();
        private readonly DataTable dt = new();

        readonly string conString = "Host=127.0.0.1;Port=5432;Database=postgres;Username=postgres;Password=1076;Include Error Detail=true;";
        readonly string sql = ("SELECT * FROM public.\"Product\";");
        public ProductCategory()
        {
            InitializeComponent();
            var con = new NpgsqlConnection(conString);
            con.Open();

            NpgsqlDataAdapter adapter = new(sql, con);
            ds.Reset();
            adapter.Fill(ds);
            dt = ds.Tables[0];
            Products_DG.ItemsSource = dt.DefaultView;

            con.Close();
        }

        private void Button_Add(object sender, RoutedEventArgs e)
        {
            AddItem AddItem = new();
            AddItem.Show();
            Close();
        }

        private void Button_Delete(object sender, RoutedEventArgs e)
        {
            DeleteItem DeleteItem = new();
            DeleteItem.Show();
            Close();
        }

        private void Button_Edit(object sender, RoutedEventArgs e)
        {
            EditItem EditItem = new();
            EditItem.Show();
            Close();
        }

        private void Button_Close(object sender, RoutedEventArgs e)
        {
            MainWindow MainWindow = new();
            MainWindow.Show();
            Close();
        }
    }
}
