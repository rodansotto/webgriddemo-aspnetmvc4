using System;
using System.Collections.Generic;

namespace MyMvc4App.Models
{
    public partial class Address
    {
        public Address()
        {
            this.CustomerAddresses = new List<CustomerAddress>();
            this.SalesOrderHeaders = new List<SalesOrderHeader>();
            this.SalesOrderHeaders1 = new List<SalesOrderHeader>();
        }

        public int AddressID { get; set; }
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string City { get; set; }
        public string StateProvince { get; set; }
        public string CountryRegion { get; set; }
        public string PostalCode { get; set; }
        public System.Guid rowguid { get; set; }
        public System.DateTime ModifiedDate { get; set; }
        public virtual ICollection<CustomerAddress> CustomerAddresses { get; set; }
        public virtual ICollection<SalesOrderHeader> SalesOrderHeaders { get; set; }
        public virtual ICollection<SalesOrderHeader> SalesOrderHeaders1 { get; set; }
    }
}
