using System;
using System.Collections.Generic;
using System.Configuration;
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
    /// Interaction logic for FullReg.xaml
    /// </summary>
    public partial class FullReg : Window
    {
        public FullReg()
        {
            InitializeComponent();
        }
        private string GetConnectinString()
        {
            return ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show(GetConnectinString());
        }
    }
}
