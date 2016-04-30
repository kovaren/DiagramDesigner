using DiagramDesigner.LogicRBP;
using DiagramDesigner.ResourcesLogic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DiagramDesigner.LogicTBP
{
    class DmpTBP : BaseLogic
    {
        private int alsId;

        private List<BaseResource> resources;

        public List<BaseResource> Resources { get { return resources; } set { resources = value; } }

        public List<string> resourceNames { get { return resources.Select(x => x.Name).ToList(); } }

        public int AlsID
        {
            get { return alsId; }
            set { alsId = value; }
        }

        public DmpTBP(Guid id, Guid designerID)
        {
            this.ID = id;
            this.DesignerID = designerID;
            this.Name = "DMP";
            this.Resources = new List<BaseResource>();
        }
        public DmpTBP(Guid id, Guid designerID, List<BaseLogic> operationPool)
        {
            this.ID = id;
            this.DesignerID = designerID;
            this.Name = "DMP";
            this.Resources = operationPool.SelectMany(x => ((OperationRBP)x).Resources).ToList();
        }
    }
}
