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
        private DataRowView element;
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
        //public DataRowView Element
        //{
        //    get { return element; }
        //    set { element = value; }
        //}

        #endregion

        #region Constructors
       
        public InformationResource(Guid id, Guid designerID)
        {
            this.ID = id;
            this.DesignerID = designerID;
            this.Name = "NewInformationResource";
            this.DisplayName = "Информационный ресурс";
            this.BegDate = DateTime.Now; 
            this.Document = "Document";
            this.Type = null;
            this.Element = null;

        }

        #endregion
    }
}
