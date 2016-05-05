using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Xml;
using System.Xml.Linq;
using Microsoft.Win32;
using DiagramDesigner.LogicRBP;
using DiagramDesigner.LogicTBP;
using System.ComponentModel;
using System.Data;
using System.Windows.Data;

namespace DiagramDesigner
{
    public partial class Window1 : Window
    {
        public static RoutedCommand GenerateTBP = new RoutedCommand();
        public static RoutedCommand GenerateALS = new RoutedCommand();
        public Window1()
        {
            InitializeComponent();
            this.Title = "Business Process Modeller";
            this.CommandBindings.Add(new CommandBinding(Window1.GenerateTBP, Generate_TBP));
        }

        private void DesignerTabs_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            if (((TabControl)sender).SelectedItem.Equals(RBPDesignerTab))
            {
                Toolbox.Content = FindResource("RBPStencils");
                contentControl.Content = FindResource("RBPToolbar");
            }
            else
            {
                Toolbox.Content = FindResource("TBPStencils");
                contentControl.Content = FindResource("TBPToolbar");
            }
        }
        
        //Generate TBP
        #region Generate TBP
        private double GlobalOffsetX;
        private void DrawConnection(Guid sourceID, Guid sinkID)
        {

            String sourceConnectorName = "Bottom";
            String sinkConnectorName = "Top";
            
            Connector sourceConnector = GetConnector(sourceID, sourceConnectorName, TBPDesigner);
            Connector sinkConnector = GetConnector(sinkID, sinkConnectorName, TBPDesigner);

            Connection connection = new Connection(sourceConnector, sinkConnector);

            TBPDesigner.Children.Add(connection);
        }
        #region Content
        private object GetDMPContent()
        {
            //temporary solution for getting content of DMP
            String xaml = "<Ellipse ToolTip='Decision Making Point' xmlns='http://schemas.microsoft.com/winfx/2006/xaml/presentation' xmlns:x='http://schemas.microsoft.com/winfx/2006/xaml' xmlns:s='clr-namespace:System;assembly=mscorlib' xmlns:dd='clr-namespace:DiagramDesigner;assembly=DiagramDesigner'><Ellipse.Style><Style TargetType='Ellipse'><Style.Resources><ResourceDictionary /></Style.Resources><Setter Property='Shape.Fill'><Setter.Value><SolidColorBrush>#FF4682B4</SolidColorBrush></Setter.Value></Setter><Setter Property='Shape.Stretch'><Setter.Value><x:Static Member='Stretch.Fill' /></Setter.Value></Setter><Setter Property='UIElement.IsHitTestVisible'><Setter.Value><s:Boolean>False</s:Boolean></Setter.Value></Setter><Setter Property='UIElement.SnapsToDevicePixels'><Setter.Value><s:Boolean>True</s:Boolean></Setter.Value></Setter><Setter Property='FrameworkElement.Tag'><Setter.Value>DMP</Setter.Value></Setter></Style></Ellipse.Style><dd:DesignerItem.DragThumbTemplate><ControlTemplate><Ellipse><Ellipse.Style><Style TargetType='Ellipse'><Style.BasedOn><Style TargetType='Ellipse'><Style.Resources><ResourceDictionary /></Style.Resources><Setter Property='Shape.Fill'><Setter.Value><SolidColorBrush>#FF4682B4</SolidColorBrush></Setter.Value></Setter><Setter Property='Shape.Stretch'><Setter.Value><x:Static Member='Stretch.Fill' /></Setter.Value></Setter><Setter Property='UIElement.IsHitTestVisible'><Setter.Value><s:Boolean>False</s:Boolean></Setter.Value></Setter><Setter Property='UIElement.SnapsToDevicePixels'><Setter.Value><s:Boolean>True</s:Boolean></Setter.Value></Setter><Setter Property='FrameworkElement.Tag'><Setter.Value>DMP</Setter.Value></Setter></Style></Style.BasedOn><Style.Resources><ResourceDictionary /></Style.Resources><Setter Property='UIElement.IsHitTestVisible'><Setter.Value><s:Boolean>True</s:Boolean></Setter.Value></Setter></Style></Ellipse.Style></Ellipse></ControlTemplate></dd:DesignerItem.DragThumbTemplate></Ellipse>";
            StringReader stringReader = new StringReader(xaml);
            XmlReader xmlReader = XmlReader.Create(stringReader);
            Object content = XamlReader.Load(xmlReader);
            return content;
        }

        private object GetOperationTBPContent()
        {
            String xaml = "<Path ToolTip='Operation' xmlns='http://schemas.microsoft.com/winfx/2006/xaml/presentation' xmlns:x='http://schemas.microsoft.com/winfx/2006/xaml' xmlns:s='clr-namespace:System;assembly=mscorlib' xmlns:dd='clr-namespace:DiagramDesigner;assembly=DiagramDesigner'><Path.Style><Style TargetType='Path'><Style.Resources><ResourceDictionary /></Style.Resources><Setter Property='Path.Data'><Setter.Value><StreamGeometry>M0,0L60,0 60,40 0,40z</StreamGeometry></Setter.Value></Setter><Setter Property='Shape.Fill'><Setter.Value><SolidColorBrush>#FFFFE4B5</SolidColorBrush></Setter.Value></Setter><Setter Property='Shape.Stretch'><Setter.Value><x:Static Member='Stretch.Fill' /></Setter.Value></Setter><Setter Property='UIElement.IsHitTestVisible'><Setter.Value><s:Boolean>False</s:Boolean></Setter.Value></Setter><Setter Property='UIElement.SnapsToDevicePixels'><Setter.Value><s:Boolean>True</s:Boolean></Setter.Value></Setter><Setter Property='FrameworkElement.Tag'><Setter.Value>OperationTBP</Setter.Value></Setter></Style></Path.Style><dd:DesignerItem.DragThumbTemplate><ControlTemplate><Path><Path.Style><Style TargetType='Path'><Style.BasedOn><Style TargetType='Path'><Style.Resources><ResourceDictionary /></Style.Resources><Setter Property='Path.Data'><Setter.Value><StreamGeometry>M0,0L60,0 60,40 0,40z</StreamGeometry></Setter.Value></Setter><Setter Property='Shape.Fill'><Setter.Value><SolidColorBrush>#FFFFE4B5</SolidColorBrush></Setter.Value></Setter><Setter Property='Shape.Stretch'><Setter.Value><x:Static Member='Stretch.Fill' /></Setter.Value></Setter><Setter Property='UIElement.IsHitTestVisible'><Setter.Value><s:Boolean>False</s:Boolean></Setter.Value></Setter><Setter Property='UIElement.SnapsToDevicePixels'><Setter.Value><s:Boolean>True</s:Boolean></Setter.Value></Setter><Setter Property='FrameworkElement.Tag'><Setter.Value>OperationTBP</Setter.Value></Setter></Style></Style.BasedOn><Style.Resources><ResourceDictionary /></Style.Resources><Setter Property='UIElement.IsHitTestVisible'><Setter.Value><s:Boolean>True</s:Boolean></Setter.Value></Setter></Style></Path.Style></Path></ControlTemplate></dd:DesignerItem.DragThumbTemplate></Path>";
            StringReader stringReader = new StringReader(xaml);
            XmlReader xmlReader = XmlReader.Create(stringReader);
            Object content = XamlReader.Load(xmlReader);
            return content;
        }

        private object GetStartTBPContent()
        {
            String xaml = "<Path ToolTip='Start' xmlns='http://schemas.microsoft.com/winfx/2006/xaml/presentation' xmlns:x='http://schemas.microsoft.com/winfx/2006/xaml' xmlns:s='clr-namespace:System;assembly=mscorlib' xmlns:dd='clr-namespace:DiagramDesigner;assembly=DiagramDesigner'><Path.Style><Style TargetType='Path'><Style.Resources><ResourceDictionary /></Style.Resources><Setter Property='Path.Data'><Setter.Value><StreamGeometry>M0,0L100,0 50,-100 0,0z</StreamGeometry></Setter.Value></Setter><Setter Property='Shape.Fill'><Setter.Value><SolidColorBrush>#FF9ACD32</SolidColorBrush></Setter.Value></Setter><Setter Property='Shape.Stretch'><Setter.Value><x:Static Member='Stretch.Fill' /></Setter.Value></Setter><Setter Property='UIElement.IsHitTestVisible'><Setter.Value><s:Boolean>False</s:Boolean></Setter.Value></Setter><Setter Property='UIElement.SnapsToDevicePixels'><Setter.Value><s:Boolean>True</s:Boolean></Setter.Value></Setter><Setter Property='FrameworkElement.Tag'><Setter.Value>StartTBP</Setter.Value></Setter></Style></Path.Style><dd:DesignerItem.DragThumbTemplate><ControlTemplate><Path><Path.Style><Style TargetType='Path'><Style.BasedOn><Style TargetType='Path'><Style.Resources><ResourceDictionary /></Style.Resources><Setter Property='Path.Data'><Setter.Value><StreamGeometry>M0,0L100,0 50,-100 0,0z</StreamGeometry></Setter.Value></Setter><Setter Property='Shape.Fill'><Setter.Value><SolidColorBrush>#FF9ACD32</SolidColorBrush></Setter.Value></Setter><Setter Property='Shape.Stretch'><Setter.Value><x:Static Member='Stretch.Fill' /></Setter.Value></Setter><Setter Property='UIElement.IsHitTestVisible'><Setter.Value><s:Boolean>False</s:Boolean></Setter.Value></Setter><Setter Property='UIElement.SnapsToDevicePixels'><Setter.Value><s:Boolean>True</s:Boolean></Setter.Value></Setter><Setter Property='FrameworkElement.Tag'><Setter.Value>StartTBP</Setter.Value></Setter></Style></Style.BasedOn><Style.Resources><ResourceDictionary /></Style.Resources><Setter Property='UIElement.IsHitTestVisible'><Setter.Value><s:Boolean>True</s:Boolean></Setter.Value></Setter></Style></Path.Style></Path></ControlTemplate></dd:DesignerItem.DragThumbTemplate></Path>";
            StringReader stringReader = new StringReader(xaml);
            XmlReader xmlReader = XmlReader.Create(stringReader);
            Object content = XamlReader.Load(xmlReader);
            return content;
        }

        private object GetEndTBPContent()
        {
            String xaml = "<Path ToolTip='End' xmlns='http://schemas.microsoft.com/winfx/2006/xaml/presentation' xmlns:x='http://schemas.microsoft.com/winfx/2006/xaml' xmlns:s='clr-namespace:System;assembly=mscorlib' xmlns:dd='clr-namespace:DiagramDesigner;assembly=DiagramDesigner'><Path.Style><Style TargetType='Path'><Style.Resources><ResourceDictionary /></Style.Resources><Setter Property='Path.Data'><Setter.Value><StreamGeometry>M0,0L100,0 50,100 0,0z</StreamGeometry></Setter.Value></Setter><Setter Property='Shape.Fill'><Setter.Value><SolidColorBrush>#FF6B8E23</SolidColorBrush></Setter.Value></Setter><Setter Property='Shape.Stretch'><Setter.Value><x:Static Member='Stretch.Fill' /></Setter.Value></Setter><Setter Property='UIElement.IsHitTestVisible'><Setter.Value><s:Boolean>False</s:Boolean></Setter.Value></Setter><Setter Property='UIElement.SnapsToDevicePixels'><Setter.Value><s:Boolean>True</s:Boolean></Setter.Value></Setter><Setter Property='FrameworkElement.Tag'><Setter.Value>EndTBP</Setter.Value></Setter></Style></Path.Style><dd:DesignerItem.DragThumbTemplate><ControlTemplate><Path><Path.Style><Style TargetType='Path'><Style.BasedOn><Style TargetType='Path'><Style.Resources><ResourceDictionary /></Style.Resources><Setter Property='Path.Data'><Setter.Value><StreamGeometry>M0,0L100,0 50,100 0,0z</StreamGeometry></Setter.Value></Setter><Setter Property='Shape.Fill'><Setter.Value><SolidColorBrush>#FF6B8E23</SolidColorBrush></Setter.Value></Setter><Setter Property='Shape.Stretch'><Setter.Value><x:Static Member='Stretch.Fill' /></Setter.Value></Setter><Setter Property='UIElement.IsHitTestVisible'><Setter.Value><s:Boolean>False</s:Boolean></Setter.Value></Setter><Setter Property='UIElement.SnapsToDevicePixels'><Setter.Value><s:Boolean>True</s:Boolean></Setter.Value></Setter><Setter Property='FrameworkElement.Tag'><Setter.Value>EndTBP</Setter.Value></Setter></Style></Style.BasedOn><Style.Resources><ResourceDictionary /></Style.Resources><Setter Property='UIElement.IsHitTestVisible'><Setter.Value><s:Boolean>True</s:Boolean></Setter.Value></Setter></Style></Path.Style></Path></ControlTemplate></dd:DesignerItem.DragThumbTemplate></Path>";
            StringReader stringReader = new StringReader(xaml);
            XmlReader xmlReader = XmlReader.Create(stringReader);
            Object content = XamlReader.Load(xmlReader);
            return content;
        }

        private object GetErrorContent()
        {
            //temporary solution for getting content of Error
            String xaml = @"<Path ToolTip='Error' xmlns='http://schemas.microsoft.com/winfx/2006/xaml/presentation' xmlns:x='http://schemas.microsoft.com/winfx/2006/xaml' xmlns:s='clr-namespace:System;assembly=mscorlib' xmlns:dd='clr-namespace:DiagramDesigner;assembly=DiagramDesigner'> 
            <Path.Style> 
                <Style x:Key='Error' TargetType='Path'> 
                    <Setter Property='Data' Value='M5.479333,0.50000942 L5.4897767,0.50025856 5.2427388,0.50650674 C2.6864701,0.63612658 0.63612691,2.6864575 0.50650671,5.242739 L0.50024948,5.4901393 0.50000104,5.4797271 C0.50000057,4.2054856 0.98624137,2.9312447 1.9587228,1.9587552 2.9307166,0.98625833 4.2049609,0.50000983 5.479333,0.50000942 z M5.4999996,0.5 L65.57,0.5 C68.330987,0.5 70.57,2.7389989 70.57,5.5000017 L70.57,65.645998 C70.57,68.408022 68.330987,70.645998 65.57,70.645998 L5.4999996,70.645998 C2.739013,70.645998 0.49999952,68.408022 0.5,65.645998 L0.5,5.5000017 0.50024948,5.4901393 0.50569877,5.7185734 C0.56268004,6.9120691 1.0470215,8.0889896 1.9587228,9.0006984 L28.435536,35.477515 1.9587228,61.954321 C0.013759797,63.899321 0.013759797,67.052309 1.9587228,68.996294 3.9027098,70.941256 7.0557023,70.941256 9.0006958,68.996294 L35.477501,42.519488 61.954308,68.996294 C63.8993,70.941256 67.051293,70.941256 68.996285,68.996294 70.941247,67.052309 70.941247,63.899321 68.996285,61.954321 L42.519471,35.477515 68.996285,9.0006984 C70.941247,7.0557197 70.941247,3.9027422 68.996285,1.9587552 67.0523,0.013761436 63.8993,0.013761436 61.954308,1.9587552 L35.477501,28.435553 9.0006958,1.9587552 C8.0889803,1.0470394 6.9118353,0.56269032 5.7182055,0.50570816 L5.4897767,0.50025856 z'/> 
                    <Setter Property='Fill' Value='#FFD85E59'/> 
                    <Setter Property='Stretch' Value='Fill'/> 
                    <Setter Property='IsHitTestVisible' Value='False'/> 
                    <Setter Property='SnapsToDevicePixels' Value='True'/> 
                    <Setter Property='Stroke' Value='#FFD85E59'/> 
                    <Setter Property='Tag' Value='ErrorTBP'/> 
                </Style> 
            </Path.Style> 
                <dd:DesignerItem.DragThumbTemplate> 
                    <ControlTemplate> 
                        <Path> 
                            <Path.Style> 
                                <Style x:Key='Error_DragThumb' TargetType='Path' BasedOn='{StaticResource Error}'> 
                                    <Setter Property='IsHitTestVisible' Value='true'/> 
                                </Style>
                            </Path.Style> 
                        </Path> 
                    </ControlTemplate> 
                </dd:DesignerItem.DragThumbTemplate> 
            </Path>";
            StringReader stringReader = new StringReader(xaml);
            XmlReader xmlReader = XmlReader.Create(stringReader);
            Object content = XamlReader.Load(xmlReader);
            return content;
        }
        #endregion
        private void Generate_TBP(object sender, ExecutedRoutedEventArgs e)
        {
            //if (((Expander)App.Current.MainWindow.FindName("RBPtools")).IsEnabled == false)
            //{
            //    MessageBox.Show("TBP generation may be executed only on RBP model.");
            //    e.Handled = true;
            //    return;
            //}
            
            IEnumerable<DesignerItem> designerItems = RBPDesigner.Children.OfType<DesignerItem>();
            IEnumerable<Connection> connections = RBPDesigner.Children.OfType<Connection>();

            //add operations to items pool
            List<DesignerItem> operationPool = designerItems.Where(p => p.BoundLogicItem.GetType() == typeof(OperationRBP)).ToList();
            if (operationPool.Count < 2)
            {
                MessageBox.Show("Not enough operations for generation.");
                e.Handled = true;
                return;
            }

            BackgroundWorker worker = new BackgroundWorker();

            //build map of operations
            this.GlobalOffsetX = 100;

            ProgressBarPopup pbp = new ProgressBarPopup();
            pbp.Owner = App.Current.MainWindow;
            pbp.Show();

            //worker.DoWork += new DoWorkEventHandler(
            //    delegate(object o, DoWorkEventArgs args)
            //    {

            //add start to canvas
            DesignerItem startTBP = this.CreateItem(0, GlobalOffsetX);

            startTBP.Content = this.GetStartTBPContent();
            startTBP.BoundLogicItem = new StartTBP(Guid.NewGuid(), startTBP.ID);
            startTBP.dispName = startTBP.BoundLogicItem.Name;
            startTBP.Tag = "StartTBP";

            TBPDesigner.Children.Add(startTBP);
            TBPDesigner.SetConnectorDecoratorTemplate(startTBP);
            DrawBranch(startTBP, operationPool);

            //    }
            //    );

            //worker.RunWorkerAsync();

            //works corectly, looks ugly
            //DrawNonconditionalConnections();
            pbp.Close();
            
            TBPDesignerTab.Visibility = System.Windows.Visibility.Visible;
            DesignerTabs.SelectedIndex = 1;
        }
        private DesignerItem AttachDMP(DesignerItem item, List<DesignerItem> operationPool)
        {

            //create dmp below stated item
            DesignerItem dmp = this.CreateDMP(Canvas.GetTop(item) + 140, Canvas.GetLeft(item));

            dmp.Content = this.GetDMPContent();
            dmp.BoundLogicItem = new DmpTBP(Guid.NewGuid(), dmp.ID, operationPool.Select(x => x.BoundLogicItem).ToList());
            dmp.dispName = dmp.BoundLogicItem.Name;

            TBPDesigner.Children.Add(dmp);

            TBPDesigner.SetConnectorDecoratorTemplate(dmp);

            this.DrawConnection(item.ID, dmp.ID);

            return dmp;
        }
        private void AttachEndTBP(DesignerItem item)
        {

            //create dmp below stated item
            DesignerItem end = this.CreateDMP(Canvas.GetTop(item) + 140, Canvas.GetLeft(item));

            end.Content = this.GetEndTBPContent();
            end.BoundLogicItem = new EndTBP(Guid.NewGuid(), end.ID);
            end.dispName = end.BoundLogicItem.Name;
            end.Tag = "EndTBP";

            TBPDesigner.Children.Add(end);

            TBPDesigner.SetConnectorDecoratorTemplate(end);

            this.DrawConnection(item.ID, end.ID);
        }
        private DesignerItem CreateItem(Double OffsetY, Double OffsetX)
        {
            DesignerItem item = new DesignerItem(Guid.NewGuid());

            //item size must be dynamically defined, this is temporary solution
            item.Width = 78;
            item.Height = 78;

            //item postion on canvas
            Canvas.SetLeft(item, OffsetX);
            Canvas.SetTop(item, OffsetY);

            return item;
        }
        private DesignerItem CreateDMP(Double OffsetY, Double OffsetX)
        {
            DesignerItem item = new DesignerItem("DMP", Guid.NewGuid());

            //item size must be dynamically defined, this is temporary solution
            item.Width = 78;
            item.Height = 78;

            //item postion on canvas
            Canvas.SetLeft(item, OffsetX);
            Canvas.SetTop(item, OffsetY);

            return item;
        }
        private void DrawBranch(DesignerItem root, List<DesignerItem> operationPool)
        {
            bool branchHasErrors = false;
            if (operationPool.Count != 0)
            {
                //add a dmp under the root
                DesignerItem dmp = this.AttachDMP(root, operationPool);

                //add the local root
                DesignerItem operation = this.CreateItem(Canvas.GetTop(dmp) + 140, this.GlobalOffsetX);

                operation.Content = this.GetOperationTBPContent();
                operation.BoundLogicItem = new OperationTBP(operationPool[0].BoundLogicItem.ID, operation.ID);

                //move parameters from RBP operation to the created TBP
                operation.BoundLogicItem.Name = operationPool[0].BoundLogicItem.Name;
                operation.dispName = operation.BoundLogicItem.Name;
                operation.Tag = "OperationTBP";

                TBPDesigner.Children.Add(operation);
                TBPDesigner.SetConnectorDecoratorTemplate(operation);
                this.DrawConnection(dmp.ID, operation.ID);

                operationPool.RemoveAt(0);
                if (operationPool.Count == 0)
                    branchHasErrors = true;
                for (int i = 0; i < operationPool.Count; i++)
                {
                    DesignerItem error = this.CreateItem(Canvas.GetTop(dmp) + 140, this.GlobalOffsetX + 140 * (i + 1));
                    error.Content = this.GetErrorContent();
                    error.BoundLogicItem = new GrossError(Guid.NewGuid(), error.ID);
                    error.Tag = "ErrorTBP";

                    TBPDesigner.Children.Add(error);
                    TBPDesigner.SetConnectorDecoratorTemplate(error);
                    this.DrawConnection(dmp.ID, error.ID);
                    AttachEndTBP(error);
                    branchHasErrors = true;
                }

                //recursive call
                this.DrawBranch(operation, operationPool);

            }
            //add end if this operation is a leaf of the tree
            if (!branchHasErrors)
                if (operationPool.Count == 0) this.AttachEndTBP(root);
            //inc offset for next branch
            this.GlobalOffsetX += 100;
            //recoursive exit
            return;
        }
        private void DrawNonconditionalConnections()
        {
            //get decision making points
            List<DesignerItem> DMPList = TBPDesigner.Children.OfType<DesignerItem>().Where(p => p.BoundLogicItem.GetType() == typeof(DmpTBP)).ToList();
            List<DesignerItem> EndList = TBPDesigner.Children.OfType<DesignerItem>().Where(p => p.BoundLogicItem.GetType() == typeof(EndTBP)).ToList();
            foreach (DesignerItem dmp in DMPList)
            {
                foreach (DesignerItem end in EndList)
                {
                    if (Canvas.GetLeft(end) == Canvas.GetLeft(dmp)) DrawConnection(dmp.ID, end.ID);
                }

            }

        }
        #endregion
        
        //Helper Methods
        #region Helper Methods
                
        private void GetConnectors(DependencyObject parent, List<Connector> connectors)
        {
            int childrenCount = VisualTreeHelper.GetChildrenCount(parent);
            for (int i = 0; i < childrenCount; i++)
            {
                DependencyObject child = VisualTreeHelper.GetChild(parent, i);
                if (child is Connector)
                {
                    connectors.Add(child as Connector);
                }
                else
                    GetConnectors(child, connectors);
            }
        }

        private Connector GetConnector(Guid itemID, String connectorName, DesignerCanvas canvas)
        {
            DesignerItem designerItem = (from item in canvas.Children.OfType<DesignerItem>()
                                         where item.ID == itemID
                                         select item).FirstOrDefault();

            Control connectorDecorator = designerItem.Template.FindName("PART_ConnectorDecorator", designerItem) as Control;
            connectorDecorator.ApplyTemplate();

            return connectorDecorator.Template.FindName(connectorName, connectorDecorator) as Connector;
        }

        #endregion
    }
}
