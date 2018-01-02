using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;

namespace invoice_ms.Models
{
    public class Client
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public string Address { get; set; }
        public List<ContactInfo> Contactinfo { get; set; }
        public Client(){
            this.Contactinfo = new List<ContactInfo>();
        }

       

        public static bool InsertClient(Client newClient) {
            using (var cmd = new SqlCommand("InsertClient", Connection.getConnection()))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@name", newClient.Name);
                cmd.Parameters.AddWithValue("@city", newClient.City);
                cmd.Parameters.AddWithValue("@country", newClient.Country);
                cmd.Parameters.AddWithValue("@add", newClient.Address);
                var contactTable = ContactInfo.convertToDataTable(newClient.Contactinfo);
                var param = new SqlParameter() { ParameterName = "@contacts", SqlDbType = SqlDbType.Structured, Value = contactTable };
                cmd.Parameters.Add(param);
                //cmd.Parameters.AddWithValue("@contacts", newClient.ContactInfo.ToArray());
                var result = cmd.ExecuteNonQuery();
                if (result > 0)
                    return true;
                else
                    return false;
            };
        }
        public static bool UpdateClient(Client newClient)
        {
            using (var cmd = new SqlCommand("UpdateClient", Connection.getConnection()))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@id", newClient.Id);
                cmd.Parameters.AddWithValue("@name", newClient.Name);
                cmd.Parameters.AddWithValue("@city", newClient.City);
                cmd.Parameters.AddWithValue("@country", newClient.Country);
                cmd.Parameters.AddWithValue("@add", newClient.Address);
                var contactTable = ContactInfo.convertToDataTable(newClient.Contactinfo);
                var param = new SqlParameter() { ParameterName = "@contacts", SqlDbType = SqlDbType.Structured, Value = contactTable };
                cmd.Parameters.Add(param);
                //cmd.Parameters.AddWithValue("@contacts", newClient.ContactInfo.ToArray());
                var result = cmd.ExecuteNonQuery();
                if (result > 0)
                    return true;
                else
                    return false;
            };
        }

        public static List<Client> GetAll()
        {
            using (var cmd = new SqlCommand("getAllClient", Connection.getConnection()))
            {

                var records = new List<Client>();
                cmd.CommandType = CommandType.StoredProcedure;
                var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    records.Add(new Client()
                    {
                        Id = Convert.ToInt32(reader["client_id"]),
                        Address = reader["off_address"].ToString(),
                        Name = reader["name"].ToString(),
                        City = reader["city"].ToString(),
                        Country = reader["country"].ToString(),
                      
                    });
                }
                return records;
            }
        }
        public static List<ContactInfo> GetContactInfo(int id)
        {
            using (var cmd = new SqlCommand("getClientContactInfo", Connection.getConnection()))
            {

                var records = new List<ContactInfo>();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@id", id);
                var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    records.Add(new ContactInfo()
                    {
                        Id = Convert.ToInt32(reader["contactType_id"]),
                        Name = reader["contactType_name"].ToString(),
                        Contact=reader["contact"].ToString()
                        
                    });
                }
                return records;
            }
        }

        public static bool deleteClient(int id)
        {
            using (var cmd = new SqlCommand("deleteClient", Connection.getConnection()))
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