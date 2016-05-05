﻿using System;
using System.Collections.Generic;
using System.Data;
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
    /// Interaction logic for WindowALS.xaml
    /// </summary>
    public partial class WindowALS : Window
    {
        public WindowALS()
        {
            InitializeComponent();
        }
        public WindowALS(DataTable dt, string expr)
        {
           
            InitializeComponent();
            dgALS.DataContext = dt.DefaultView;
            rtbALS.Document.Blocks.Clear();
            rtbALS.Document.Blocks.Add(new Paragraph(new Run(expr)));
        }
    }
}