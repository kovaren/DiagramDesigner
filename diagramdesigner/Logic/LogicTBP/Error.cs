using DiagramDesigner.LogicRBP;
using DiagramDesigner.ResourcesLogic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DiagramDesigner.LogicTBP
{
    abstract class Error : BaseLogic
    {
        List<BaseResource> resources;
        public List<BaseResource> Resources { get { return resources; } set { resources = value; } }
        public Error(Guid id, Guid designerID)
        {
            this.ID= id;
            this.DesignerID= designerID;
        }
    }

    class TolerableError : Error
    {
        public TolerableError(Guid id, Guid designerID)
            : base(id, designerID)
        {
            Name = "TolerableError";
        }
    }

    class GrossError : Error
    {
        public GrossError(Guid id, Guid designerID)
            : base(id, designerID)
        {
            Name = "GrossError";
        }
    }
}