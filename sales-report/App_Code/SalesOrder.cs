using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for SalesOrder
/// </summary>
public class SalesOrder
{
    public string StoreName { get; set; }
    public string SoldTo { get; set; }
    public string AccountNumber { get; set; }
    public string InvoiceNo { get; set; }
    public string CustomerPONo {get; set; }
    public DateTime OrderDate { get; set; }
    public DateTime DueDate { get; set; }
    public decimal InvoiceTotal { get; set; }
    public string ProductNum { get; set; }
    public int OrderQty { get; set; }
    public decimal UnitNet { get; set; }
    public decimal LineTotal { get; set; }

    public SalesOrder(string storeName,
                             string soldTo,
                             string accountNumber,
                             string invoiceNo,
                             string customerPONo,
                             DateTime orderDate,
                             DateTime dueDate,
                             decimal invoiceTotal,
                             string productNum,
                             int orderQty,
                             decimal unitNet,
                             decimal lineTotal)
    {
        StoreName = storeName;
        SoldTo = soldTo;
        AccountNumber = accountNumber;
        InvoiceNo = invoiceNo;
        CustomerPONo = customerPONo;
        OrderDate = orderDate;
        DueDate = dueDate;
        InvoiceTotal = invoiceTotal;
        ProductNum = productNum;
        OrderQty = orderQty;
        UnitNet = unitNet;
        LineTotal = lineTotal;

    }
}

