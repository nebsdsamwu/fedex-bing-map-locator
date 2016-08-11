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
            SearchLocationsResponse locsResp = GetFedExLocations(zipCode);
            if (locsResp.GeoMapLocations.Count > 0)
            {
                fedExLocatorMap.Focus();
                AddPushpins(locsResp);
                GeoMapLocation firstLoc = locsResp.GeoMapLocations.First();
                Location center = new Location(firstLoc.Latitude, firstLoc.Longitude);
                fedExLocatorMap.SetView(center, 12); // zoom in to first location
            }
            else
            {
                MessageBox.Show("Unable to find any FedEx location. \nPlease try another ZIP code.");
            }
        }

        private void AddPushpins(SearchLocationsResponse locsResp)
        {
            MapLayer layerPin = new MapLayer();
            int idx = 0;
            foreach (GeoMapLocation loc in locsResp.GeoMapLocations)
            {
                idx += 1;
                Pushpin pin = new Pushpin();
                pin.Location = new Location(loc.Latitude, loc.Longitude);
                pin.Content = idx;
                string addressStr = ComposeAddress(loc);
                pin.ToolTip = addressStr;

                pin.MouseDown += new MouseButtonEventHandler(PushpinClicked);

                MapLayer.SetPosition(pin, pin.Location);
                fedExLocatorMap.Children.Add(pin);
            }
        }

        private void PushpinClicked(object sender, MouseEventArgs e)
        {
            Pushpin pin = (Pushpin)sender;
            MessageBox.Show(pin.Content.ToString());
        }

        private string ComposeAddress(GeoMapLocation loc)
        {
            Address addr = loc.LocationInfo.LocationContactAndAddress.Address;

            string streetStr = "";
            if (addr.StreetLines.Length > 0)
            {
                for (int i = 0; i < addr.StreetLines.Length; i++)
                {
                    streetStr += addr.StreetLines[i] + "\n";
                }
            }

            string addressStr = streetStr 
                              + addr.City + "\n"
                              + addr.StateOrProvinceCode + "\n"
                              + addr.PostalCode + "\n"
                              + addr.CountryCode + "\n";

            return addressStr;
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

        private SearchLocationsResponse GetFedExLocations(string zipCode)
        {
            zipCode = "91748";                         // For test
            Address address = new Address();
            address.PostalCode = zipCode;
            address.CountryCode = "US"; // CountryCode is required
            SearchLocationsResponse repsonse = LocatorBiz.Locate(address);
            return repsonse;
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
                fedexLocator.Focus();
                this.Close();
            }
        }
    }
}
