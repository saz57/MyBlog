using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using MyBlog.Models;
using System.Configuration;
using System.Threading.Tasks;

namespace MyBlog.Repository
{
    public class SQLRepository : IRepository
    {
        private SqlConnection _sqlConnection;
        private string _tableName;


        public SQLRepository()
        {
            _tableName = "Post";
            _sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);
        }

        public T Get<T>(int id) where T : Post
        {
            Task<T> task = AsyncGet<T>(id);
            task.Wait();
            return task.Result;
        }

        public List<T> Get<T>(int pageNumber, int pageSize) where T : Post
        {
            Task<List<T>> task = AsyncGet<T>(pageNumber, pageSize);
            task.Wait();
            return task.Result;
        }

        public void Put<T>(T item) where T : Post
        {
            _sqlConnection.OpenAsync().Wait();

            string sqlExpression = "INSERT INTO "+_tableName+" (Autor, Content) VALUES (@Autor, @Content)";
            SqlCommand find = new SqlCommand("SELECT * FROM " + _tableName + " WHERE Autor = @Autor AND Content = @Content", _sqlConnection);

            find.Parameters.Add("@Autor", SqlDbType.VarChar, 50);
            find.Parameters["@Autor"].Value = item.Autor;
            find.Parameters.Add("@Content", SqlDbType.VarChar);
            find.Parameters["@Content"].Value = item.Content;

            object WhatFound = find.ExecuteScalar();
            if (WhatFound == null) 
            {

                SqlCommand command = new SqlCommand(sqlExpression, _sqlConnection);

                command.Parameters.Add("@Autor", SqlDbType.VarChar, 50);
                command.Parameters["@Autor"].Value = item.Autor;
                command.Parameters.Add("@Content", SqlDbType.VarChar);
                command.Parameters["@Content"].Value = item.Content;

                int result = command.ExecuteNonQuery();
            }
            _sqlConnection.Close();
        }

        public void DeleteById(int Id)
        {
            _sqlConnection.OpenAsync().Wait();
            string sqlExpression = "DELETE FROM " + _tableName + " WHERE PostID = " + Id.ToString();
            SqlCommand command = new SqlCommand(sqlExpression, _sqlConnection);
            int result = command.ExecuteNonQuery();
            _sqlConnection.Close();
        }

        public void Update<T>(T post) where T : Post
        {
            _sqlConnection.OpenAsync().Wait();
            string sqlExpression = "UPDATE " + _tableName + " SET Autor = '" + post.Autor + "', Content = '" + post.Content + "' WHERE PostID = " + post.PostId.ToString();
            SqlCommand command = new SqlCommand(sqlExpression, _sqlConnection);
            int result = command.ExecuteNonQuery();
            _sqlConnection.Close();
        }

        private async Task<List<T>> AsyncGet<T>(int pageNumber, int pageSize) where T : Post
        {
            _sqlConnection.OpenAsync().Wait();
            List<T> items = new List<T>();
            string sqlExpression = "SELECT TOP " + pageSize.ToString() + " * FROM " + _tableName + " WHERE PostId NOT IN ( SELECT TOP " + pageSize*(pageNumber-1) + " PostId  FROM " + _tableName +")";
            SqlCommand command = new SqlCommand(sqlExpression, _sqlConnection);
            SqlDataReader reader = command.ExecuteReader();

            if (reader.HasRows)
            {
                while (await reader.ReadAsync())
                {
                    items.Add(new Post()
                    {
                        PostId = reader.GetInt32(0),
                        Autor = reader.GetString(1),
                        Content = reader.GetString(2)
                    } as T);
                }
            }

            _sqlConnection.Close();
            return items;
        }

        private async Task<T> AsyncGet<T>(int id) where T : Post
        {
            _sqlConnection.OpenAsync().Wait();
            T item = new Post() as T;
            string sqlExpression = "SELECT * FROM " + _tableName + " WHERE PostId = " + id.ToString();
            SqlCommand command = new SqlCommand(sqlExpression, _sqlConnection);
            SqlDataReader reader = command.ExecuteReader();

            if (reader.HasRows)
            {
                while (await reader.ReadAsync())
                {
                    item = (new Post()
                    {
                        PostId = reader.GetInt32(0),
                        Autor = reader.GetString(1),
                        Content = reader.GetString(2)
                    } as T);
                }
            }

            _sqlConnection.Close();
            return item;
        }
    }
}
