using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using DiagramDesigner.LogicRBP;
using DiagramDesigner.ResourcesLogic;
using System.Collections.ObjectModel;
using System.IO;
using System.Xml;
using System.Windows.Markup;
using DiagramDesigner.BusinessLogic;
using DiagramDesigner.LogicTBP;
using System.Xml.Linq;
using System.Globalization;

namespace DiagramDesigner
{
    /// <summary>
    /// Interaction logic for ResourceWindow.xaml
    /// </summary>
    public partial class ResourceWindow : Window
    {
        Operation Operation;
        public Resources Resources;
        List<BaseResource> OtherResources;
        public ResourceWindow(Operation operation, List<BaseResource> otherResources)
        {
            InitializeComponent();
            Operation = operation;
            Title = "Resources : " + Operation.Name;
            Resources = operation.Resources;
            OtherResources = otherResources;
            FillData(operation);
            Open_Executed();
        }

        private void FillData(Operation operation)
        {
            DesignerItem operationItem = this.CreateItem(operation.DesignerID, "Operation", this.Height / 3, this.Width / 3, 78);

            operationItem.Content = this.GetOperationContent();
            //operationItem.BoundLogicItem = operation;
            operationItem.dispName = operation.Name;
            operationItem.Tag = "Operation";

            ResourceDesigner.Children.Add(operationItem);
            ResourceDesigner.SetConnectorDecoratorTemplate(operationItem); 
            ResourceDesigner.OperationGuid = operationItem.ID;
            ResourceDesigner.ResourceWindow = this;

            var toolBox = new Toolbox();
            foreach (var resource in OtherResources)
            {
                var resourceItem = CreateOtherResource(resource);
                var toolBoxItem = new ToolboxItem() { DesignerItem = resourceItem, Content = resourceItem };
                toolBox.Items.Add(toolBoxItem);
            }
            otherResourcesToolbox.Content = toolBox;
            ResourceDesigner.Toolbox = toolBox;
        }

        DesignerItem CreateOtherResource(BaseResource resource)
        {
            var resourceItem = this.CreateItem(resource, 30);
            switch (resource.Name)
            {
                case "Information":
                    resourceItem.Content = this.GetInformationContent();
                    break;
                case "Finance":
                    resourceItem.Content = this.GetFinanceContent();
                    break;
                case "Labour":
                    resourceItem.Content = this.GetLabourContent();
                    break;
                case "Equipment":
                    resourceItem.Content = this.GetEquipmentContent();
                    break;
                case "Product":
                    resourceItem.Content = this.GetProductContent();
                    break;
                case "Service":
                    resourceItem.Content = this.GetServiceContent();
                    break;
                case "BusinessPartner":
                    resourceItem.Content = this.GetBusinessPartnerContent();
                    break;
            }

            return resourceItem;
        }

        private object GetBusinessPartnerContent()
        {
            String xaml = @"<Path ToolTip='Information' xmlns='http://schemas.microsoft.com/winfx/2006/xaml/presentation' xmlns:x='http://schemas.microsoft.com/winfx/2006/xaml' xmlns:s='clr-namespace:System;assembly=mscorlib' xmlns:dd='clr-namespace:DiagramDesigner;assembly=DiagramDesigner'><Path.Style><Style TargetType='Path'><Style.Resources><ResourceDictionary /></Style.Resources><Setter Property='Path.Data'><Setter.Value><StreamGeometry>M0,0L60,0 60,40 0,40z</StreamGeometry></Setter.Value></Setter><Setter Property='Shape.Fill'><Setter.Value><ImageBrush ImageSource='C:\Users\Vanya\Desktop\Новая модель ресурсов\DiagramDesigner\Resources\Images\ResourceImages\Partner.png'/></Setter.Value></Setter><Setter Property='Shape.Stretch'><Setter.Value><x:Static Member='Stretch.Fill' /></Setter.Value></Setter><Setter Property='UIElement.IsHitTestVisible'><Setter.Value><s:Boolean>False</s:Boolean></Setter.Value></Setter><Setter Property='UIElement.SnapsToDevicePixels'><Setter.Value><s:Boolean>True</s:Boolean></Setter.Value></Setter><Setter Property='FrameworkElement.Tag'><Setter.Value>Information</Setter.Value></Setter></Style></Path.Style><dd:DesignerItem.DragThumbTemplate><ControlTemplate><Path><Path.Style><Style TargetType='Path'><Style.BasedOn><Style TargetType='Path'><Style.Resources><ResourceDictionary /></Style.Resources><Setter Property='Path.Data'><Setter.Value><StreamGeometry>M0,0L60,0 60,40 0,40z</StreamGeometry></Setter.Value></Setter><Setter Property='Shape.Fill'><Setter.Value><ImageBrush ImageSource='C:\Users\Vanya\Desktop\Новая модель ресурсов\DiagramDesigner\Resources\Images\ResourceImages\Partner.png'/></Setter.Value></Setter><Setter Property='Shape.Stretch'><Setter.Value><x:Static Member='Stretch.Fill' /></Setter.Value></Setter><Setter Property='UIElement.IsHitTestVisible'><Setter.Value><s:Boolean>False</s:Boolean></Setter.Value></Setter><Setter Property='UIElement.SnapsToDevicePixels'><Setter.Value><s:Boolean>True</s:Boolean></Setter.Value></Setter><Setter Property='FrameworkElement.Tag'><Setter.Value>Information</Setter.Value></Setter></Style></Style.BasedOn><Style.Resources><ResourceDictionary /></Style.Resources><Setter Property='UIElement.IsHitTestVisible'><Setter.Value><s:Boolean>True</s:Boolean></Setter.Value></Setter></Style></Path.Style></Path></ControlTemplate></dd:DesignerItem.DragThumbTemplate></Path>";
            StringReader stringReader = new StringReader(xaml);
            XmlReader xmlReader = XmlReader.Create(stringReader);
            Object content = XamlReader.Load(xmlReader);
            return content;
        }
        private object GetServiceContent()
        {
            String xaml = @"<Path ToolTip='Information' xmlns='http://schemas.microsoft.com/winfx/2006/xaml/presentation' xmlns:x='http://schemas.microsoft.com/winfx/2006/xaml' xmlns:s='clr-namespace:System;assembly=mscorlib' xmlns:dd='clr-namespace:DiagramDesigner;assembly=DiagramDesigner'><Path.Style><Style TargetType='Path'><Style.Resources><ResourceDictionary /></Style.Resources><Setter Property='Path.Data'><Setter.Value><StreamGeometry>M0,0L60,0 60,40 0,40z</StreamGeometry></Setter.Value></Setter><Setter Property='Shape.Fill'><Setter.Value><ImageBrush ImageSource='C:\Users\Vanya\Desktop\Новая модель ресурсов\DiagramDesigner\Resources\Images\ResourceImages\service.png'/></Setter.Value></Setter><Setter Property='Shape.Stretch'><Setter.Value><x:Static Member='Stretch.Fill' /></Setter.Value></Setter><Setter Property='UIElement.IsHitTestVisible'><Setter.Value><s:Boolean>False</s:Boolean></Setter.Value></Setter><Setter Property='UIElement.SnapsToDevicePixels'><Setter.Value><s:Boolean>True</s:Boolean></Setter.Value></Setter><Setter Property='FrameworkElement.Tag'><Setter.Value>Information</Setter.Value></Setter></Style></Path.Style><dd:DesignerItem.DragThumbTemplate><ControlTemplate><Path><Path.Style><Style TargetType='Path'><Style.BasedOn><Style TargetType='Path'><Style.Resources><ResourceDictionary /></Style.Resources><Setter Property='Path.Data'><Setter.Value><StreamGeometry>M0,0L60,0 60,40 0,40z</StreamGeometry></Setter.Value></Setter><Setter Property='Shape.Fill'><Setter.Value><ImageBrush ImageSource='C:\Users\Vanya\Desktop\Новая модель ресурсов\DiagramDesigner\Resources\Images\ResourceImages\service.png'/></Setter.Value></Setter><Setter Property='Shape.Stretch'><Setter.Value><x:Static Member='Stretch.Fill' /></Setter.Value></Setter><Setter Property='UIElement.IsHitTestVisible'><Setter.Value><s:Boolean>False</s:Boolean></Setter.Value></Setter><Setter Property='UIElement.SnapsToDevicePixels'><Setter.Value><s:Boolean>True</s:Boolean></Setter.Value></Setter><Setter Property='FrameworkElement.Tag'><Setter.Value>Information</Setter.Value></Setter></Style></Style.BasedOn><Style.Resources><ResourceDictionary /></Style.Resources><Setter Property='UIElement.IsHitTestVisible'><Setter.Value><s:Boolean>True</s:Boolean></Setter.Value></Setter></Style></Path.Style></Path></ControlTemplate></dd:DesignerItem.DragThumbTemplate></Path>";
            StringReader stringReader = new StringReader(xaml);
            XmlReader xmlReader = XmlReader.Create(stringReader);
            Object content = XamlReader.Load(xmlReader);
            return content;
        }
        private object GetProductContent()
        {
            String xaml = @"<Path ToolTip='Information' xmlns='http://schemas.microsoft.com/winfx/2006/xaml/presentation' xmlns:x='http://schemas.microsoft.com/winfx/2006/xaml' xmlns:s='clr-namespace:System;assembly=mscorlib' xmlns:dd='clr-namespace:DiagramDesigner;assembly=DiagramDesigner'><Path.Style><Style TargetType='Path'><Style.Resources><ResourceDictionary /></Style.Resources><Setter Property='Path.Data'><Setter.Value><StreamGeometry>M0,0L60,0 60,40 0,40z</StreamGeometry></Setter.Value></Setter><Setter Property='Shape.Fill'><Setter.Value><ImageBrush ImageSource='C:\Users\Vanya\Desktop\Новая модель ресурсов\DiagramDesigner\Resources\Images\ResourceImages\product.png'/></Setter.Value></Setter><Setter Property='Shape.Stretch'><Setter.Value><x:Static Member='Stretch.Fill' /></Setter.Value></Setter><Setter Property='UIElement.IsHitTestVisible'><Setter.Value><s:Boolean>False</s:Boolean></Setter.Value></Setter><Setter Property='UIElement.SnapsToDevicePixels'><Setter.Value><s:Boolean>True</s:Boolean></Setter.Value></Setter><Setter Property='FrameworkElement.Tag'><Setter.Value>Information</Setter.Value></Setter></Style></Path.Style><dd:DesignerItem.DragThumbTemplate><ControlTemplate><Path><Path.Style><Style TargetType='Path'><Style.BasedOn><Style TargetType='Path'><Style.Resources><ResourceDictionary /></Style.Resources><Setter Property='Path.Data'><Setter.Value><StreamGeometry>M0,0L60,0 60,40 0,40z</StreamGeometry></Setter.Value></Setter><Setter Property='Shape.Fill'><Setter.Value><ImageBrush ImageSource='C:\Users\Vanya\Desktop\Новая модель ресурсов\DiagramDesigner\Resources\Images\ResourceImages\product.png'/></Setter.Value></Setter><Setter Property='Shape.Stretch'><Setter.Value><x:Static Member='Stretch.Fill' /></Setter.Value></Setter><Setter Property='UIElement.IsHitTestVisible'><Setter.Value><s:Boolean>False</s:Boolean></Setter.Value></Setter><Setter Property='UIElement.SnapsToDevicePixels'><Setter.Value><s:Boolean>True</s:Boolean></Setter.Value></Setter><Setter Property='FrameworkElement.Tag'><Setter.Value>Information</Setter.Value></Setter></Style></Style.BasedOn><Style.Resources><ResourceDictionary /></Style.Resources><Setter Property='UIElement.IsHitTestVisible'><Setter.Value><s:Boolean>True</s:Boolean></Setter.Value></Setter></Style></Path.Style></Path></ControlTemplate></dd:DesignerItem.DragThumbTemplate></Path>";
            StringReader stringReader = new StringReader(xaml);
            XmlReader xmlReader = XmlReader.Create(stringReader);
            Object content = XamlReader.Load(xmlReader);
            return content;
        }
        private object GetEquipmentContent()
        {
            String xaml = @"<Path ToolTip='Information' xmlns='http://schemas.microsoft.com/winfx/2006/xaml/presentation' xmlns:x='http://schemas.microsoft.com/winfx/2006/xaml' xmlns:s='clr-namespace:System;assembly=mscorlib' xmlns:dd='clr-namespace:DiagramDesigner;assembly=DiagramDesigner'><Path.Style><Style TargetType='Path'><Style.Resources><ResourceDictionary /></Style.Resources><Setter Property='Path.Data'><Setter.Value><StreamGeometry>M0,0L60,0 60,40 0,40z</StreamGeometry></Setter.Value></Setter><Setter Property='Shape.Fill'><Setter.Value><ImageBrush ImageSource='C:\Users\Vanya\Desktop\Новая модель ресурсов\DiagramDesigner\Resources\Images\ResourceImages\equipment.png'/></Setter.Value></Setter><Setter Property='Shape.Stretch'><Setter.Value><x:Static Member='Stretch.Fill' /></Setter.Value></Setter><Setter Property='UIElement.IsHitTestVisible'><Setter.Value><s:Boolean>False</s:Boolean></Setter.Value></Setter><Setter Property='UIElement.SnapsToDevicePixels'><Setter.Value><s:Boolean>True</s:Boolean></Setter.Value></Setter><Setter Property='FrameworkElement.Tag'><Setter.Value>Information</Setter.Value></Setter></Style></Path.Style><dd:DesignerItem.DragThumbTemplate><ControlTemplate><Path><Path.Style><Style TargetType='Path'><Style.BasedOn><Style TargetType='Path'><Style.Resources><ResourceDictionary /></Style.Resources><Setter Property='Path.Data'><Setter.Value><StreamGeometry>M0,0L60,0 60,40 0,40z</StreamGeometry></Setter.Value></Setter><Setter Property='Shape.Fill'><Setter.Value><ImageBrush ImageSource='C:\Users\Vanya\Desktop\Новая модель ресурсов\DiagramDesigner\Resources\Images\ResourceImages\equipment.png'/></Setter.Value></Setter><Setter Property='Shape.Stretch'><Setter.Value><x:Static Member='Stretch.Fill' /></Setter.Value></Setter><Setter Property='UIElement.IsHitTestVisible'><Setter.Value><s:Boolean>False</s:Boolean></Setter.Value></Setter><Setter Property='UIElement.SnapsToDevicePixels'><Setter.Value><s:Boolean>True</s:Boolean></Setter.Value></Setter><Setter Property='FrameworkElement.Tag'><Setter.Value>Information</Setter.Value></Setter></Style></Style.BasedOn><Style.Resources><ResourceDictionary /></Style.Resources><Setter Property='UIElement.IsHitTestVisible'><Setter.Value><s:Boolean>True</s:Boolean></Setter.Value></Setter></Style></Path.Style></Path></ControlTemplate></dd:DesignerItem.DragThumbTemplate></Path>";
            StringReader stringReader = new StringReader(xaml);
            XmlReader xmlReader = XmlReader.Create(stringReader);
            Object content = XamlReader.Load(xmlReader);
            return content;
        }
        private object GetLabourContent()
        {
            String xaml = @"<Path ToolTip='Information' xmlns='http://schemas.microsoft.com/winfx/2006/xaml/presentation' xmlns:x='http://schemas.microsoft.com/winfx/2006/xaml' xmlns:s='clr-namespace:System;assembly=mscorlib' xmlns:dd='clr-namespace:DiagramDesigner;assembly=DiagramDesigner'><Path.Style><Style TargetType='Path'><Style.Resources><ResourceDictionary /></Style.Resources><Setter Property='Path.Data'><Setter.Value><StreamGeometry>M0,0L60,0 60,40 0,40z</StreamGeometry></Setter.Value></Setter><Setter Property='Shape.Fill'><Setter.Value><ImageBrush ImageSource='C:\Users\Vanya\Desktop\Новая модель ресурсов\DiagramDesigner\Resources\Images\ResourceImages\labour.png'/></Setter.Value></Setter><Setter Property='Shape.Stretch'><Setter.Value><x:Static Member='Stretch.Fill' /></Setter.Value></Setter><Setter Property='UIElement.IsHitTestVisible'><Setter.Value><s:Boolean>False</s:Boolean></Setter.Value></Setter><Setter Property='UIElement.SnapsToDevicePixels'><Setter.Value><s:Boolean>True</s:Boolean></Setter.Value></Setter><Setter Property='FrameworkElement.Tag'><Setter.Value>Information</Setter.Value></Setter></Style></Path.Style><dd:DesignerItem.DragThumbTemplate><ControlTemplate><Path><Path.Style><Style TargetType='Path'><Style.BasedOn><Style TargetType='Path'><Style.Resources><ResourceDictionary /></Style.Resources><Setter Property='Path.Data'><Setter.Value><StreamGeometry>M0,0L60,0 60,40 0,40z</StreamGeometry></Setter.Value></Setter><Setter Property='Shape.Fill'><Setter.Value><ImageBrush ImageSource='C:\Users\Vanya\Desktop\Новая модель ресурсов\DiagramDesigner\Resources\Images\ResourceImages\labour.png'/></Setter.Value></Setter><Setter Property='Shape.Stretch'><Setter.Value><x:Static Member='Stretch.Fill' /></Setter.Value></Setter><Setter Property='UIElement.IsHitTestVisible'><Setter.Value><s:Boolean>False</s:Boolean></Setter.Value></Setter><Setter Property='UIElement.SnapsToDevicePixels'><Setter.Value><s:Boolean>True</s:Boolean></Setter.Value></Setter><Setter Property='FrameworkElement.Tag'><Setter.Value>Information</Setter.Value></Setter></Style></Style.BasedOn><Style.Resources><ResourceDictionary /></Style.Resources><Setter Property='UIElement.IsHitTestVisible'><Setter.Value><s:Boolean>True</s:Boolean></Setter.Value></Setter></Style></Path.Style></Path></ControlTemplate></dd:DesignerItem.DragThumbTemplate></Path>";
            StringReader stringReader = new StringReader(xaml);
            XmlReader xmlReader = XmlReader.Create(stringReader);
            Object content = XamlReader.Load(xmlReader);
            return content;
        }
        private object GetFinanceContent()
        {
            String xaml = @"<Path ToolTip='Information' xmlns='http://schemas.microsoft.com/winfx/2006/xaml/presentation' xmlns:x='http://schemas.microsoft.com/winfx/2006/xaml' xmlns:s='clr-namespace:System;assembly=mscorlib' xmlns:dd='clr-namespace:DiagramDesigner;assembly=DiagramDesigner'><Path.Style><Style TargetType='Path'><Style.Resources><ResourceDictionary /></Style.Resources><Setter Property='Path.Data'><Setter.Value><StreamGeometry>M0,0L60,0 60,40 0,40z</StreamGeometry></Setter.Value></Setter><Setter Property='Shape.Fill'><Setter.Value><ImageBrush ImageSource='C:\Users\Vanya\Desktop\Новая модель ресурсов\DiagramDesigner\Resources\Images\ResourceImages\finance.png'/></Setter.Value></Setter><Setter Property='Shape.Stretch'><Setter.Value><x:Static Member='Stretch.Fill' /></Setter.Value></Setter><Setter Property='UIElement.IsHitTestVisible'><Setter.Value><s:Boolean>False</s:Boolean></Setter.Value></Setter><Setter Property='UIElement.SnapsToDevicePixels'><Setter.Value><s:Boolean>True</s:Boolean></Setter.Value></Setter><Setter Property='FrameworkElement.Tag'><Setter.Value>Information</Setter.Value></Setter></Style></Path.Style><dd:DesignerItem.DragThumbTemplate><ControlTemplate><Path><Path.Style><Style TargetType='Path'><Style.BasedOn><Style TargetType='Path'><Style.Resources><ResourceDictionary /></Style.Resources><Setter Property='Path.Data'><Setter.Value><StreamGeometry>M0,0L60,0 60,40 0,40z</StreamGeometry></Setter.Value></Setter><Setter Property='Shape.Fill'><Setter.Value><ImageBrush ImageSource='C:\Users\Vanya\Desktop\Новая модель ресурсов\DiagramDesigner\Resources\Images\ResourceImages\finance.png'/></Setter.Value></Setter><Setter Property='Shape.Stretch'><Setter.Value><x:Static Member='Stretch.Fill' /></Setter.Value></Setter><Setter Property='UIElement.IsHitTestVisible'><Setter.Value><s:Boolean>False</s:Boolean></Setter.Value></Setter><Setter Property='UIElement.SnapsToDevicePixels'><Setter.Value><s:Boolean>True</s:Boolean></Setter.Value></Setter><Setter Property='FrameworkElement.Tag'><Setter.Value>Information</Setter.Value></Setter></Style></Style.BasedOn><Style.Resources><ResourceDictionary /></Style.Resources><Setter Property='UIElement.IsHitTestVisible'><Setter.Value><s:Boolean>True</s:Boolean></Setter.Value></Setter></Style></Path.Style></Path></ControlTemplate></dd:DesignerItem.DragThumbTemplate></Path>";
            StringReader stringReader = new StringReader(xaml);
            XmlReader xmlReader = XmlReader.Create(stringReader);
            Object content = XamlReader.Load(xmlReader);
            return content;
        }
        private object GetInformationContent()
        {
            String xaml = @"<Path ToolTip='Information' xmlns='http://schemas.microsoft.com/winfx/2006/xaml/presentation' xmlns:x='http://schemas.microsoft.com/winfx/2006/xaml' xmlns:s='clr-namespace:System;assembly=mscorlib' xmlns:dd='clr-namespace:DiagramDesigner;assembly=DiagramDesigner'><Path.Style><Style TargetType='Path'><Style.Resources><ResourceDictionary /></Style.Resources><Setter Property='Path.Data'><Setter.Value><StreamGeometry>M0,0L60,0 60,40 0,40z</StreamGeometry></Setter.Value></Setter><Setter Property='Shape.Fill'><Setter.Value><ImageBrush ImageSource='C:\Users\Vanya\Desktop\Новая модель ресурсов\DiagramDesigner\Resources\Images\ResourceImages\information.png'/></Setter.Value></Setter><Setter Property='Shape.Stretch'><Setter.Value><x:Static Member='Stretch.Fill' /></Setter.Value></Setter><Setter Property='UIElement.IsHitTestVisible'><Setter.Value><s:Boolean>False</s:Boolean></Setter.Value></Setter><Setter Property='UIElement.SnapsToDevicePixels'><Setter.Value><s:Boolean>True</s:Boolean></Setter.Value></Setter><Setter Property='FrameworkElement.Tag'><Setter.Value>Information</Setter.Value></Setter></Style></Path.Style><dd:DesignerItem.DragThumbTemplate><ControlTemplate><Path><Path.Style><Style TargetType='Path'><Style.BasedOn><Style TargetType='Path'><Style.Resources><ResourceDictionary /></Style.Resources><Setter Property='Path.Data'><Setter.Value><StreamGeometry>M0,0L60,0 60,40 0,40z</StreamGeometry></Setter.Value></Setter><Setter Property='Shape.Fill'><Setter.Value><ImageBrush ImageSource='C:\Users\Vanya\Desktop\Новая модель ресурсов\DiagramDesigner\Resources\Images\ResourceImages\information.png'/></Setter.Value></Setter><Setter Property='Shape.Stretch'><Setter.Value><x:Static Member='Stretch.Fill' /></Setter.Value></Setter><Setter Property='UIElement.IsHitTestVisible'><Setter.Value><s:Boolean>False</s:Boolean></Setter.Value></Setter><Setter Property='UIElement.SnapsToDevicePixels'><Setter.Value><s:Boolean>True</s:Boolean></Setter.Value></Setter><Setter Property='FrameworkElement.Tag'><Setter.Value>Information</Setter.Value></Setter></Style></Style.BasedOn><Style.Resources><ResourceDictionary /></Style.Resources><Setter Property='UIElement.IsHitTestVisible'><Setter.Value><s:Boolean>True</s:Boolean></Setter.Value></Setter></Style></Path.Style></Path></ControlTemplate></dd:DesignerItem.DragThumbTemplate></Path>";
            StringReader stringReader = new StringReader(xaml);
            XmlReader xmlReader = XmlReader.Create(stringReader);
            Object content = XamlReader.Load(xmlReader);
            return content;
        }
        private object GetOperationContent()
        {
            String xaml = "<Path ToolTip='Operation' xmlns='http://schemas.microsoft.com/winfx/2006/xaml/presentation' xmlns:x='http://schemas.microsoft.com/winfx/2006/xaml' xmlns:s='clr-namespace:System;assembly=mscorlib' xmlns:dd='clr-namespace:DiagramDesigner;assembly=DiagramDesigner'><Path.Style><Style TargetType='Path'><Style.Resources><ResourceDictionary /></Style.Resources><Setter Property='Path.Data'><Setter.Value><StreamGeometry>M0,0L60,0 60,40 0,40z</StreamGeometry></Setter.Value></Setter><Setter Property='Shape.Fill'><Setter.Value><SolidColorBrush>#FFFFE4B5</SolidColorBrush></Setter.Value></Setter><Setter Property='Shape.Stretch'><Setter.Value><x:Static Member='Stretch.Fill' /></Setter.Value></Setter><Setter Property='UIElement.IsHitTestVisible'><Setter.Value><s:Boolean>False</s:Boolean></Setter.Value></Setter><Setter Property='UIElement.SnapsToDevicePixels'><Setter.Value><s:Boolean>True</s:Boolean></Setter.Value></Setter><Setter Property='FrameworkElement.Tag'><Setter.Value>OperationTBP</Setter.Value></Setter></Style></Path.Style><dd:DesignerItem.DragThumbTemplate><ControlTemplate><Path><Path.Style><Style TargetType='Path'><Style.BasedOn><Style TargetType='Path'><Style.Resources><ResourceDictionary /></Style.Resources><Setter Property='Path.Data'><Setter.Value><StreamGeometry>M0,0L60,0 60,40 0,40z</StreamGeometry></Setter.Value></Setter><Setter Property='Shape.Fill'><Setter.Value><SolidColorBrush>#FFFFE4B5</SolidColorBrush></Setter.Value></Setter><Setter Property='Shape.Stretch'><Setter.Value><x:Static Member='Stretch.Fill' /></Setter.Value></Setter><Setter Property='UIElement.IsHitTestVisible'><Setter.Value><s:Boolean>False</s:Boolean></Setter.Value></Setter><Setter Property='UIElement.SnapsToDevicePixels'><Setter.Value><s:Boolean>True</s:Boolean></Setter.Value></Setter><Setter Property='FrameworkElement.Tag'><Setter.Value>OperationTBP</Setter.Value></Setter></Style></Style.BasedOn><Style.Resources><ResourceDictionary /></Style.Resources><Setter Property='UIElement.IsHitTestVisible'><Setter.Value><s:Boolean>True</s:Boolean></Setter.Value></Setter></Style></Path.Style></Path></ControlTemplate></dd:DesignerItem.DragThumbTemplate></Path>";
            StringReader stringReader = new StringReader(xaml);
            XmlReader xmlReader = XmlReader.Create(stringReader);
            Object content = XamlReader.Load(xmlReader);
            return content;
        }
        
        private DesignerItem CreateItem(Guid designerID, string name, Double OffsetY, Double OffsetX, int size)
        {
            DesignerItem item = new DesignerItem(designerID, name);

            //item size must be dynamically defined, this is temporary solution
            item.Width = size;
            item.Height = size;

            //item postion on canvas
            Canvas.SetLeft(item, OffsetX);
            Canvas.SetTop(item, OffsetY);

            return item;
        }

        private DesignerItem CreateItem(BaseResource resource, int size)
        {
            DesignerItem item = new DesignerItem(resource.Name, resource.DesignerID);

            item.Width = size;
            item.Height = size;
            item.BoundLogicItem = resource;
            item.dispName = resource.Name;
            item.Tag = resource.Name;

            return item;
        }

        public void UpdateResource(DesignerItem designerItem)
        {
            if (designerItem.BoundLogicItem is InformationResource)
            {
                //MessageBox.Show("UpdateResource1 : " + designerItem.BoundLogicItem.ID.ToString());
                var item = designerItem.BoundLogicItem as InformationResource;
                var resourceWindow = new InformationWindow(item);
                resourceWindow.ShowDialog();

                if (resourceWindow.DialogResult.HasValue && resourceWindow.DialogResult.Value)
                {
                    if (Resources.Contains(item))
                        Resources.Remove(item);
                    Resources.Add(resourceWindow.InformationResource);
                    designerItem.BoundLogicItem = resourceWindow.InformationResource;
                }
                else
                {
                    ResourceDesigner.RemoveChild(designerItem);
                    //Toolbox.
                }
                return;
            }

            if (designerItem.BoundLogicItem is FinancialResource)
            {
                var item = designerItem.BoundLogicItem as FinancialResource;
                var resourceWindow = new FinanceWindow(item);
                resourceWindow.ShowDialog();

                if (resourceWindow.DialogResult.HasValue && resourceWindow.DialogResult.Value)
                {
                    if (Resources.Contains(item))
                        Resources.Remove(item);
                    Resources.Add(resourceWindow.FinancialResource);
                    designerItem.BoundLogicItem = resourceWindow.FinancialResource;
                }
                return;
            }

            if (designerItem.BoundLogicItem is LabourForce)
            {
                var item = designerItem.BoundLogicItem as LabourForce;
                var resourceWindow = new LabourWindow(item);
                resourceWindow.ShowDialog();

                if (resourceWindow.DialogResult.HasValue && resourceWindow.DialogResult.Value)
                {
                    if (Resources.Contains(item))
                        Resources.Remove(item);
                    Resources.Add(resourceWindow.LabourForce);
                    designerItem.BoundLogicItem = resourceWindow.LabourForce;
                }
                return;
            }

            if (designerItem.BoundLogicItem is Equipment)
            {
                var item = designerItem.BoundLogicItem as Equipment;
                var resourceWindow = new EquipmentWindow(item);
                resourceWindow.ShowDialog();

                if (resourceWindow.DialogResult.HasValue && resourceWindow.DialogResult.Value)
                {
                    if (Resources.Contains(item))
                        Resources.Remove(item);
                    Resources.Add(resourceWindow.Equipment);
                    designerItem.BoundLogicItem = resourceWindow.Equipment;
                }
                return;
            }

            if (designerItem.BoundLogicItem is Product)
            {
                var item = designerItem.BoundLogicItem as Product;
                var resourceWindow = new ProductWindow(item);
                resourceWindow.ShowDialog();

                if (resourceWindow.DialogResult.HasValue && resourceWindow.DialogResult.Value)
                {
                    if (Resources.Contains(item))
                        Resources.Remove(item);
                    Resources.Add(resourceWindow.Product);
                    designerItem.BoundLogicItem = resourceWindow.Product;
                }
                return;
            }

            if (designerItem.BoundLogicItem is Service)
            {
                var item = designerItem.BoundLogicItem as Service;
                var resourceWindow = new ServiceWindow(item);
                resourceWindow.ShowDialog();

                if (resourceWindow.DialogResult.HasValue && resourceWindow.DialogResult.Value)
                {
                    if (Resources.Contains(item))
                        Resources.Remove(item);
                    Resources.Add(resourceWindow.Service);
                    designerItem.BoundLogicItem = resourceWindow.Service;
                }
                return;
            }
        }

        public void AddResource(DesignerItem designerItem)
        {
            switch(designerItem.Tag.ToString())
            {
                case "Information":
                    var informationWindow = new InformationWindow(new InformationResource());
                    informationWindow.ShowDialog();
                    if (informationWindow.DialogResult.HasValue && informationWindow.DialogResult.Value)
                    {
                        Resources.Add(informationWindow.InformationResource);
                        designerItem.BoundLogicItem = informationWindow.InformationResource;
                        //designerItem.dispName = informationWindow.InformationResource.Title;
                    }
                    break;
                    //resource = new InformationResource(Guid.NewGuid(), this.id, "Operation" + num); break;
                case "Finance":
                    var financeWindow = new FinanceWindow(new FinancialResource());
                    financeWindow.ShowDialog();
                    if (financeWindow.DialogResult.HasValue && financeWindow.DialogResult.Value)
                    {
                        Resources.Add(financeWindow.FinancialResource);
                        designerItem.BoundLogicItem = financeWindow.FinancialResource;
                        //designerItem.dispName = financeWindow.FinancialResource.Title;
                    }
                    break;
                case "Labour":
                    var labourWindow = new LabourWindow(new LabourForce());
                    labourWindow.ShowDialog();
                    if (labourWindow.DialogResult.HasValue && labourWindow.DialogResult.Value)
                    {
                        Resources.Add(labourWindow.LabourForce);
                        designerItem.BoundLogicItem = labourWindow.LabourForce;
                        //designerItem.dispName = financeWindow.FinancialResource.Title;
                    }
                    break;
                case "Equipment":
                    var equipmentWindow = new EquipmentWindow(new Equipment());
                    equipmentWindow.ShowDialog();
                    if (equipmentWindow.DialogResult.HasValue && equipmentWindow.DialogResult.Value)
                    {
                        Resources.Add(equipmentWindow.Equipment);
                        designerItem.BoundLogicItem = equipmentWindow.Equipment;
                        //designerItem.dispName = financeWindow.FinancialResource.Title;
                    }
                    break;
                case "Product":
                    var productWindow = new ProductWindow(new Product());
                    productWindow.ShowDialog();
                    if (productWindow.DialogResult.HasValue && productWindow.DialogResult.Value)
                    {
                        Resources.Add(productWindow.Product);
                        designerItem.BoundLogicItem = productWindow.Product;
                        //designerItem.dispName = financeWindow.FinancialResource.Title;
                    }
                    break;
                case "Service":
                    var serviceWindow = new ServiceWindow(new Service());
                    serviceWindow.ShowDialog();
                    if (serviceWindow.DialogResult.HasValue && serviceWindow.DialogResult.Value)
                    {
                        Resources.Add(serviceWindow.Service);
                        designerItem.BoundLogicItem = serviceWindow.Service;
                        //designerItem.dispName = financeWindow.FinancialResource.Title;
                    }
                    break;
                default: return;
            }
        }

        private void removeButton_Click(object sender, RoutedEventArgs e)
        {
            //if (dataGrid.SelectedIndex != -1)
            //    _resources.RemoveAt(dataGrid.SelectedIndex);
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            //var menuItem = sender as MenuItem;
            //if (menuItem == InformationMenuItem)
            //{
            //    var informationWindow = new InformationWindow(new InformationResource());
            //    informationWindow.ShowDialog();

            //    if (informationWindow.DialogResult.HasValue && informationWindow.DialogResult.Value)
            //    {
            //        _resources.Add(informationWindow.InformationResource);
            //    }
            //    return;
            //}

            //if (menuItem == FinanceMenuItem)
            //{
            //    var financeWindow = new FinanceWindow(new FinancialResource());
            //    financeWindow.ShowDialog();

            //    if (financeWindow.DialogResult.HasValue && financeWindow.DialogResult.Value)
            //    {
            //        _resources.Add(financeWindow.FinancialResource);
            //    }
            //    return;
            //}

            //if (menuItem == LabourMenuItem)
            //{
            //    var labourWindow = new LabourWindow(new LabourForce());
            //    labourWindow.ShowDialog();

            //    if (labourWindow.DialogResult.HasValue && labourWindow.DialogResult.Value)
            //    {
            //        _resources.Add(labourWindow.LabourForce);
            //    }
            //    return;
            //}

            //if (menuItem == EquipmentMenuItem)
            //{
            //    var equipmentWindow = new EquipmentWindow(new Equipment());
            //    equipmentWindow.ShowDialog();

            //    if (equipmentWindow.DialogResult.HasValue && equipmentWindow.DialogResult.Value)
            //    {
            //        _resources.Add(equipmentWindow.Equipment);
            //    }
            //    return;
            //}

            //if (menuItem == ProductMenuItem)
            //{
            //    var productWindow = new ProductWindow(new Product());
            //    productWindow.ShowDialog();

            //    if (productWindow.DialogResult.HasValue && productWindow.DialogResult.Value)
            //    {
            //        _resources.Add(productWindow.Product);
            //    }
            //    return;
            //}

            //if (menuItem == ServiceMenuItem)
            //{
            //    var serviceWindow = new ServiceWindow(new Service());
            //    serviceWindow.ShowDialog();

            //    if (serviceWindow.DialogResult.HasValue && serviceWindow.DialogResult.Value)
            //    {
            //        _resources.Add(serviceWindow.Service);
            //    }
            //    return;
            //}
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Save_Executed();
            //Resources = _resources.ToList();
            DialogResult = true;
        }

        private void Save_Executed()
        {
            IEnumerable<DesignerItem> designerItems = ResourceDesigner.Children.OfType<DesignerItem>();
            IEnumerable<Connection> connections = ResourceDesigner.Children.OfType<Connection>();
            List<BaseLogic> logicItems = new List<BaseLogic>();

            //populate logicItems list from designerItems collection
            foreach (DesignerItem item in designerItems)
            {
                logicItems.Add(item.BoundLogicItem);
            }

            XElement designerItemsXML = SerializeDesignerItems(designerItems);
            XElement logicItemsXML = SerializeLogicItems(logicItems);
            XElement connectionsXML = SerializeConnections(connections);

            XElement root = new XElement("Root");
            root.Add(designerItemsXML);
            root.Add(logicItemsXML);
            root.Add(connectionsXML);

            SaveFile(root);
        }

        private void Open_Executed()
        {
            XElement root = LoadSerializedDataFromFile();

            if (root == null)
                return;

            ResourceDesigner.Children.Clear();
            ResourceDesigner.SelectionService.ClearSelection();

            //deserialize logic items fron xml and populate logic item list
            IEnumerable<XElement> LogicItemsXML = root.Elements("LogicItems").Elements("LogicItem");
            List<BaseLogic> LogicItems = new List<BaseLogic>();
            foreach (XElement itemXML in LogicItemsXML)
            {
                BaseLogic item = DeserializeLogicItem(itemXML);
                LogicItems.Add(item);
            }

            IEnumerable<XElement> DesignerItemsXML = root.Elements("DesignerItems").Elements("DesignerItem");
            foreach (XElement itemXML in DesignerItemsXML)
            {
                Guid id = new Guid(itemXML.Element("ID").Value);
                DesignerItem item = DeserializeDesignerItem(itemXML, id, 0, 0);
                //get logic item from logic item list
                item.BoundLogicItem = LogicItems.Find(p => p.DesignerID == item.ID);
                item.dispName = item.BoundLogicItem.Name;
                //if (item.BoundLogicItem is BaseResource)
                    ResourceDesigner.AddChild(item);
                //else
                //    ResourceDesigner.Children.Add(item);
                SetConnectorDecoratorTemplate(item);
            }


            this.InvalidateVisual();

            IEnumerable<XElement> connectionsXML = root.Elements("Connections").Elements("Connection");
            foreach (XElement connectionXML in connectionsXML)
            {
                Guid sourceID = new Guid(connectionXML.Element("SourceID").Value);
                Guid sinkID = new Guid(connectionXML.Element("SinkID").Value);

                String sourceConnectorName = connectionXML.Element("SourceConnectorName").Value;
                String sinkConnectorName = connectionXML.Element("SinkConnectorName").Value;

                Connector sourceConnector = GetConnector(sourceID, sourceConnectorName);
                Connector sinkConnector = GetConnector(sinkID, sinkConnectorName);

                Connection connection = new Connection(sourceConnector, sinkConnector);
                Canvas.SetZIndex(connection, Int32.Parse(connectionXML.Element("zIndex").Value));
                ResourceDesigner.Children.Add(connection);
            }
        }

        private XElement LoadSerializedDataFromFile()
        {
            try
            {
                return XElement.Load(Operation.Name + Operation.ID);
            }
            catch (Exception e)
            {
                //MessageBox.Show(e.StackTrace, e.Message, MessageBoxButton.OK, MessageBoxImage.Error);
                //MessageBox.Show("New operation");
            }

            return null;
        }

        void SaveFile(XElement xElement)
        {
            try
            {
                xElement.Save(Operation.Name + Operation.ID);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.StackTrace, ex.Message, MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private XElement LoadSerializedDataFromClipBoard()
        {
            if (Clipboard.ContainsData(DataFormats.Xaml))
            {
                String clipboardData = Clipboard.GetData(DataFormats.Xaml) as String;

                if (String.IsNullOrEmpty(clipboardData))
                    return null;
                try
                {
                    return XElement.Load(new StringReader(clipboardData));
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.StackTrace, e.Message, MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }

            return null;
        }

        private XElement SerializeDesignerItems(IEnumerable<DesignerItem> designerItems)
        {
            XElement serializedItems = new XElement("DesignerItems",
                                       from item in designerItems
                                       let contentXaml = XamlWriter.Save(((DesignerItem)item).Content)
                                       select new XElement("DesignerItem",
                                                  new XElement("Left", Canvas.GetLeft(item)),
                                                  new XElement("Top", Canvas.GetTop(item)),
                                                  new XElement("Width", item.Width),
                                                  new XElement("Height", item.Height),
                                                  new XElement("ID", item.ID),
                                                  new XElement("zIndex", Canvas.GetZIndex(item)),
                                                  new XElement("IsGroup", item.IsGroup),
                                                  new XElement("ParentID", item.ParentID),
                                                  new XElement("Content", contentXaml),
                                                  new XElement("Tag", item.Tag)
                                              )
                                   );

            return serializedItems;
        }

        private XElement SerializeLogicItems(List<BaseLogic> logicItems)
        {
            XElement serializedItems = new XElement("LogicItems",
                                       from item in logicItems
                                       select new XElement("LogicItem",
                                                  new XElement("ID", item.ID),
                                                  new XElement("DesignerID", item.DesignerID),
                                                  new XElement("Name", item.Name)
                                              )
                                   );
            serializedItems = new XElement("LogicItems", logicItems.Select(x => SerializeLogicItem(x)).ToList());
            
            return serializedItems;
        }

        private XElement SerializeLogicItem(BaseLogic baseLogic)
        {
            var xElement = new XElement("LogicItem",
                new XElement("ID", baseLogic.ID),
                new XElement("DesignerID", baseLogic.DesignerID),
                new XElement("Name", baseLogic.Name));

            if (baseLogic is InformationResource)
            {
                var item = baseLogic as InformationResource;
                xElement.Add(new XElement[]
                {
                    new XElement("Category", item.Category),
                    new XElement("CreationDate", item.CreationDate),
                    new XElement("Document", item.Document.Content),
                    new XElement("Title", item.Title)
                });
            }

            return xElement;
        }

        private XElement SerializeConnections(IEnumerable<Connection> connections)
        {
            var serializedConnections = new XElement("Connections",
                           from connection in connections
                           select new XElement("Connection",
                                      new XElement("SourceID", connection.Source.ParentDesignerItem.ID),
                                      new XElement("SinkID", connection.Sink.ParentDesignerItem.ID),
                                      new XElement("SourceConnectorName", connection.Source.Name),
                                      new XElement("SinkConnectorName", connection.Sink.Name),
                                      new XElement("SourceArrowSymbol", connection.SourceArrowSymbol),
                                      new XElement("SinkArrowSymbol", connection.SinkArrowSymbol),
                                      new XElement("zIndex", Canvas.GetZIndex(connection))
                                     )
                                  );

            return serializedConnections;
        }

        private static DesignerItem DeserializeDesignerItem(XElement itemXML, Guid id, double OffsetX, double OffsetY)
        {
            DesignerItem item = new DesignerItem(id);
            item.Width = Double.Parse(itemXML.Element("Width").Value, CultureInfo.InvariantCulture);
            item.Height = Double.Parse(itemXML.Element("Height").Value, CultureInfo.InvariantCulture);
            item.ParentID = new Guid(itemXML.Element("ParentID").Value);
            item.IsGroup = Boolean.Parse(itemXML.Element("IsGroup").Value);
            item.Tag = itemXML.Element("Tag").Value.ToString();
            Canvas.SetLeft(item, Double.Parse(itemXML.Element("Left").Value, CultureInfo.InvariantCulture) + OffsetX);
            Canvas.SetTop(item, Double.Parse(itemXML.Element("Top").Value, CultureInfo.InvariantCulture) + OffsetY);
            Canvas.SetZIndex(item, Int32.Parse(itemXML.Element("zIndex").Value));
            StringReader sr = new StringReader(itemXML.Element("Content").Value);
            XmlReader xmlr = XmlReader.Create(sr);
            Object content = XamlReader.Load(xmlr);
            item.Content = content;
            return item;
        }

        private static BaseLogic DeserializeLogicItem(XElement itemXML)
        {
            BaseLogic item = new BaseLogic();
            switch (itemXML.Element("Name").Value)
            {
                case "Information":
                    item = new InformationResource()
                    {
                        ID = new Guid(itemXML.Element("ID").Value),
                        DesignerID = new Guid(itemXML.Element("DesignerID").Value),
                        Name = itemXML.Element("Name").Value,
                        CreationDate = DateTime.Parse(itemXML.Element("CreationDate").Value),
                        Category = (Category)Enum.Parse(typeof(Category), itemXML.Element("Category").Value),
                        Document = new Document() { Content = Encoding.ASCII.GetBytes(itemXML.Element("Document").Value) },
                        Title = itemXML.Element("Title").Value
                    };
                    break;
                default:
                    item.ID = new Guid(itemXML.Element("ID").Value);
                    item.DesignerID = new Guid(itemXML.Element("DesignerID").Value);
                    item.Name = itemXML.Element("Name").Value;
                    break;
            }
            return item;
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

        private Connector GetConnector(Guid itemID, String connectorName)
        {
            DesignerItem designerItem = (from item in ResourceDesigner.Children.OfType<DesignerItem>()
                                         where item.ID == itemID
                                         select item).FirstOrDefault();

            Control connectorDecorator = designerItem.Template.FindName("PART_ConnectorDecorator", designerItem) as Control;
            connectorDecorator.ApplyTemplate();

            return connectorDecorator.Template.FindName(connectorName, connectorDecorator) as Connector;
        }
    }
}