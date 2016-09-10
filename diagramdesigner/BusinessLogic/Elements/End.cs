using DiagramDesigner.LogicRBP;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;

namespace DiagramDesigner.LogicTBP
{
    public class End : BaseLogic
    {
        public End(Guid designerID)
            : base(designerID)
        {
            Name = "End";
        }
    }
}
