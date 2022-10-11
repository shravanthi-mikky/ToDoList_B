using CommonLayer.Model;
using Microsoft.Extensions.Configuration;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace RepositoryLayer.Services
{
    public class ListRL : IListRL
    {
        IConfiguration config;
        SqlConnection sqlConnection;
        string ConnString = "Data Source=LAPTOP-2UH1FDRP\\MSSQLSERVER01;Initial Catalog=BookStore;Integrated Security=True;";
        public ListRL(IConfiguration config)
        {
            this.config = config;
        }

        public ListItemModel AddTask(ListItemModel book, int UserId)
        {
            try
            {
                using (sqlConnection = new SqlConnection(ConnString))
                {
                    SqlCommand com = new SqlCommand("Sp_AddItem", sqlConnection);
                    com.CommandType = System.Data.CommandType.StoredProcedure;

                    sqlConnection.Open();
                    
                    com.Parameters.AddWithValue("@title", book.title);
                    com.Parameters.AddWithValue("@status", book.status);
                    com.Parameters.AddWithValue("@duedate", book.dueDate);
                    //com.Parameters.AddWithValue("@totalRating", book.startDate);
                    com.Parameters.AddWithValue("@description", book.description);
                    com.Parameters.AddWithValue("@UserId", UserId);

                    com.ExecuteNonQuery();
                    return book;

                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            finally
            {
                sqlConnection.Close();
            }
        }

    }
}
