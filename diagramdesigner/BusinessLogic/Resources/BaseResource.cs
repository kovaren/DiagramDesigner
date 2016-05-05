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
        public Guid ID
        {
            get { return id; }
            set { id = value; }
        }

        private String name;
        public String Name
        {
            get { return name; }
            set { name = value; }
        }

        private String title;
        public String Title
        {
            get { return title; }
            set { title = value; }
        }
        #endregion

        #region Constructors
        public BaseResource()
        {
            id = Guid.NewGuid();
        }

        public BaseResource(Guid id)
        {
            this.id = id;
        }

        #endregion
    }
}
