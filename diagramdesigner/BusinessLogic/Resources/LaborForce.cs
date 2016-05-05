using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace DiagramDesigner.ResourcesLogic
{
    class LaborForce : BaseResource
    {
        #region Attributes
        
        private Double priceperhour;
        private DataRowView position;
        private String category;
        private String boss;
        private DataRowView role;
        private Int16 ammount;
        private DataRowView type;
        public Double PricePerHour
        {
            get { return priceperhour; }
            set { priceperhour = value; }
        }
        public DataRowView Position
        {
            get { return position; }
            set { position = value; }
        }
        public String Category
        {
            get { return category; }
            set { category = value; }
        }
        public String Boss
        {
            get { return boss; }
            set { boss = value; }
        }
        public DataRowView Role
        {
            get { return role; }
            set { role = value; }
        }
        public Int16 Ammount
        {
            get { return ammount; }
            set { ammount = value; }
        }
        public DataRowView Type
        {
            get { return type; }
            set { type = value; }
        }
        #endregion

        #region Constructors
       
        public LaborForce(Guid id)
        {
            this.ID = id;
            this.Name = "Labor force";
            this.Title = null;
            this.PricePerHour = 0;
            this.Position = null;
            this.Category = "Category";
            this.Boss = "Boss";
            this.Role = null;
            this.Ammount = 0;
            this.Type = null;
        }

        #endregion
    }
}
