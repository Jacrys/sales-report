using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Data.Linq.Mapping;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using OfficeOpenXml;

public partial class _Default : Page
{

    List<SalesOrder> salesOrders;
    protected void Page_Load(object sender, EventArgs e)
    {
        

        var today = DateTime.Today;
        var thisMonth = new DateTime(today.Year, today.Month, 1);
        var firstDayOfLastMonth = thisMonth.AddMonths(-1);
        var lastDayOfLastMonth = thisMonth.AddDays(-1);
        frmStartDate.Text = firstDayOfLastMonth.ToString("yyyy-MM-dd");
        frmEndDate.Text = lastDayOfLastMonth.ToString("yyyy-MM-dd");

        //DataContext db = new DataContext(@"Data\AdventureWorks_Data.mdf");
        //Table<Customer> Customers = db.GetTable<Customer>();

    }



    [Database(Name = "AdventureWorks")]
    public class AdventureWorks : DataContext
    {
        //public Table<DirInfo> DirectoryInformation;
        public AdventureWorks(string connection) : base(connection) { }
    }

    public List<SalesOrder> LookupSalesOrders(DateTime start, DateTime end)
    {
        DataContext db = new DataContext(@"Data Source = Data\AdventureWorks_Data.mdf; Persist Security Info=False;User ID = SalesReport; Initial Catalog = AdventureWorks");

        Table<Customer> Customers = db.GetTable<Customer>();
        Table<Person> Persons = db.GetTable<Person>();
        Table<Product> Products = db.GetTable<Product>();
        Table<SalesOrderDetail> SalesOrderLineItems = db.GetTable<SalesOrderDetail>();
        Table<SalesOrderHeader> SalesOrders = db.GetTable<SalesOrderHeader>();
        Table<Store> Stores = db.GetTable<Store>();

        var soquery =
            from SOD in SalesOrderLineItems
            where SOD.SalesOrderHeader.DueDate >= start && SOD.SalesOrderHeader.DueDate <= end
            select new SalesOrder(SOD.SalesOrderHeader.Customer.Store.Name, SOD.SalesOrderHeader.Customer.Person.FirstName + " " + SOD.SalesOrderHeader.Customer.Person.LastName, SOD.SalesOrderHeader.AccountNumber, SOD.SalesOrderHeader.SalesOrderNumber, SOD.SalesOrderHeader.PurchaseOrderNumber, SOD.SalesOrderHeader.OrderDate, SOD.SalesOrderHeader.DueDate, SOD.SalesOrderHeader.TotalDue, SOD.Product.ProductNumber, SOD.OrderQty, SOD.UnitNet, SOD.LineTotal);
        return soquery.ToList<SalesOrder>();
    }


    protected void Submit_Click(object sender, EventArgs e)
    {
        salesOrders = LookupSalesOrders(DateTime.Parse(frmStartDate.Text), DateTime.Parse(frmEndDate.Text));
        tblSalesOrders.DataSource = salesOrders.Take(15);
        tblSalesOrders.DataBind();
        gridPanel.Update();
        tblSalesOrders.Visible = true;
        btnDownload.Visible = true;
    }


    private class Column : SpreadsheetBuilder.ColumnTemplate<SalesOrder> { }
    protected void Download_Click(object sender, EventArgs e)
    {
        var pkg = new ExcelPackage();
        var wbk = pkg.Workbook;
        var sheet = wbk.Worksheets.Add("Invoice Data");

        var normalStyle = "Normal";
        var acctStyle = wbk.CreateAccountingFormat();
        var dateStyle = wbk.CreateDateFormat();
        var intStyle = wbk.CreateIntegerFormat();
        var data = salesOrders;

        var columns = new[]
        {
            new Column { Title = "Sold At", Style = normalStyle, Action = i => i.StoreName},
            new Column { Title = "Sold To", Style = normalStyle, Action = i => i.SoldTo},
            new Column { Title = "Account Number", Style = normalStyle, Action = i => i.AccountNumber },
            new Column { Title = "Invoice #", Style = normalStyle, Action = i => i.InvoiceNo },
            new Column { Title = "Customer PO #", Style = normalStyle, Action = i => i.CustomerPONo },
            new Column { Title = "Order Date", Style = dateStyle, Action = i => i.OrderDate },
            new Column { Title = "Due Date", Style = dateStyle, Action = i => i.DueDate },
            new Column { Title = "Invoice Total", Style = acctStyle, Action = i => i.InvoiceTotal },
            new Column { Title = "Product Number", Style = normalStyle, Action = i => i.ProductNum },
            new Column { Title = "Order Qty", Style = intStyle, Action = i => i.OrderQty },
            new Column { Title = "Unit Net", Style = acctStyle, Action = i => i.UnitNet },
            new Column { Title = "Line Total", Style = acctStyle, Action = i => i.LineTotal },
        };

        this.Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
        this.Response.AddHeader(
                  "content-disposition",
                  string.Format("attachment;  filename={0}", "ExcellData.xlsx"));
        this.Response.BinaryWrite(pkg.GetAsByteArray());
    }
}