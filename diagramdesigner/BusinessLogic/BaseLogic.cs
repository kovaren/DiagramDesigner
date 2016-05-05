using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DiagramDesigner.LogicRBP
{
    public class BaseLogic
     {
        #region Attributes
        private Guid id;
        public Guid ID
        {
            get { return id; }
            set { id = value; }
        }


        private Guid designerID;
        public Guid DesignerID
        {
            get { return designerID; }
            set { designerID = value; }
        }

        private String name;
        public String Name
        {
            get { return name; }
            set { name = value; }
        }
        #endregion

        #region Constructors
        public BaseLogic(Guid id)
        {
            this.id = id;
          
        }

        public BaseLogic()
            : this(Guid.NewGuid())
        {
        }

        public BaseLogic(Guid id, Guid designerID)
        {
            this.id = id;
            this.designerID = designerID;
            this.Name = "Base";
            
        }

        #endregion

    }
}
