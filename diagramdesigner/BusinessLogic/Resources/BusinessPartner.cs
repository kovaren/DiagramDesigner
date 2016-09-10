using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace DiagramDesigner.ResourcesLogic
{
    class BusinessPartner : BaseResource
    {
        private String contactname;
        private String address;
        private String phone;
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

        public BusinessPartner() : base()
        {
            this.Name = "NewPartner";
            this.ContactName = "ContactName";
            this.Address = "Address";
            this.Phone = "+0 (000) 000-00-00";          
        }
    }
}
