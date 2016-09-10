using DiagramDesigner.ResourcesLogic;
using System;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Xml;

namespace DiagramDesigner
{
    // Represents a selectable item in the Toolbox/>.
    public class ToolboxItem : ContentControl
    {
        public DesignerItem DesignerItem = null;
        // caches the start point of the drag operation
        private Point? dragStartPoint = null;

        public String Class { get; set; }

        static ToolboxItem()
        {
                        // set the key to reference the style for this control
            FrameworkElement.DefaultStyleKeyProperty.OverrideMetadata(
                typeof(ToolboxItem), new FrameworkPropertyMetadata(typeof(ToolboxItem)));
        }

        protected override void OnPreviewMouseDown(MouseButtonEventArgs e)
        {
            base.OnPreviewMouseDown(e);
            this.dragStartPoint = new Point?(e.GetPosition(this));
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);
            if (e.LeftButton != MouseButtonState.Pressed)
                this.dragStartPoint = null;

            if (this.dragStartPoint.HasValue)
            {
                // XamlWriter.Save() has limitations in exactly what is serialized,
                // see SDK documentation; short term solution only;
                string xamlString = XamlWriter.Save(this.Content);
                DragObject dataObject = new DragObject();
                dataObject.Xaml = xamlString;
                dataObject.Class = dataObject.GetClassByTag();
                this.Tag = dataObject.Class;
                

                WrapPanel panel = VisualTreeHelper.GetParent(this) as WrapPanel;
                if (panel != null)
                {
                    // desired size for DesignerCanvas is the stretched Toolbox item size
                    double scale = 1.3;
                    dataObject.DesiredSize = new Size(panel.ItemWidth * scale, panel.ItemHeight * scale);
                }

                if (DesignerItem != null)
                    dataObject.DesignerItem = DesignerItem;

                #region Раннее
                //var _dataObject = new object();
                //object designerItem = this;
                //if (designerItem is DesignerItem)
                //{
                //    _dataObject = designerItem;
                //}
                //else
                //{
                //    // XamlWriter.Save() has limitations in exactly what is serialized,
                //    // see SDK documentation; short term solution only;
                //    string xamlString = XamlWriter.Save(this.Content);
                //    DragObject dataObject = new DragObject();
                //    dataObject.Xaml = xamlString;
                //    dataObject.Class = dataObject.GetClassByTag();
                //    this.Tag = dataObject.Class; WrapPanel panel = VisualTreeHelper.GetParent(this) as WrapPanel;

                //    if (panel != null)
                //    {
                //        // desired size for DesignerCanvas is the stretched Toolbox item size
                //        double scale = 1.3;
                //        dataObject.DesiredSize = new Size(panel.ItemWidth * scale, panel.ItemHeight * scale);
                //    }
                //    _dataObject = dataObject;
                //}
                #endregion

                DragDrop.DoDragDrop(this, dataObject, DragDropEffects.Copy);

                e.Handled = true;
            }
        }
    }

    // Wraps info of the dragged object into a class
    public class DragObject
    {
        // Xaml string that represents the serialized content
        public String Xaml { get; set; }

        // Defines width and height of the DesignerItem
        // when this DragObject is dropped on the DesignerCanvas
        public Size? DesiredSize { get; set; }

        // Class of dragged object (represents what item has been selected from the toolbar)
        public String Class { get; set; }

        public String GetClassByTag()
        {
            string lv_return = "";
            
            //this doesnt work
            
            //Object content = XamlReader.Load(XmlReader.Create(new StringReader(this.Xaml)));
            //if (content != null)
            //{
            //    lv_return = ((Grid)content).ToolTip.ToString();
            //}
            
            //this works
            if (this.Xaml.Contains("FrameworkElement.Tag"))
            {
                int start = this.Xaml.IndexOf("<Setter Property=\"FrameworkElement.Tag\"><Setter.Value>") + 54;
                int i = start;
                while (this.Xaml[i] != '<')
                {
                    lv_return += this.Xaml[i];
                    i++;
                }
                return lv_return;
            }
            else return null;
        }

        public DesignerItem DesignerItem = null;
    }
}
