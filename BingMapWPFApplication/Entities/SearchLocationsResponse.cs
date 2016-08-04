using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GlobalShipAddressWebServiceClient.LocationsServiceWebReference;

namespace BingMapWPFApplication.Entities
{
    public class SearchLocationsResponse
    {
        private string totalResultsAvailableField;

        private string resultsReturnedField;

        private Address formattedAddressField;

        private AddressToLocationRelationshipDetail[] addressToLocationRelationshipsField;

        private ResponseStatus status;

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

    public class ResponseStatus
    {
        public bool Succeeded { get; set; }
        Exception Error { get; set; }
    }
}
