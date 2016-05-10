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
    /// Interaction logic for EquipmentWindow.xaml
    /// </summary>
    public partial class EquipmentWindow : Window
    {
        public Equipment Equipment;
        public EquipmentWindow(Equipment equipment)
        {
            InitializeComponent();
            Equipment = equipment;
            FillData();
        }
        
        private void okButton_Click(object sender, RoutedEventArgs e)
        {
            if (DataIsOK())
            {
                var equipment = new Equipment(
                    titleBox.Text,
                    double.Parse(priceBox.Text),
                    stateBox.Text
                    );
                if (!equipment.Equals(Equipment))
                {
                    Equipment = equipment;
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
            titleBox.Text = Equipment.Title;
            priceBox.Text = Equipment.PricePerHour.ToString();
            stateBox.Text = Equipment.State;
        }

        private bool DataIsOK()
        {
            return
                titleBox.Text.Any(x => Char.IsLetterOrDigit(x))
                && priceBox.Text.Any(x => Char.IsLetterOrDigit(x))
                && stateBox.Text.Any(x => Char.IsLetterOrDigit(x));
        }
    }
}
