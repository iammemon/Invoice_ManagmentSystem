using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace invoice_ms.Models
{
    public class Invoice
    {
        public int Id { get; set; }
        public int Client_id { get; set; }
        public int Bank_id { get; set; }
        public int Status_no { get; set; }
        public string Date { get; set; }
        public string DueDate { get; set; }
        public List<ProductInfo> products { get; set; }
        public Invoice() {
            products = new List<ProductInfo>();
        }

        public static bool InsertInvoice(Invoice invoice)
        {
            using (var cmd = new SqlCommand("Insertinvoice", Connection.getConnection()))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@clientId", invoice.Client_id);
                cmd.Parameters.AddWithValue("@bankId", invoice.Bank_id);
                cmd.Parameters.AddWithValue("@status", invoice.Status_no);
                cmd.Parameters.AddWithValue("@inv_date", invoice.Date);
                cmd.Parameters.AddWithValue("@due_date", invoice.DueDate);
                var contactTable = ProductInfo.convertToDataTable(invoice.products);
                var param = new SqlParameter() { ParameterName = "@products", SqlDbType = SqlDbType.Structured, Value = contactTable };
                cmd.Parameters.Add(param);
                //cmd.Parameters.AddWithValue("@contacts", newClient.ContactInfo.ToArray());
                var result = cmd.ExecuteNonQuery();
                if (result > 0)
                    return true;
                else
                    return false;
            };
        }
        public static bool UpdateInvoice(Invoice newInvoice)
        {
            using (var cmd = new SqlCommand("updateInvoice", Connection.getConnection()))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@invoice_id", newInvoice.Id);
                cmd.Parameters.AddWithValue("@clientId", newInvoice.Client_id);
                cmd.Parameters.AddWithValue("@bankId", newInvoice.Bank_id);
                cmd.Parameters.AddWithValue("@status", newInvoice.Status_no);
                cmd.Parameters.AddWithValue("@inv_date", newInvoice.Date);
                cmd.Parameters.AddWithValue("@due_date", newInvoice.DueDate);
                var contactTable = ProductInfo.convertToDataTable(newInvoice.products);
                var param = new SqlParameter() { ParameterName = "@products", SqlDbType = SqlDbType.Structured, Value = contactTable };
                cmd.Parameters.Add(param);
                //cmd.Parameters.AddWithValue("@contacts", newClient.ContactInfo.ToArray());
                var result = cmd.ExecuteNonQuery();
                if (result > 0)
                    return true;
                else
                    return false;
            };
        }
        public static List<Invoice> GetAll()
        {
            using (var cmd = new SqlCommand("getAllInvoice", Connection.getConnection()))
            {

                var records = new List<Invoice>();
                cmd.CommandType = CommandType.StoredProcedure;
                var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    records.Add(new Invoice()
                    {
                        Id = Convert.ToInt32(reader["invoice_id"]),
                        Client_id = Convert.ToInt32(reader["client_id"]),
                        Bank_id = Convert.ToInt32(reader["bank_id"]),
                        Status_no = Convert.ToInt32(reader["status_no"]),
                        Date = reader["invoice_date"].ToString(),
                        DueDate = reader["due_date"].ToString()


                    });
                }
                return records;
            }
        }
        public static bool deleteInvoice(int id)
        {
            using (var cmd = new SqlCommand("deleteInvoice", Connection.getConnection()))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@id", id);
                var result = cmd.ExecuteNonQuery();
                if (result > 0)
                    return true;
                else
                    return false;

            }
        }
        public static Invoice getInvoice(int id)
        {
            using (var cmd = new SqlCommand("GetInvoice", Connection.getConnection()))
            {

                var record = new Invoice();
                cmd.Parameters.AddWithValue("@id",id );
                cmd.CommandType = CommandType.StoredProcedure;
                var reader = cmd.ExecuteReader();
                while (reader.Read())
                {

                    record.Id = Convert.ToInt32(reader["invoice_id"]);
                    record.Client_id = Convert.ToInt32(reader["client_id"]);
                    record.Bank_id = Convert.ToInt32(reader["bank_id"]);
                    record.Status_no = Convert.ToInt32(reader["status_no"]);
                    record.Date = reader["invoice_date"].ToString();
                    record.DueDate = reader["due_date"].ToString();
                    record.products.Add(new ProductInfo()
                    {
                        Id = Convert.ToInt32(reader["product_id"]),
                        Qty = Convert.ToInt32(reader["quantity"]),
                        Rate = Convert.ToDecimal(reader["rate"])
                    });



                }
                return record;
            }
        }

    }

}