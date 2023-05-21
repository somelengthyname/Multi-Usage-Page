using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace MyRestServices
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService1" in both code and config file together.
    [ServiceContract]
    public interface IService1
    {

        [OperationContract]
        [WebGet(UriTemplate = "findNearestStore?x={zipcode}&y={storeName}", ResponseFormat = WebMessageFormat.Json)]
        List<StoreObject> findNearestStore(string zipcode, string storeName);

        [OperationContract]
        [WebGet(UriTemplate = "crimedata?x={zipcode}", ResponseFormat = WebMessageFormat.Json)]
        DataC crimedata(string zipcode);

        [OperationContract]
        [WebGet(UriTemplate = "NewsFocus?x={topic}&y={sortBy}&z={fromType}", ResponseFormat = WebMessageFormat.Json)]
        NewsObject NewsFocus(string topic, string sortBy, string fromType);
        // TODO: Add your service operations here
    }


}
