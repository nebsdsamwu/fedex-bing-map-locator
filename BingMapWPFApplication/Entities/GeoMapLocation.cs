using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GlobalShipAddressWebServiceClient.LocationsServiceWebReference;

namespace BingMapWPFApplication.Entities
{
    public class GeoMapLocation
    {
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public LocationDetail LocationInfo { get; set; }
    }
}
