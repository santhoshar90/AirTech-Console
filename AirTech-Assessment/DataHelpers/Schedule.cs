using AirTech_Assessment.Helpers;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text;

namespace AirTech_Assessment.DataHelpers
{
    /// <summary>
    /// The class that deals with the flight schedules.
    /// </summary>
    public class Schedule
    {
        public int Flight { get; set; }
        public string Departure { get; set; }
        public string Arrival { get; set; }
        public int Day { get; set; }

        public static List<Schedule> GetScheduleList(string strSchdulePath)
        {
            
            string strJSON = Utils.LoadJson(strSchdulePath);
            JObject results = JObject.Parse(strJSON);
            List<Schedule> schedules = new List<Schedule>();
            foreach (var result in results["schedulelist"])
            {
                Schedule schedule = new Schedule();
                schedule.Day = (int)result["day"];
                schedule.Flight = (int)result["flight"];
                schedule.Arrival = (string)result["arrival"];
                schedule.Departure = (string)result["departure"];
                schedules.Add(schedule);
            }



            return schedules;


        }

    }
}
