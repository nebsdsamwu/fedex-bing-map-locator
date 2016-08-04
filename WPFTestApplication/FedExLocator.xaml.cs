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
using System.Windows.Navigation;
using System.Windows.Shapes;

// for BingMapWPFAppication
using System.Globalization;
using Microsoft.Maps.MapControl.WPF;
using Microsoft.Maps.MapControl.WPF.Design;


namespace BingMapWPFAppication
{
    /// <summary>
    /// Interaction logic for SwitchMapViews.xaml
    /// </summary>
    public partial class FedExLocator : Window
    {
        LocationConverter locConvertor = new LocationConverter();

        public FedExLocator()
        {
            InitializeComponent();
            fedExLocatorMap.Focus();
            fedExLocatorMap.ViewChangeOnFrame += new EventHandler<MapEventArgs>(viewMap_ViewChangeOnFrame);
        }

        private void viewMap_ViewChangeOnFrame(object sender, MapEventArgs e)
        {
            Map map = sender as Map;

            if (map != null)
            {
                Location mapCenter = map.Center;
                txtLatitude.Text = string.Format(CultureInfo.InvariantCulture, "{0:F5}", mapCenter.Latitude);
                txtLongitude.Text = string.Format(CultureInfo.InvariantCulture, "{0:F5}", mapCenter.Longitude);
            }
        }

        private void ChangeMapView_Click(object sender, RoutedEventArgs e)
        {
            string[] tagInfo = ((Button)sender).Tag.ToString().Split(' ');
            Location center = (Location)locConvertor.ConvertFrom(tagInfo[0]);
            double zoom = System.Convert.ToDouble(tagInfo[1]);
            fedExLocatorMap.SetView(center, zoom);
        }

        private void AnimationLevel_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBoxItem cbi = (ComboBoxItem)(((ComboBox)sender).SelectedItem);
            string v = cbi.Content as string;
            if (!string.IsNullOrEmpty(v) && fedExLocatorMap != null)
            {
                AnimationLevel newLevel = (AnimationLevel)Enum.Parse(typeof(AnimationLevel), v, true);
                fedExLocatorMap.AnimationLevel = newLevel;
            }
        }

        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            if (TargetZip.Trim() == "")
            {
                MessageBox.Show("Please enter a ZIP code");
            }
            else
            {
                MessageBox.Show(TargetZip);
            }
        }

        public string TargetZip
        {
            get { return txtTargetZip.Text; }
        }
    }
}
