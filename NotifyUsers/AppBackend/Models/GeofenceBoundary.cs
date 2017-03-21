using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;

namespace AppBackend.Models
{

    public class Rootobject
    {
        public D d { get; set; }
    }

    public class D
    {
        public string __copyright { get; set; }
        public Result[] results { get; set; }
    }

    public class Result
    {
        public __Metadata __metadata { get; set; }
        public string EntityID { get; set; }
        public string Name { get; set; }
        public float Longitude { get; set; }
        public float Latitude { get; set; }
        public string Boundary { get; set; }
        public string Confidence { get; set; }
        public string Locality { get; set; }
        public string AddressLine { get; set; }
        public string AdminDistrict { get; set; }
        public string CountryRegion { get; set; }
        public string PostalCode { get; set; }
    }

    public class __Metadata
    {
        public string uri { get; set; }
    }
    

}