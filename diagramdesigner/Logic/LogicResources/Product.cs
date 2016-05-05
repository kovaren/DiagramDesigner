using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace DiagramDesigner.ResourcesLogic
{
    class Product : BaseResource
    {
         #region Attributes

        private Double price;
        private DataRowView currency;
        private Double amount;
        private Double amountAvailable;
        private DataRowView measure;
        private DataRowView goal;
        private DataRowView type;
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
        public Double Amount
        {
            get { return amount; }
            set { amount = value; }
        }
        public Double AmountAvailable
        {
            get { return amountAvailable; }
            set { amountAvailable = value; }
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

        public Product(Guid id)
            : base(id)
        {
            this.Name = "Product";
            this.Title = null;
            this.Price = 0;
            this.Currency = null;
            this.Amount = 0;
            this.AmountAvailable = 0;
            this.Measure = null;
            this.Goal = null;
            this.Type = null;
        }

        #endregion
    }
}
