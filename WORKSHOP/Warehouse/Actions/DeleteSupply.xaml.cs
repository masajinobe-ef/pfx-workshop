using Npgsql;
using System.Text.RegularExpressions;
using System;
using System.Windows;
using System.Windows.Input;

namespace WORKSHOP.Warehouse.Actions
{
    public partial class DeleteSupply : Window
    {
        public DeleteSupply()
        {
            InitializeComponent();
        }

        readonly string conString = "Host=127.0.0.1;Port=5432;Database=postgres;Username=postgres;Password=1076;Include Error Detail=true;";
        private void Button_Delete(object sender, RoutedEventArgs e)
        {
            try
            {
                using (NpgsqlConnection con = new(conString))
                {
                    con.Open();

                    using NpgsqlCommand cmd = new();
                    cmd.Connection = con;
                    cmd.CommandText = "DELETE FROM public.\"Supply\" WHERE sp_id = @id;";
                    cmd.Parameters.AddWithValue("id", Int32.Parse(number.Text));
                    cmd.ExecuteNonQuery();

                    con.Close();
                }
                MessageBox.Show("Строка удалена успешно.", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception)
            {
                MessageBox.Show("Ошибка удаления строки.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Limits_id(object sender, TextCompositionEventArgs e)
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