using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace DiagramDesigner.ResourcesLogic
{
    public class BaseResource
    {
         #region Attributes
        private Guid id;
        protected DataRowView element;
        public Guid ID
        {
            get { return id; }
            set { id = value; }
        }
        public DataRowView Element
        {
            get { return element; }
            set { element = value; }
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

        private String displayName;
        public String DisplayName
        {
            get { return name; }
            set { name = value; }
        }
        #endregion

        #region Constructors
        public BaseResource(Guid id)
        {
            this.id = id;
          
        }

        public BaseResource()
            : this(Guid.NewGuid())
        {
        }

        public BaseResource(Guid id, Guid designerID)
        {
            this.id = id;
            this.designerID = designerID;
            this.Name = "BaseResource";
            
        }

        #endregion
    }
}
