using DiagramDesigner.LogicRBP;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;

namespace DiagramDesigner.LogicTBP
{
    public class Start : BaseLogic
    {
        public Start(Guid designerID)
            : base(designerID)
        {
            Name = "Start";
        }
    }
}
