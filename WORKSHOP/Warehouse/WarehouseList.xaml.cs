using Npgsql;
using System.Data;
using System.Windows;
using WORKSHOP.Warehouse.Actions;

namespace WORKSHOP.Warehouse
{
    public partial class WarehouseList : Window
    {
        private readonly DataSet ds1 = new();
        private readonly DataTable dt1 = new();

        private readonly DataSet ds2 = new();
        private readonly DataTable dt2 = new();

        readonly string conString = "Host=127.0.0.1;Port=5432;Database=postgres;Username=postgres;Password=1076;Include Error Detail=true;";
        readonly string sql1 = ("SELECT " +
            "public.\"Warehouse\".sp_id, " +
            "public.\"Supply\".sp_item, " +
            "public.\"Supply\".sp_date, " +
            "public.\"Supply\".sp_total, " +
            "public.\"Supply\".s_id, " +
            "public.\"Warehouse\".w_action, " +
            "public.\"Warehouse\".w_time " +
            "FROM public.\"Warehouse\" " +
            "JOIN public.\"Supply\" ON public.\"Warehouse\".sp_id = public.\"Supply\".sp_id");

        readonly string sql2 = ("SELECT public.\"Warehouse\".p_id, " +
            "public.\"Product\".p_name, " +
            "public.\"Product\".p_price, " +
            "public.\"Warehouse\".w_action, " +
            "public.\"Warehouse\".w_time " +
            "FROM public.\"Warehouse\" " +
            "JOIN public.\"Product\" ON public.\"Warehouse\".p_id = public.\"Product\".p_id");
        public WarehouseList()
        {
            InitializeComponent();
            var con = new NpgsqlConnection(conString);
            con.Open();

            NpgsqlDataAdapter adapter1 = new(sql1, con);
            ds1.Reset();
            adapter1.Fill(ds1);
            dt1 = ds1.Tables[0];
            Supply_DG.ItemsSource = dt1.DefaultView;

            NpgsqlDataAdapter adapter2 = new(sql2, con);
            ds2.Reset();
            adapter2.Fill(ds2);
            dt2 = ds2.Tables[0];
            Product_DG.ItemsSource = dt2.DefaultView;

            con.Close();
        }

        private void Button_Supply(object sender, RoutedEventArgs e)
        {
            AddSupply AddSupply = new();
            AddSupply.Show();
            Close();
        }

        private void Button_Delete(object sender, RoutedEventArgs e)
        {
            DeleteSupply DeleteSupply = new();
            DeleteSupply.Show();
            Close();
        }

        private void Button_Supplier(object sender, RoutedEventArgs e)
        {
            SupplierList SupplierList = new();
            SupplierList.Show();
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
