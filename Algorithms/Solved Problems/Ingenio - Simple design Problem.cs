using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Algorithms.Solved_Problems
{
    class Ingenio___Simple_design_Problem
    {

        //Please come up with an object model to represent “Store Hours”.  
        //Store hours are basically weekly schedule of a store’s hours.  See example below.  
        //Just provide the class names, property names with their data types.  No need to provide any additional code including constructors, methods, etc.

        public enum Day { Monday, Tuesday, Wednesday, Thursday, Friday, Saturday, Sunday };

        public class Time
        {
            public int Hour { get; set; }
            public int Minute { get; set; }
        }

        public class DailyHours
        {
            public bool IsClosed { get; set; }
            public Time OpenTime { get; set; }
            public Time CloseTime { get; set; }
            public Day DayOfWeek{get;set;}
        }

        public class StoreHours
        {
            public Dictionary<Day, DailyHours> Hours { get; set; }
        }




    }
}
