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

namespace DiagramDesigner
{
    /// <summary>
    /// Interaction logic for ResourceWindow.xaml
    /// </summary>
    public partial class ResourceWindow : Window
    {
        public new List<BaseResource> Resources;
        ObservableCollection<BaseResource> _resources = new ObservableCollection<BaseResource>();
        public ResourceWindow(OperationRBP operation)
        {
            InitializeComponent();
            this.Title = "Resources : " + operation.Name;
            FillData(operation.Resources);
        }

        private void FillData(List<BaseResource> resourceCollection)
        {
            foreach (var resource in resourceCollection)
            {
                _resources.Add(new BaseResource()
                {
                    ID = resource.ID,
                    Title = resource.Title,
                    Name = resource.Name
                });
            }
            dataGrid.ItemsSource = _resources;
            dataGrid.RowStyle = dataGrid.Resources["DataGridRow"] as Style;
        }

        private void Row_DoubleClick(object sender, MouseButtonEventArgs e)
        {
            DataGridRow row = sender as DataGridRow;
            
            if (row.Item is InformationResource)
            {
                var item = row.Item as InformationResource;
                var informationWindow = new InformationWindow(item);
                informationWindow.ShowDialog();

                if (informationWindow.DialogResult.HasValue && informationWindow.DialogResult.Value)
                {
                    _resources.Remove(item);
                    _resources.Add(informationWindow.InformationResource);
                }
            }
        }

        private void addButton_Click(object sender, RoutedEventArgs e)
        {
            _resources.Add(new BaseResource() { ID = Guid.NewGuid(), Title = "New resource", Name = "Resource" });
        }

        private void removeButton_Click(object sender, RoutedEventArgs e)
        {
            if (dataGrid.SelectedIndex != -1)
                _resources.RemoveAt(dataGrid.SelectedIndex);
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            var menuItem = sender as MenuItem;
            if (menuItem == InformationMenuItem)
            {
                var informationWindow = new InformationWindow(new InformationResource());
                informationWindow.ShowDialog();

                if (informationWindow.DialogResult.HasValue && informationWindow.DialogResult.Value)
                {
                    _resources.Add(informationWindow.InformationResource);
                }
            }
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Resources = _resources.ToList();
            DialogResult = true;
        }
    }
}