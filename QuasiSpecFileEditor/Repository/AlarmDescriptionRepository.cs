using QuasiSpecFileEditor.DAL;
using QuasiSpecFileEditor.Models;
using QuasiSpecFileEditor.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QuasiSpecFileEditor.Repository
{
    public class AlarmDescriptionRepository : GenericRepository<AlarmDescription>, IAlarmDescriptionRepository
    {
        public AlarmDescriptionRepository(IUnitOfWork<SpecFileDataContext> unitOfWork) : base(unitOfWork)
        {

        }

        public AlarmDescriptionRepository(SpecFileDataContext context) : base(context)
        {

        }

        public void CreateAlarmDescription(AlarmDescription alarmDesc)
        {
            if (alarmDesc.Priority == null || alarmDesc.Priority == 0)
            {
                
            }
            else
            {
                if (GetAll().Where(c => c.ModelID == alarmDesc.ModelID).Select(p => p.Priority).Contains(alarmDesc.Priority))
                {
                    var prevDesc = GetAll().Where(c => c.ModelID == alarmDesc.ModelID && c.Priority == alarmDesc.Priority).SingleOrDefault();
                    prevDesc.Priority = null;
                }
            }

            Insert(alarmDesc);
        }

        public void UpdateAlarmDescription(AlarmDescription alarmDesc)
        {
            var alarmToUpdate = GetById(alarmDesc.AlarmDescriptionID);

            if (alarmDesc.Priority == null || alarmDesc.Priority == 0)
            {
                
            }
            else
            {
                if (GetAll().Where(c => c.ModelID == alarmDesc.ModelID).Select(p => p.Priority).Contains(alarmDesc.Priority))
                {
                    var prevDesc = GetAll().Where(c => c.ModelID == alarmDesc.ModelID && c.Priority == alarmDesc.Priority).SingleOrDefault();
                    prevDesc.Priority = alarmToUpdate.Priority;
                }
            }
            
            alarmToUpdate.Criteria = alarmDesc.Criteria;
            alarmToUpdate.Priority = alarmDesc.Priority;
            alarmToUpdate.Hold = alarmDesc.Hold;
            alarmToUpdate.ParameterName = alarmDesc.ParameterName;
            alarmToUpdate.TargetLimit = alarmDesc.TargetLimit;

            //db.Entry(alarmToUpdate).State = EntityState.Modified;
            //unit.Save();
        }
    }
}