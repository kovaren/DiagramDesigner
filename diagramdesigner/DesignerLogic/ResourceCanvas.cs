using DiagramDesigner.LogicTBP;
using DiagramDesigner.ResourcesLogic;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Markup;
using System.Xml;

namespace DiagramDesigner
{
    public partial class ResourceCanvas : DesignerCanvas
    {
        public int itemCounter = 0;

        public Guid OperationGuid = Guid.NewGuid();
        public ResourceWindow ResourceWindow = null;
        public Toolbox Toolbox = null;

        protected override void OnDrop(DragEventArgs e)
        {
            DragObject dragObject = e.Data.GetData(typeof(DragObject)) as DragObject;
            if (dragObject != null && !String.IsNullOrEmpty(dragObject.Xaml))
            {
                DesignerItem newItem = null;
                if (dragObject.DesignerItem == null)
                {
                    Object content = XamlReader.Load(XmlReader.Create(new StringReader(dragObject.Xaml)));

                    if (content != null)
                    {
                        itemCounter = this.Children.OfType<DesignerItem>().Where(x => x.Tag.ToString() == dragObject.Class).Count();

                        newItem = new DesignerItem(Guid.NewGuid(), dragObject.Class, ++itemCounter);
                        newItem.Content = content;

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

                        DrawConnection(newItem.ID, OperationGuid);
                        ResourceWindow.UpdateResource(newItem);

                        //update selection
                        this.SelectionService.SelectItem(newItem);
                        newItem.Focus();
                    }
                    e.Handled = true;
                }
                else
                {
                    newItem = dragObject.DesignerItem;
                    if (!(newItem.Parent is DesignerCanvas))
                    {
                        newItem.Width = 60;
                        newItem.Height = 60;

                        //Toolbox.Items.Remove(Toolbox.Items.OfType<ToolboxItem>().Where(x => x.DesignerItem == newItem).FirstOrDefault());
                        ((ToolboxItem)Toolbox.Items[0]).Content = null;
                        Toolbox.Items.RemoveAt(0);
                        //var parent = newItem.Parent;


                        Point position = e.GetPosition(this);
                        DesignerCanvas.SetLeft(newItem, Math.Max(0, position.X - newItem.Width / 2));
                        DesignerCanvas.SetTop(newItem, Math.Max(0, position.Y - newItem.Height / 2));

                        Canvas.SetZIndex(newItem, this.Children.Count);
                        this.Children.Add(newItem);

                        SetConnectorDecoratorTemplate(newItem);

                        DrawConnection(newItem.ID, OperationGuid);
                        ResourceWindow.UpdateResource(newItem);

                        this.SelectionService.SelectItem(newItem);
                        newItem.Focus();
                    }

                    e.Handled = true;
                }
                newItem.MouseDoubleClick += DesignerItem_MouseDoubleClick;

                //MessageBox.Show("resourcecanvas: " + newItem.BoundLogicItem.ID.ToString());
            }
        }

        void DesignerItem_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var item = sender as DesignerItem;
            var otherItems = Children.OfType<DesignerItem>().Select(x => x.BoundLogicItem).ToList();
            if (item.Tag.ToString() == "Information")
            {
                ResourceWindow.UpdateResource(item);
                
                //var operation = item.BoundLogicItem as Operation;
                //var otherResources = otherItems.OfType<Operation>().SelectMany(x => x.Resources.GetAll()).ToList();
                //otherResources.RemoveAll(x => operation.Resources.GetAll().Contains(x));

                //var resourceWindow = new ResourceWindow(operation, otherResources);
                //resourceWindow.ShowDialog();
                //if (resourceWindow.DialogResult.HasValue && resourceWindow.DialogResult.Value)
                //{
                //    ((Operation)(item).BoundLogicItem).Resources = resourceWindow.Resources;
                //    sender = item;
                //}
            }
        }

        private void DrawConnection(Guid sourceID, Guid sinkID)
        {
            DesignerItem sourceItem = (from item in this.Children.OfType<DesignerItem>()
                                       where item.ID == sourceID
                                       select item).FirstOrDefault();
            DesignerItem sinkItem = (from item in this.Children.OfType<DesignerItem>()
                                     where item.ID == sinkID
                                     select item).FirstOrDefault();

            double sourceTop = Canvas.GetTop(sourceItem);
            double sourceLeft = Canvas.GetLeft(sourceItem);
            double sinkTop = Canvas.GetTop(sinkItem);
            double sinkLeft = Canvas.GetLeft(sinkItem);

            string sourceConnectorName = "Top";
            string sinkConnectorName = "Bottom";

            if (sourceTop + sourceItem.Height / 2 < sinkTop
                && sourceLeft + sourceItem.Width / 2 < sinkLeft)
            {
                sourceConnectorName = "Bottom";
                sinkConnectorName = "Left";
            }

            if (sourceTop + sourceItem.Height / 2 < sinkTop
                && sourceLeft + sourceItem.Width / 2 > sinkLeft)
            {
                sourceConnectorName = "Bottom";
                sinkConnectorName = "Top";
            }

            if (sourceTop + sourceItem.Height / 2 > sinkTop
                && sourceLeft + sourceItem.Width / 2 < sinkLeft)
            {
                sourceConnectorName = "Right";
                sinkConnectorName = "Left";
            }

            if (sourceTop + sourceItem.Height / 2 > sinkTop
                && sourceLeft > sinkLeft + sinkItem.Width)
            {
                sourceConnectorName = "Left";
                sinkConnectorName = "Right";
            }

            if (sourceTop > sinkTop + sinkItem.Height
                && sourceLeft + sourceItem.Width / 2 > sinkLeft)
            {
                sourceConnectorName = "Top";
                sinkConnectorName = "Bottom";
            }

            Connector sourceConnector = GetConnector(sourceID, sourceConnectorName);
            Connector sinkConnector = GetConnector(sinkID, sinkConnectorName);

            Connection connection = new Connection(sourceConnector, sinkConnector);

            Children.Add(connection);
        }

        private Connector GetConnector(Guid itemID, String connectorName)
        {
            DesignerItem designerItem = (from item in this.Children.OfType<DesignerItem>()
                                         where item.ID == itemID
                                         select item).FirstOrDefault();

            Control connectorDecorator = designerItem.Template.FindName("PART_ConnectorDecorator", designerItem) as Control;
            connectorDecorator.ApplyTemplate();

            return connectorDecorator.Template.FindName(connectorName, connectorDecorator) as Connector;
        }

        public void RemoveChild(DesignerItem element)
        {
            var connection = Children.OfType<Connection>().Where(x => x.Source.ParentDesignerItem == element).FirstOrDefault();
            Children.Remove(connection);
            Children.Remove(element);
        }
    }
}