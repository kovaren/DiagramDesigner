using DiagramDesigner.ResourcesLogic;
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

namespace DiagramDesigner
{
    /// <summary>
    /// Interaction logic for LabourWindow.xaml
    /// </summary>
    public partial class LabourWindow : Window
    {
        public LabourForce LabourForce;
        public LabourWindow(LabourForce labourForce)
        {
            InitializeComponent();
            LabourForce = labourForce;
            FillData();
        }
        
        private void okButton_Click(object sender, RoutedEventArgs e)
        {
            if (DataIsOK())
            {
                var labourForce = new LabourForce(
                    titleBox.Text,
                    double.Parse(priceBox.Text),
                    positionBox.Text
                    );
                if (!labourForce.Equals(LabourForce))
                {
                    LabourForce = labourForce;
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
            titleBox.Text = LabourForce.Title;
            priceBox.Text = LabourForce.PricePerHour.ToString();
            positionBox.Text = LabourForce.Position;
        }

        private bool DataIsOK()
        {
            return
                titleBox.Text.Any(x => Char.IsLetterOrDigit(x))
                && priceBox.Text.Any(x => Char.IsLetterOrDigit(x))
                && positionBox.Text.Any(x => Char.IsLetterOrDigit(x));
        }
    }
}
