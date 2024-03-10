using System.Windows;

namespace WORKSHOP
{
    public partial class Auth : Window
    {
        public Auth()
        {
            InitializeComponent();
        }

        private void Login_Click(object sender, RoutedEventArgs e)
        {
            string username = txtUsername.Text;
            string password = txtPassword.Password;

            if (username == "admin" && password == "1076")
            {
                MainWindow MainWindow = new();
                MainWindow.Show();
                Close();
            }
            else
            {
                MessageBox.Show("Логин или пароль неверны.");
            }
        }
    }
}
