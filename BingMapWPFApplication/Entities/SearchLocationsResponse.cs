using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GlobalShipAddressWebServiceClient.LocationsServiceWebReference;
using Microsoft.Maps.MapControl.WPF;

namespace BingMapWPFApplication.Entities
{
    public class SearchLocationsResponse
    {
        private string totalResultsAvailableField;

        private string resultsReturnedField;

        private Address formattedAddressField;

        private AddressToLocationRelationshipDetail[] addressToLocationRelationshipsField;

        public bool Succeeded { get; set; }

        public List<Location> Locations { get; set; }

        public List<Exception> Errors { get; set; }

        public List<Notification> Notifications { get; set; }

        public SearchLocationsResponse()
        {
            this.Succeeded = false;
            this.Errors = new List<Exception>();
            this.Locations = new List<Location>();
            this.Notifications = new List<Notification>();
        }

        public string TotalResultsAvailable
        {
            get
            {
                return this.totalResultsAvailableField;
            }
            set
            {
                this.totalResultsAvailableField = value;
            }
        }

        public string ResultsReturned
        {
            get
            {
                return this.resultsReturnedField;
            }
            set
            {
                this.resultsReturnedField = value;
            }
        }

        public Address FormattedAddress
        {
            get
            {
                return this.formattedAddressField;
            }
            set
            {
                this.formattedAddressField = value;
            }
        }

        public AddressToLocationRelationshipDetail[] AddressToLocationRelationships
        {
            get
            {
                return this.addressToLocationRelationshipsField;
            }
            set
            {
                this.addressToLocationRelationshipsField = value;
            }
        }
    }
}
