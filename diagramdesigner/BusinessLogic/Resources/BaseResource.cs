using DiagramDesigner.BusinessLogic;
using DiagramDesigner.LogicRBP;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace DiagramDesigner.ResourcesLogic
{
    public enum Category { None, Input, Output, Control, Mechanism}
    public class BaseResource : BaseLogic
    {
        private String title;
        public String Title
        {
            get { return title; }
            set { title = value; }
        }
        private Category category;
        public Category Category
        {
            get { return category; }
            set { category = value; }
        }

        public BaseResource() : base ()
        { }
        public BaseResource(Guid designerID) : base (designerID)
        { }
        public BaseResource(string title) : base()
        {
            Title = title;
        }
        public override bool Equals(object obj)
        {
            var resource = obj as BaseResource;
            if (resource == null)
                return false;
            if (resource.ID == this.ID)
                return true;
            return false;
        }
    }
}