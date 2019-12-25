using AirTech_Assessment.Common;
using AirTech_Assessment.DataHelpers;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;

namespace AirTech_Assessment
{
    class Program
    {
        static void Main(string[] args)
        {
            //Getting the settings from appsettings.json file.
            AppSettings settings = AppSettings.GetSettings();

            //User story - 1
            List<Schedule> lstSchedule = Schedule.GetScheduleList(settings.SchedulePath);

            Console.WriteLine("FLIGHT SCHEDULES");
            Console.WriteLine("---------------------------------");
            foreach (Schedule item in lstSchedule)
            {
                Console.WriteLine(string.Format("Flight: {0}, departure: {1}, arrival: {2}, day: {3}", item.Flight, item.Departure, item.Arrival, item.Day));
            }
            Console.WriteLine("---------------------------------");
            //User story - 2
            List<Orders> lstOrders = Orders.GetOrdersList(settings.OrderPath);
            List<PlaneOrderDetails> planeLoadOrderDetails = PlaneOrderDetails.GetPlaneOrderDetails(lstOrders, lstSchedule);
            Console.WriteLine("ORDER DETAILS");
            Console.WriteLine("---------------------------------");
            foreach (PlaneOrderDetails order in planeLoadOrderDetails)
            {
                int LoadCapacity = settings.PlaneLoadCapacity;
                if (order.IsLoaded)
                {
                    Console.WriteLine(string.Format("order: {0}, flightNumber: {1}, departure: {2}, arrival:{3}, day: {4}", order.Order, order.FlightNumber, order.Departure, order.Arrival, order.Day));
                }
                else
                {
                    Console.WriteLine(string.Format("order: {0}, flightNumber: Not scheduled", order.Order));
                }
            }
            Console.WriteLine("---------------------------------");
            Console.Read();


        }
      
    }
}
