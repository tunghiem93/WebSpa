using CMS_Common;
using CMS_DataModel.Models;
using CMS_DTO.CMSDiscount;
using CMS_DTO.CMSOrder;
using CMS_DTO.CMSReport;
using CMS_Shared.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CMS_Shared.CMSReports
{
    public class CMSReportFactory
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

                    var listOrder = db.CMS_Order.Where(o => o.ReceiptCreatedDate >= from && o.ReceiptCreatedDate <= to && o.Status == (byte)Commons.EStatus.Actived).ToList();
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
    }
}
