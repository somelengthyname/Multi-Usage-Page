using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyRestServices
{

    public class StoreObject
    {
        //public RootLongLat storeObject { get; set; }
        public string name { get; set; }
        public string address { get; set; }
    }

    public class RootLongLat
    {
        public List<DataResults> data { get; set; }
    }

    public class DataResults
    {
        //public List<Results> results { get; set; }
        public string latitude { get; set; }
        public string longitude { get; set; }
        public string label { get; set; }
        public string name { get; set; }
        public string type { get; set; }
        public string number { get; set; }
        public string street { get; set; }
        public string postal_code { get; set; }
        public int confidence { get; set; }
        public string region { get; set; }
        public string region_code { get; set; }
        public string map_url { get; set; }
    }

    public class Root
    {
        public string authenticationResultCode { get; set; }
        public string errorMessage { get; set; }
        public string copyright { get; set; }
        public List<ResourceSet> resourceSets { get; set; }

    }

    public class ResourceSet
    {
        public int estimatedTotal { get; set; }
        public List<Resource> resources { get; set; }
    }

    public class Resource
    {
        public string name { get; set; }
        public Point point { get; set; }
        public string PhoneNumber { get; set; }
        public string entityType { get; set; }
        public address Address { get; set; }

    }

    public class Point
    {
        public string type { get; set; }
        public List<string> coordinates { get; set; }
    }

    public class address
    {
        public string addressLine { get; set; }
        public string adminDistrict { get; set; }
        public string countryRegion { get; set; }
        public string formattedAddress { get; set; }
        public string locality { get; set; }
        public string postalCode { get; set; }

    }
}