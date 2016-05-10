using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace DiagramDesigner.ResourcesLogic
{
    public class LabourForce : BaseResource
    {
        private double pricePerHour;
        private string position;
        public double PricePerHour
        {
            get { return pricePerHour; }
            set { pricePerHour = value; }
        }
        public string Position
        {
            get { return position; }
            set { position = value; }
        }

        public LabourForce() : base()
        {
            this.Name = "Labour force";
            this.Title = string.Empty;
            this.PricePerHour = 0;
            this.Position = string.Empty;
        }

        public LabourForce(string title, double pricePerHour, string position)
        {
            this.Name = "Labour force";
            this.Title = title;
            this.PricePerHour = pricePerHour;
            this.Position = position;
        }

        public override bool Equals(object obj)
        {
            var resource = obj as LabourForce;
            if (resource == null)
                return false;
            if (resource.PricePerHour == this.PricePerHour
                && resource.Position == this.Position
                && resource.Title == this.Title)
                return true;
            return false;
        }
    }
}