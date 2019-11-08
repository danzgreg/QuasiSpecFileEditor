using QuasiSpecFileEditor.DAL;
using QuasiSpecFileEditor.Models;
using QuasiSpecFileEditor.Repository;
using QuasiSpecFileEditor.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QuasiSpecFileEditor.Repository
{
    public class AlarmRepository : GenericRepository<Alarm>, IAlarmRepository
    {
        public AlarmRepository(IUnitOfWork<SpecFileDataContext> unitOfWork) : base(unitOfWork)
        {

        }

        public AlarmRepository(SpecFileDataContext context) : base(context)
        {

        }
    }
}