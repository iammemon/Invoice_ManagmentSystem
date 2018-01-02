using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace invoice_ms.Models
{
    public class ProductInfo
    {
        public int Id { get; set; }
        public int Qty { get; set; }
        public decimal Rate { get; set; }

        public static DataTable convertToDataTable(List<ProductInfo> list)
        {
            var table = new DataTable();
            table.Columns.Add("product_id");
            table.Columns.Add("qty");
            table.Columns.Add("rate");
            foreach (var item in list)
            {
                table.Rows.Add(item.Id, item.Qty, item.Rate);
            }
            return table;
        }
    }
}