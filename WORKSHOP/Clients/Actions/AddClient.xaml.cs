using Npgsql;
using System;
using System.Windows;

namespace WORKSHOP.Clients
{
    public partial class AddClient : Window
    {
        public AddClient()
        {
            InitializeComponent();
        }

        readonly string conString = "Host=127.0.0.1;Port=5432;Database=postgres;Username=postgres;Password=1076;Include Error Detail=true;";
        readonly string sql_client = "INSERT INTO public.\"Client\" " +
            "(c_fio, c_city, c_address, c_phone) " +
            "VALUES (@value1, @value2, @value3, @value4);";
        private void Button_Add(object sender, RoutedEventArgs e)
        {
            try
            {
                if (!(string.IsNullOrEmpty(fio.Text)
                    || string.IsNullOrEmpty(city.Text)
                    || string.IsNullOrEmpty(address.Text)
                    || string.IsNullOrEmpty(phone_number.Text)))
                {
                    using (NpgsqlConnection con = new(conString))
                    {
                        con.Open();

                        using NpgsqlCommand cmd = new(sql_client, con);
                        cmd.Parameters.AddWithValue("value1", fio.Text);
                        cmd.Parameters.AddWithValue("value2", city.Text);
                        cmd.Parameters.AddWithValue("value3", address.Text);
                        cmd.Parameters.AddWithValue("value4", phone_number.Text);
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
                MessageBox.Show("Ошибка добавления строки.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Button_Close(object sender, RoutedEventArgs e)
        {
            ClientsList ClientsList = new();
            ClientsList.Show();
            Close();
        }
    }
}
