using AirTech_Assessment.Helpers;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text;

namespace AirTech_Assessment.DataHelpers
{
    /// <summary>
    /// The class responsible for parsing the data stored in the JSON file
    /// which contains the order details.
    /// </summary>
    public class Orders
    {
        public string OrderId { get; set; }
        public string Destination { get; set; }

        public static List<Orders> GetOrdersList(string strOrdersListPath)
        {
            string strJSON = Utils.LoadJson(strOrdersListPath);
            List<Orders> orders = new List<Orders>();
            JObject results = JObject.Parse(strJSON);

            foreach (var result in results)
            {
                Orders order = _populateOrder(result); 
                orders.Add(order);
            }

            return orders;
        }

        private static Orders _populateOrder(KeyValuePair<string, JToken> result)
        {
            Orders order = new Orders();
            order.OrderId = result.Key;
            order.Destination = (string)result.Value["destination"];
            return order;
        }
    }
}
