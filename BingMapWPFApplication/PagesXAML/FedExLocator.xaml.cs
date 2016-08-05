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

// for BingMapWPFApplication
using System.Globalization;
using Microsoft.Maps.MapControl.WPF;
using Microsoft.Maps.MapControl.WPF.Design;
using BingMapWPFApplication.LocatorLogic;
using GlobalShipAddressWebServiceClient.LocationsServiceWebReference;

namespace BingMapWPFApplication
{
    /// <summary>
    /// Interaction logic for SwitchMapViews.xaml
    /// </summary>
    public partial class FedExLocator : Window
    {
        LocationConverter locConvertor = new LocationConverter();

        public FedExLocator(string zipCode)
        {
            InitializeComponent();
            //SearchByZip(zipCode);
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

        private void SearchByZip(string zipCode)
        {
            Address address = new Address();
            address.PostalCode = zipCode;
            address.CountryCode = "US"; // CountryCode is required
            LocatorBiz.Locate(address);
            MessageBox.Show(zipCode);
        }

        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            //if (TargetZip.Trim() == "")
            //{
            //    MessageBox.Show("Please enter a ZIP code");
            //}
            //else
            {
                FedExLocator fedexLocator = new FedExLocator(txtTargetZip.Text);
                fedexLocator.Show();
                this.Close();
                MessageBox.Show(txtTargetZip.Text);
            }
        }
    }
}
