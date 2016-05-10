using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace DiagramDesigner.ResourcesLogic
{
    public enum UnitOfMeasure
    {
        Item,
        Kilogram,
        Metre,
        Litre
    }
    public class Product : BaseResource
    {
        private double price;
        private UnitOfMeasure unitOfMeasure;
        private double amount;
        public double Price
        {
            get { return price; }
            set { price = value; }
        }
        public UnitOfMeasure UnitOfMeasure
        {
            get { return unitOfMeasure; }
            set { unitOfMeasure = value; }
        }
        public double Amount
        {
            get { return amount; }
            set { amount = value; }
        }

        public Product()
            : base()
        {
            Name = "Product";
            Price = 0;
            UnitOfMeasure = UnitOfMeasure.Item;
            Amount = 0;
        }
        public Product(string title, double price, double amount, UnitOfMeasure unitOfMeasure)
            : base(title)
        {
            Name = "Product";
            Price = price;
            UnitOfMeasure = UnitOfMeasure.Item;
            Amount = amount;
        }
        public override bool Equals(object obj)
        {
            var resource = obj as Product;
            if (resource == null)
                return false;
            if (resource.Price == this.Price
                && resource.Amount == this.Amount
                && resource.UnitOfMeasure == this.UnitOfMeasure
                && resource.Title == this.Title)
                return true;
            return false;
        }
    }
}