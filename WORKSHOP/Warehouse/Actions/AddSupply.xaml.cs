using Npgsql;
using System.Text.RegularExpressions;
using System;
using System.Windows;
using System.Windows.Input;

namespace WORKSHOP.Warehouse.Actions
{
    public partial class AddSupply : Window
    {
        public AddSupply()
        {
            InitializeComponent();
        }

        readonly string conString = "Host=127.0.0.1;Port=5432;Database=postgres;Username=postgres;Password=1076;Include Error Detail=true;";
        readonly string sql = "INSERT INTO public.\"Supply\" " +
            "(sp_item, sp_date, sp_total, s_id) " +
            "VALUES (@value1, @value2, @value3, @value4);";

        private void Button_Add(object sender, RoutedEventArgs e)
        {
            try
            {
                if (!(string.IsNullOrEmpty(item.Text)
                    || string.IsNullOrEmpty(date.Text)
                    || string.IsNullOrEmpty(total.Text)
                    || string.IsNullOrEmpty(supplier.Text)))
                {
                    using (NpgsqlConnection con = new(conString))
                    {
                        con.Open();

                        using NpgsqlCommand cmd = new(sql, con);
                        cmd.Parameters.AddWithValue("value1", item.Text);
                        cmd.Parameters.AddWithValue("value2", date.Text);
                        cmd.Parameters.AddWithValue("value3", Int32.Parse(total.Text));
                        cmd.Parameters.AddWithValue("value4", Int32.Parse(supplier.Text));
                        cmd.ExecuteNonQuery();

                        con.Close();
                    }
                    WarehouseList WarehouseList = new();
                    WarehouseList.Show();
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

        private void Date_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            date.Text = DateTime.Now.ToString("dd-MM-yyyy");
        }

        private void Total(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void Supplier(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void Button_Close(object sender, RoutedEventArgs e)
        {
            WarehouseList WarehouseList = new();
            WarehouseList.Show();
            Close();
        }
    }
}
