using DiagramDesigner.ResourcesLogic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DiagramDesigner.BusinessLogic
{
    public class Resources
    {
        public List<BaseResource> Input { get; set; }
        public List<BaseResource> Output { get; set; }
        public List<BaseResource> Control { get; set; }
        public List<BaseResource> Mechanism { get; set; }

        public Resources()
        {
            Input = new List<BaseResource>();
            Output = new List<BaseResource>();
            Control = new List<BaseResource>();
            Mechanism = new List<BaseResource>();
        }

        public void Add(BaseResource resource)
        {
            switch (resource.Category)
            {
                case Category.Input:
                    Input.Add(resource);
                    break;
                case Category.Output:
                    Output.Add(resource);
                    break;
                case Category.Control:
                    Control.Add(resource);
                    break;
                case Category.Mechanism:
                    Mechanism.Add(resource);
                    break;
                default: throw new Exception("Неизвестный тип ресурса!");
            }
        }
        public void Remove(BaseResource resource)
        {
            switch (resource.Category)
            {
                case Category.Input:
                    break;
                case Category.Output:
                    break;
                case Category.Control:
                    break;
                case Category.Mechanism:
                    break;
                default: throw new Exception("Неизвестный тип ресурса!");
            }
        }
        public List<BaseResource> GetAll()
        {
            var all = Input.Concat(Output).Concat(Control).Concat(Mechanism).ToList();
            all.ForEach(x => x.Category = Category.None);
            return all;
        }
        public bool Contains(BaseResource resource)
        {
            return Input.Contains(resource)
                || Output.Contains(resource)
                || Control.Contains(resource)
                || Mechanism.Contains(resource);
        }
        public int Count()
        {
            return Input.Count 
                + Output.Count 
                + Control.Count 
                + Mechanism.Count;
        }
    }
}