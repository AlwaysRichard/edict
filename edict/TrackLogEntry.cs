using System;
using System.Collections.Generic;
using System.Text;

namespace edict
{
    class TrackLogEntry
    {
        public DateTime time { get; set; }
        public double lat { get; set; }
        public double lon { get; set; }
        public double altitue { get; set; }
        public double heading { get; set; }
        public double speed { get; set; }

        public TrackLogEntry(string textLine)
        {
            string[] itmes = textLine.Split(',');
            string[] dateItems = itmes[0].Split(':');
            this.time = new DateTime(
                (int) Int32.Parse(dateItems[0]), (int) Int32.Parse(dateItems[1]), (int) Int32.Parse(dateItems[2]), 
                (int) Int32.Parse(dateItems[3]), (int) Int32.Parse(dateItems[4]), (int) Int32.Parse(dateItems[5]));
            this.lat = Convert.ToDouble(itmes[1]);
            this.lon = Convert.ToDouble(itmes[2]);
            this.altitue = Convert.ToDouble(itmes[3]);
            this.heading = Convert.ToDouble(itmes[4]);
            this.speed = Convert.ToDouble(itmes[5]);
        }
    }
}
