using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using DiagramDesigner.ResourcesLogic;

namespace DiagramDesigner.LogicRBP
{
    public class OperationRBP : BaseLogic
    {
        public String MyProperty { get; set; }

        private List<BaseResource> resources;

        public List<BaseResource> Resources { get { return resources; } set { resources = value; } }

        public OperationRBP(Guid id, Guid designerID, string name = "Operation")
        {
            this.ID = id;
            this.DesignerID = designerID;
            this.Name = name;
            this.MyProperty = "Test";
            this.Resources = new List<BaseResource>();
        }
    }
}
