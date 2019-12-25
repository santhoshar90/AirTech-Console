using AirTech_Assessment.Common;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace AirTech_Assessment.DataHelpers
{
    /// <summary>
    /// The class that helps in tabluating the details of the orders with the flight
    /// that is associated to it.
    /// </summary>
    //order: order-001, flightNumber: 1, departure: <departure_city>, arrival: <arrival_city>, day: x
    public class PlaneOrderDetails
    {
        public string Order { get; set; }

        public int FlightNumber { get; set; }

        public string Departure { get; set; }
        public string Arrival { get; set; }

        public int Day { get; set; }

        public bool IsLoaded { get; set; }

        /// <summary>
        /// The method which takes all the schedules and orders as input, then returns the 
        /// flight the order is associated to it.
        /// </summary>
        /// <param name="lstOrders"></param>
        /// <param name="lstSchedules"></param>
        /// <returns></returns>
        public static List<PlaneOrderDetails> GetPlaneOrderDetails(List<Orders> lstOrders, List<Schedule> lstSchedules)
        {
            // Taking the flight load capacity from appsetting, so that in future if capacity
            // changes, we just need to change the value in appsettings.json
            AppSettings settings = AppSettings.GetSettings();
            int planeLoadCapacity = settings.PlaneLoadCapacity;

            //Storing all the Flight Inventory Details to a dictionary so that we can
            // track all the flights with their inventories.
            Dictionary<int, FlightInventoryDetails> dicLoadingDetails = new Dictionary<int, FlightInventoryDetails>();
            foreach (Schedule scheduleitem in lstSchedules)
            {
                FlightInventoryDetails flightInventoryDetails = FlightInventoryDetails.PopulateFlightInventoryDetails(scheduleitem);
                dicLoadingDetails.Add(scheduleitem.Flight, flightInventoryDetails);
            }

            List<PlaneOrderDetails> lstDetails = new List<PlaneOrderDetails>();
            // iterating through each order item and assigning it to the appropriate flight
            // based on destination and load capacity and returning the details             
            foreach (Orders orderItem in lstOrders)
            {
                PlaneOrderDetails orderDetails = new PlaneOrderDetails();
                orderDetails.Order = orderItem.OrderId;
                orderDetails.IsLoaded = false;
                foreach (KeyValuePair<int, FlightInventoryDetails> kvp in dicLoadingDetails)
                {
                    if (orderItem.Destination.ToLower() == kvp.Value.Schedule.Arrival.ToLower() && !kvp.Value.LoadComplete)
                    {
                        orderDetails.IsLoaded = true;
                        kvp.Value.Orders.Add(orderItem);
                        kvp.Value.LoadCount++;
                        if (kvp.Value.LoadCount == planeLoadCapacity)
                        {
                            kvp.Value.LoadComplete = true;
                        }
                        orderDetails.Arrival = kvp.Value.Schedule.Arrival;
                        orderDetails.Day = kvp.Value.Schedule.Day;
                        orderDetails.FlightNumber = kvp.Key;
                        orderDetails.Departure = kvp.Value.Schedule.Departure;

                        break;
                    }
                }
                lstDetails.Add(orderDetails);
            }

            return lstDetails;
        }
       

    }

}
