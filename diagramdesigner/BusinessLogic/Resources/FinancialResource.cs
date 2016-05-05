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

        #endregion

        #region Constructors
        public FinancialResource(Guid id)
        {
            this.ID = id;
            this.Name = "NewFinancialResource";
            this.Title = null;
            this.Ammount = 0;
            this.Currency = null;
            this.Type = null;            
        }

        #endregion
    }
}
