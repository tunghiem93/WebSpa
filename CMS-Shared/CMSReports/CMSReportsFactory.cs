using CMS_Common;
using CMS_DataModel.Models;
using CMS_DTO.CMSDiscount;
using CMS_DTO.CMSOrder;
using CMS_DTO.CMSReport;
using CMS_Shared.Utilities;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CMS_Shared.CMSReports
{
    public class CMSReportFactory : ReportFactory
    {
        public CMS_ReportModels GetReportData(DateTime from, DateTime to)
        {
            CMS_ReportModels ret = null;
            try
            {
                using (var db = new CMS_Context())
                {
                    CMS_ReportModels data = new CMS_ReportModels
                    {
                        From = from,
                        To = to,
                    };

                    var listOrder = db.CMS_Order.Where(o => DbFunctions.TruncateTime(o.ReceiptCreatedDate) >= from.Date && DbFunctions.TruncateTime(o.ReceiptCreatedDate) <= to.Date && o.Status == (byte)Commons.EStatus.Actived).ToList();
                    var listOrderID = listOrder.Select(o => o.ID).ToList();
                    var listOrderDetail = db.CMS_OrderDetail.Where(o => listOrderID.Contains(o.OrderID) && o.Status == (byte)Commons.EStatus.Actived).ToList();
                    var listProductID = listOrderDetail.Select(o => o.ProductID).ToList();
                    var listProduct = db.CMS_Products.Where(o => listProductID.Contains(o.ID)).ToList();

                    /* get expense data */
                    var expenseData = new CMS_ReportExpensetModels();

                    var listOrderExpense = listOrder.Where(o => o.OrderType == (byte)Commons.EOrderType.Expense).ToList();
                    var listOrderExpenseID = listOrderExpense.Select(o => o.ID).ToList();
                    var listOrderDetailEx = listOrderDetail.Where(o => listOrderExpenseID.Contains(o.OrderID)).ToList();

                    expenseData.TotalBill = listOrderExpense.Sum(o => o.TotalBill) ?? 0;
                    expenseData.TotalItem = (double)(listOrderDetailEx.Sum(o => o.Quantity) ?? 0);
                    expenseData.ListOrder = listOrderExpense.Select(o => new CMS_OrderModels()
                    {
                        Id = o.ID,
                        OrderNo = o.OrderNo,
                        TotalBill = o.TotalBill,
                        SubTotal = o.SubTotal,
                        TotalDiscount = o.TotalDiscount,
                    }).ToList();

                    foreach (var order in expenseData.ListOrder)
                    {
                        order.Items = listOrderDetailEx.Where(o => o.OrderID == order.Id).Join(listProduct, od => od.ProductID, p => p.ID, (od, p) => new CMS_ItemModels
                        {
                            ID = od.ID,
                            ProductID = od.ProductID,
                            ProductName = p != null ? p.Name : od.Remark,
                            Quantity = (double)(od.Quantity ?? 1),
                            Price = od.Price ?? 0,
                            TotalPrice = (double)(od.Price * (double)od.Quantity),
                            DiscountValue = od.DiscountValue,
                        }).ToList();
                    }
                    data.ReportExpense = expenseData;

                    /* get expense data */
                    var receiptData = new CMS_ReportReceiptModels();

                    var listOrderRc = listOrder.Where(o => o.OrderType == (byte)Commons.EOrderType.Normal).ToList();
                    var listOrderRcID = listOrderRc.Select(o => o.ID).ToList();
                    var listOrderDetailRc = listOrderDetail.Where(o => listOrderRcID.Contains(o.OrderID)).ToList();

                    receiptData.TotalBill = listOrderRc.Sum(o => o.TotalBill) ?? 0;
                    receiptData.TotalDiscount = listOrderRc.Sum(o => o.TotalDiscount) ?? 0;
                    receiptData.TotalItem = (double)(listOrderDetailRc.Sum(o => o.Quantity) ?? 0);
                    receiptData.ListOrder = listOrderRc.Select(o => new CMS_OrderModels()
                    {
                        Id = o.ID,
                        OrderNo = o.OrderNo,
                        TotalBill = o.TotalBill,
                        SubTotal = o.SubTotal,
                        TotalDiscount = o.TotalDiscount,
                    }).ToList();

                    foreach (var order in receiptData.ListOrder)
                    {
                        order.Items = listOrderDetailRc.Where(o => o.OrderID == order.Id).Join(listProduct, od => od.ProductID, p => p.ID, (od, p) => new CMS_ItemModels
                        {
                            ID = od.ID,
                            ProductID = od.ProductID,
                            ProductName = p != null ? p.Name : od.Remark,
                            Quantity = (double)(od.Quantity ?? 1),
                            Price = od.Price ?? 0,
                            TotalPrice = (double)(od.Price * (double)od.Quantity),
                            DiscountValue = od.DiscountValue,
                        }).ToList();
                    }
                    data.ReportReceipt = receiptData;

                    ret = data;
                }
            }
            catch (Exception ex)
            {
                NSLog.Logger.Error("ErrorGetReportData :", ex);
            }
            return ret;
        }

        public ExcelPackage Report(CMS_ReportModels request)
        {
            ExcelPackage pck = new ExcelPackage();
            ExcelWorksheet wsReceipt = pck.Workbook.Worksheets.Add("Báo cáo thu");
            ExcelWorksheet wsExpense = pck.Workbook.Worksheets.Add("Báo cáo chi");
            int totalCols = 14;
            CreateReportHeader(wsReceipt, wsExpense, totalCols, "BÁO CÁO THU CHI", request.From, request.To);
            // Format hedaer report
            wsReceipt.Cells[1, 1, 3, totalCols].Style.Border.Top.Style = ExcelBorderStyle.Thin;
            wsReceipt.Cells[1, 1, 3, totalCols].Style.Border.Left.Style = ExcelBorderStyle.Thin;
            wsReceipt.Cells[1, 1, 3, totalCols].Style.Border.Right.Style = ExcelBorderStyle.Thin;
            wsReceipt.Cells[1, 1, 3, totalCols].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;

            CMS_ReportModels data = new CMS_ReportModels();
            try
            {
                data = GetReportData(request.From, request.To);
                if (data.ReportReceipt.ListOrder != null && data.ReportReceipt.ListOrder.Any())
                {
                    int row = 5;
                    wsReceipt.Cells[row, 1].Value = "Mã hóa đơn";
                    wsReceipt.Cells[row, 2].Value = "Ngày tạo";
                    wsReceipt.Cells[row, 3].Value = "Nhân viên";
                    wsReceipt.Cells[row, 4].Value = "Khách hàng";
                    wsReceipt.Cells[row, 5].Value = "Số điện thoại";
                    wsReceipt.Cells[row, 6].Value = "Email";
                    wsReceipt.Cells[row, 7].Value = "Địa chỉ";
                    wsReceipt.Cells[row, 8].Value = "Tên mặt hàng";
                    wsReceipt.Cells[row, 9].Value = "Số lượng";
                    wsReceipt.Cells[row, 10].Value = "Giá";
                    wsReceipt.Cells[row, 11].Value = "Giá trị giảm giá";
                    wsReceipt.Cells[row, 12].Value = "Tổng giá";
                    wsReceipt.Cells[row, 13].Value = "Tổng giảm giá hóa đơn";
                    wsReceipt.Cells[row, 14].Value = "Tổng tiền hóa đơn";
                    wsReceipt.Row(row).Height = 15;
                    wsReceipt.Row(row).Style.Font.Bold = true;

                    // format text
                    wsReceipt.Cells[row, 1, row, totalCols].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                    wsReceipt.Cells[row, 1, row, totalCols].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    // format border
                    wsReceipt.Cells[row, 1, row, totalCols].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                    wsReceipt.Cells[row, 1, row, totalCols].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                    wsReceipt.Cells[row, 1, row, totalCols].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                    wsReceipt.Cells[row, 1, row, totalCols].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                    // format backdround color
                    wsReceipt.Cells[row, 1, row, totalCols].Style.Fill.PatternType = ExcelFillStyle.Solid;
                    wsReceipt.Cells[row, 1, row, totalCols].Style.Fill.BackgroundColor.SetColor(Color.FromArgb(217, 217, 217));
                    //end Columns Name header  

                    int _firstRow = row;
                    //List item in data
                    row++;
                    foreach (var item in data.ReportReceipt.ListOrder)
                    {
                        wsReceipt.Cells[row, 1].Value = item.OrderNo;
                        wsReceipt.Cells[row, 2].Value = item.CreatedDate.ToString();
                        wsReceipt.Cells[row, 3].Value = item.EmployeeName;
                        wsReceipt.Cells[row, 4].Value = item.CustomerName;
                        wsReceipt.Cells[row, 5].Value = item.Phone;
                        wsReceipt.Cells[row, 6].Value = item.Email;
                        wsReceipt.Cells[row, 7].Value = item.Address;                        
                        wsReceipt.Cells[row, 13].Value = data.ReportReceipt.TotalDiscount;
                        wsReceipt.Cells[row, 14].Value = data.ReportReceipt.TotalBill;
                        if (item.Items != null && item.Items.Any())
                        {
                            row++;
                            foreach (var itemChild in item.Items)
                            {
                                wsReceipt.Cells[row, 8].Value = itemChild.ProductName;
                                wsReceipt.Cells[row, 9].Value = itemChild.Quantity;
                                wsReceipt.Cells[row, 10].Value = itemChild.Price;
                                wsReceipt.Cells[row, 11].Value = itemChild.DiscountValue;
                                wsReceipt.Cells[row, 12].Value = itemChild.TotalPrice;
                                row++;
                            }
                        }
                    }
                    wsReceipt.Cells[_firstRow, 1, row - 1, totalCols].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                    wsReceipt.Cells[_firstRow, 1, row - 1, totalCols].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    //format for col 7
                    wsReceipt.Cells[_firstRow, 7, row - 1, totalCols].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                    wsReceipt.Cells[_firstRow, 7, row - 1, totalCols].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                    // format border
                    wsReceipt.Cells[_firstRow, 1, row - 1, totalCols].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                    wsReceipt.Cells[_firstRow, 1, row - 1, totalCols].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                    wsReceipt.Cells[_firstRow, 1, row - 1, totalCols].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                    wsReceipt.Cells[_firstRow, 1, row - 1, totalCols].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;

                    wsReceipt.Cells[_firstRow, totalCols].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                    wsReceipt.Cells[_firstRow, totalCols].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    wsReceipt.Cells[_firstRow, totalCols].Style.Fill.PatternType = ExcelFillStyle.Solid;
                    wsReceipt.Cells[_firstRow, totalCols].Style.Fill.BackgroundColor.SetColor(Color.FromArgb(217, 217, 217));

                    // Format report
                    wsReceipt.Column(1).Width = 30;
                    wsReceipt.Column(2).Width = 15;
                    wsReceipt.Column(3).Width = 15;
                    wsReceipt.Column(4).Width = 15;
                    wsReceipt.Column(5).Width = 15;
                    wsReceipt.Column(6).Width = 20;
                    wsReceipt.Column(7).Width = 30;
                    wsReceipt.Column(8).Width = 15;
                    wsReceipt.Column(9).Width = 15;
                    wsReceipt.Column(10).Width = 15;
                    wsReceipt.Column(11).Width = 15;
                    wsReceipt.Column(12).Width = 20;
                    wsReceipt.Column(13).Width = 15;
                    wsReceipt.Column(14).Width = 20;
                    wsReceipt.Cells.Style.WrapText = true;
                }
            }
            catch (Exception ex)
            {
                NSLog.Logger.Error("Report_Receipt:", ex);
            }
            return pck;
        }
    }
}
