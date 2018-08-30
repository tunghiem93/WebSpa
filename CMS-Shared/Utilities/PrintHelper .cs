using CMS_DTO.CMSOrder;
using PdfSharp;
using PdfSharp.Drawing;
using PdfSharp.Pdf;
using SelectPdf;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS_Shared.Utilities
{
    public class PrintHelper
    {
        public static void Print(CMS_OrderModels modelOrder, string path)
        {
            try
            {
                PrintFromHTML(modelOrder, path);
            }
            catch(Exception ex) { };
            
        }

        public void PrintManual(CMS_OrderModels modelOrder)
        {
            //PdfDocument document = new PdfDocument();
            //document.Info.Author = "Quang Tran TP";
            //document.Info.Keywords = "Lamode";

            //PdfPage page = document.AddPage();
            //page.Size = PageSize.A4;

            //// Obtain an XGraphics object to render to
            //XGraphics gfx = XGraphics.FromPdfPage(page);

            //// Create a font
            //double fontHeight = 36;
            //XFont font = new XFont("Times New Roman", fontHeight, XFontStyle.BoldItalic);

            //// Get the centre of the page
            //double y = page.Height / 2;
            //int lineCount = 0;
            //double linePadding = 10;

            //// Create a rectangle to draw the text in and draw in it
            //XRect rect = new XRect(0, y, page.Width, fontHeight);
            //gfx.DrawString("Hello, World! ", font,
            //               XBrushes.Black, rect, XStringFormats.Center);
            //lineCount++;
            //y += fontHeight;

            //rect = new XRect(0, y, page.Width, fontHeight);
            //gfx.DrawString("This is not" +
            //    " auto-wrap when the edge of the page is reached",
            //               font, XBrushes.Black,
            //               rect, XStringFormats.TopLeft);
            //lineCount++;
            //y += fontHeight;

            //double topY = y - (lineCount * fontHeight) - linePadding;

            //// Draw a line below the text
            //gfx.DrawLine(XPens.Black, 0, y, page.Width, y + linePadding);

            //// Draw a line above the text
            //gfx.DrawLine(XPens.Black, 0, topY, page.Width, topY);

            //// Save and show the document
            //document.Save("C:\\Users\\NoName\\Desktop\\a.pdf");
            //Process.Start("C:\\Users\\NoName\\Desktop\\a.pdf");
        }

        public static void PrintFromHTML(CMS_OrderModels modelOrder, string path)
        {
            var body = CommonHelper.CreateBodyMail(modelOrder);

            HtmlToPdf converter = new HtmlToPdf();
            converter.Options.PdfPageSize = PdfPageSize.A5;
            converter.Options.WebPageWidth = 650;
            converter.Options.WebPageHeight = 0;

            // convert the url to pdf 
            SelectPdf.PdfDocument doc = converter.ConvertHtmlString(body);

            // save pdf document 
            doc.Save(path);

            // close pdf document 
            doc.Close();
        }
        
    }

}
