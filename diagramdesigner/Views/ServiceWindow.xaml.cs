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
using DiagramDesigner.ResourcesLogic;

namespace DiagramDesigner
{
    /// <summary>
    /// Interaction logic for ServiceWindow.xaml
    /// </summary>
    public partial class ServiceWindow : Window
    {
        public Service Service;
        public ServiceWindow(Service service)
        {
            InitializeComponent();
            Service = service;
            FillData();
        }

        private void okButton_Click(object sender, RoutedEventArgs e)
        {
            if (DataIsOK())
            {
                var service = new Service(
                    titleBox.Text,
                    double.Parse(priceBox.Text),
                    double.Parse(durationBox.Text)
                    );
                if (!service.Equals(Service))
                {
                    Service = service;
                    DialogResult = true;
                }
                else
                    DialogResult = false;
            }
            else
                MessageBox.Show("Some fields are empty or invalid!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
        }

        private void FillData()
        {
            titleBox.Text = Service.Title;
            priceBox.Text = Service.PricePerHour.ToString();
            durationBox.Text = Service.Duration.ToString();
        }

        private bool DataIsOK()
        {
            return
                titleBox.Text.Any(x => Char.IsLetterOrDigit(x))
                && NumericDataIsOK(priceBox.Text)
                && NumericDataIsOK(durationBox.Text);
        }

        private bool NumericDataIsOK(string value)
        {
            return value.Any(x => Char.IsDigit(x))
                && value.All(x => !Char.IsLetter(x))
                && value.Where(x => Char.IsPunctuation(x)).All(x => x == ',')
                && value.Where(x => x == ',').Count() <= 1;
        }
    }
}
