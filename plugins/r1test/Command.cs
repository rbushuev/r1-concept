using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autodesk.Revit.ApplicationServices;
using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Autodesk.Revit.UI.Selection;

namespace r1test
{
    [TransactionAttribute(TransactionMode.ReadOnly)]
    public class Command : IExternalCommand
    {

        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {

            UIApplication uiapp = commandData.Application;
            UIDocument uidoc = uiapp.ActiveUIDocument;
            Document doc = uidoc.Document;
            string name = doc.Title;
            string path = doc.PathName;

            var a = GetActiveWorkset(doc);

            Debug.Write($"asd");

            FilteredWorksetCollector fwc = new FilteredWorksetCollector(doc);
            foreach (Workset w in fwc)
            {
                ElementWorksetFilter ewf = new ElementWorksetFilter(w.Id, false);
                ICollection<ElementId> elemIds = new FilteredElementCollector(doc).WherePasses(ewf).ToElementIds();
                int foundElems = elemIds.Count;
                var msg = foundElems.ToString() + ". " + w.Name + "\n";
                Debug.Write(msg);
            }

            TaskDialog.Show("R1 Module", a.Name);

            return Result.Succeeded;
        }

        public Workset GetActiveWorkset(Document doc)
        {
            WorksetTable worksetTable = doc.GetWorksetTable();
            WorksetId activeId = worksetTable.GetActiveWorksetId();
            Workset workset = worksetTable.GetWorkset(activeId);
            return workset;
        }
    }
}
