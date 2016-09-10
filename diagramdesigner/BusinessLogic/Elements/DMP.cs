using DiagramDesigner.BusinessLogic;
using DiagramDesigner.LogicRBP;
using DiagramDesigner.ResourcesLogic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DiagramDesigner.LogicTBP
{
    class DMP : BaseLogic
    {
        private int alsId;

        private List<BaseResource> resources;

        public List<BaseResource> Resources { get { return resources; } set { resources = value; } }

        public List<string> ResourcesAvailable { get { return resources.Select(x => x.Title).ToList(); } }

        public int AlsID
        {
            get { return alsId; }
            set { alsId = value; }
        }

        public DMP(Guid designerID) : base (designerID)
        {
            this.Name = "DMP";
            this.Resources = new List<BaseResource>();
        }
        public DMP(Guid designerID, List<BaseLogic> operationPool) : base(designerID)
        {
            this.Name = "DMP";
            this.Resources = operationPool.SelectMany(x => ((Operation)x).Resources.GetAll()).ToList();
        }
    }
}
