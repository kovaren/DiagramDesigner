using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace DiagramDesigner.ResourcesLogic
{
    class FinancialResource : BaseResource
    {
         #region Attributes


        private double ammount;
        private DataRowView currency;
        private DataRowView type;
        private DataRowView element;
        public double Ammount
        {
            get { return ammount; }
            set { ammount = value; }
        }
        public DataRowView Currency
        {
            get { return currency; }
            set { currency = value; }
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
        public FinancialResource(Guid id, Guid designerID)
        {
            this.ID = id;
            this.DesignerID = designerID;
            this.Name = "NewFinancialResource";
            this.DisplayName = "Финансовый ресурс";
            this.Ammount = 0;
            this.Currency = null;
            this.Type = null;
            this.Element = null;
 
            
        }

        #endregion
    }
}
