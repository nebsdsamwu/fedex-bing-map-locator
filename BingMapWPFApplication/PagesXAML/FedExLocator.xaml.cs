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
using BingMapWPFApplication.Entities;
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
            List<Location> fedexLocs = GetFedExLocations(zipCode);
            fedExLocatorMap.Focus();
            AddPushpins(fedexLocs);
            //fedExLocatorMap.SetView(center, zoom);
        }

        private void AddPushpins(List<Location> fedexLocs)
        {
            foreach (Location loc in fedexLocs)
            {
                MapLayer layerPin = new MapLayer();
                Pushpin pin = new Pushpin();
                pin.Location = loc;
                MapLayer.SetPosition(pin, new Location(loc.Latitude, loc.Longitude));
                fedExLocatorMap.Children.Add(pin);
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

        private List<Location> GetFedExLocations(string zipCode)
        {
            zipCode = "91748";                         // For test
            Address address = new Address();
            address.PostalCode = zipCode;
            address.CountryCode = "US"; // CountryCode is required
            SearchLocationsResponse repsonse = LocatorBiz.Locate(address);
            return repsonse.Locations;
        }

        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            if (txtTargetZip.Text.Trim() == "")
            {
                MessageBox.Show("Please enter a ZIP code");
            }
            else
            {
                FedExLocator fedexLocator = new FedExLocator(txtTargetZip.Text);
                fedexLocator.Show();
                this.Close();
                MessageBox.Show(txtTargetZip.Text);
            }
        }
    }
}
