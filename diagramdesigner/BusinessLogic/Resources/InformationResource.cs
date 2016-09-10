using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace DiagramDesigner.ResourcesLogic
{
    public class Document
    {
        byte[] content;
        string name;
        public byte[] Content
        {
            get
            {
                return content;
            }
            set 
            { 
                content = value; 
                IsEmpty = false; 
            }
        }
        public string Name
        {
            get
            {
                return name;
            }
            set
            {
                name = value;
                IsEmpty = false;
            }
        }
        public bool IsEmpty;
        public Document()
        {
            Content = new byte[0];
            Name = "No file chosen";
            IsEmpty = true;
        }
        public override string ToString() { return Name; }
    }
    public class InformationResource : BaseResource
    {
        private DateTime creationDate;
        private Document document;
        public DateTime CreationDate
        {
            get { return creationDate; }
            set { creationDate = value; }
        }
        public Document Document
        {
            get { return document; }
            set { document = value; }
        }

        public InformationResource() : base()
        {
            Title = string.Empty;
            CreationDate = DateTime.Now;
            Document = new Document();
            Name = "Information";
        }
        public InformationResource(Guid designerID)
            : base(designerID)
        {
            Title = string.Empty;
            CreationDate = DateTime.Now;
            Document = new Document();
            Name = "Information";
        }
        public InformationResource(string title, DateTime creationDate, Document document)
            : base (title)
        {
            CreationDate = creationDate;
            Document = document;
            Name = "Information";
        }

        public override bool Equals(object obj)
        {
            var resource = obj as InformationResource;
            if (resource == null)
                return false;
            if (resource.CreationDate == this.CreationDate
                && resource.Document == this.Document
                && resource.Title == this.Title)
                return base.Equals(obj);
            return false;
        }
    }
}