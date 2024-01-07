using Npgsql;
using System;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Input;

namespace WORKSHOP
{
    public partial class EditClient : Window
    {
        public EditClient()
        {
            InitializeComponent();
        }

        readonly string conString = "Host=127.0.0.1;Port=5432;Database=postgres;Username=postgres;Password=1076;";
        readonly string sql_client = "UPDATE public.\"Client\" " +
            "SET c_fio = @value1, " +
            "c_city = @value2, " +
            "c_address = @value3, " +
            "c_phone = @value4 " +
            "WHERE c_id = @id;";
        private void Button_Edit(object sender, RoutedEventArgs e)
        {
            try
            {
                if (!(string.IsNullOrEmpty(fio.Text)
                || string.IsNullOrEmpty(city.Text)
                || string.IsNullOrEmpty(address.Text)
                || string.IsNullOrEmpty(phone_number.Text)
                || string.IsNullOrEmpty(number.Text)))
                {
                    using (NpgsqlConnection con = new(conString))
                    {
                        con.Open();

                        using NpgsqlCommand cmd = new(sql_client, con);
                        cmd.Parameters.AddWithValue("value1", fio.Text);
                        cmd.Parameters.AddWithValue("value2", city.Text);
                        cmd.Parameters.AddWithValue("value3", address.Text);
                        cmd.Parameters.AddWithValue("value4", phone_number.Text);
                        cmd.Parameters.AddWithValue("id", Int32.Parse(number.Text));
                        cmd.ExecuteNonQuery();

                        con.Close();
                    }
                    ClientsList ClientsList = new();
                    ClientsList.Show();
                    Close();
                }
                else
                {
                    MessageBox.Show("Заполните данные.", "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Ошибка редактирования.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Limits_id(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void Button_Close(object sender, RoutedEventArgs e)
        {
            ClientsList ClientsList = new();
            ClientsList.Show();
            Close();
        }
    }
}
