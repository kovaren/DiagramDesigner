using DiagramDesigner.LogicRBP;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DiagramDesigner.LogicTBP
{
    class Error : BaseLogic
    {
        public String MyProperty { get; set; }
        public Error(Guid id, Guid designerID)
        {
            this.ID= id;
            this.DesignerID= designerID;
            this.Name = "Error";
        }
    }
}