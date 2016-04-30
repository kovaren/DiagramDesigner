using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace DiagramDesigner.ResourcesLogic
{
    class BusinessPartner : BaseResource
    {
        #region Attributes
        
        private String contactname;
        private String address;
        private String phone;
        private DataRowView typeofpartner;
        private DataRowView typeofpartnership;
        private DataRowView type;
        private DataRowView element;
        public String ContactName
        {
            get { return contactname; }
            set { contactname = value; }
        }
        public String Address
        {
            get { return address; }
            set { address = value; }
        }
        public String Phone
        {
            get { return phone; }
            set { phone = value; }
        }
        public DataRowView TypeofPartner
        {
            get { return typeofpartner; }
            set { typeofpartner = value; }
        }
        public DataRowView TypeofPartnership
        {
            get { return typeofpartnership; }
            set { typeofpartnership = value; }
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
       
        public BusinessPartner(Guid id, Guid designerID)
        {
            this.ID = id;
            this.DesignerID = designerID;
            this.Name = "NewPartner";
            this.ContactName = "ContactName";
            this.Address = "Address";
            this.Phone = "+0 (000) 000-00-00";
            this.TypeofPartner = null;
            this.TypeofPartnership = null;
            this.Type = null;
            this.Element = null;
            
        }

        #endregion
    }
}
