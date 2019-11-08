using QuasiSpecFileEditor.DAL;
using QuasiSpecFileEditor.Models;
using QuasiSpecFileEditor.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QuasiSpecFileEditor.Repository
{
    public class ParameterRepository : GenericRepository<Parameter>, IParameterRepository
    {
        public ParameterRepository(IUnitOfWork<SpecFileDataContext> unitOfWork) : base(unitOfWork)
        {

        }

        public ParameterRepository(SpecFileDataContext context) : base(context)
        {

        }
    }
}