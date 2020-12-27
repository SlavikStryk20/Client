using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
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
    /// Interaction logic for AddBox.xaml
    /// </summary>
    public partial class AddBox : Window
    {
        SqlDataAdapter adapter;
        DataTable userstable;
        public AddBox()
        {
            InitializeComponent();
        }
        private string GetConnectinString()
        {
            return ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (PiBSender.Text.Length > 10 && PiBReceive.Text.Length > 10)
            {
                string nameS, surnameS;
                string nameR, surnameR;
                var pibS = PiBSender.Text.Split();
                var pibR = PiBReceive.Text.Split();
                surnameS = pibS[0];
                surnameR = pibR[0];
                nameS = pibS[1];
                nameR = pibR[1];

                string query = "Sp_InsertCargo";
                userstable = new DataTable();
                SqlConnection connection;
                try
                {
                    SqlConnection sqlConnection = new SqlConnection(GetConnectinString());
                    sqlConnection.Open();
                    string post = "20";
                    Random rnd = new Random();
                    post += $"{rnd.Next(10000, 99999)}";
                    SqlCommand sqlCommand = new SqlCommand();
                    sqlCommand.CommandType = CommandType.StoredProcedure;
                    sqlCommand.CommandText = "sp_InsertCargos";
                    sqlCommand.Parameters.AddWithValue("@description", Description.Text);
                    sqlCommand.Parameters.AddWithValue("@citySender", citySender.Text);
                    sqlCommand.Parameters.AddWithValue("@cityReceive", cityReceive.Text);
                    sqlCommand.Parameters.AddWithValue("@phoneNumberSender", phoneNumberSender.Text);
                    sqlCommand.Parameters.AddWithValue("@phoneNumberReceive", phoneNumberReceive.Text);
                    sqlCommand.Parameters.AddWithValue("@nameSender", nameS);
                    sqlCommand.Parameters.AddWithValue("@surNameSender", surnameS);
                    sqlCommand.Parameters.AddWithValue("@nameReceiver", nameR);
                    sqlCommand.Parameters.AddWithValue("@surNameReceive", surnameR);
                    sqlCommand.Parameters.AddWithValue("@price", Price.Text);
                    sqlCommand.Parameters.AddWithValue("@post", post);
                    sqlCommand.Parameters.AddWithValue("@dateSend", DateTime.Now);
                    sqlCommand.Connection = sqlConnection;
                    sqlCommand.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }
    }
}
