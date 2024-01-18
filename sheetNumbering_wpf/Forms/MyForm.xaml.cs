using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;


namespace sheetNumbering_wpf
{
    /// <summary>
    /// Interaction logic for Window.xaml
    /// </summary>

    public partial class MyForm : Window
    {
        public bool OperationCancelled { get; private set; } = true;
        public Document myDoc = null;
        public MyForm(Document doc, List<Reference> curref)
        {
            InitializeComponent();
            myDoc = doc;

            foreach (Reference view in curref)
            {
                lbxData.Items.Add(view.ElementId);
            }

            for (int i = 1; i <= 20; i++)
            {
                cmbData.Items.Add(i.ToString());
            }

        }


        void MainWindow_Closed(object sender, EventArgs e)
        {
            TaskDialog.Show("Close", "Application is getting closed");
            Close();
            
        }


        internal string getstartnum()
        {
             if (cmbData.SelectedItem != null) 
                { return cmbData.SelectedItem.ToString();} 

            return null;
        }

        private void Ok_Click(object sender, RoutedEventArgs e)
        {
            OperationCancelled = false;
            this.DialogResult = true; this.Close();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {

            this.DialogResult = false; this.Close();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            OperationCancelled = false;
            this.Close();
        }
    }
}
