using Microsoft.Data.SqlClient;
using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Models
{
    public class CategoryDataAccessLayer
    {
        private readonly string _connectionString;

        public CategoryDataAccessLayer()
        {
            _connectionString = "Server=(localdb)\\mssqllocaldb;Database=Library;Trusted_Connection=True;MultipleActiveResultSets=true";
        }
        public List<Categories> GetAll()
        {
            List<Categories> categories = new List<Categories>();

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();

                string sql = "SELECT * FROM Categories";
                SqlCommand cmd = new SqlCommand(sql, conn);
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    Categories category = new Categories();
                    category.Id = (int)reader["Id"];
                    category.Name = reader["Name"].ToString();

                    categories.Add(category);
                }

                conn.Close();
            }

            return categories;
        }
        public Categories GetCategroy(int? id)
        {
            Categories? category= null;
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();

                string sql = "SELECT * FROM Categories WHERE id = @Id";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@Id", id);
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    category = new Categories();
                    category.Id = (int)reader["Id"];
                    category.Name = reader["Name"].ToString();
                }

                conn.Close();
            }

            return category;
        }
        public bool Create(Categories obj)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(_connectionString))
                {
                    conn.Open();

                    string sql = "INSERT INTO Categories (Name) VALUES (@Name)";
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@Name", obj.Name);
                    int rowsAffected = cmd.ExecuteNonQuery();

                    conn.Close();
                }

                return true;
            } catch (Exception ex)
            {
                return false;
            }
        }
        public void Update(Categories obj)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();

                string sql = "UPDATE Categories SET Name = @Name WHERE Id = @Id";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@Name", obj.Name);
                cmd.Parameters.AddWithValue("@Id", obj.Id);
                int rowsAffected = cmd.ExecuteNonQuery();

                conn.Close();
            }
        }
        public void Remove(int id)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();

                string sql = "DELETE FROM Categories WHERE Id = @Id";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@Id", id);
                int rowsAffected = cmd.ExecuteNonQuery();

                conn.Close();
            }
        }
    }
}
