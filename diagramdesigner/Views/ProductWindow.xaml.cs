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
    /// Interaction logic for ProductWindow.xaml
    /// </summary>
    public partial class ProductWindow : Window
    {
        public Product Product;
        public ProductWindow(Product product)
        {
            InitializeComponent();
            Product = product;
            FillData();
        }

        private void okButton_Click(object sender, RoutedEventArgs e)
        {
            if (DataIsOK())
            {
                var product = new Product(
                    titleBox.Text,
                    double.Parse(priceBox.Text),
                    double.Parse(amountBox.Text),
                    (UnitOfMeasure)UOMBox.SelectedValue
                    );
                if (!product.Equals(Product))
                {
                    Product = product;
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
            titleBox.Text = Product.Title;
            priceBox.Text = Product.Price.ToString();
            amountBox.Text = Product.Amount.ToString();
            UOMBox.ItemsSource = Enum.GetValues(typeof(UnitOfMeasure)).Cast<UnitOfMeasure>();
            UOMBox.SelectedValue = Product.UnitOfMeasure;
        }

        private bool DataIsOK()
        {
            return
                titleBox.Text.Any(x => Char.IsLetterOrDigit(x))
                && NumericDataIsOK(priceBox.Text)
                && NumericDataIsOK(amountBox.Text)
                && UOMBox.SelectedIndex != -1;
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
