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
        private DataRowView element;
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
        //public DataRowView Element
        //{
        //    get { return element; }
        //    set { element = value; }
        //}
        public DataRowView Measure
        {
            get { return measure; }
            set { measure = value; }
        }
        #endregion

        #region Constructors
        public Equipment(Guid id, Guid designerID)
        {
            this.ID = id;
            this.DesignerID = designerID;
            this.Name = "NewEquipment";
            this.DisplayName = "Оборудование";
            this.PricePerHour = 0;
            this.Ammount = 0;
            this.Type = "";
            this.Drv = null;
            this.Element = null;
            this.Measure = null;
            
        }

        #endregion
    }
}
