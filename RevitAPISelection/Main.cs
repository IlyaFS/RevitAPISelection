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

            IList<Reference> selectedElementRefList = uidoc.Selection.PickObjects(ObjectType.Element, "Выберите трубы");
            var elementList = new List<Element>();

            double Value = 0;
            double Sum = 0;

            foreach (var selectedElement in selectedElementRefList)
            {
                Element element = doc.GetElement(selectedElement);
                elementList.Add(element);

                if (element is Pipe)
                {
                    Parameter vParameter = element.get_Parameter(BuiltInParameter.CURVE_ELEM_LENGTH);
                    if (vParameter.StorageType == StorageType.Double)
                    {
                        Value = UnitUtils.ConvertFromInternalUnits(vParameter.AsDouble(), UnitTypeId.Meters);
                    }
                    Sum += Value;
                }
            }
            TaskDialog.Show("Результат", $"Длина выбранных труб: {Sum}м");





            #region
            //IList<Reference> selectedElementRefList = uidoc.Selection.PickObjects(ObjectType.Element, "Выберите стены");
            //var elementList = new List<Element>();

            //double Value = 0;
            //double Sum = 0;

            //foreach (var selectedElement in selectedElementRefList)
            //{
            //    Element element = doc.GetElement(selectedElement);
            //    elementList.Add(element);

            //    if (element is Wall)
            //    {
            //        Parameter vParameter = element.get_Parameter(BuiltInParameter.HOST_VOLUME_COMPUTED);
            //        if (vParameter.StorageType == StorageType.Double)
            //        {
            //            Value = UnitUtils.ConvertFromInternalUnits(vParameter.AsDouble(), UnitTypeId.CubicMeters);
            //        }
            //        Sum += Value;
            //    }
            //}
            //TaskDialog.Show("Результат", $"Объем выбранных стен: {Sum}м3");

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
            //List<FamilyInstance> fInstances = new FilteredElementCollector(doc, doc.ActiveView.Id)
            //    .OfCategory(BuiltInCategory.OST_StructuralColumns)
            //    .WhereElementIsNotElementType()
            //    .Cast<FamilyInstance>()
            //    .ToList();
            //TaskDialog.Show("Количество ", fInstances.Count.ToString());

            //Выбор воздуховодов по этажам
            //var doc = commandData.Application.ActiveUIDocument.Document;
            //var levels = new FilteredElementCollector(doc)
            //      .OfClass(typeof(Level))
            //      .OfType<Level>()
            //      .ToList();

            //foreach (Level level in levels)
            //{
            //    var ducts = new FilteredElementCollector(doc)
            //         .OfClass(typeof(Duct))
            //      .OfType<Duct>()
            //      .Where(duct => duct.get_Parameter(BuiltInParameter.RBS_START_LEVEL_PARAM).AsValueString() == level.Name)
            //      .Count();
            //    TaskDialog.Show("Результат", $"Этаж: {level.Name}, \nКоличество воздуховодов: {ducts}");
            //}
            #endregion

            return Result.Succeeded;
        }
    }
}
