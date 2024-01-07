using Npgsql;
using System;
using System.Windows;

namespace WORKSHOP.Warehouse.Actions
{
    public partial class AddSupplier : Window
    {
        public AddSupplier()
        {
            InitializeComponent();
        }

        readonly string conString = "Host=127.0.0.1;Port=5432;Database=postgres;Username=postgres;Password=1076;Include Error Detail=true;";
        readonly string sql = "INSERT INTO public.\"Supplier\" " +
            "(s_name, s_site, s_description) " +
            "VALUES (@value1, @value2, @value3);";

        private void Button_Add(object sender, RoutedEventArgs e)
        {
            try
            {
                if (!(string.IsNullOrEmpty(name.Text)
                    || string.IsNullOrEmpty(site.Text)
                    || string.IsNullOrEmpty(description.Text)))
                {
                    using (NpgsqlConnection con = new(conString))
                    {
                        con.Open();

                        using NpgsqlCommand cmd = new(sql, con);
                        cmd.Parameters.AddWithValue("value1", name.Text);
                        cmd.Parameters.AddWithValue("value2", site.Text);
                        cmd.Parameters.AddWithValue("value3", description.Text);
                        cmd.ExecuteNonQuery();

                        con.Close();
                    }
                    SupplierList SupplierList = new();
                    SupplierList.Show();
                    Close();
                }
                else
                {
                    MessageBox.Show("Заполните данные.", "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Ошибка добавления строки.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Button_Close(object sender, RoutedEventArgs e)
        {
            SupplierList SupplierList = new();
            SupplierList.Show();
            Close();
        }
    }
}
