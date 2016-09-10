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
using Microsoft.Win32;
using System.IO;

namespace DiagramDesigner
{
    /// <summary>
    /// Interaction logic for InformationWindow.xaml
    /// </summary>
    public partial class InformationWindow : Window
    {
        public InformationResource InformationResource;
        Document Document;
        bool ResourceCreated = false;

        public InformationWindow(InformationResource informationResource)
        {
            InitializeComponent();
            InformationResource = informationResource;
            FillData();
            Closing += InformationWindow_Closing;
        }

        void InformationWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            DialogResult = ResourceCreated;
        }

        private void okButton_Click(object sender, RoutedEventArgs e)
        {
            if (DataIsOK())
            {
                InformationResource.Title = titleBox.Text;
                InformationResource.CreationDate = dateBox.SelectedDate.Value;
                InformationResource.Document = Document;
                InformationResource.Category = (Category)categoryBox.SelectedItem;
                ResourceCreated = true;
                Close();

                //var informationResource = new InformationResource(InformationResource.DesignerID)
                //{
                //    ID = InformationResource.ID,
                //    Title = titleBox.Text,
                //    CreationDate = dateBox.SelectedDate.Value,
                //    Document = Document,
                //    Category = (Category)categoryBox.SelectedItem
                //};
                //if (!informationResource.Equals(InformationResource))
                //{
                //    InformationResource = informationResource;
                //    DialogResult = true;
                //}
                //else
                //    DialogResult = false;
            }
            else
                MessageBox.Show("Some fields are empty or invalid!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
        }

        private void openFileButton_Click(object sender, RoutedEventArgs e)
        {
            var openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == true)
            {
                bool chosen = true;
                var fileSize = new FileInfo(openFileDialog.FileName).Length;

                if (fileSize > 104857600)
                {
                    var result = MessageBox.Show(
                        "The file size exceeds 100 Mb. Are you sure you want to choose it?",
                        "Warning", MessageBoxButton.YesNo, MessageBoxImage.Question);

                    if (result == MessageBoxResult.No)
                    { chosen = false; }
                }

                if (chosen)
                {
                    Document = new Document()
                    {
                        Content = File.ReadAllBytes(openFileDialog.FileName),
                        Name = openFileDialog.SafeFileName
                    };
                    chosenFileBox.Content = openFileDialog.SafeFileName;
                    deleteDocButton.Visibility = System.Windows.Visibility.Visible;
                }
            }
        }

        private void FillData()
        {
            var itemsSource = Enum.GetValues(typeof(Category)).Cast<Category>().ToList();
            itemsSource.RemoveAt(0);
            categoryBox.ItemsSource = itemsSource;
            categoryBox.SelectedValue = InformationResource.Category;
            titleBox.Text = InformationResource.Title;
            dateBox.SelectedDate = InformationResource.CreationDate;
            chosenFileBox.Content = InformationResource.Document.ToString();
            Document = InformationResource.Document;
            if (!Document.IsEmpty)
            {
                deleteDocButton.Visibility = System.Windows.Visibility.Visible;
            }
        }

        private void deleteDocButton_Click(object sender, MouseButtonEventArgs e)
        {
            Document = new Document();
            chosenFileBox.Content = Document.ToString();
            ((UIElement)sender).Visibility = System.Windows.Visibility.Hidden;
        }

        private bool DataIsOK()
        {
            return
                titleBox.Text.Any(x => Char.IsLetterOrDigit(x))
                && dateBox.SelectedDate.HasValue
                && categoryBox.SelectedIndex != -1;
        }
    }
}
