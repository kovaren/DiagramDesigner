using DiagramDesigner.LogicRBP;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;

namespace DiagramDesigner.LogicTBP
{
    public class OperationTBP:BaseLogic
    {
        public String MyProperty { get; set; }

        public OperationTBP(Guid id, Guid designerID)
        {
            this.ID= id;
            this.DesignerID= designerID;
            this.Name = "NewOperationTBP";
            this.MyProperty = "Test";
        }


    }
}
