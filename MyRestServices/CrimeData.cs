using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyRestServices
{
    public class DataC
    {
        //public CrimeData data { get; set; }
        //public string info { get; set; }
        public string state { get; set; }
        public string fromDate { get; set; }
        public string toDate { get; set; }

        public int violent_crime { get; set; }
        public int homicide { get; set; }
        public int robbery { get; set; }
        public int aggAssault { get;set ; }

        public int property_crime { get; set; }
        public int burgarly { get; set; }
        public int larceny { get; set; }
        public int motor_theft { get;set; }
        public int arson { get; set; }
    }
    public class CrimeData
    {
        public List<ResultsCrime> results { get; set; }
        public Page pagination { get; set; }
    }

    public class Page
    {
        public int count { get; set; }
        public int page { get; set;}
        public int pages { get; set; }
        public int per_page { get; set; }
    }

    public class ResultsCrime
    {
        public int state_id { get; set; }
        public string state_abbr { get;set; }
        public int year { get; set; }
        public int population { get; set; }
        public int violent_crime { get; set; }
        public int homicide { get; set; }
        public string rape_legacy { get; set; }
        public string rape_revised { get; set; }
        public int robbery { get; set; }
        public int aggravated_assault { get; set; }
        public int property_crime { get; set; }
        public int burgarly { get; set; }
        public int larceny { get; set; }
        public int motor_vehicle_theft { get; set; }
        public int arson { get; set; }
    }

    public class Location
    {
        public List<DataResults> data { get; set; }
    }

    
}