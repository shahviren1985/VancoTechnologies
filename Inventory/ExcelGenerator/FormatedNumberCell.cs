using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Spreadsheet;

namespace ITM.ExcelGenerator
{
    public class FormatedNumberCell : NumberCell
    {
        public FormatedNumberCell(string header, string text, int index, Stylesheet styleSheet)
            : base(header, text, index, styleSheet)
        {
            this.StyleIndex = 2;
        }

    }
}
