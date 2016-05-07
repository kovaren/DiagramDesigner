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
        public byte[] Content { get { return content; } set { content = value; IsEmpty = false; } }
        public string Name { get { return name; } set { name = value; IsEmpty = false; } }
        public bool IsEmpty;
        public Document()
        {
            Content = new byte[0];
            Name = "No file chosen";
            IsEmpty = true;
        }

        public override string ToString()
        {
            return Name;
        }
    }
    public class InformationResource : BaseResource
    {
        #region Attributes
        
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
        #endregion

        #region Constructors

        public InformationResource() : base()
        {
            Name = "Information";
            Title = string.Empty;
            CreationDate = DateTime.Now;
            Document = new Document();
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
            if (resource.CreationDate == this.CreationDate
                && resource.Document == this.Document
                && resource.Title == this.Title)
                return true;
            return false;
        }

        #endregion
    }
}
