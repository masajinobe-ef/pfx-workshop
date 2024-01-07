using Npgsql;
using System;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Input;

namespace WORKSHOP
{
    public partial class EditItem : Window
    {
        public EditItem()
        {
            InitializeComponent();
        }

        readonly string conString = "Host=127.0.0.1;Port=5432;Database=postgres;Username=postgres;Password=1076;Include Error Detail=true;";
        readonly string sql_product = "UPDATE public.\"Product\" " +
            "SET p_brand = @value1, " +
            "p_name = @value2, " +
            "p_description = @value3, " +
            "p_price = @value4, " +
            "p_category = @value5 " +
            "WHERE p_id = @value6;";
        private void Button_Edit(object sender, RoutedEventArgs e)
        {
            try
            {
                if (!(string.IsNullOrEmpty(brand.Text)
                || string.IsNullOrEmpty(name.Text)
                || string.IsNullOrEmpty(description.Text)
                || string.IsNullOrEmpty(price.Text)
                || string.IsNullOrEmpty(category.Text)
                || string.IsNullOrEmpty(number.Text)))
                {
                    using (NpgsqlConnection con = new(conString))
                    {
                        con.Open();

                        using NpgsqlCommand cmd = new(sql_product, con);
                        cmd.Parameters.AddWithValue("value1", brand.Text);
                        cmd.Parameters.AddWithValue("value2", name.Text);
                        cmd.Parameters.AddWithValue("value3", description.Text);
                        cmd.Parameters.AddWithValue("value4", Int32.Parse(price.Text));
                        cmd.Parameters.AddWithValue("value5", category.Text);
                        cmd.Parameters.AddWithValue("value6", Int32.Parse(number.Text));
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
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                MessageBox.Show("Ошибка редактирования строки.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Price(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void Quantity(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void Limits_id(object sender, TextCompositionEventArgs e)
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
