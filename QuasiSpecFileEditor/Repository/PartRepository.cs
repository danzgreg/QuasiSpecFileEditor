using QuasiSpecFileEditor.DAL;
using QuasiSpecFileEditor.Models;
using QuasiSpecFileEditor.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QuasiSpecFileEditor.Repository
{
    public class PartRepository : GenericRepository<Part>, IPartRepository
    {
        public PartRepository(IUnitOfWork<SpecFileDataContext> unitOfWork) : base(unitOfWork)
        {

        }

        public PartRepository(SpecFileDataContext context) : base(context)
        {

        }
    }
}