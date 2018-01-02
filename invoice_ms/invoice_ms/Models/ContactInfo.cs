using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace invoice_ms.Models
{
    public class ContactInfo
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Contact { get; set; }

        public static DataTable convertToDataTable(List<ContactInfo> list) {
            var table = new DataTable();
            table.Columns.Add("id");
            table.Columns.Add("name");
            table.Columns.Add("contact");
            foreach (var item in list)
            {
                table.Rows.Add(item.Id, item.Name, item.Contact);
            }
            return table;
        }
    }
}