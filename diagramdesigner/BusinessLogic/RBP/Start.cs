using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;

namespace DiagramDesigner.LogicRBP
{
    public class Start:BaseLogic
    {
       public Start(Guid id, Guid designerID)
        {
            this.ID= id;
            this.DesignerID= designerID;
            this.Name = "Start";
            
        }
    }
}
