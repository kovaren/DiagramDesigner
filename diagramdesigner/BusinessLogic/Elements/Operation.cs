using DiagramDesigner.BusinessLogic;
using DiagramDesigner.LogicRBP;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;

namespace DiagramDesigner.LogicTBP
{
    public class Operation : BaseLogic
    {
        private Resources resources;
        public Resources Resources { get { return resources; } set { resources = value; } }
        public Operation(Guid designerID, string name = "Operation")
            : base(designerID)
        {
            this.Name = name;
            this.Resources = new Resources();
        }
        public Operation(Guid ID, Guid designerID, string name = "Operation")
            : base(designerID)
        {
            this.Name = name;
            this.Resources = new Resources();
        }
    }
}
