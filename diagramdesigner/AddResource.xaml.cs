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

namespace DiagramDesigner
{
    /// <summary>
    /// Interaction logic for AddResource.xaml
    /// </summary>
    public partial class AddResource : Window
    {
        public List<BaseResource> resources = new List<BaseResource>();
        public AddResource()
        {
            InitializeComponent();
        }

        public void AddResources()
        {
            var selectedItems = listBox.SelectedItems;
            foreach (var item in selectedItems)
            {
                string content = ((ListBoxItem)item).Content.ToString();
                if (content == "Информационный ресурс")
                {
                    resources.Add(new InformationResource(Guid.NewGuid(), Guid.NewGuid()));
                }
                if (content == "Финансовый ресурс")
                {
                    resources.Add(new FinancialResource(Guid.NewGuid(), Guid.NewGuid()));
                }
                if (content == "Трудовой ресурс")
                {
                    resources.Add(new LaborForce(Guid.NewGuid(), Guid.NewGuid()));
                }
                if (content == "Оборудование")
                {
                    resources.Add(new Equipment(Guid.NewGuid(), Guid.NewGuid()));
                }
                if (content == "Товар")
                {
                    resources.Add(new Good(Guid.NewGuid(), Guid.NewGuid()));
                }
                if (content == "Услуга")
                {
                    resources.Add(new Service(Guid.NewGuid(), Guid.NewGuid()));
                }
            }
        }

        private void OKButton_Click(object sender, RoutedEventArgs e)
        {
            AddResources();
            DialogResult = true;
        }
    }
}
