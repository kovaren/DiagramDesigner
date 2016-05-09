using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;

namespace DiagramDesigner.ResourcesLogic
{
    public enum Currency
    {
        [Description("Russian rouble")]
        RUR,
        [Description("United states dollar")]
        USD,
        [Description("Euro")]
        EUR,
        [Description("UK pound")]
        GBP,
        [Description("Swiss franc")]
        CHF,
        [Description("Chinese yuan")]
        CNY,
        [Description("Japanese yen")]
        JPY
    }
    public class FinancialResource : BaseResource
    {
        private double sum;
        private Currency currency;
        public double Sum
        {
            get { return sum; }
            set { sum = value; }
        }
        public Currency Currency
        {
            get { return currency; }
            set { currency = value; }
        }

        public FinancialResource()
            : base()
        {
            this.Name = "Finance";
            this.Title = string.Empty;
            this.Sum = 0;
            this.Currency = Currency.RUR;
        }
        public FinancialResource(string title, double sum, Currency currency)
            : base(title)
        {
            this.Name = "Finance";
            this.Sum = sum;
            this.Currency = currency;
        }

        public override bool Equals(object obj)
        {
            var resource = obj as FinancialResource;
            if (resource == null)
                return false;
            if (resource.Sum == this.Sum
                && resource.Currency == this.Currency
                && resource.Title == this.Title)
                return true;
            return false;
        }
    }
}
