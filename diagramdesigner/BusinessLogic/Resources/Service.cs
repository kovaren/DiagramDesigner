using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace DiagramDesigner.ResourcesLogic
{
    public class Service : BaseResource
    {
        private double pricePerHour;
        private double duration;

        public double PricePerHour
        {
            get { return pricePerHour; }
            set { pricePerHour = value; }
        }
        public double Duration
        {
            get { return duration; }
            set { duration = value; }
        }

        public Service() : base()
        {
            Name = "Service";
            PricePerHour = 0;
            Duration = 0;
        }
        public Service(string title, double price, double duration)
            : base(title)
        {
            Name = "Service";
            Title = title;
            PricePerHour = price;
            Duration = duration;
        }
        public override bool Equals(object obj)
        {
            var resource = obj as Service;
            if (resource == null)
                return false;
            if (resource.PricePerHour == this.PricePerHour
                && resource.Duration == this.Duration
                && resource.Title == this.Title)
                return true;
            return false;
        }
    }
    
}
