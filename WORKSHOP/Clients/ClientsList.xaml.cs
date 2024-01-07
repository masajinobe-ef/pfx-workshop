using Npgsql;
using System.Data;
using System.Windows;
using WORKSHOP.Clients;
using WORKSHOP.Clients.Actions;

namespace WORKSHOP
{
    public partial class ClientsList : Window
    {
        private readonly DataSet ds = new();
        private readonly DataTable dt = new();

        readonly string conString = "Host=127.0.0.1;Port=5432;Database=postgres;Username=postgres;Password=1076;Include Error Detail=true;";
        readonly string sql = ("SELECT * FROM public.\"Client\";");
        public ClientsList()
        {
            InitializeComponent();
            var con = new NpgsqlConnection(conString);
            con.Open();

            NpgsqlDataAdapter adapter = new(sql, con);
            ds.Reset();
            adapter.Fill(ds);
            dt = ds.Tables[0];
            Clients_DG.ItemsSource = dt.DefaultView;

            con.Close();
        }

        private void Button_Add(object sender, RoutedEventArgs e)
        {
            AddClient AddClient = new();
            AddClient.Show();
            Close();
        }

        private void Button_Delete(object sender, RoutedEventArgs e)
        {
            DeleteClient DeleteClient = new();
            DeleteClient.Show();
            Close();
        }

        private void Button_Edit(object sender, RoutedEventArgs e)
        {
            EditClient EditClient = new();
            EditClient.Show();
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
