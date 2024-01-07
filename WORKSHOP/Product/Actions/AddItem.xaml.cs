using Npgsql;
using System;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Input;

namespace WORKSHOP
{
    public partial class AddItem : Window
    {
        public AddItem()
        {
            InitializeComponent();
        }

        readonly string conString = "Host=127.0.0.1;Port=5432;Database=postgres;Username=postgres;Password=1076;Include Error Detail=true;";
        readonly string sql = "INSERT INTO public.\"Product\" " +
            "(p_brand, p_name, p_description, p_price, p_category) " +
            "VALUES (@value1, @value2, @value3, @value4, @value5);";
        private void Button_Add(object sender, RoutedEventArgs e)
        {
            try
            {
                if (!(string.IsNullOrEmpty(brand.Text) 
                    || string.IsNullOrEmpty(name.Text) 
                    || string.IsNullOrEmpty(description.Text) 
                    || string.IsNullOrEmpty(price.Text)
                    || string.IsNullOrEmpty(category.Text)))
                {
                    using (NpgsqlConnection con = new(conString))
                    {
                        con.Open();

                        using NpgsqlCommand cmd = new(sql, con);
                        cmd.Parameters.AddWithValue("value1", brand.Text);
                        cmd.Parameters.AddWithValue("value2", name.Text);
                        cmd.Parameters.AddWithValue("value3", description.Text);
                        cmd.Parameters.AddWithValue("value4", Int32.Parse(price.Text));
                        cmd.Parameters.AddWithValue("value5", category.Text);
                        cmd.ExecuteNonQuery();

                        con.Close();
                    }
                    ProductCategory ProductCategory = new();
                    ProductCategory.Show();
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

        private void Price(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void Button_Close(object sender, RoutedEventArgs e)
        {
            ProductCategory ProductCategory = new();
            ProductCategory.Show();
            Close();
        }
    }
}
