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
        public Error(Guid designerID) : base(designerID)
        {
            this.ID = Guid.NewGuid();
        }
    }

    class TolerableError : Error
    {
        public TolerableError(Guid designerID)
            : base(designerID)
        {
            Name = "TolerableError";
        }
    }

    class GrossError : Error
    {
        public GrossError(Guid designerID)
            : base(designerID)
        {
            Name = "GrossError";
        }
    }
}