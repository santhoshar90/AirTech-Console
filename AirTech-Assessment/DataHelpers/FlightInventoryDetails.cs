using System;
using System.Collections.Generic;
using System.Text;

namespace AirTech_Assessment.DataHelpers
{
    /// <summary>
    /// The class which handled the details of Flight schedule and all the orders
    /// that the Flight is carrying along with the count of orders.
    /// </summary>
    class FlightInventoryDetails
    {
        public Schedule Schedule { get; set; }

        public List<Orders> Orders { get; set; }

        public int LoadCount { get; set; }

        public bool LoadComplete { get; set; }


        internal static FlightInventoryDetails PopulateFlightInventoryDetails(Schedule scheduleitem)
        {
            FlightInventoryDetails flightInventoryDetails = new FlightInventoryDetails();
            flightInventoryDetails.Schedule = scheduleitem;
            flightInventoryDetails.Orders = new List<Orders>();
            return flightInventoryDetails;
        }
    }
}