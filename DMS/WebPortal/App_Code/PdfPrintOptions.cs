using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for PdfPrintOptions
/// </summary>
public class PdfPrintOptions
{
    public string Id { get; set; }
    public bool Display { get; set; }
    public int FontSize { get; set; }

    public bool Bold { get; set; }

    public int LLX { get; set; }
    public int LLY { get; set; }
    public int URX { get; set; }
    public int URY { get; set; }
    public string Text { get; set; }

    public string LineType { get; set; }

    public string ImagePath { get; set; }

    public int Scale { get; set; }
}