using QuasiSpecFileEditor.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QuasiSpecFileEditor.ViewModels
{
    public class AlarmDescriptionDisplay
    {
        public List<Alarm> Alarms { get; set; }

        public List<Alarm> MyAlarms { get; set; }

        //public List<AlarmDescription> MyAlarmDescriptions { get; set; }

        public AlarmDescription AlarmDescription { get; set; }

        public int[] selectedAlarm { get; set; }
    }
}