using Autodesk.Revit.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Media.TextFormatting;
using System.Windows.Navigation;
using System.Windows.Shapes;


namespace sheetNumbering_wpf
{
    /// <summary>
    /// Interaction logic for Window.xaml
    /// </summary>

    public partial class MyFormResult : Window
    {
        public MyFormResult(Document doc, List<String> curref)
        {
            InitializeComponent();

            foreach(string currefitem in curref)
            {
                lbxData.Items.Add(currefitem);
            }

            this.Closed += new EventHandler(MainWindow_Closed);
        }

        void MainWindow_Closed(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
             this.Close();
        }
    }
}
