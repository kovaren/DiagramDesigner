using System.Windows;
using System.Windows.Input;

namespace DiagramDesigner
{
    public partial class Window1 : Window
    {
        public Window1()
        {
            InitializeComponent();
            this.Title = "RBP Model Editor";
        }

        private void DesignerTabs_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {

        }

    }
}
