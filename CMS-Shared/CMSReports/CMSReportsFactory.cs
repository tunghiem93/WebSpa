using CMS_Common;
using CMS_DataModel.Models;
using CMS_DTO.CMSOrder;
using CMS_DTO.CMSReport;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using System;
using System.Data.Entity;
using System.Drawing;
using System.Linq;

namespace CMS_Shared.CMSReports
{
    public class CMSReportFactory : ReportFactory
    {
        public CMS_ReportModels GetReportData(DateTime from, DateTime to, bool isDelete = false)
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

                    var listOrder = db.CMS_Order.Where(o => DbFunctions.TruncateTime(o.ReceiptCreatedDate) >= from.Date && DbFunctions.TruncateTime(o.ReceiptCreatedDate) <= to.Date).ToList();
                    if (isDelete == false)
                    {
                        listOrder = listOrder.Where(o => o.Status == (byte)Commons.EStatus.Actived).ToList();
                    }
                    var listOrderID = listOrder.Select(o => o.ID).ToList();
                    var listOrderDetail = db.CMS_OrderDetail.Where(o => listOrderID.Contains(o.OrderID) && (!string.IsNullOrEmpty(o.ProductID) || !string.IsNullOrEmpty(o.Remark))).ToList();

                    /* get product db */
                    var listProductID = listOrderDetail.Select(o => o.ProductID).ToList();
                    var listProduct = db.CMS_Products.Where(o => listProductID.Contains(o.ID)).ToList();

                    /* get cus db */
                    var listCusID = listOrder.Select(o => o.CustomerID).ToList();
                    var listCustomerDB = db.CMS_Customer.Where(o => listCusID.Contains(o.ID)).ToList();

                    /* get list emp db */
                    var listEmployeeID = listOrder.Select(o => o.Cashier).ToList();
                    var listEmployeeDb = db.CMS_Employee.Where(o => listEmployeeID.Contains(o.ID)).ToList();

                    /* get expense data */
                    var expenseData = new CMS_ReportExpensetModels();

                    var listOrderExpense = listOrder.Where(o => o.OrderType == (byte)Commons.EOrderType.Expense).ToList();
                    var listOrderExpenseID = listOrderExpense.Select(o => o.ID).ToList();
                    var listOrderDetailEx = listOrderDetail.Where(o => listOrderExpenseID.Contains(o.OrderID)).ToList();

                    expenseData.TotalBill = listOrderExpense.Where(o => o.Status != (byte)Commons.EStatus.Deleted).Sum(o => o.TotalBill) ?? 0;
                    expenseData.TotalItem = (double)(listOrderDetailEx.Where(o => o.Status != (byte)Commons.EStatus.Deleted).Sum(o => o.Quantity) ?? 0);
                    expenseData.ListOrder = listOrderExpense.Select(o => new CMS_OrderModels()
                    {
                        Id = o.ID,
                        OrderNo = o.OrderNo,
                        ReceiptNo = o.ReceiptNo,
                        ReceiptCreatedDate = o.ReceiptCreatedDate ?? Commons.MinDate,
                        TotalBill = o.TotalBill,
                        SubTotal = o.SubTotal,
                        Status = o.Status,
                        Remark = o.Remark,
                        Reason = o.Reason,
                        TotalDiscount = o.TotalDiscount,
                        CustomerId = o.CustomerID,
                        CustomerName = listCustomerDB.Where(c => c.ID == o.CustomerID).Select(c => c.FirstName).FirstOrDefault(),
                        Phone = listCustomerDB.Where(c => c.ID == o.CustomerID).Select(c => c.Phone).FirstOrDefault(),
                        Email = listCustomerDB.Where(c => c.ID == o.CustomerID).Select(c => c.Email).FirstOrDefault(),
                        EmployeeName = listEmployeeDb.Where(e => e.ID == o.Cashier).Select(e => e.Name).FirstOrDefault(),
                    }).ToList();

                    foreach (var order in expenseData.ListOrder)
                    {
                        order.Items = listOrderDetailEx.Where(o => o.OrderID == order.Id)
                            .GroupJoin(listProduct, od => od.ProductID, p => p.ID, (od, p) => new { od, p = p.FirstOrDefault() })
                            .Select(o => new CMS_ItemModels()
                            {
                                ID = o.od.ID,
                                ProductID = o.od.ProductID,
                                ProductName = o.p?.Name ?? o.od.Remark,
                                Quantity = (double)(o.od.Quantity ?? 1),
                                Price = o.od.Price ?? 0,
                                TotalPrice = ((double)o.od.Price * (double)o.od.Quantity),
                                DiscountValue = o.od.DiscountValue,
                            }).ToList();
                    }
                    data.ReportExpense = expenseData;

                    /* get expense data */
                    var receiptData = new CMS_ReportReceiptModels();

                    var listOrderRc = listOrder.Where(o => o.OrderType == (byte)Commons.EOrderType.Normal).ToList();
                    var listOrderRcID = listOrderRc.Select(o => o.ID).ToList();
                    var listOrderDetailRc = listOrderDetail.Where(o => listOrderRcID.Contains(o.OrderID)).ToList();

                    receiptData.TotalBill = listOrderRc.Where(o => o.Status != (byte)Commons.EStatus.Deleted).Sum(o => o.TotalBill) ?? 0;
                    receiptData.TotalDiscount = listOrderRc.Where(o => o.Status != (byte)Commons.EStatus.Deleted).Sum(o => o.TotalDiscount) ?? 0;
                    receiptData.TotalItem = (double)(listOrderDetailRc.Where(o => o.Status != (byte)Commons.EStatus.Deleted).Sum(o => o.Quantity) ?? 0);
                    receiptData.ListOrder = listOrderRc.Select(o => new CMS_OrderModels()
                    {
                        Id = o.ID,
                        OrderNo = o.OrderNo,
                        ReceiptNo = o.ReceiptNo,
                        ReceiptCreatedDate = o.ReceiptCreatedDate ?? Commons.MinDate,
                        TotalBill = o.TotalBill,
                        SubTotal = o.SubTotal,
                        TotalDiscount = o.TotalDiscount,
                        Status = o.Status,
                        Remark = o.Remark,
                        Reason = o.Reason,
                        CustomerName = listCustomerDB.Where(c => c.ID == o.CustomerID).Select(c => c.FirstName).FirstOrDefault(),
                        Phone = listCustomerDB.Where(c => c.ID == o.CustomerID).Select(c => c.Phone).FirstOrDefault(),
                        Email = listCustomerDB.Where(c => c.ID == o.CustomerID).Select(c => c.Email).FirstOrDefault(),
                        EmployeeName = listEmployeeDb.Where(e => e.ID == o.Cashier).Select(e => e.Name).FirstOrDefault(),
                    }).ToList();

                    foreach (var order in receiptData.ListOrder)
                    {
                        order.Items = listOrderDetailRc.Where(o => o.OrderID == order.Id)
                            .GroupJoin(listProduct, od => od.ProductID, p => p.ID, (od, p) => new { od, p = p.FirstOrDefault() })
                            .Select(o => new CMS_ItemModels()
                            {
                                ID = o.od.ID,
                                ProductID = o.od.ProductID,
                                ProductName = o.p?.Name ?? o.od.Remark,
                                Quantity = (double)(o.od.Quantity ?? 1),
                                Price = o.od.Price ?? 0,
                                TotalPrice = ((double)o.od.Price * (double)o.od.Quantity),
                                DiscountValue = o.od.DiscountValue,
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
            int totalCols = 12;
            CreateReportHeader(wsReceipt, wsExpense, totalCols, "BÁO CÁO THU CHI", request.From, request.To);
            // Format hedaer report Receipt 
            wsReceipt.Cells[1, 1, 3, totalCols].Style.Border.Top.Style = ExcelBorderStyle.Thin;
            wsReceipt.Cells[1, 1, 3, totalCols].Style.Border.Left.Style = ExcelBorderStyle.Thin;
            wsReceipt.Cells[1, 1, 3, totalCols].Style.Border.Right.Style = ExcelBorderStyle.Thin;
            wsReceipt.Cells[1, 1, 3, totalCols].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
            //wsReceipt.Cells[4, 1, 4, totalCols].Merge = true;
            // Format hedaer report Expense 
            wsExpense.Cells[1, 1, 3, totalCols].Style.Border.Top.Style = ExcelBorderStyle.Thin;
            wsExpense.Cells[1, 1, 3, totalCols].Style.Border.Left.Style = ExcelBorderStyle.Thin;
            wsExpense.Cells[1, 1, 3, totalCols].Style.Border.Right.Style = ExcelBorderStyle.Thin;
            wsExpense.Cells[1, 1, 3, totalCols].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
            //wsExpense.Cells[4, 1, 4, totalCols].Merge = true;
            CMS_ReportModels data = new CMS_ReportModels();
            try
            {
                data = GetReportData(request.From, request.To, request.IsIncludeDelete);

                /* receipt order */
                if (data.ReportReceipt.ListOrder != null && data.ReportReceipt.ListOrder.Any())
                {
                    int row = 4;

                    /* sum total */
                    wsReceipt.Cells[row, 1].Value = "Tổng Tiền";
                    wsReceipt.Row(row).Style.Font.Bold = true;
                    wsReceipt.Cells[row, 1, row, 2].Style.Fill.PatternType = ExcelFillStyle.Solid;
                    wsReceipt.Cells[row, 1, row, 2].Style.Fill.BackgroundColor.SetColor(Color.FromArgb(217, 217, 217));
                    wsReceipt.Cells[row, 2].Value = string.Format("{0:0,0 VNĐ}", data.ReportReceipt.TotalBill);
                    row++;
                    wsReceipt.Cells[row, 1, row, totalCols].Merge = true;
                    row++;

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
                    row++;
                    int _firstRow = row;
                    
                    //List item in data
                    foreach (var item in data.ReportReceipt.ListOrder)
                    {
                        if (item.Status == (byte)Commons.EStatus.Deleted)
                        {
                            wsReceipt.Cells[row, 1, row, totalCols].Style.Font.Strike = true;
                        }
                        wsReceipt.Cells[row, 1].Value = item.ReceiptNo + " (" + item.OrderNo + ")"; ;
                        wsReceipt.Cells[row, 2].Value = item.ReceiptCreatedDate.ToString("dd/MM/yyyy");
                        wsReceipt.Cells[row, 3].Value = item.EmployeeName;
                        wsReceipt.Cells[row, 4].Value = item.CustomerName;
                        wsReceipt.Cells[row, 5].Value = item.Phone;
                        wsReceipt.Cells[row, 6].Value = item.Email;
                        wsReceipt.Cells[row, 7].Value = item.Address;
                        int _firstChild = row + 1;
                        if (item.Items != null && item.Items.Any())
                        {
                            foreach (var itemChild in item.Items)
                            {
                                wsReceipt.Cells[row, 8].Value = itemChild.ProductName;
                                wsReceipt.Cells[row, 9].Value = itemChild.Quantity;
                                wsReceipt.Cells[row, 10].Value = string.Format("{0:0,0 VNĐ}", itemChild.Price);
                                wsReceipt.Cells[row, 11].Value = itemChild.DiscountValue;
                                wsReceipt.Cells[row, 12].Value = string.Format("{0:0,0 VNĐ}", itemChild.TotalPrice);
                                wsReceipt.Cells[row, 1, row, totalCols].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                wsReceipt.Cells[row, 1, row, totalCols].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                // format border
                                wsReceipt.Cells[row, 1, row, totalCols].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                                wsReceipt.Cells[row, 1, row, totalCols].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                                wsReceipt.Cells[row, 1, row, totalCols].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                                wsReceipt.Cells[row, 1, row, totalCols].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                                if (item.Status == (byte)Commons.EStatus.Deleted)
                                    wsReceipt.Cells[row, 8, row, totalCols].Style.Font.Strike = true;
                                row++;
                            }
                        }

                        wsReceipt.Cells[row, 11].Value = "Giảm giá";
                        //wsReceipt.Row(row).Style.Font.Bold = true;
                        wsReceipt.Cells[row, 11, row, totalCols].Style.Fill.PatternType = ExcelFillStyle.Solid;
                        wsReceipt.Cells[row, 11, row, totalCols].Style.Fill.BackgroundColor.SetColor(Color.FromArgb(217, 217, 217));
                        wsReceipt.Cells[row, 12].Value = string.Format("{0:0,0 VNĐ}", item.TotalDiscount);
                        wsReceipt.Cells[row, 8, row, 10].Merge = true;
                        wsReceipt.Cells[_firstChild, 1, row, 7].Merge = true;
                        if (item.Status == (byte)Commons.EStatus.Deleted)
                            wsReceipt.Cells[row, 11, row, totalCols].Style.Font.Strike = true;
                        row++;

                        /*  */
                        wsReceipt.Cells[row, 11].Value = "Tổng";
                        wsReceipt.Row(row).Style.Font.Bold = true;
                        wsReceipt.Cells[row, 11, row, totalCols].Style.Fill.PatternType = ExcelFillStyle.Solid;
                        wsReceipt.Cells[row, 11, row, totalCols].Style.Fill.BackgroundColor.SetColor(Color.FromArgb(217, 217, 217));
                        wsReceipt.Cells[row, 12].Value = string.Format("{0:0,0 VNĐ}", item.TotalBill);
                        wsReceipt.Cells[row, 8, row, 10].Merge = true;
                        wsReceipt.Cells[_firstChild, 1, row, 7].Merge = true;
                        var range = wsReceipt.Cells[_firstChild, 1, row, 7];
                        wsReceipt.Cells[range.Start.Address].Value = item.Reason;
                        if (item.Status == (byte)Commons.EStatus.Deleted)
                            wsReceipt.Cells[row, 11, row, totalCols].Style.Font.Strike = true;
                        row++;
                    }

                    /* sum total */
                    wsReceipt.Cells[row, 11].Value = "Tổng Tiền";
                    wsReceipt.Row(row).Style.Font.Bold = true;
                    wsReceipt.Cells[row, 11, row, totalCols].Style.Fill.PatternType = ExcelFillStyle.Solid;
                    wsReceipt.Cells[row, 11, row, totalCols].Style.Fill.BackgroundColor.SetColor(Color.FromArgb(217, 217, 217));
                    wsReceipt.Cells[row, 12].Value = string.Format("{0:0,0 VNĐ}", data.ReportReceipt.TotalBill);

                    wsReceipt.Cells[_firstRow, 1, row - 1, totalCols].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                    wsReceipt.Cells[_firstRow, 1, row - 1, totalCols].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    //format for col 7
                    wsReceipt.Cells[_firstRow, 7, row - 1, totalCols].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                    wsReceipt.Cells[_firstRow, 7, row - 1, totalCols].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    // format border
                    wsReceipt.Cells[_firstRow, 1, row - 1, totalCols].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                    wsReceipt.Cells[_firstRow, 1, row - 1, totalCols].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                    wsReceipt.Cells[_firstRow, 1, row - 1, totalCols].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                    wsReceipt.Cells[_firstRow, 1, row - 1, totalCols].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;

                    wsReceipt.Cells[_firstRow, totalCols].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                    wsReceipt.Cells[_firstRow, totalCols].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                    // Format report
                    wsReceipt.Column(1).Width = 20;
                    wsReceipt.Column(2).Width = 20;
                    wsReceipt.Column(3).Width = 20;
                    wsReceipt.Column(4).Width = 20;
                    wsReceipt.Column(5).Width = 20;
                    wsReceipt.Column(6).Width = 20;
                    wsReceipt.Column(7).Width = 30;
                    wsReceipt.Column(8).Width = 30;
                    wsReceipt.Column(9).Width = 15;
                    wsReceipt.Column(10).Width = 15;
                    wsReceipt.Column(11).Width = 15;
                    wsReceipt.Column(12).Width = 15;
                    wsReceipt.Column(13).Width = 15;
                    wsReceipt.Column(14).Width = 20;
                    wsReceipt.Cells.Style.WrapText = true;
                }

                /* expense order */
                if (data.ReportExpense.ListOrder != null && data.ReportExpense.ListOrder.Any())
                {
                    int row = 4;

                    /* sum total */
                    wsExpense.Cells[row, 1].Value = "Tổng Tiền";
                    wsExpense.Row(row).Style.Font.Bold = true;
                    wsExpense.Cells[row, 1, row, 2].Style.Fill.PatternType = ExcelFillStyle.Solid;
                    wsExpense.Cells[row, 1, row, 2].Style.Fill.BackgroundColor.SetColor(Color.FromArgb(217, 217, 217));
                    wsExpense.Cells[row, 2].Value = string.Format("{0:0,0 VNĐ}", data.ReportExpense.TotalBill);
                    row++;
                    wsExpense.Cells[row, 1, row, totalCols].Merge = true;
                    row++;

                    wsExpense.Cells[row, 1].Value = "Mã hóa đơn";
                    wsExpense.Cells[row, 2].Value = "Ngày tạo";
                    wsExpense.Cells[row, 3].Value = "Nhân viên";
                    wsExpense.Cells[row, 4].Value = "Khách hàng";
                    wsExpense.Cells[row, 5].Value = "Số điện thoại";
                    wsExpense.Cells[row, 6].Value = "Email";
                    wsExpense.Cells[row, 7].Value = "Địa chỉ";
                    wsExpense.Cells[row, 8].Value = "Tên mặt hàng";
                    wsExpense.Cells[row, 9].Value = "Số lượng";
                    wsExpense.Cells[row, 10].Value = "Giá";
                    wsExpense.Cells[row, 11].Value = "Giá trị giảm giá";
                    wsExpense.Cells[row, 12].Value = "Tổng giá";
                    wsExpense.Row(row).Height = 15;
                    wsExpense.Row(row).Style.Font.Bold = true;

                    // format text
                    wsExpense.Cells[row, 1, row, totalCols].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                    wsExpense.Cells[row, 1, row, totalCols].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    // format border
                    wsExpense.Cells[row, 1, row, totalCols].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                    wsExpense.Cells[row, 1, row, totalCols].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                    wsExpense.Cells[row, 1, row, totalCols].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                    wsExpense.Cells[row, 1, row, totalCols].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                    // format backdround color
                    wsExpense.Cells[row, 1, row, totalCols].Style.Fill.PatternType = ExcelFillStyle.Solid;
                    wsExpense.Cells[row, 1, row, totalCols].Style.Fill.BackgroundColor.SetColor(Color.FromArgb(217, 217, 217));
                    //end Columns Name header
                    row++;
                    int _firstRow = row;
                    //List item in data

                    foreach (var item in data.ReportExpense.ListOrder)
                    {
                        if (item.Status == (byte)Commons.EStatus.Deleted)
                            wsExpense.Cells[row, 1, row, totalCols].Style.Font.Strike = true;
                        wsExpense.Cells[row, 1].Value = item.ReceiptNo + " (" + item.OrderNo + ")";
                        wsExpense.Cells[row, 2].Value = item.ReceiptCreatedDate.ToString("dd/MM/yyyy");
                        wsExpense.Cells[row, 3].Value = item.EmployeeName;
                        wsExpense.Cells[row, 4].Value = item.CustomerName;
                        wsExpense.Cells[row, 5].Value = item.Phone;
                        wsExpense.Cells[row, 6].Value = item.Email;
                        wsExpense.Cells[row, 7].Value = item.Address;
                        int _firstChild = row + 1;
                        if (item.Items != null && item.Items.Any())
                        {
                            foreach (var itemChild in item.Items)
                            {
                                wsExpense.Cells[row, 8].Value = itemChild.ProductName;
                                wsExpense.Cells[row, 9].Value = itemChild.Quantity;
                                wsExpense.Cells[row, 10].Value = string.Format("{0:0,0 VNĐ}", itemChild.Price);
                                wsExpense.Cells[row, 11].Value = itemChild.DiscountValue;
                                wsExpense.Cells[row, 12].Value = string.Format("{0:0,0 VNĐ}", itemChild.TotalPrice);
                                wsExpense.Cells[row, 1, row, totalCols].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                wsExpense.Cells[row, 1, row, totalCols].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                // format border
                                wsExpense.Cells[row, 1, row, totalCols].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                                wsExpense.Cells[row, 1, row, totalCols].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                                wsExpense.Cells[row, 1, row, totalCols].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                                wsExpense.Cells[row, 1, row, totalCols].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;

                                if (item.Status == (byte)Commons.EStatus.Deleted)
                                    wsExpense.Cells[row, 1, row, totalCols].Style.Font.Strike = true;
                                row++;
                            }
                        }
                        wsExpense.Cells[row, 11].Value = "Tổng";
                        wsExpense.Row(row).Style.Font.Bold = true;
                        wsExpense.Cells[row, 11, row, totalCols].Style.Fill.PatternType = ExcelFillStyle.Solid;
                        wsExpense.Cells[row, 11, row, totalCols].Style.Fill.BackgroundColor.SetColor(Color.FromArgb(217, 217, 217));
                        wsExpense.Cells[row, 12].Value = string.Format("{0:0,0 VNĐ}", item.TotalBill);
                        wsExpense.Cells[row, 8, row, 10].Merge = true;
                        wsExpense.Cells[_firstChild, 1, row, 7].Merge = true;
                        if (item.Status == (byte)Commons.EStatus.Deleted)
                            wsExpense.Cells[row, 1, row, totalCols].Style.Font.Strike = true;
                        row++;
                    }

                    /* sum total */
                    wsExpense.Cells[row, 11].Value = "Tổng Tiền";
                    wsExpense.Row(row).Style.Font.Bold = true;
                    wsExpense.Cells[row, 11, row, totalCols].Style.Fill.PatternType = ExcelFillStyle.Solid;
                    wsExpense.Cells[row, 11, row, totalCols].Style.Fill.BackgroundColor.SetColor(Color.FromArgb(217, 217, 217));
                    wsExpense.Cells[row, 12].Value = string.Format("{0:0,0 VNĐ}", data.ReportExpense.TotalBill);

                    wsExpense.Cells[_firstRow, 1, row - 1, totalCols].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                    wsExpense.Cells[_firstRow, 1, row - 1, totalCols].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    //format for col 7
                    wsExpense.Cells[_firstRow, 7, row - 1, totalCols].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                    wsExpense.Cells[_firstRow, 7, row - 1, totalCols].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    // format border
                    wsExpense.Cells[_firstRow, 1, row - 1, totalCols].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                    wsExpense.Cells[_firstRow, 1, row - 1, totalCols].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                    wsExpense.Cells[_firstRow, 1, row - 1, totalCols].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                    wsExpense.Cells[_firstRow, 1, row - 1, totalCols].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;

                    wsExpense.Cells[_firstRow, totalCols].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                    wsExpense.Cells[_firstRow, totalCols].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                    // Format report
                    wsExpense.Column(1).Width = 20;
                    wsExpense.Column(2).Width = 20;
                    wsExpense.Column(3).Width = 20;
                    wsExpense.Column(4).Width = 20;
                    wsExpense.Column(5).Width = 20;
                    wsExpense.Column(6).Width = 20;
                    wsExpense.Column(7).Width = 30;
                    wsExpense.Column(8).Width = 30;
                    wsExpense.Column(9).Width = 15;
                    wsExpense.Column(10).Width = 15;
                    wsExpense.Column(11).Width = 15;
                    wsExpense.Column(12).Width = 15;
                    wsExpense.Column(13).Width = 15;
                    wsExpense.Column(14).Width = 20;
                    wsExpense.Cells.Style.WrapText = true;
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
