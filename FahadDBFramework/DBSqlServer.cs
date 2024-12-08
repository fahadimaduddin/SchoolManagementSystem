using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using System.Reflection;

namespace FahadDBFramework
{
    public class DBSqlServer
    {
        // ExecuteReader, ExecuteScalar and ExecuteNoQuery
        private string cs;
        public DBSqlServer(string cs)
        {
            this.cs = cs;
        }

        // Array, Collection, Generics, Dataset and DataTable
        public DataTable GetDataList(string storedProceName)
        {
            DataTable dt = new DataTable();
            using (SqlConnection con = new SqlConnection(cs))
            {
                using (SqlCommand cmd = new SqlCommand(storedProceName, con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    con.Open();
                    SqlDataReader dr = cmd.ExecuteReader();
                    dt.Load(dr);
                }
            }
            return dt;
        }
        public DataTable GetDataList(string storedProceName, DbParameter parameter)
        {
            DataTable dt = new DataTable();
            using (SqlConnection con = new SqlConnection(cs))
            {
                using (SqlCommand cmd = new SqlCommand(storedProceName, con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    con.Open();
                    cmd.Parameters.AddWithValue(parameter.Parameter, parameter.Value);
                    SqlDataReader dr = cmd.ExecuteReader();
                    dt.Load(dr);
                }
            }
            return dt;
        }
        public DataTable GetDataList(string storedProceName, DbParameter[] parameters)
        {
            DataTable dt = new DataTable();
            using (SqlConnection con = new SqlConnection(cs))
            {
                using (SqlCommand cmd = new SqlCommand(storedProceName, con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    con.Open();
                    foreach (var para in parameters)
                    {
                        cmd.Parameters.AddWithValue(para.Parameter, para.Value);
                    }
                    SqlDataReader dr = cmd.ExecuteReader();
                    dt.Load(dr);
                }
            }
            return dt;
        }

        public void SaveOrUpdateRecord(string storedProceName, object obj)
        {
            using (SqlConnection con = new SqlConnection(cs))
            {
                using (SqlCommand cmd = new SqlCommand(storedProceName, con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    con.Open();
                    //Parameters.
                    Type type = obj.GetType();
                    BindingFlags flags = BindingFlags.Public | BindingFlags.Instance;
                    PropertyInfo[] properties = type.GetProperties(flags);
                    foreach (var property in properties)
                    {
                        cmd.Parameters.AddWithValue("@" + property.Name, property.GetValue(obj, null));
                    }
                    cmd.ExecuteNonQuery();
                }
            }
        }

        // Overloading Function
        public object getScalarValue(string storedProceName)
        {
            object value = null;
            using (SqlConnection con = new SqlConnection(cs))
            {
                using (SqlCommand cmd = new SqlCommand(storedProceName, con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    con.Open();
                    value = cmd.ExecuteScalar();
                }
            }
            return value;
        }
        public object getScalarValue(string storedProceName, DbParameter parameter)
        {
            object value = null;
            using (SqlConnection con = new SqlConnection(cs))
            {
                using (SqlCommand cmd = new SqlCommand(storedProceName, con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    con.Open();
                    cmd.Parameters.AddWithValue(parameter.Parameter, parameter.Value);
                    value = cmd.ExecuteScalar();
                }
            }
            return value;
        }
        public object getScalarValue(string storedProceName, DbParameter[] parameters)
        {
            object value = null;
            using (SqlConnection con = new SqlConnection(cs))
            {
                using (SqlCommand cmd = new SqlCommand(storedProceName, con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    con.Open();
                    foreach (var para in parameters)
                    {
                        cmd.Parameters.AddWithValue(para.Parameter, para.Value);
                    }
                    value = cmd.ExecuteScalar();
                }
            }
            return value;
        }
    }
}
