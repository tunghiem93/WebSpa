using OfficeOpenXml;
using OfficeOpenXml.Style;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS_Shared.CMSReports
{
    public class ReportFactory
    {
        public void CreateReportHeader(ExcelWorksheet wsReceipt, ExcelWorksheet wsExpense, int totalCols, string reportName, DateTime fromDate, DateTime toDate)
        {
            // Format for Receipt
            wsReceipt.Cells[1, 1].Value = "LAMODEHOME SPA";
            wsReceipt.Cells[1, 1, 1, totalCols].Merge = true;
            // Report Name
            wsReceipt.Cells[2, 1].Value = reportName;
            wsReceipt.Cells[2, 1, 2, totalCols].Merge = true;
            wsReceipt.Row(2).Style.Font.Size = 16;
            wsReceipt.Row(2).Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
            wsReceipt.Row(2).Style.VerticalAlignment = ExcelVerticalAlignment.Center;
            wsReceipt.Row(2).Height = 40;
            // Time
            wsReceipt.Cells[3, 1, 3, totalCols].Merge = true;
            wsReceipt.Cells[3, 1].Value = "Thời gian: Từ " + fromDate.ToString("dd/MM/yyyy HH:mm") + " Đến " + toDate.ToString("dd/MM/yyyy HH:mm");
            wsReceipt.Row(3).Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;

            wsReceipt.Cells[1, 1, 3, totalCols].Style.Fill.PatternType = ExcelFillStyle.Solid;
            wsReceipt.Cells[1, 1, 3, totalCols].Style.Fill.BackgroundColor.SetColor(Color.FromArgb(217, 217, 217));
            wsReceipt.Cells[2, 1, 2, totalCols].Style.Font.Bold = true;

            //Fomat for Expense
            wsExpense.Cells[1, 1].Value = "LAMODEHOME SPA";
            wsExpense.Cells[1, 1, 1, totalCols].Merge = true;
            // Report Name
            wsExpense.Cells[2, 1].Value = reportName;
            wsExpense.Cells[2, 1, 2, totalCols].Merge = true;
            wsExpense.Row(2).Style.Font.Size = 16;
            wsExpense.Row(2).Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
            wsExpense.Row(2).Style.VerticalAlignment = ExcelVerticalAlignment.Center;
            wsExpense.Row(2).Height = 40;
            // Time
            wsExpense.Cells[3, 1, 3, totalCols].Merge = true;
            wsExpense.Cells[3, 1].Value = "Thời gian: Từ " + fromDate.ToString("dd/MM/yyyy HH:mm") + " Đến " + toDate.ToString("dd/MM/yyyy HH:mm");
            wsExpense.Row(3).Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;


            wsExpense.Cells[1, 1, 3, totalCols].Style.Border.Top.Style = ExcelBorderStyle.Thin;
            wsExpense.Cells[1, 1, 3, totalCols].Style.Border.Left.Style = ExcelBorderStyle.Thin;
            wsExpense.Cells[1, 1, 3, totalCols].Style.Border.Right.Style = ExcelBorderStyle.Thin;
            wsExpense.Cells[1, 1, 3, totalCols].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;

            wsExpense.Cells[1, 1, 3, totalCols].Style.Fill.PatternType = ExcelFillStyle.Solid;
            wsExpense.Cells[1, 1, 3, totalCols].Style.Fill.BackgroundColor.SetColor(Color.FromArgb(217, 217, 217));
            wsExpense.Cells[2, 1, 2, totalCols].Style.Font.Bold = true;
        }
    }
}
