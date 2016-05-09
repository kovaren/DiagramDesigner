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
    /// Interaction logic for FinanceWindow.xaml
    /// </summary>
    public partial class FinanceWindow : Window
    {
        public FinancialResource FinancialResource;
        public FinanceWindow(FinancialResource financialResource)
        {
            InitializeComponent();
            FinancialResource = financialResource;
            FillData();
        }

        private void okButton_Click(object sender, RoutedEventArgs e)
        {
            if (DataIsOK())
            {
                var financialResource = new FinancialResource(
                    titleBox.Text,
                    double.Parse(sumBox.Text),
                    (Currency)currencyBox.SelectedValue
                    );
                if (!financialResource.Equals(FinancialResource))
                {
                    FinancialResource = financialResource;
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
            titleBox.Text = FinancialResource.Title;
            sumBox.Text = FinancialResource.Sum.ToString();
            currencyBox.ItemsSource = Enum.GetValues(typeof(Currency)).Cast<Currency>();
            currencyBox.SelectedValue = FinancialResource.Currency;
        }

        private bool DataIsOK()
        {
            return
                titleBox.Text.Any(x => Char.IsLetterOrDigit(x))
                && sumBox.Text.Any(x => Char.IsDigit(x))
                && sumBox.Text.All(x => !Char.IsLetter(x))
                && sumBox.Text.Where(x => Char.IsPunctuation(x)).All(x => x == ',')
                && sumBox.Text.Where(x => x == ',').Count() <= 1
                && currencyBox.SelectedIndex != -1;
        }
    }
}