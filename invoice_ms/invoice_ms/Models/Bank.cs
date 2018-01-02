using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;

namespace invoice_ms.Models
{
    public class Bank
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string Iban { get; set; }
        public string Swift { get; set; }

        public static bool AddBank(Bank detail) {
            using (var cmd = new SqlCommand("InsertBank", Connection.getConnection())) {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@name", detail.Name);
                cmd.Parameters.AddWithValue("@city", detail.City);
                cmd.Parameters.AddWithValue("@country", detail.Country);
                cmd.Parameters.AddWithValue("@iban", detail.Iban);
                cmd.Parameters.AddWithValue("@swift", detail.Swift);
                var result=cmd.ExecuteNonQuery();
                if (result > 0)
                    return true;
                else
                    return false;

            }
        }

        public static bool deleteBank(int id) {
            using (var cmd = new SqlCommand("deleteBank", Connection.getConnection()))
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

        public static List<Bank>  GetAll() {
            using (var cmd = new SqlCommand("getAllBanks", Connection.getConnection())) {

                var records = new List<Bank>();
                cmd.CommandType = CommandType.StoredProcedure;
                var reader = cmd.ExecuteReader();
                while (reader.Read()) {
                    records.Add(new Bank()
                    {
                        Id = Convert.ToInt32(reader["bank_id"]),
                        Name = reader["name"].ToString(),
                        City=reader["city"].ToString(),
                        Country=reader["country"].ToString(),
                        Iban=reader["iban"].ToString(),
                        Swift=reader["swift"].ToString()
                    });
                }
                return records;
            }
        }

        public static bool updateBank(Bank updatedBank) {
            using (var cmd = new SqlCommand("updateBank", Connection.getConnection()))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@id", updatedBank.Id);
                cmd.Parameters.AddWithValue("@name", updatedBank.Name);
                cmd.Parameters.AddWithValue("@city", updatedBank.City);
                cmd.Parameters.AddWithValue("@country", updatedBank.Country);
                cmd.Parameters.AddWithValue("@iban", updatedBank.Iban);
                cmd.Parameters.AddWithValue("@swift", updatedBank.Swift);
                var result = cmd.ExecuteNonQuery();
                if (result > 0)
                    return true;
                else
                    return false;

            }
        } 

    }
}