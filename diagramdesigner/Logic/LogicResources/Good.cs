using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace DiagramDesigner.ResourcesLogic
{
    class Good : BaseResource
    {
         #region Attributes

        private Double price;
        private DataRowView currency;
        private Double ammount;
        private Double availableammount;
        private DataRowView measure;
        private DataRowView goal;
        private DataRowView type;
        private DataRowView element;
        public Double Price
        {
            get { return price; }
            set { price = value; }
        }
        public DataRowView Currency
        {
            get { return currency; }
            set { currency = value; }
        }
        public Double Ammount
        {
            get { return ammount; }
            set { ammount = value; }
        }
        public Double AvailableAmmount 
        {
            get { return availableammount; }
            set { availableammount = value; }
        }
        public DataRowView Measure
        {
            get { return measure; }
            set { measure = value; }
        }
        public DataRowView Goal
        {
            get { return goal; }
            set { goal = value; }
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
       

        public Good(Guid id, Guid designerID)
        {
            this.ID = id;
            this.DesignerID = designerID;
            this.Name = "NewGood";
            this.DisplayName = "Товар";
            this.Price = 0;
            this.Currency = null;
            this.Ammount = 0;
            this.AvailableAmmount = 0;
            this.Measure = null;
            this.Goal = null;
            this.Type = null;
            this.Element = null;
        }

        #endregion
    }
}
