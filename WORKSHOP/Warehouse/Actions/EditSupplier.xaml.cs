using Npgsql;
using System.Text.RegularExpressions;
using System;
using System.Windows;
using System.Windows.Input;

namespace WORKSHOP.Warehouse.Actions
{
    public partial class EditSupplier : Window
    {
        public EditSupplier()
        {
            InitializeComponent();
        }

        readonly string conString = "Host=127.0.0.1;Port=5432;Database=postgres;Username=postgres;Password=1076;";
        readonly string sql = "UPDATE public.\"Supplier\" " +
            "SET s_name = @value1, " +
            "s_site = @value2, " +
            "s_description = @value3 " +
            "WHERE s_id = @id;";
        private void Button_Edit(object sender, RoutedEventArgs e)
        {
            try
            {
                if (!(string.IsNullOrEmpty(name.Text)
                || string.IsNullOrEmpty(site.Text)
                || string.IsNullOrEmpty(description.Text)
                || string.IsNullOrEmpty(number.Text)))
                {
                    using (NpgsqlConnection con = new(conString))
                    {
                        con.Open();

                        using NpgsqlCommand cmd = new(sql, con);
                        cmd.Parameters.AddWithValue("value1", name.Text);
                        cmd.Parameters.AddWithValue("value2", site.Text);
                        cmd.Parameters.AddWithValue("value3", description.Text);
                        cmd.Parameters.AddWithValue("id", Int32.Parse(number.Text));
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
            SupplierList SupplierList = new();
            SupplierList.Show();
            Close();
        }
    }
}