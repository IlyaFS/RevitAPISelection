﻿using Autodesk.Revit.DB;
using Autodesk.Revit.UI.Selection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RevitAPISelection
{
    public class WallFilter : ISelectionFilter
    {
        public bool AllowElement(Element elem)
        {
            return elem is Curve;
        }

        public bool AllowReference(Reference reference, XYZ position)
        {
            return false;
        }
    }
}