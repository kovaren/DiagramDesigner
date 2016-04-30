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

namespace DiagramDesigner
{
    public partial class DesignerCanvas
    {
        public static RoutedCommand Group = new RoutedCommand();
        public static RoutedCommand Ungroup = new RoutedCommand();
        public static RoutedCommand BringForward = new RoutedCommand();
        public static RoutedCommand BringToFront = new RoutedCommand();
        public static RoutedCommand SendBackward = new RoutedCommand();
        public static RoutedCommand SendToBack = new RoutedCommand();
        public static RoutedCommand AlignTop = new RoutedCommand();
        public static RoutedCommand AlignVerticalCenters = new RoutedCommand();
        public static RoutedCommand AlignBottom = new RoutedCommand();
        public static RoutedCommand AlignLeft = new RoutedCommand();
        public static RoutedCommand AlignHorizontalCenters = new RoutedCommand();
        public static RoutedCommand AlignRight = new RoutedCommand();
        public static RoutedCommand DistributeHorizontal = new RoutedCommand();
        public static RoutedCommand DistributeVertical = new RoutedCommand();
        public static RoutedCommand SelectAll = new RoutedCommand();
        public static RoutedCommand GenerateTBP = new RoutedCommand();
        public static RoutedCommand GenerateALS = new RoutedCommand();

        public DesignerCanvas()
        {
            this.CommandBindings.Add(new CommandBinding(DesignerCanvas.GenerateTBP, Generate_TBP));
            this.CommandBindings.Add(new CommandBinding(DesignerCanvas.GenerateALS, Generate_ALS));
            this.CommandBindings.Add(new CommandBinding(ApplicationCommands.New, New_Executed));
            this.CommandBindings.Add(new CommandBinding(ApplicationCommands.Open, Open_Executed));
            this.CommandBindings.Add(new CommandBinding(ApplicationCommands.Save, Save_Executed));
            this.CommandBindings.Add(new CommandBinding(ApplicationCommands.Print, Print_Executed));
            this.CommandBindings.Add(new CommandBinding(ApplicationCommands.Cut, Cut_Executed, Cut_Enabled));
            this.CommandBindings.Add(new CommandBinding(ApplicationCommands.Copy, Copy_Executed, Copy_Enabled));
            this.CommandBindings.Add(new CommandBinding(ApplicationCommands.Paste, Paste_Executed, Paste_Enabled));
            this.CommandBindings.Add(new CommandBinding(ApplicationCommands.Delete, Delete_Executed, Delete_Enabled));
            this.CommandBindings.Add(new CommandBinding(DesignerCanvas.Group, Group_Executed, Group_Enabled));
            this.CommandBindings.Add(new CommandBinding(DesignerCanvas.Ungroup, Ungroup_Executed, Ungroup_Enabled));
            this.CommandBindings.Add(new CommandBinding(DesignerCanvas.BringForward, BringForward_Executed, Order_Enabled));
            this.CommandBindings.Add(new CommandBinding(DesignerCanvas.BringToFront, BringToFront_Executed, Order_Enabled));
            this.CommandBindings.Add(new CommandBinding(DesignerCanvas.SendBackward, SendBackward_Executed, Order_Enabled));
            this.CommandBindings.Add(new CommandBinding(DesignerCanvas.SendToBack, SendToBack_Executed, Order_Enabled));
            this.CommandBindings.Add(new CommandBinding(DesignerCanvas.AlignTop, AlignTop_Executed, Align_Enabled));
            this.CommandBindings.Add(new CommandBinding(DesignerCanvas.AlignVerticalCenters, AlignVerticalCenters_Executed, Align_Enabled));
            this.CommandBindings.Add(new CommandBinding(DesignerCanvas.AlignBottom, AlignBottom_Executed, Align_Enabled));
            this.CommandBindings.Add(new CommandBinding(DesignerCanvas.AlignLeft, AlignLeft_Executed, Align_Enabled));
            this.CommandBindings.Add(new CommandBinding(DesignerCanvas.AlignHorizontalCenters, AlignHorizontalCenters_Executed, Align_Enabled));
            this.CommandBindings.Add(new CommandBinding(DesignerCanvas.AlignRight, AlignRight_Executed, Align_Enabled));
            this.CommandBindings.Add(new CommandBinding(DesignerCanvas.DistributeHorizontal, DistributeHorizontal_Executed, Distribute_Enabled));
            this.CommandBindings.Add(new CommandBinding(DesignerCanvas.DistributeVertical, DistributeVertical_Executed, Distribute_Enabled));
            this.CommandBindings.Add(new CommandBinding(DesignerCanvas.SelectAll, SelectAll_Executed));
            SelectAll.InputGestures.Add(new KeyGesture(Key.A, ModifierKeys.Control));

            this.AllowDrop = true;
            Clipboard.Clear();
        }

        //Generate TBP
        #region Generate TBP
        private double GlobalOffsetX;

        private void DrawConnection(Guid sourceID, Guid sinkID)
        {
           
            String sourceConnectorName = "Bottom";
            String sinkConnectorName = "Top";

            Connector sourceConnector = GetConnector(sourceID, sourceConnectorName);
            Connector sinkConnector = GetConnector(sinkID, sinkConnectorName);

            Connection connection = new Connection(sourceConnector, sinkConnector);
            
            this.Children.Add(connection);
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

        private DesignerItem AttachDMP(DesignerItem item, List<DesignerItem> operationPool)
        {

            //create dmp below stated item
            DesignerItem dmp = this.CreateDMP(Canvas.GetTop(item) + 140, Canvas.GetLeft(item));

            dmp.Content = this.GetDMPContent();
            dmp.BoundLogicItem = new DmpTBP(Guid.NewGuid(),dmp.ID, operationPool.Select(x => x.BoundLogicItem).ToList());
            dmp.dispName = dmp.BoundLogicItem.Name;

            this.Children.Add(dmp);

            SetConnectorDecoratorTemplate(dmp);

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

            this.Children.Add(end);

            SetConnectorDecoratorTemplate(end);

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
                //add dmp under root
                DesignerItem dmp = this.AttachDMP(root, operationPool);

                #region
                //foreach (DesignerItem item in operationPool)
                //{
                //    DesignerItem operation = this.CreateItem(Canvas.GetTop(dmp) + 140, this.GlobalOffsetX);

                //    operation.Content = this.GetOperationTBPContent();
                //    operation.BoundLogicItem = new OperationTBP(item.BoundLogicItem.ID, operation.ID);

                //    //move parameters from RBP operation to the created TBP
                //    operation.BoundLogicItem.Name = item.BoundLogicItem.Name;
                //    operation.dispName = operation.BoundLogicItem.Name;

                //    this.Children.Add(operation);
                //    SetConnectorDecoratorTemplate(operation);
                //    this.DrawConnection(dmp.ID, operation.ID);

                //    //remove operation from pool
                //    List<DesignerItem> newPool = operationPool.Where(p => p.ID != item.ID).ToList();

                //    //recursive call
                //    this.DrawBranch(operation, newPool);
                //}
                #endregion
                //var tempRoot = root;
                //if (operationPool.Count > 0)
                //    tempRoot = operationPool[0];
                operationPool.RemoveAt(0);
                    DesignerItem operation = this.CreateItem(Canvas.GetTop(dmp) + 140, this.GlobalOffsetX);

                    operation.Content = this.GetOperationTBPContent();
                    operation.BoundLogicItem = new OperationTBP(root.BoundLogicItem.ID, operation.ID);

                    //move parameters from RBP operation to the created TBP
                    operation.BoundLogicItem.Name = root.BoundLogicItem.Name;
                    operation.dispName = operation.BoundLogicItem.Name;

                    this.Children.Add(operation);
                    SetConnectorDecoratorTemplate(operation);
                    this.DrawConnection(dmp.ID, operation.ID);

                    if (operationPool.Count == 0)
                        branchHasErrors = true;
                    for (int i = 0; i < operationPool.Count; i++)
                    {
                        DesignerItem error = this.CreateItem(Canvas.GetTop(dmp) + 140, this.GlobalOffsetX + 140 * (i + 1));
                        error.Content = this.GetErrorContent();
                        error.BoundLogicItem = new Error(root.BoundLogicItem.ID, error.ID);

                        this.Children.Add(error);
                        SetConnectorDecoratorTemplate(error);
                        this.DrawConnection(dmp.ID, error.ID);
                        AttachEndTBP(error);
                        branchHasErrors = true;
                    }

                    //remove operation from pool
                    //List<DesignerItem> newPool = operationPool.Where(p => p.ID != root.ID).ToList();
                    //operationPool.RemoveAt(0);
                    var newPool = operationPool;
                    //recursive call
                    //operation = tempRoot;
                    this.DrawBranch(operation, newPool);
                
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
            List<DesignerItem> DMPList = this.Children.OfType<DesignerItem>().Where(p => p.BoundLogicItem.GetType() == typeof(DmpTBP)).ToList();
            List<DesignerItem> EndList = this.Children.OfType<DesignerItem>().Where(p => p.BoundLogicItem.GetType() == typeof(EndTBP)).ToList();
            foreach (DesignerItem dmp in DMPList)
            {
                foreach (DesignerItem end in EndList)
                {
                    if (Canvas.GetLeft(end) == Canvas.GetLeft(dmp)) DrawConnection(dmp.ID, end.ID);
                }
                    
            }

        }

        private void Generate_TBP(object sender, ExecutedRoutedEventArgs e)
        {
            if (((Expander)App.Current.MainWindow.FindName("RBPtools")).IsEnabled == false)
            {
                MessageBox.Show("TBP generation may be executed only on RBP model.");
                e.Handled = true;
                return;
            }

            // Creating new tab
            // TabItem newtab = new TabItem();
            // newtab.Header = "NewModel";
            // ((TabControl)App.Current.MainWindow.FindName("DesignerTabs")).Items.Add(newtab);

            IEnumerable<DesignerItem> designerItems = this.Children.OfType<DesignerItem>();
            IEnumerable<Connection> connections = this.Children.OfType<Connection>();

            //add operations to items pool
            List<DesignerItem> operationPool = designerItems.Where(p => p.BoundLogicItem.GetType() == typeof(OperationRBP)).ToList();
            if (operationPool.Count < 2)
            {
                MessageBox.Show("Not enough operations for generation.");
                e.Handled = true;
                return;
            }

            this.New_Executed(this, e);
            BackgroundWorker worker = new BackgroundWorker();

            //build map of operations
            this.GlobalOffsetX = 0;

            ProgressBarPopup pbp = new ProgressBarPopup();
            pbp.Owner = App.Current.MainWindow;
            pbp.Show();

            //worker.DoWork += new DoWorkEventHandler(
            //    delegate(object o, DoWorkEventArgs args)
            //    {

            //add start to canvas
            DesignerItem startTBP = this.CreateItem(0, 0);

            startTBP.Content = this.GetStartTBPContent();
            startTBP.BoundLogicItem = new StartTBP(Guid.NewGuid(), startTBP.ID);
            startTBP.dispName = startTBP.BoundLogicItem.Name;

            this.Children.Add(startTBP);
            SetConnectorDecoratorTemplate(startTBP);
            DrawBranch(startTBP, operationPool);

            //    }
            //    );

            //worker.RunWorkerAsync();

            //works corectly, looks ugly
            //DrawNonconditionalConnections();
            pbp.Close();


            ((Expander)App.Current.MainWindow.FindName("RBPtools")).IsExpanded = false;
            ((Expander)App.Current.MainWindow.FindName("RBPtools")).IsEnabled = false;

            ((Expander)App.Current.MainWindow.FindName("TBPtools")).IsExpanded = true;
            ((Expander)App.Current.MainWindow.FindName("TBPtools")).IsEnabled = true;
        }
        #endregion

        //Generate ALS
        #region Generate ALS
        private Boolean CanvasIsTBP()
        {
            if (((Expander)App.Current.MainWindow.FindName("TBPtools")).IsExpanded == true)
            {
                return true;
            }
            else return false;
        }
        private DesignerItem SkipOperation (DesignerItem operation,IEnumerable<Connection> connections)
        {
            return connections.First(p => p.Source.ParentDesignerItem.ID == operation.ID).Sink.ParentDesignerItem;
        }

        
        private DataTable FormALSMatrix(IEnumerable<DesignerItem> designerItems, IEnumerable<Connection> connections)
        {
            int DMPcount = designerItems.Count(p => p.BoundLogicItem.GetType() == typeof(DmpTBP));
            string[,] matrix = new string[DMPcount, DMPcount+1];


            Connection startconn = connections.First(p => p.Source.ParentDesignerItem.BoundLogicItem.GetType() == typeof(StartTBP));
            DesignerItem startDMP = startconn.Sink.ParentDesignerItem;
            int counter = 0;
            ((DmpTBP)startDMP.BoundLogicItem).AlsID = counter;

            List<DesignerItem> currentlevel = new List<DesignerItem>();
            List<DesignerItem> nextlevel = new List<DesignerItem>();
            currentlevel.Add(startDMP);

            while (true)
            {
                foreach (DesignerItem di in currentlevel)
                {
                    foreach (Connection c in connections.Where(p => p.Source.ParentDesignerItem.ID == di.ID))
                    {
                        counter++;
                        DesignerItem nextItem = SkipOperation(c.Sink.ParentDesignerItem, connections);
                        
                        if (nextItem.BoundLogicItem.GetType() == typeof(DmpTBP))
                        {
                            ((DmpTBP)nextItem.BoundLogicItem).AlsID = counter;
                            nextlevel.Add(nextItem);
                            matrix[((DmpTBP)di.BoundLogicItem).AlsID, ((DmpTBP)nextItem.BoundLogicItem).AlsID] = counter.ToString();
                        }
                        else
                        {
                            matrix[((DmpTBP)di.BoundLogicItem).AlsID, DMPcount] = "K";
                        }
                    }
                }
                currentlevel.Clear();
                foreach (DesignerItem d_nl in nextlevel)
                {
                    currentlevel.Add(d_nl);
                }
                nextlevel.Clear();
                if (currentlevel.Count == 0) break;
            }

            DataTable dt = new DataTable();
            dt.Columns.Add("DMP");
            for(int i = 0; i<DMPcount; i++)
            {
                dt.Columns.Add("S" + i);
            }
            dt.Columns.Add("K");

            for(int i = 0; i<DMPcount; i++)
            {
                DataRow dr = dt.NewRow();                
                for(int j=0; j<DMPcount+1; j++)
                {
                    dr[0] = "S" + i;
                    dr[j+1] = matrix[i, j];
                }
                dt.Rows.Add(dr);
            }
            return dt;
            
        }

        private string FormALSExpression(DataTable ALSMatrix)
        {
            string expr = "";
            for (int j = 0; j<ALSMatrix.Rows.Count;j++)
            {
                expr += "↓"+j+"A" + j;
                for (int i=1; i<ALSMatrix.Columns.Count;i++)
                {
                    if (ALSMatrix.Rows[j][i].ToString() != "")
	                {
                        expr += "↑" + ALSMatrix.Rows[j][i].ToString();
	                }
                   
                }
            }
            return expr;
        }

        private void Generate_ALS(object sender, ExecutedRoutedEventArgs e)
        {
            String ALS = "H";
            if(!CanvasIsTBP())
            {
                MessageBox.Show("ALS expression can only be generated from TBP model.","Error!",MessageBoxButton.OK,MessageBoxImage.Error);
                e.Handled = true;
                return;
            }

            
            int counter = 1;
            IEnumerable<DesignerItem> designerItems = this.Children.OfType<DesignerItem>();
            IEnumerable<Connection> connections = this.Children.OfType<Connection>();

            DataTable dt = new DataTable();
            dt = FormALSMatrix(designerItems, connections);
            string expr = FormALSExpression(dt);

            //foreach (DesignerItem dmp in designerItems.Where(p => p.BoundLogicItem.GetType() == typeof(DmpTBP)))
            //{
            //    if (counter !=1) ALS += "↓";
            //    ALS += counter + "{" + dmp.BoundLogicItem.ID.ToString() + "}";
            //    int internalCounter = 1;
            //    foreach (Connection conn in connection.Where(p => p.Source.ParentDesignerItem.BoundLogicItem.ID == dmp.BoundLogicItem.ID))
            //    {
            //        ALS+= "P"+internalCounter+" "+"↑";
            //    }
            //    counter++;
            //}
            //MessageBox.Show(ALS);

            WindowALS als = new WindowALS(dt, expr);
            als.Owner = App.Current.MainWindow;
            als.Show();

          
        }
        #endregion

        //New Command
        #region New Command

        private void New_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            this.Children.Clear();
            this.SelectionService.ClearSelection();
            ((Expander)App.Current.MainWindow.FindName("RBPtools")).IsExpanded = true;
            ((Expander)App.Current.MainWindow.FindName("RBPtools")).IsEnabled = true;

            ((Expander)App.Current.MainWindow.FindName("TBPtools")).IsExpanded = false;
            ((Expander)App.Current.MainWindow.FindName("TBPtools")).IsEnabled = false;
        }

        #endregion

        //Open Command
        #region Open Command

        private void Open_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            XElement root = LoadSerializedDataFromFile();

            if (root == null)
                return;

            this.Children.Clear();
            this.SelectionService.ClearSelection();

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
                this.Children.Add(item);
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
                this.Children.Add(connection);
            }
        }

        #endregion

        //Save Command
        #region Save Command

        private void Save_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            IEnumerable<DesignerItem> designerItems = this.Children.OfType<DesignerItem>();
            IEnumerable<Connection> connections = this.Children.OfType<Connection>();
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

        #endregion

        //Print Command
        #region Print Command

        private void Print_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            SelectionService.ClearSelection();

            PrintDialog printDialog = new PrintDialog();

            if (true == printDialog.ShowDialog())
            {
                printDialog.PrintVisual(this, "WPF Diagram");
            }
        }

        #endregion

        //Copy Command
        #region Copy Command

        private void Copy_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            CopyCurrentSelection();
        }

        private void Copy_Enabled(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = SelectionService.CurrentSelection.Count() > 0;
        }

        #endregion

        //Paste Command
        #region Paste Command

        private void Paste_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            XElement root = LoadSerializedDataFromClipBoard();

            if (root == null)
                return;

            // create DesignerItems
            Dictionary<Guid, Guid> mappingOldToNewIDs = new Dictionary<Guid, Guid>();
            List<ISelectable> newItems = new List<ISelectable>();
            IEnumerable<XElement> itemsXML = root.Elements("DesignerItems").Elements("DesignerItem");

            double offsetX = Double.Parse(root.Attribute("OffsetX").Value, CultureInfo.InvariantCulture);
            double offsetY = Double.Parse(root.Attribute("OffsetY").Value, CultureInfo.InvariantCulture);

            foreach (XElement itemXML in itemsXML)
            {
                Guid oldID = new Guid(itemXML.Element("ID").Value);
                Guid newID = Guid.NewGuid();
                mappingOldToNewIDs.Add(oldID, newID);
                DesignerItem item = DeserializeDesignerItem(itemXML, newID, offsetX, offsetY);
                this.Children.Add(item);
                SetConnectorDecoratorTemplate(item);
                newItems.Add(item);
            }

            // update group hierarchy
            SelectionService.ClearSelection();
            foreach (DesignerItem el in newItems)
            {
                if (el.ParentID != Guid.Empty)
                    el.ParentID = mappingOldToNewIDs[el.ParentID];
            }


            foreach (DesignerItem item in newItems)
            {
                if (item.ParentID == Guid.Empty)
                {
                    SelectionService.AddToSelection(item);
                }
            }

            // create Connections
            IEnumerable<XElement> connectionsXML = root.Elements("Connections").Elements("Connection");
            foreach (XElement connectionXML in connectionsXML)
            {
                Guid oldSourceID = new Guid(connectionXML.Element("SourceID").Value);
                Guid oldSinkID = new Guid(connectionXML.Element("SinkID").Value);

                if (mappingOldToNewIDs.ContainsKey(oldSourceID) && mappingOldToNewIDs.ContainsKey(oldSinkID))
                {
                    Guid newSourceID = mappingOldToNewIDs[oldSourceID];
                    Guid newSinkID = mappingOldToNewIDs[oldSinkID];

                    String sourceConnectorName = connectionXML.Element("SourceConnectorName").Value;
                    String sinkConnectorName = connectionXML.Element("SinkConnectorName").Value;

                    Connector sourceConnector = GetConnector(newSourceID, sourceConnectorName);
                    Connector sinkConnector = GetConnector(newSinkID, sinkConnectorName);

                    Connection connection = new Connection(sourceConnector, sinkConnector);
                    Canvas.SetZIndex(connection, Int32.Parse(connectionXML.Element("zIndex").Value));
                    this.Children.Add(connection);

                    SelectionService.AddToSelection(connection);
                }
            }

            DesignerCanvas.BringToFront.Execute(null, this);

            // update paste offset
            root.Attribute("OffsetX").Value = (offsetX + 10).ToString();
            root.Attribute("OffsetY").Value = (offsetY + 10).ToString();
            Clipboard.Clear();
            Clipboard.SetData(DataFormats.Xaml, root);
        }

        private void Paste_Enabled(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = Clipboard.ContainsData(DataFormats.Xaml);
        }

        #endregion

        //Delete Command
        #region Delete Command

        private void Delete_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            DeleteCurrentSelection();
        }

        private void Delete_Enabled(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = this.SelectionService.CurrentSelection.Count() > 0;
        }

        #endregion

        //Cut Command
        #region Cut Command

        private void Cut_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            CopyCurrentSelection();
            DeleteCurrentSelection();
        }

        private void Cut_Enabled(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = this.SelectionService.CurrentSelection.Count() > 0;
        }

        #endregion

        //Group Command
        #region Group Command

        private void Group_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            var items = from item in this.SelectionService.CurrentSelection.OfType<DesignerItem>()
                        where item.ParentID == Guid.Empty
                        select item;

            Rect rect = GetBoundingRectangle(items);

            DesignerItem groupItem = new DesignerItem();
            groupItem.IsGroup = true;
            groupItem.Width = rect.Width;
            groupItem.Height = rect.Height;
            Canvas.SetLeft(groupItem, rect.Left);
            Canvas.SetTop(groupItem, rect.Top);
            Canvas groupCanvas = new Canvas();
            groupItem.Content = groupCanvas;
            Canvas.SetZIndex(groupItem, this.Children.Count);
            this.Children.Add(groupItem);

            foreach (DesignerItem item in items)
                item.ParentID = groupItem.ID;

            this.SelectionService.SelectItem(groupItem);
        }

        private void Group_Enabled(object sender, CanExecuteRoutedEventArgs e)
        {
            int count = (from item in SelectionService.CurrentSelection.OfType<DesignerItem>()
                         where item.ParentID == Guid.Empty
                         select item).Count();

            e.CanExecute = count > 1;
        }

        #endregion

        //Ungroup Command
        #region Ungroup Command

        private void Ungroup_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            var groups = (from item in SelectionService.CurrentSelection.OfType<DesignerItem>()
                          where item.IsGroup && item.ParentID == Guid.Empty
                          select item).ToArray();

            foreach (DesignerItem groupRoot in groups)
            {
                var children = from child in SelectionService.CurrentSelection.OfType<DesignerItem>()
                               where child.ParentID == groupRoot.ID
                               select child;

                foreach (DesignerItem child in children)
                    child.ParentID = Guid.Empty;

                this.SelectionService.RemoveFromSelection(groupRoot);
                this.Children.Remove(groupRoot);
                UpdateZIndex();
            }
        }

        private void Ungroup_Enabled(object sender, CanExecuteRoutedEventArgs e)
        {
            var groupedItem = from item in SelectionService.CurrentSelection.OfType<DesignerItem>()
                              where item.ParentID != Guid.Empty
                              select item;


            e.CanExecute = groupedItem.Count() > 0;
        }

        #endregion

        //BringForward Command
        #region BringForward Command

        private void BringForward_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            List<UIElement> ordered = (from item in SelectionService.CurrentSelection
                                       orderby Canvas.GetZIndex(item as UIElement) descending
                                       select item as UIElement).ToList();

            int count = this.Children.Count;

            for (int i = 0; i < ordered.Count; i++)
            {
                int currentIndex = Canvas.GetZIndex(ordered[i]);
                int newIndex = Math.Min(count - 1 - i, currentIndex + 1);
                if (currentIndex != newIndex)
                {
                    Canvas.SetZIndex(ordered[i], newIndex);
                    IEnumerable<UIElement> it = this.Children.OfType<UIElement>().Where(item => Canvas.GetZIndex(item) == newIndex);

                    foreach (UIElement elm in it)
                    {
                        if (elm != ordered[i])
                        {
                            Canvas.SetZIndex(elm, currentIndex);
                            break;
                        }
                    }
                }
            }
        }

        private void Order_Enabled(object sender, CanExecuteRoutedEventArgs e)
        {
            //e.CanExecute = SelectionService.CurrentSelection.Count() > 0;
            e.CanExecute = true;
        }

        #endregion

        //BringToFront Command
        #region BringToFront Command

        private void BringToFront_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            List<UIElement> selectionSorted = (from item in SelectionService.CurrentSelection
                                               orderby Canvas.GetZIndex(item as UIElement) ascending
                                               select item as UIElement).ToList();

            List<UIElement> childrenSorted = (from UIElement item in this.Children
                                              orderby Canvas.GetZIndex(item as UIElement) ascending
                                              select item as UIElement).ToList();

            int i = 0;
            int j = 0;
            foreach (UIElement item in childrenSorted)
            {
                if (selectionSorted.Contains(item))
                {
                    int idx = Canvas.GetZIndex(item);
                    Canvas.SetZIndex(item, childrenSorted.Count - selectionSorted.Count + j++);
                }
                else
                {
                    Canvas.SetZIndex(item, i++);
                }
            }
        }

        #endregion

        //SendBackward Command
        #region SendBackward Command

        private void SendBackward_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            List<UIElement> ordered = (from item in SelectionService.CurrentSelection
                                       orderby Canvas.GetZIndex(item as UIElement) ascending
                                       select item as UIElement).ToList();

            int count = this.Children.Count;

            for (int i = 0; i < ordered.Count; i++)
            {
                int currentIndex = Canvas.GetZIndex(ordered[i]);
                int newIndex = Math.Max(i, currentIndex - 1);
                if (currentIndex != newIndex)
                {
                    Canvas.SetZIndex(ordered[i], newIndex);
                    IEnumerable<UIElement> it = this.Children.OfType<UIElement>().Where(item => Canvas.GetZIndex(item) == newIndex);

                    foreach (UIElement elm in it)
                    {
                        if (elm != ordered[i])
                        {
                            Canvas.SetZIndex(elm, currentIndex);
                            break;
                        }
                    }
                }
            }
        }

        #endregion

        //SendToBack Command
        #region SendToBack Command

        private void SendToBack_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            List<UIElement> selectionSorted = (from item in SelectionService.CurrentSelection
                                               orderby Canvas.GetZIndex(item as UIElement) ascending
                                               select item as UIElement).ToList();

            List<UIElement> childrenSorted = (from UIElement item in this.Children
                                              orderby Canvas.GetZIndex(item as UIElement) ascending
                                              select item as UIElement).ToList();
            int i = 0;
            int j = 0;
            foreach (UIElement item in childrenSorted)
            {
                if (selectionSorted.Contains(item))
                {
                    int idx = Canvas.GetZIndex(item);
                    Canvas.SetZIndex(item, j++);

                }
                else
                {
                    Canvas.SetZIndex(item, selectionSorted.Count + i++);
                }
            }
        }        

        #endregion

        //AlignTop Command
        #region AlignTop Command

        private void AlignTop_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            var selectedItems = from item in SelectionService.CurrentSelection.OfType<DesignerItem>()
                                where item.ParentID == Guid.Empty
                                select item;

            if (selectedItems.Count() > 1)
            {
                double top = Canvas.GetTop(selectedItems.First());

                foreach (DesignerItem item in selectedItems)
                {
                    double delta = top - Canvas.GetTop(item);
                    foreach (DesignerItem di in SelectionService.GetGroupMembers(item))
                    {
                        Canvas.SetTop(di, Canvas.GetTop(di) + delta);
                    }
                }
            }
        }

        private void Align_Enabled(object sender, CanExecuteRoutedEventArgs e)
        {
            //var groupedItem = from item in SelectionService.CurrentSelection.OfType<DesignerItem>()
            //                  where item.ParentID == Guid.Empty
            //                  select item;


            //e.CanExecute = groupedItem.Count() > 1;
            e.CanExecute = true;
        }

        #endregion

        //AlignVerticalCenters Command
        #region AlignVerticalCenters Command

        private void AlignVerticalCenters_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            var selectedItems = from item in SelectionService.CurrentSelection.OfType<DesignerItem>()
                                where item.ParentID == Guid.Empty
                                select item;

            if (selectedItems.Count() > 1)
            {
                double bottom = Canvas.GetTop(selectedItems.First()) + selectedItems.First().Height / 2;

                foreach (DesignerItem item in selectedItems)
                {
                    double delta = bottom - (Canvas.GetTop(item) + item.Height / 2);
                    foreach (DesignerItem di in SelectionService.GetGroupMembers(item))
                    {
                        Canvas.SetTop(di, Canvas.GetTop(di) + delta);
                    }
                }
            }
        }

        #endregion

        //AlignBottom Command
        #region AlignBottom Command

        private void AlignBottom_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            var selectedItems = from item in SelectionService.CurrentSelection.OfType<DesignerItem>()
                                where item.ParentID == Guid.Empty
                                select item;

            if (selectedItems.Count() > 1)
            {
                double bottom = Canvas.GetTop(selectedItems.First()) + selectedItems.First().Height;

                foreach (DesignerItem item in selectedItems)
                {
                    double delta = bottom - (Canvas.GetTop(item) + item.Height);
                    foreach (DesignerItem di in SelectionService.GetGroupMembers(item))
                    {
                        Canvas.SetTop(di, Canvas.GetTop(di) + delta);
                    }
                }
            }
        }

        #endregion

        //AlignLeft Command
        #region AlignLeft Command

        private void AlignLeft_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            var selectedItems = from item in SelectionService.CurrentSelection.OfType<DesignerItem>()
                                where item.ParentID == Guid.Empty
                                select item;

            if (selectedItems.Count() > 1)
            {
                double left = Canvas.GetLeft(selectedItems.First());

                foreach (DesignerItem item in selectedItems)
                {
                    double delta = left - Canvas.GetLeft(item);
                    foreach (DesignerItem di in SelectionService.GetGroupMembers(item))
                    {
                        Canvas.SetLeft(di, Canvas.GetLeft(di) + delta);
                    }
                }
            }
        }

        #endregion

        //AlignHorizontalCenters Command
        #region AlignHorizontalCenters Command

        private void AlignHorizontalCenters_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            var selectedItems = from item in SelectionService.CurrentSelection.OfType<DesignerItem>()
                                where item.ParentID == Guid.Empty
                                select item;

            if (selectedItems.Count() > 1)
            {
                double center = Canvas.GetLeft(selectedItems.First()) + selectedItems.First().Width / 2;

                foreach (DesignerItem item in selectedItems)
                {
                    double delta = center - (Canvas.GetLeft(item) + item.Width / 2);
                    foreach (DesignerItem di in SelectionService.GetGroupMembers(item))
                    {
                        Canvas.SetLeft(di, Canvas.GetLeft(di) + delta);
                    }
                }
            }
        }

        #endregion

        //AlignRight Command
        #region AlignRight Command

        private void AlignRight_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            var selectedItems = from item in SelectionService.CurrentSelection.OfType<DesignerItem>()
                                where item.ParentID == Guid.Empty
                                select item;

            if (selectedItems.Count() > 1)
            {
                double right = Canvas.GetLeft(selectedItems.First()) + selectedItems.First().Width;

                foreach (DesignerItem item in selectedItems)
                {
                    double delta = right - (Canvas.GetLeft(item) + item.Width);
                    foreach (DesignerItem di in SelectionService.GetGroupMembers(item))
                    {
                        Canvas.SetLeft(di, Canvas.GetLeft(di) + delta);
                    }
                }
            }
        }

        #endregion

        //DistributeHorizontal Command
        #region DistributeHorizontal Command

        private void DistributeHorizontal_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            var selectedItems = from item in SelectionService.CurrentSelection.OfType<DesignerItem>()
                                where item.ParentID == Guid.Empty
                                let itemLeft = Canvas.GetLeft(item)
                                orderby itemLeft
                                select item;

            if (selectedItems.Count() > 1)
            {
                double left = Double.MaxValue;
                double right = Double.MinValue;
                double sumWidth = 0;
                foreach (DesignerItem item in selectedItems)
                {
                    left = Math.Min(left, Canvas.GetLeft(item));
                    right = Math.Max(right, Canvas.GetLeft(item) + item.Width);
                    sumWidth += item.Width;
                }

                double distance = Math.Max(0, (right - left - sumWidth) / (selectedItems.Count() - 1));
                double offset = Canvas.GetLeft(selectedItems.First());

                foreach (DesignerItem item in selectedItems)
                {
                    double delta = offset - Canvas.GetLeft(item);
                    foreach (DesignerItem di in SelectionService.GetGroupMembers(item))
                    {
                        Canvas.SetLeft(di, Canvas.GetLeft(di) + delta);
                    }
                    offset = offset + item.Width + distance;
                }
            }
        }

        private void Distribute_Enabled(object sender, CanExecuteRoutedEventArgs e)
        {
            //var groupedItem = from item in SelectionService.CurrentSelection.OfType<DesignerItem>()
            //                  where item.ParentID == Guid.Empty
            //                  select item;


            //e.CanExecute = groupedItem.Count() > 1;
            e.CanExecute = true;
        }

        #endregion

        //DistributeVertical Command
        #region DistributeVertical Command

        private void DistributeVertical_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            var selectedItems = from item in SelectionService.CurrentSelection.OfType<DesignerItem>()
                                where item.ParentID == Guid.Empty
                                let itemTop = Canvas.GetTop(item)
                                orderby itemTop
                                select item;

            if (selectedItems.Count() > 1)
            {
                double top = Double.MaxValue;
                double bottom = Double.MinValue;
                double sumHeight = 0;
                foreach (DesignerItem item in selectedItems)
                {
                    top = Math.Min(top, Canvas.GetTop(item));
                    bottom = Math.Max(bottom, Canvas.GetTop(item) + item.Height);
                    sumHeight += item.Height;
                }

                double distance = Math.Max(0, (bottom - top - sumHeight) / (selectedItems.Count() - 1));
                double offset = Canvas.GetTop(selectedItems.First());

                foreach (DesignerItem item in selectedItems)
                {
                    double delta = offset - Canvas.GetTop(item);
                    foreach (DesignerItem di in SelectionService.GetGroupMembers(item))
                    {
                        Canvas.SetTop(di, Canvas.GetTop(di) + delta);
                    }
                    offset = offset + item.Height + distance;
                }
            }
        }

        #endregion

        //SelectAll Command
        #region SelectAll Command

        private void SelectAll_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            SelectionService.SelectAll();
        }

        #endregion

        //Helper Methods
        #region Helper Methods

        private XElement LoadSerializedDataFromFile()
        {
            OpenFileDialog openFile = new OpenFileDialog();
            openFile.Filter = "Designer Files (*.xml)|*.xml|All Files (*.*)|*.*";

            if (openFile.ShowDialog() == true)
            {
                try
                {
                    return XElement.Load(openFile.FileName);
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.StackTrace, e.Message, MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }

            return null;
        }

        void SaveFile(XElement xElement)
        {
            SaveFileDialog saveFile = new SaveFileDialog();
            saveFile.Filter = "Files (*.xml)|*.xml|All Files (*.*)|*.*";
            if (saveFile.ShowDialog() == true)
            {
                try
                {
                    xElement.Save(saveFile.FileName);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.StackTrace, ex.Message, MessageBoxButton.OK, MessageBoxImage.Error);
                }
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
                                                  new XElement("Content", contentXaml)
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
            
          

            return serializedItems;
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
            item.ID = new Guid(itemXML.Element("ID").Value);
            item.DesignerID = new Guid(itemXML.Element("DesignerID").Value);
            item.Name = itemXML.Element("Name").Value;
          
            return item;
        }

        private void CopyCurrentSelection()
        {
            IEnumerable<DesignerItem> selectedDesignerItems =
                this.SelectionService.CurrentSelection.OfType<DesignerItem>();

            List<Connection> selectedConnections =
                this.SelectionService.CurrentSelection.OfType<Connection>().ToList();

            foreach (Connection connection in this.Children.OfType<Connection>())
            {
                if (!selectedConnections.Contains(connection))
                {
                    DesignerItem sourceItem = (from item in selectedDesignerItems
                                               where item.ID == connection.Source.ParentDesignerItem.ID
                                               select item).FirstOrDefault();

                    DesignerItem sinkItem = (from item in selectedDesignerItems
                                             where item.ID == connection.Sink.ParentDesignerItem.ID
                                             select item).FirstOrDefault();

                    if (sourceItem != null &&
                        sinkItem != null &&
                        BelongToSameGroup(sourceItem, sinkItem))
                    {
                        selectedConnections.Add(connection);
                    }
                }
            }

            XElement designerItemsXML = SerializeDesignerItems(selectedDesignerItems);
            XElement connectionsXML = SerializeConnections(selectedConnections);

            XElement root = new XElement("Root");
            root.Add(designerItemsXML);
            root.Add(connectionsXML);

            root.Add(new XAttribute("OffsetX", 10));
            root.Add(new XAttribute("OffsetY", 10));

            Clipboard.Clear();
            Clipboard.SetData(DataFormats.Xaml, root);
        }

        private void DeleteCurrentSelection()
        {
            foreach (Connection connection in SelectionService.CurrentSelection.OfType<Connection>())
            {
                this.Children.Remove(connection);
            }

            foreach (DesignerItem item in SelectionService.CurrentSelection.OfType<DesignerItem>())
            {
                Control cd = item.Template.FindName("PART_ConnectorDecorator", item) as Control;

                List<Connector> connectors = new List<Connector>();
                GetConnectors(cd, connectors);

                foreach (Connector connector in connectors)
                {
                    foreach (Connection con in connector.Connections)
                    {
                        this.Children.Remove(con);
                    }
                }
                this.Children.Remove(item);
            }

            SelectionService.ClearSelection();
            UpdateZIndex();
        }

        private void UpdateZIndex()
        {
            List<UIElement> ordered = (from UIElement item in this.Children
                                       orderby Canvas.GetZIndex(item as UIElement)
                                       select item as UIElement).ToList();

            for (int i = 0; i < ordered.Count; i++)
            {
                Canvas.SetZIndex(ordered[i], i);
            }
        }

        private static Rect GetBoundingRectangle(IEnumerable<DesignerItem> items)
        {
            double x1 = Double.MaxValue;
            double y1 = Double.MaxValue;
            double x2 = Double.MinValue;
            double y2 = Double.MinValue;

            foreach (DesignerItem item in items)
            {
                x1 = Math.Min(Canvas.GetLeft(item), x1);
                y1 = Math.Min(Canvas.GetTop(item), y1);

                x2 = Math.Max(Canvas.GetLeft(item) + item.Width, x2);
                y2 = Math.Max(Canvas.GetTop(item) + item.Height, y2);
            }

            return new Rect(new Point(x1, y1), new Point(x2, y2));
        }

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

        private Connector GetConnector(Guid itemID, String connectorName)
        {
            DesignerItem designerItem = (from item in this.Children.OfType<DesignerItem>()
                                         where item.ID == itemID
                                         select item).FirstOrDefault();

            Control connectorDecorator = designerItem.Template.FindName("PART_ConnectorDecorator", designerItem) as Control;
            connectorDecorator.ApplyTemplate();

            return connectorDecorator.Template.FindName(connectorName, connectorDecorator) as Connector;
        }

        private bool BelongToSameGroup(IGroupable item1, IGroupable item2)
        {
            IGroupable root1 = SelectionService.GetGroupRoot(item1);
            IGroupable root2 = SelectionService.GetGroupRoot(item2);

            return (root1.ID == root2.ID);
        }

        #endregion
    }
}
