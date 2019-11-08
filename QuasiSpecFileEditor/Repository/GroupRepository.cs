using QuasiSpecFileEditor.DAL;
using QuasiSpecFileEditor.Models;
using QuasiSpecFileEditor.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QuasiSpecFileEditor.Repository
{
    public class GroupRepository : GenericRepository<Group>, IGroupRepository
    {
        public GroupRepository(IUnitOfWork<SpecFileDataContext> unitOfWork) : base(unitOfWork)
        {

        }

        public GroupRepository(SpecFileDataContext context) : base(context)
        {

        }

        public void CreateGroup(Group group, int[] selectedParts, int[] selectedParameters)
        {
            if (selectedParts == null)
            {

            }
            else
            {
                foreach (var item in selectedParts)
                {
                    var part = Context.Parts.Find(item);
                    part.GroupID = group.GroupID;
                }
            }
            if (selectedParameters == null)
            {

            }
            else
            {
                group.Parameters = new List<Parameter>();
                foreach (var item in selectedParameters)
                {
                    group.Parameters.Add(Context.Parameters.Find(item));
                }
            }
            
        }

        public void EditGroup(Group group, int[] selectedParts, int[] selectedParameters)
        {
            var groupToUpdate = GetById(group.GroupID);
            groupToUpdate.GroupName = group.GroupName;
            if (selectedParts == null)
            {
                foreach (var part in Context.Parts.Where(c => c.GroupID == group.GroupID))
                {
                    part.GroupID = null;
                }
            }
            else
            {
                var sParts = new HashSet<int>(selectedParts);
                var groupParts = new HashSet<int>(groupToUpdate.Parts.Select(c => c.PartID));
                foreach (var part in Context.Parts)
                {
                    if (sParts.Contains(part.PartID))
                    {
                        if (!groupParts.Contains(part.PartID))
                        {
                            groupToUpdate.Parts.Add(part);
                        }
                    }
                    else
                    {
                        if (groupParts.Contains(part.PartID))
                        {
                            groupToUpdate.Parts.Remove(part);
                        }
                    }
                }
            }

            if (selectedParameters == null)
            {
                groupToUpdate.Parameters = groupToUpdate.Parameters;
                groupToUpdate.Parameters = new List<Parameter>();
                //SetEntryModified(groupToUpdate);
            }
            else
            {
                var sParameters = new HashSet<int>(selectedParameters);
                var groupParameters = new HashSet<int>(groupToUpdate.Parameters.Select(c => c.ParameterID));
                foreach (var parameter in Context.Parameters)
                {
                    if (sParameters.Contains(parameter.ParameterID))
                    {
                        if (!groupParameters.Contains(parameter.ParameterID))
                        {
                            groupToUpdate.Parameters.Add(parameter);
                        }
                    }
                    else
                    {
                        if (groupParameters.Contains(parameter.ParameterID))
                        {
                            groupToUpdate.Parameters.Remove(parameter);
                        }
                    }
                }
            }
        }

        public Group GetByName(int modelID, string groupName)
        {
            return Context.Groups.Where(c => c.ModelID == modelID && c.GroupName == groupName).SingleOrDefault();
        }
    }
}