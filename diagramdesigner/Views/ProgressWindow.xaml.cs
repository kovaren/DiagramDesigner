using System;
using System.Collections.Generic;
using System.ComponentModel;
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
    /// Interaction logic for ProgressWindow.xaml
    /// </summary>
    public partial class ProgressBarPopup : Window
    {
        BackgroundWorker worker;

        public ProgressBarPopup()
        {
            InitializeComponent();
        }

        public ProgressBarPopup(String text)
        {
            ((TextBlock)this.FindName("pbText")).Text = text;
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            this.worker = new BackgroundWorker {WorkerReportsProgress = true};
           // this.worker.ProgressChanged += new ProgressChangedEventHandler(worker_ProgressChanged);

            //this.worker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(worker_RunWorkerCompleted);

            //this.worker.DoWork += new DoWorkEventHandler(worker_DoWork);

            this.worker.RunWorkerAsync();
        }
    }
}
