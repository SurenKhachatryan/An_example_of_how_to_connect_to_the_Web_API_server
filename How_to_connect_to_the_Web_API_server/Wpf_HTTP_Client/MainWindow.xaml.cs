using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Script.Serialization;
using System.Windows;
using Wpf_HTTP_Client.Models;
using System.Linq;
using System.Net.Http.Formatting;

namespace Wpf_HTTP_Client
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private const string url = "http://localhost:54754/api/Users";
        private HttpClient client = new HttpClient();
        private HttpResponseMessage response;
        private User user;
        private JavaScriptSerializer jsSerializer;

        public MainWindow()
        {
            InitializeComponent();
            GetAllUsers();
        }

        /// <summary>
        /// Создает нового пользователя
        /// </summary>
        private void Add_New_User_Button_Click(object sender, RoutedEventArgs e)
        {
            user = new User()
            {
                Fitst_Name = CreateUserTextBoxFirst_Name.Text,
                Last_Name = CreateUserTextBoxLast_Name.Text,
                Age = int.Parse(CreateUserTextBoxAge.Text)

            };

            response = client.PostAsync(url, user, new JsonMediaTypeFormatter()).Result;

            if (response.StatusCode != HttpStatusCode.Created)
                MessageBox.Show("Error 404 Not Found");
            else
            {
                CreateUserTextBoxFirst_Name.Clear();
                CreateUserTextBoxLast_Name.Clear();
                CreateUserTextBoxAge.Clear();
                GetAllUsers();
            }

        }

        /// <summary>
        /// Получает Пользователя по определенному ID и присваивает значения к TextBox-ам
        /// </summary>
        private void UpdateUserButtonGetFromUserID_Click(object sender, RoutedEventArgs e)
        {
            if (UpdateUserTextBoxUserID.Text == "")
            {
                MessageBox.Show("Please Enter UserID and try again.");
                return;
            }

            string resposeContent = null;
            response = client.GetAsync(url + $"/{UpdateUserTextBoxUserID.Text}").Result;

            if (response.StatusCode == HttpStatusCode.OK)
            {
                jsSerializer = new JavaScriptSerializer();

                resposeContent = response.Content.ReadAsStringAsync().Result;
                user = jsSerializer.Deserialize<User>(resposeContent);

                UpdateUserTextBoxFirst_Name.Text = user.Fitst_Name;
                UpdateUserTextBoxLast_Name.Text = user.Last_Name;
                UpdateUserTextBoxAge.Text = user.Age.ToString();
            }
            else
                MessageBox.Show("Error 404 Not Found");
        }

        /// <summary>
        /// Получает данные о всех пользователей
        /// </summary>
        private void ButtonGetAllUsers_Click(object sender, RoutedEventArgs e)
        {
            GetAllUsers();
        }

        /// <summary>
        /// Получает данные о всех пользователей
        /// </summary>
        private async void GetAllUsers()
        {
            string resposeContent = null;

            response = client.GetAsync(url).Result;

            if (response.StatusCode == HttpStatusCode.OK)
            {
                jsSerializer = new JavaScriptSerializer();
                resposeContent = await response.Content.ReadAsStringAsync();
                DataGridUser.ItemsSource = jsSerializer.Deserialize<List<User>>(resposeContent);
            }
            else
                MessageBox.Show("Error 404 Not Found");
        }
    }
}
