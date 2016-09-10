using DiagramDesigner.LogicRBP;
using DiagramDesigner.LogicTBP;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace DiagramDesigner
{

    public class AttributePanel : INotifyPropertyChanged
    { 
        
        private String name;
        public String Name 
        {
            get { return name; }
            set
            {
                name = value;
                OnPropertyChanged("Name");
            }
        }
        public Guid LogicID { get; set; }
        public Guid DesignerID { get; set; }

        public AttributePanel ()
        {
            Name = "";
            LogicID = Guid.Empty;
            DesignerID = Guid.Empty;
            //attributes are not visible by default
            ((StackPanel)App.Current.MainWindow.FindName("AttributeStackPanel")).Visibility = System.Windows.Visibility.Hidden;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string name)
        {
            PropertyChangedEventHandler handler = PropertyChanged;

            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(name));

                //update name of the selected item, when name is changed in attribute panel
                Window mw = App.Current.MainWindow;
                DesignerCanvas designer = (DesignerCanvas)mw.FindName("RBPDesigner");
                DesignerItem item = ((DesignerItem)designer.SelectionService.CurrentSelection[0]);
                item.dispName = this.Name;
                ((Operation)item.BoundLogicItem).Name = this.Name;
            }
        }
    }
}
