using System;
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

namespace BingMapWPFApplication.PagesXAML
{
    /// <summary>
    /// Interaction logic for AskZIP.xaml
    /// </summary>
    public partial class AskZIP : Window
    {
        public AskZIP()
        {
            InitializeComponent();
        }

        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            FedExLocator fedexLocator = new FedExLocator(txtZip.Text);
            fedexLocator.Show();
            this.Close();
        }
    }
}
