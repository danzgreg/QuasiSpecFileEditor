using QuasiSpecFileEditor.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuasiSpecFileEditor.Repository
{
    interface IAlarmDescriptionRepository : IGenericRepository<AlarmDescription>
    {
        void CreateAlarmDescription(AlarmDescription alarmDesc);

        void UpdateAlarmDescription(AlarmDescription alamDesc);
    }
}
