using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace DiagramDesigner.ResourcesLogic
{
    public class Equipment : BaseResource
    {
        private double pricePerHour;
        private string state;
        public double PricePerHour
        {
            get { return pricePerHour; }
            set { pricePerHour = value; }
        }
        public string State
        {
            get { return state; }
            set { state = value; }
        }

        public Equipment() : base()
        {
            Name = "Equipment";
            PricePerHour = 0;
            State = string.Empty;
        }
        public Equipment(string title, double pricePerHour, string state)
            : base(title)
        {
            Name = "Equipment";
            PricePerHour = pricePerHour;
            State = state;
        }
        public override bool Equals(object obj)
        {
            var resource = obj as Equipment;
            if (resource == null)
                return false;
            if (resource.PricePerHour == this.PricePerHour
                && resource.State == this.State
                && resource.Title == this.Title)
                return true;
            return false;
        }
    }
}