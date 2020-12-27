using System;
using System.Data.SqlClient;
using System.Data;
using System.Configuration.Assemblies;
using System.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Client
{
    /// <summary>
    /// Interaction logic for Reg_Log.xaml
    /// </summary>
    public partial class Reg_Log : Window
    {
        SqlDataAdapter adapter;
        DataTable userstable;
        private string phone_number;
        private string password;
        public string Phone_number { get { return phone_number; } }
        public string Password { get { return password; } }
        public Reg_Log()
        {
            InitializeComponent();
            ConnectDB();
        }
        private string GetConnectinString()
        {
            return ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
        }
        private void num_TextChanged(object sender, TextChangedEventArgs e)
        {
            phone_number = num.Text;
        }

        private void ConnectDB()
        {
            string sql = "SELECT * from Users";
            userstable = new DataTable();
            SqlConnection connection;
            try
            {
                connection = new SqlConnection(GetConnectinString());
                SqlCommand command = new SqlCommand(sql, connection);
                adapter = new SqlDataAdapter(command);
                adapter.InsertCommand = new SqlCommand("sp_InsertUsers", connection);
                adapter.InsertCommand.CommandType = CommandType.StoredProcedure;
                adapter.InsertCommand.Parameters.Add(new SqlParameter("@name", SqlDbType.NChar, 30, "Name"));
                adapter.InsertCommand.Parameters.Add(new SqlParameter("@surname", SqlDbType.NChar, 30, "SurName"));
                adapter.InsertCommand.Parameters.Add(new SqlParameter("@email", SqlDbType.NChar, 40, "[E-Mail]"));
                adapter.InsertCommand.Parameters.Add(new SqlParameter("@password", SqlDbType.NChar, 40, "Password"));
                adapter.InsertCommand.Parameters.Add(new SqlParameter("@city", SqlDbType.NChar, 20, "City"));
                adapter.InsertCommand.Parameters.Add(new SqlParameter("@numbuild", SqlDbType.Int, 10, "NumBuild"));
                SqlParameter parameter = adapter.InsertCommand.Parameters.Add("@id", SqlDbType.Int, 0, "id");
                parameter.Direction = ParameterDirection.Output;

                connection.Open();
                adapter.Fill(userstable);
            }
            catch (Exception ex)
            { MessageBox.Show(ex.Message); }


        }
        private void pass_TextChanged(object sender, TextChangedEventArgs e)
        {
            password = pass.Text;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            SqlConnection connection = new SqlConnection();
            connection.ConnectionString = GetConnectinString();
            connection.Open();

            SqlCommand command = new SqlCommand();
            command.CommandText = "insert into Users(Password,PhoneNumber)values(@password,@phonenumber)";
            command.Parameters.AddWithValue("@password", pass.Text);
            command.Parameters.AddWithValue("@phonenumber", num.Text);
            command.Connection = connection;
            if (pass.Text.Length >= 4 && num.Text.Length > 5)   
            {
                command.ExecuteNonQuery();
                if (MessageBox.Show("Бажаєте заповнити повну інформацію про себе?","Quest", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    var form = new FullReg();
                    form.WindowStartupLocation = this.WindowStartupLocation;
                    form.ShowDialog();
                }
                this.Close();
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

        }
    }
}
