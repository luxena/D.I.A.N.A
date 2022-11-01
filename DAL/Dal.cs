using ENTITY;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Json;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Text;

namespace DAL
{
    public class Dal
    {
        public Configuration GetConfiguration()
        {
            Configuration configuration = new Configuration();

            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName)
                .AddJsonFile("appsettings.json");

            IConfiguration config = builder.AddJsonFile("appsettings.json", true, true).Build();

            configuration.DBConnection = config.GetConnectionString("DBConnection");

            return configuration;
        }

        public string Return(string request)
        {
            return Process(request);
        }

        public string Process(string request)
        {
            string table = "Phrases";
            return GetEntity(request,table).Meaning;
        }

        public Entity GetEntity(string request, string table)
        {
            Entity entity = new Entity();

            string connectionString = GetConfiguration().DBConnection;
            SqlConnection con = new SqlConnection(connectionString);

            using (con)
            {
                try
                {
                    con.Open();
                    string query = @"SELECT * FROM " + table + " WHERE Topic = @Topic";

                    SqlCommand cmd = new SqlCommand(query,con);
                    cmd.Parameters.AddWithValue(@"Topic",request);

                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        entity.Topic = reader["Topic"].ToString();
                        entity.Meaning = reader["Meaning"].ToString();
                    }
                }
                catch (SqlException ex)
                {
                    Console.WriteLine(ex.Message);
                }

                con.Close();
            }

            return entity;
        }

        public void Learn(string request,string response,string table)
        {
            string connectionString = GetConfiguration().DBConnection;
            SqlConnection con = new SqlConnection(connectionString);

            using (con)
            {
                try
                {
                    con.Open();
                    string query = @"INSERT INTO " + table + " VALUES (@Topic,@Meaning)";

                    SqlCommand cmd = new SqlCommand(query, con);
                    cmd.Parameters.AddWithValue(@"Topic", request);
                    cmd.Parameters.AddWithValue(@"Meaning", response);
                    cmd.ExecuteNonQuery();

                  
                }
                catch (SqlException ex)
                {
                    Console.WriteLine(ex.Message);
                }

                con.Close();
            }
        }
    }
}
