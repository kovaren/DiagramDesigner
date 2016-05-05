using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace DiagramDesigner.ResourcesLogic
{
    class Equipment : BaseResource
    {
        #region Attributes

        private Double priceperhour;
        private DataRowView measure;
        private Double ammount;
        private String type;
        public Double PricePerHour
        {
            get { return priceperhour; }
            set { priceperhour = value; }
        }
        public Double Ammount
        {
            get { return ammount; }
            set { ammount = value; }
        }
        public String Type
        {
            get { return type; }
            set { type = value; }
        }

        private DataRowView drv;
        public DataRowView Drv
        {
            get { return drv; }
            set { drv = value; }
        }
        public DataRowView Measure
        {
            get { return measure; }
            set { measure = value; }
        }
        #endregion

        #region Constructors
        public Equipment(Guid id)
        {
            this.ID = id;
            this.Name = "NewEquipment";
            this.Title = null;
            this.PricePerHour = 0;
            this.Ammount = 0;
            this.Type = "";
            this.Drv = null;
            this.Measure = null;
            
        }

        #endregion
    }
}
