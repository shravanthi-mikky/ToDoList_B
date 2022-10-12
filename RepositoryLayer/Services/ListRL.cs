using CommonLayer.Model;
using Microsoft.Extensions.Configuration;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace RepositoryLayer.Services
{
    public class ListRL : IListRL
    {
        IConfiguration config;
        SqlConnection sqlConnection;
        string ConnString = "Data Source=LAPTOP-2UH1FDRP\\MSSQLSERVER01;Initial Catalog=ToDoList;Integrated Security=True;";
        public ListRL(IConfiguration config)
        {
            this.config = config;
        }

        public ListItemModel AddTask(ListItemModel book, int UserId)
        //public ListItemModel AddTask(ListItemModel book)
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
                    //com.Parameters.AddWithValue("@startDate", book.startDate);
                    com.Parameters.AddWithValue("@description", book.description);
                    com.Parameters.AddWithValue("@UserId",UserId);

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

        public bool DeleteItem(ListItemDelete listItemDelete)
        {
            SqlConnection conn = new SqlConnection(ConnString);
            SqlCommand com = new SqlCommand("Sp_DeleteItem", conn);
            com.CommandType = CommandType.StoredProcedure;
            com.Parameters.AddWithValue("@ItemId", listItemDelete.ItemId);

            conn.Open();
            int i = com.ExecuteNonQuery();
            conn.Close();
            if (i >= 1)
            {
                return true;
            }
            return false;
        }

        public List<ListItemModel> GetAllTasks()
        {
            List<ListItemModel> books = new List<ListItemModel>();
            SqlConnection conn = new SqlConnection(ConnString);
            using (conn)
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("SpGetAll", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    conn.Open();
                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            books.Add(new ListItemModel
                            {
                                UserId = Convert.ToInt32(reader["UserId"]),
                                title = reader["Title"].ToString(),
                                status = reader["Status"].ToString(),
                                startDate = (DateTime)reader["StartDate"],
                                description = reader["Description"].ToString(),
                                dueDate = reader["DueDate"].ToString()
                            });
                        }
                        return books;
                    }
                    else
                    {
                        return null;
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    conn.Close();
                }

            }
        }
        /*
        public ListItemModel UpdateBook(ListItemModel book, long bookid)
        {
            try
            {
                SqlConnection conn = new SqlConnection(ConnString);
                using (conn)
                {

                    SqlCommand com = new SqlCommand("Sp_Updatebook", conn);
                    com.CommandType = CommandType.StoredProcedure;
                    com.Parameters.AddWithValue("@bookId", bookid);
                    com.Parameters.AddWithValue("@title", book.title);
                    com.Parameters.AddWithValue("@status", book.status);
                    com.Parameters.AddWithValue("@duedate", book.dueDate);
                    //com.Parameters.AddWithValue("@startDate", book.startDate);
                    com.Parameters.AddWithValue("@description", book.description);

                    conn.Open();
                    int i = com.ExecuteNonQuery();
                    conn.Close();
                    if (i >= 1)
                    {
                        return book;
                    }
                    return null;
                }
            }
            catch (Exception)
            {

                throw;
            }

        }
        */
    }
}
