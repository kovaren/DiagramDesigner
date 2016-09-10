using System;
using System.Linq;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Markup;
using System.Xml;
using DiagramDesigner.LogicRBP;
using DiagramDesigner.LogicTBP;
using DiagramDesigner.ResourcesLogic;

namespace DiagramDesigner
{
    public partial class DesignerCanvas : Canvas
    {
        int itemCounter = 0;

        private Point? rubberbandSelectionStartPoint = null;
        private SelectionService selectionService;
        internal SelectionService SelectionService
        {
            get
            {
                if (selectionService == null)
                    selectionService = new SelectionService(this);

                return selectionService;
            }
        }

        protected override void OnMouseDown(MouseButtonEventArgs e)
        {
            base.OnMouseDown(e);
            if (e.Source == this)
            {
                // in case that this click is the start of a 
                // drag operation we cache the start point
                this.rubberbandSelectionStartPoint = new Point?(e.GetPosition(this));

                // if you click directly on the canvas all 
                // selected items are 'de-selected'
                SelectionService.ClearSelection();
                Focus();
                e.Handled = true;
            }
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);

            // if mouse button is not pressed we have no drag operation, ...
            if (e.LeftButton != MouseButtonState.Pressed)
                this.rubberbandSelectionStartPoint = null;

            // ... but if mouse button is pressed and start
            // point value is set we do have one
            if (this.rubberbandSelectionStartPoint.HasValue)
            {
                // create rubberband adorner
                AdornerLayer adornerLayer = AdornerLayer.GetAdornerLayer(this);
                if (adornerLayer != null)
                {
                    RubberbandAdorner adorner = new RubberbandAdorner(this, rubberbandSelectionStartPoint);
                    if (adorner != null)
                    {
                        adornerLayer.Add(adorner);
                    }
                }
            }
            e.Handled = true;
        }

        protected override void OnDrop(DragEventArgs e)
        {
            base.OnDrop(e);
            DragObject dragObject = e.Data.GetData(typeof(DragObject)) as DragObject;
            if (dragObject != null && !String.IsNullOrEmpty(dragObject.Xaml))
            {
                DesignerItem newItem = null;
                Object content = XamlReader.Load(XmlReader.Create(new StringReader(dragObject.Xaml)));

                if (content != null)
                {
                    itemCounter = this.Children.OfType<DesignerItem>().Where(x => x.Tag.ToString() == dragObject.Class).Count();

                    newItem = new DesignerItem(Guid.NewGuid(), dragObject.Class, ++itemCounter);
                    newItem.Content = content;
                    newItem.MouseDoubleClick += DesignerItem_MouseDoubleClick;

                    Point position = e.GetPosition(this);

                    if (dragObject.DesiredSize.HasValue)
                    {
                        Size desiredSize = dragObject.DesiredSize.Value;
                        newItem.Width = desiredSize.Width;
                        newItem.Height = desiredSize.Height;

                        DesignerCanvas.SetLeft(newItem, Math.Max(0, position.X - newItem.Width / 2));
                        DesignerCanvas.SetTop(newItem, Math.Max(0, position.Y - newItem.Height / 2));
                    }
                    else
                    {
                        DesignerCanvas.SetLeft(newItem, Math.Max(0, position.X));
                        DesignerCanvas.SetTop(newItem, Math.Max(0, position.Y));
                    }

                    Canvas.SetZIndex(newItem, this.Children.Count);
                    this.Children.Add(newItem);

                    SetConnectorDecoratorTemplate(newItem);

                    //update selection
                    this.SelectionService.SelectItem(newItem);
                    newItem.Focus();

                }

                e.Handled = true;
            }
        }

        void DesignerItem_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var item = sender as DesignerItem;
            var otherItems = Children.OfType<DesignerItem>().Select(x => x.BoundLogicItem).ToList();
            //MessageBox.Show("Resource number: " + (item.BoundLogicItem as Operation).Resources.Count().ToString());
            if (item.Tag.ToString() == "Operation")
            {
                var operation = item.BoundLogicItem as Operation;
                var otherResources = otherItems
                    .OfType<Operation>()
                    .SelectMany(x => x.Resources.GetAll())
                    .Distinct(new ResourceComparer())
                    .ToList();
                otherResources.RemoveAll(x => operation.Resources.GetAll().Contains(x, new ResourceComparer()));
                //otherResources = otherResources.Distinct(new ResourceComparer()).ToList();

                var resourceWindow = new ResourceWindow(operation, otherResources);
                resourceWindow.ShowDialog();
                if (resourceWindow.DialogResult.HasValue && resourceWindow.DialogResult.Value)
                {
                    ((Operation)(item).BoundLogicItem).Resources = resourceWindow.Resources;
                    sender = item;
                }
                return;
            }
            if (item.Tag.ToString() == "DMP")
            {
                var dmp = item.BoundLogicItem as DMP;
                MessageBox.Show(string.Join("\n", dmp.ResourcesAvailable));
                //resourceWindow.ShowDialog();
                //if (resourceWindow.DialogResult.HasValue && resourceWindow.DialogResult.Value)
                //{
                //    ((DmpTBP)this.BoundLogicItem).Resources = resourceWindow.Resources;
                //}
                return;
            }
        }

        public void AddChild(DesignerItem element)
        {
            Children.Add(element);
            element.MouseDoubleClick += DesignerItem_MouseDoubleClick;
        }

        protected override Size MeasureOverride(Size constraint)
        {
            Size size = new Size();

            foreach (UIElement element in this.InternalChildren)
            {
                double left = Canvas.GetLeft(element);
                double top = Canvas.GetTop(element);
                left = double.IsNaN(left) ? 0 : left;
                top = double.IsNaN(top) ? 0 : top;

                //measure desired size for each child
                element.Measure(constraint);

                Size desiredSize = element.DesiredSize;
                if (!double.IsNaN(desiredSize.Width) && !double.IsNaN(desiredSize.Height))
                {
                    size.Width = Math.Max(size.Width, left + desiredSize.Width);
                    size.Height = Math.Max(size.Height, top + desiredSize.Height);
                }
            }
            // add margin 
            size.Width += 10;
            size.Height += 10;
            return size;
        }

        public void SetConnectorDecoratorTemplate(DesignerItem item)
        {
            if (item.ApplyTemplate() && item.Content is UIElement)
            {
                ControlTemplate template = DesignerItem.GetConnectorDecoratorTemplate(item.Content as UIElement);
                Control decorator = item.Template.FindName("PART_ConnectorDecorator", item) as Control;
                if (decorator != null && template != null)
                    decorator.Template = template;
            }
        }
    }

    class ResourceComparer : IEqualityComparer<BaseResource>
    {
        // Products are equal if their names and product numbers are equal.
        public bool Equals(BaseResource x, BaseResource y)
        {
            return x.ID == y.ID;
        }

        public int GetHashCode(BaseResource product)
        {
            return 0;
        }
    }
}