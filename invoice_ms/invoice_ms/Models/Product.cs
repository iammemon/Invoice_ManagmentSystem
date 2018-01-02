using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace invoice_ms.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }


        public static List<Product> GetAll()
        {
            using (var cmd = new SqlCommand("getAllProducts", Connection.getConnection()))
            {

                var records = new List<Product>();
                cmd.CommandType = CommandType.StoredProcedure;
                var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    records.Add(new Product()
                    {
                        Id = Convert.ToInt32(reader["product_id"]),
                        Name = reader["product_name"].ToString(),
                        Price = Convert.ToDecimal(reader["product_currentPrice"])
                        
                    });
                }
                return records;
            }
        }

        public static bool AddProduct(Product product)
        {
            using (var cmd = new SqlCommand("InsertProduct", Connection.getConnection()))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@name", product.Name);
                cmd.Parameters.AddWithValue("@price", product.Price);
                var result = cmd.ExecuteNonQuery();
                if (result > 0)
                    return true;
                else
                    return false;

            }
        }
        public static bool updateProduct(Product updatedProduct)
        {
            using (var cmd = new SqlCommand("updateProduct", Connection.getConnection()))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@id", updatedProduct.Id);
                cmd.Parameters.AddWithValue("@name", updatedProduct.Name);
                cmd.Parameters.AddWithValue("@price", updatedProduct.Price);
                var result = cmd.ExecuteNonQuery();
                if (result > 0)
                    return true;
                else
                    return false;

            }
        }
        public static bool deleteProduct(int id)
        {
            using (var cmd = new SqlCommand("deleteProduct", Connection.getConnection()))
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

    }
}