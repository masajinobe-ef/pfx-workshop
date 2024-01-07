using Npgsql;
using System;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Input;

namespace WORKSHOP
{
    public partial class AddOrder : Window
    {
        public AddOrder()
        {
            InitializeComponent();
        }

        readonly string conString = "Host=127.0.0.1;Port=5432;Database=postgres;Username=postgres;Password=1076;Include Error Detail=true;";
        readonly string sql = "INSERT INTO public.\"Order\" " +
            "(c_id, p_id, o_date) " +
            "VALUES (@value1, @value2, @value3);";
        private void Button_Add(object sender, RoutedEventArgs e)
        {
            try
            {
                if (!(string.IsNullOrEmpty(client.Text)
                    || string.IsNullOrEmpty(product.Text)
                    || string.IsNullOrEmpty(date.Text)))
                {
                    using (NpgsqlConnection con = new(conString))
                    {
                        con.Open();

                        using NpgsqlCommand cmd = new(sql, con);
                        cmd.Parameters.AddWithValue("value1", Int32.Parse(client.Text));
                        cmd.Parameters.AddWithValue("value2", Int32.Parse(product.Text));
                        cmd.Parameters.AddWithValue("value3", date.Text);
                        cmd.ExecuteNonQuery();

                        con.Close();
                    }
                    OrderClient OrderClient = new();
                    OrderClient.Show();
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

        private void Client(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void Product(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void Date_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            date.Text = DateTime.Now.ToString("dd-MM-yyyy");
        }

        private void Button_Close(object sender, RoutedEventArgs e)
        {
            OrderClient OrderClient = new();
            OrderClient.Show();
            Close();
        }
    }
}
