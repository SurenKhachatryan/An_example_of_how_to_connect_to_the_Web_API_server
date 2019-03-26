using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Windows;
using Wpf_HTTP_Client.Models;

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

        public MainWindow()
        {
            InitializeComponent();
            GetAllUsers();
        }

        /// <summary>
        /// Создает нового пользователя
        /// </summary>
        private async void Add_New_User_Button_Click(object sender, RoutedEventArgs e)
        {
            user = new User()
            {
                Fitst_Name = CreateUserTextBoxFirst_Name.Text,
                Last_Name = CreateUserTextBoxLast_Name.Text,
                Age = int.Parse(CreateUserTextBoxAge.Text)
            };

            response = await client.PostAsync(url, user, new JsonMediaTypeFormatter());

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
        /// Обновляет Данные Пользователя с определенного ID
        /// </summary>
        private void Button_Click_Update_User_Data(object sender, RoutedEventArgs e)
        {
            user = new User()
            {
                UserId = int.Parse(UpdateUserTextBoxUserID.Text),
                Fitst_Name = UpdateUserTextBoxFirst_Name.Text,
                Last_Name = UpdateUserTextBoxLast_Name.Text,
                Age = int.Parse(UpdateUserTextBoxAge.Text)
            };

            response = client.PutAsync(url + $"/{UpdateUserTextBoxUserID.Text}", user, new JsonMediaTypeFormatter()).Result;

            if (response.StatusCode != HttpStatusCode.OK)
                MessageBox.Show("Error 404 Not Found");
            else
            {
                UpdateUserTextBoxAge.Clear();
                UpdateUserTextBoxFirst_Name.Clear();
                UpdateUserTextBoxLast_Name.Clear();
                GetAllUsers();
            }
        }

        /// <summary>
        /// Удаляет пользователя по определенного ID
        /// </summary>
        private void DeleteUserButtonUserID_Click(object sender, RoutedEventArgs e)
        {
            if (DeleteUserTextBoxUserID.Text == "")
            {
                MessageBox.Show("Please Enter UserID and try again.");
                return;
            }

            response = client.DeleteAsync(url + $"/{DeleteUserTextBoxUserID.Text}").Result;

            if (response.StatusCode != HttpStatusCode.OK)
                MessageBox.Show("Error 404 Not Found");
            else
            {
                DeleteUserTextBoxUserID.Clear();
                GetAllUsers();
            }
        }

        /// <summary>
        /// Получает пользователя по определенному ID и присваивает значения к TextBox-ам
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
                resposeContent = response.Content.ReadAsStringAsync().Result;
                user = JsonConvert.DeserializeObject<User>(resposeContent);

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

            response = await client.GetAsync(url);

            if (response.StatusCode == HttpStatusCode.OK)
            {
                resposeContent = await response.Content.ReadAsStringAsync();
                DataGridUser.ItemsSource = JsonConvert.DeserializeObject<List<User>>(resposeContent);
            }
            else
                MessageBox.Show("Error 404 Not Found");
        }
    }
}
