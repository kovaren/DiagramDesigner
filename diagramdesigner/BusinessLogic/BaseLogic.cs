using DiagramDesigner.BusinessLogic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DiagramDesigner.LogicRBP
{
    public class BaseLogic
    {
        private string name;
        private Guid id;
        private Guid designerID;
        public string Name { get { return name; } set { name = value; } }
        public Guid ID { get { return id; } set { id = value; } }
        public Guid DesignerID { get { return designerID; } set { designerID = value; } }
        public BaseLogic()
        {
            Name = string.Empty;
            ID = Guid.NewGuid();
            DesignerID = Guid.NewGuid();
        }
        public BaseLogic(Guid designerID)
        {
            Name = string.Empty;
            ID = Guid.NewGuid();
            DesignerID = designerID;
        }
    }
}