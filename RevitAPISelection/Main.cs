using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.DB.Mechanical;
using Autodesk.Revit.DB.Plumbing;
using Autodesk.Revit.UI;
using Autodesk.Revit.UI.Selection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RevitAPISelection
{
    [Transaction(TransactionMode.Manual)]
    public class Main : IExternalCommand

    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            UIApplication uiapp = commandData.Application;
            UIDocument uidoc = uiapp.ActiveUIDocument;
            Document doc = uidoc.Document;

            //Выбор воздуховодов
            //List<Duct> fInstances = new FilteredElementCollector(doc, doc.ActiveView.Id)
            //     .OfCategory(BuiltInCategory.OST_PipeCurves)
            //     .WhereElementIsNotElementType()
            //     .Cast<Duct>()
            //     .ToList();


            //Выбор труб на активном виде
            //List<Pipe> fInstances = new FilteredElementCollector(doc, doc.ActiveView.Id)
            //    .OfCategory(BuiltInCategory.OST_PipeCurves)
            //    .WhereElementIsNotElementType()
            //    .Cast<Pipe>()
            //    .ToList();
            //TaskDialog.Show("Количество ", fInstances.Count.ToString());

            //Выбор колонн
            List<FamilyInstance> fInstances = new FilteredElementCollector(doc, doc.ActiveView.Id)
                .OfCategory(BuiltInCategory.OST_StructuralColumns)
                .WhereElementIsNotElementType()
                .Cast<FamilyInstance>()
                .ToList();
            TaskDialog.Show("Количество ", fInstances.Count.ToString());


            return Result.Succeeded;
        }
    }
}
