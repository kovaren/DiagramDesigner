using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace DiagramDesigner.ResourcesLogic
{
    class InformationResource : BaseResource
    {
        #region Attributes
        
        private DateTime begdate;
        private String document;
        private DataRowView type;
        public DateTime BegDate
        {
            get { return begdate; }
            set { begdate = value; }
        }
        public String Document
        {
            get { return document; }
            set { document = value; }
        }
        public DataRowView Type
        {
            get { return type; }
            set { type = value; }
        }
        #endregion

        #region Constructors
       
        public InformationResource(Guid id)
        {
            this.ID = id;
            this.Name = "NewInformationResource";
            this.Title = null;
            this.BegDate = DateTime.Now; 
            this.Document = "Document";
            this.Type = null;
        }

        #endregion
    }
}
