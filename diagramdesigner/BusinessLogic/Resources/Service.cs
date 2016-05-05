﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace DiagramDesigner.ResourcesLogic
{
    class Service : BaseResource
    {
        #region Attributes

        private Double priceperhour;
        private DataRowView currency;
        private Double duration;
        private DataRowView goal;
        private DataRowView type;

        public Double PricePerHour
        {
            get { return priceperhour; }
            set { priceperhour = value; }
        }
        public DataRowView Currency
        {
            get { return currency; }
            set { currency = value; }
        }
        public Double Duration
        {
            get { return duration; }
            set { duration = value; }
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
        #endregion

        #region Constructors


        public Service(Guid id)
            : base(id)
        {
            this.Name = "Service";
            this.Title = null;
            this.PricePerHour = 0;
            this.Currency = null;
            this.Duration = 0;
            this.Goal = null;
            this.Type = null;
        }

        #endregion
    }
    
}