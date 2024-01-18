#region Namespaces
using Autodesk.Revit.ApplicationServices;
using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Autodesk.Revit.UI.Selection;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;

#endregion

namespace sheetNumbering_wpf
{
    [Transaction(TransactionMode.Manual)]
    public class Command : IExternalCommand
    {
        public Result Execute(
          ExternalCommandData commandData,
          ref string message,
          ElementSet elements)
        {
            UIApplication uiapp = commandData.Application;
            UIDocument uidoc = uiapp.ActiveUIDocument;
            Application app = uiapp.Application;
            Document doc = uidoc.Document;

            // put any code needed for the form here

            // open form
            List<Reference> firstref = new List<Reference> { };
            MyForm currentForm = new MyForm(doc, firstref)
            {
                Width = 700,
                Height = 500,
                WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen,
                Topmost = false,

            };

            currentForm.ShowDialog();

            if (currentForm.OperationCancelled is true)
            {
                return Result.Failed;
            }


            List<Reference> references = new List<Reference>();
            bool flag = true;

            while (flag)
            {
                try
                {
                    Reference pickobj = uidoc.Selection.PickObject(ObjectType.Element, "Select views in order");
                    references.Add(pickobj);
                }
                catch
                {
                    flag = false;
                }
            }




            MyForm currentForm2 = new MyForm(doc, references)
            {
                Width = 700,
                Height = 500,
                WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen,
                Topmost = false,
            };

            currentForm2.ShowDialog();

            if(currentForm2.OperationCancelled is true)
            {
                return Result.Failed;
            }

            string startnum = currentForm2.getstartnum();
            int count = int.Parse(startnum);
            List<string> viewnames = new List<string>();
            Transaction t = new Transaction(doc);
            t.Start("Renumbering");
            {

                foreach (Reference reference in references)
                {
                    ElementId elementId = reference.ElementId;
                    Element element = doc.GetElement(elementId);

                    Viewport viewport = doc.GetElement(element.Id) as Viewport;

                    if (element is Viewport)
                    {
                        Parameter parameter = viewport.get_Parameter(BuiltInParameter.VIEWPORT_DETAIL_NUMBER);
                        parameter.Set(count.ToString() + "zzz");
                        count++;
                    }
                }
                int count2 = int.Parse(startnum);

                foreach (Reference reference in references)
                {
                    ElementId elementId = reference.ElementId;

                    // Use the ElementId to get the Element
                    Element element = doc.GetElement(elementId);

                    Viewport viewport = doc.GetElement(element.Id) as Viewport;
                    viewnames.Add(viewport.get_Parameter(BuiltInParameter.VIEWPORT_VIEW_NAME).AsString());

                    if (element is Viewport)
                    {
                        Parameter parameter = viewport.get_Parameter(BuiltInParameter.VIEWPORT_DETAIL_NUMBER);
                        parameter.Set(count2.ToString());
                        count2++;
                    }
                }
            }
            t.Commit();
            t.Dispose();

            MyFormResult currentForm3 = new MyFormResult(doc, viewnames)
            {
                Width = 700,
                Height = 500,
                WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen,
                Topmost = true,
            };
            currentForm3.ShowDialog();

            // get form data and do something

            return Result.Succeeded;
        }



        public static String GetMethod()
        {
            var method = MethodBase.GetCurrentMethod().DeclaringType?.FullName;
            return method;
        }


    }
}
