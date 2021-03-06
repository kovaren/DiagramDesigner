﻿using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using DiagramDesigner.Controls;
using DiagramDesigner.LogicRBP;
using System.ComponentModel;
using DiagramDesigner.LogicTBP;
using System.Collections.Generic;
using DiagramDesigner.ResourcesLogic;
using DiagramDesigner.BusinessLogic;

namespace DiagramDesigner
{
    //These attributes identify the types of the named parts that are used for templating
    [TemplatePart(Name = "PART_DragThumb", Type = typeof(DragThumb))]
    [TemplatePart(Name = "PART_ResizeDecorator", Type = typeof(Control))]
    [TemplatePart(Name = "PART_ConnectorDecorator", Type = typeof(Control))]
    [TemplatePart(Name = "PART_ContentPresenter", Type = typeof(ContentPresenter))]
    public class DesignerItem : ContentControl, ISelectable, IGroupable, INotifyPropertyChanged
    {
        #region ID
        private Guid id;
        public Guid ID
        {
            get { return id; }
        }
        #endregion

        #region Name
        private String name;
        public String dispName
        { 
            get { return name; }
            set 
            {
                name = value;
                OnPropertyChanged ("dispName");
            }
        }
        #endregion

        #region BoundLogicItem
        public BaseLogic BoundLogicItem;
        #endregion

        //for grouping
        #region ParentID
        public Guid ParentID
        {
            get { return (Guid)GetValue(ParentIDProperty); }
            set { SetValue(ParentIDProperty, value); }
        }
        public static readonly DependencyProperty ParentIDProperty = DependencyProperty.Register("ParentID", typeof(Guid), typeof(DesignerItem));
        #endregion

        #region IsGroup
        public bool IsGroup
        {
            get { return (bool)GetValue(IsGroupProperty); }
            set { SetValue(IsGroupProperty, value); }
        }
        public static readonly DependencyProperty IsGroupProperty =
            DependencyProperty.Register("IsGroup", typeof(bool), typeof(DesignerItem));
        #endregion

        #region IsSelected Property

        public bool IsSelected
        {
            get { return (bool)GetValue(IsSelectedProperty); }
            set { SetValue(IsSelectedProperty, value); }
        }
        public static readonly DependencyProperty IsSelectedProperty =
          DependencyProperty.Register("IsSelected",
                                       typeof(bool),
                                       typeof(DesignerItem),
                                       new FrameworkPropertyMetadata(false));

        #endregion

        #region DragThumbTemplate Property

        // can be used to replace the default template for the DragThumb
        public static readonly DependencyProperty DragThumbTemplateProperty =
            DependencyProperty.RegisterAttached("DragThumbTemplate", typeof(ControlTemplate), typeof(DesignerItem));

        public static ControlTemplate GetDragThumbTemplate(UIElement element)
        {
            return (ControlTemplate)element.GetValue(DragThumbTemplateProperty);
        }

        public static void SetDragThumbTemplate(UIElement element, ControlTemplate value)
        {
            element.SetValue(DragThumbTemplateProperty, value);
        }

        #endregion

        #region ConnectorDecoratorTemplate Property

        // can be used to replace the default template for the ConnectorDecorator
        public static readonly DependencyProperty ConnectorDecoratorTemplateProperty =
            DependencyProperty.RegisterAttached("ConnectorDecoratorTemplate", typeof(ControlTemplate), typeof(DesignerItem));

        public static ControlTemplate GetConnectorDecoratorTemplate(UIElement element)
        {
            return (ControlTemplate)element.GetValue(ConnectorDecoratorTemplateProperty);
        }

        public static void SetConnectorDecoratorTemplate(UIElement element, ControlTemplate value)
        {
            element.SetValue(ConnectorDecoratorTemplateProperty, value);
        }

        #endregion

        #region IsDragConnectionOver

        // while drag connection procedure is ongoing and the mouse moves over 
        // this item this value is true; if true the ConnectorDecorator is triggered
        // to be visible, see template
        public bool IsDragConnectionOver
        {
            get { return (bool)GetValue(IsDragConnectionOverProperty); }
            set { SetValue(IsDragConnectionOverProperty, value); }
        }
        public static readonly DependencyProperty IsDragConnectionOverProperty =
            DependencyProperty.Register("IsDragConnectionOver",
                                         typeof(bool),
                                         typeof(DesignerItem),
                                         new FrameworkPropertyMetadata(false));

        #endregion

        static DesignerItem()
        {
            // set the key to reference the style for this control
            FrameworkElement.DefaultStyleKeyProperty.OverrideMetadata(
                typeof(DesignerItem), new FrameworkPropertyMetadata(typeof(DesignerItem)));
        }

        public DesignerItem(Guid id, String Class, int num = 1)
        {
            this.id = id;
            this.Tag = Class;
            this.Loaded += new RoutedEventHandler(DesignerItem_Loaded);
            //this.MouseDoubleClick += DesignerItem_MouseDoubleClick;

            switch (Class)
            {
                case "Operation": this.BoundLogicItem = new Operation(this.id, "Operation" + num); break;
                case "Start": this.BoundLogicItem = new Start(this.id); break;
                case "End": this.BoundLogicItem = new End(this.id); break;
                case "DMP": this.BoundLogicItem = new DMP(this.id); break;
                case "Error": this.BoundLogicItem = new GrossError(this.id); break;

                case "Information": this.BoundLogicItem = new InformationResource(this.id); break;
                case "Finance": this.BoundLogicItem = new FinancialResource(); break;
                case "Labour": this.BoundLogicItem = new LabourForce(); break;
                case "Equipment": this.BoundLogicItem = new Equipment(); break;
                case "Product": this.BoundLogicItem = new Product(); break;
                case "Service": this.BoundLogicItem = new Service(); break;
                case "BusinessPartner": this.BoundLogicItem = new BusinessPartner(); break;
                default: return;
            }

            try
            {
                this.dispName = BoundLogicItem.Name;
            }
            catch (Exception)
            {
                throw;
            }
        }

        //void DesignerItem_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        //{
        //    if (this.Tag.ToString() == "Operation")
        //    {
        //        var operation = (OperationRBP)this.BoundLogicItem;
        //        ((DesignerCanvas)Parent).Children.
        //        var resourceWindow = new ResourceWindow(operation);
        //        resourceWindow.ShowDialog();
        //        if (resourceWindow.DialogResult.HasValue && resourceWindow.DialogResult.Value)
        //        {
        //            ((OperationRBP)this.BoundLogicItem).Resources = resourceWindow.Resources;
        //        }
        //    }
        //    if (this.Tag.ToString() == "DMP")
        //    {
        //        string s = String.Join("\n", ((DmpTBP)this.BoundLogicItem).ResourcesAvailable);
        //        MessageBox.Show(s);
        //    }
        //}

        public DesignerItem(Guid id)
        {
            this.id = id;
            this.Loaded += new RoutedEventHandler(DesignerItem_Loaded);
        }

        public DesignerItem(String tag, Guid id)
        {
            this.id = id;
            this.Tag = tag;
            this.Loaded += new RoutedEventHandler(DesignerItem_Loaded);
            //this.MouseDoubleClick += DesignerItem_MouseDoubleClick;
        }

        public DesignerItem()
            : this(Guid.NewGuid())
        {
        }


        protected override void OnPreviewMouseDown(MouseButtonEventArgs e)
        {
            base.OnPreviewMouseDown(e);
            DesignerCanvas designer = VisualTreeHelper.GetParent(this) as DesignerCanvas;
            

            // update selection
            if (designer != null)
            {
                if ((Keyboard.Modifiers & (ModifierKeys.Shift | ModifierKeys.Control)) != ModifierKeys.None)
                    if (this.IsSelected)
                    {
                        designer.SelectionService.RemoveFromSelection(this);
                    }
                    else
                    {
                        designer.SelectionService.AddToSelection(this);
                    }
                else if (!this.IsSelected)
                {
                    designer.SelectionService.SelectItem(this);
                }
                Focus();
            }



            
            e.Handled = false;
        }

        void DesignerItem_Loaded(object sender, RoutedEventArgs e)
        {
            if (base.Template != null)
            {
                ContentPresenter contentPresenter =
                    this.Template.FindName("PART_ContentPresenter", this) as ContentPresenter;
                if (contentPresenter != null)
                {
                    UIElement contentVisual = VisualTreeHelper.GetChild(contentPresenter, 0) as UIElement;
                    if (contentVisual != null)
                    {
                        DragThumb thumb = this.Template.FindName("PART_DragThumb", this) as DragThumb;
                        if (thumb != null)
                        {
                            ControlTemplate template =
                                DesignerItem.GetDragThumbTemplate(contentVisual) as ControlTemplate;
                            if (template != null)
                                thumb.Template = template;
                        }
                    }
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string dispName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;

            if (handler != null)
            {
                if (BoundLogicItem is Operation)
                {
                    handler(this, new PropertyChangedEventArgs(dispName));
                    ((Operation)this.BoundLogicItem).Name = this.dispName;
                }
                if (BoundLogicItem is BaseResource)
                {
                    handler(this, new PropertyChangedEventArgs(dispName));
                    ((BaseResource)this.BoundLogicItem).Name = this.dispName;
                }
            }
        }
    }
}