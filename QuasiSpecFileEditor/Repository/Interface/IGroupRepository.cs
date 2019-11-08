using QuasiSpecFileEditor.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuasiSpecFileEditor.Repository
{
    interface IGroupRepository : IGenericRepository<Group>
    {
        void CreateGroup(Group group, int[] selectedParts, int[] selectedParameters);

        void EditGroup(Group group, int[] selectedParts, int[] selectedParameters);

        Group GetByName(int modelID, string groupName);
    }
}
