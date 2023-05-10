using Microsoft.Data.SqlClient;

namespace WebApplication1.Models
{
    public class BookDataAccsessLayer
    {

        private readonly string _connectionString;

        public BookDataAccsessLayer()
        {
            _connectionString = "Server=(localdb)\\mssqllocaldb;Database=Library;Trusted_Connection=True;MultipleActiveResultSets=true";
        }

        public List<BookViewModel> GetAll()
        {
            List<BookViewModel> books = new List<BookViewModel>();

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();

                string sql = "SELECT b.Id, b.Title, b.Summary, c.Name AS CategoryName " +
                             "FROM Books b " +
                             "LEFT JOIN Categories c ON b.Category_id = c.Id";
                SqlCommand cmd = new SqlCommand(sql, conn);
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    BookViewModel book = new BookViewModel();
                    book.Id = (int)reader["Id"];
                    book.Title = reader["Title"].ToString();
                    book.Summary = reader["Summary"].ToString();
                    book.CategoryName = reader["CategoryName"].ToString();

                    books.Add(book);
                }

                conn.Close();
            }

            return books;
        }


        public Books GetBook(int? id)
        {
            Books? book = null;
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();

                string sql = "SELECT * FROM Books WHERE id = @Id";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@Id", id);
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    book = new Books();
                    book.Id = (int)reader["Id"];
                    book.Title = reader["title"].ToString();
                    book.Summary = reader["summary"].ToString();
                    book.Category_id = (int)reader["Category_id"];
                }

                conn.Close();
            }

            return book;
        }
        public List<Books> Create(Books obj)
        {
            List<Books> books = new List<Books>();

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();

                string sql = "INSERT INTO Books (Title, Summary, Category_id) VALUES (@Title, @Summary, @Category_id)";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@Title", obj.Title);
                cmd.Parameters.AddWithValue("@Summary", obj.Summary);
                cmd.Parameters.AddWithValue("@Category_id", obj.Category_id);
                int rowsAffected = cmd.ExecuteNonQuery();

                conn.Close();
            }

            return books;
        }
        public void Update(Books obj)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();

                string sql = "UPDATE Books SET Title = @Title, Summary = @Summary, Category_id = @Category_id WHERE Id = @Id";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@Title", obj.Title);
                cmd.Parameters.AddWithValue("@Summary", obj.Summary);
                cmd.Parameters.AddWithValue("@Category_id", obj.Category_id == null ? 0 : obj.Category_id);
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

                string sql = "DELETE FROM Books WHERE Id = @Id";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@Id", id);
                int rowsAffected = cmd.ExecuteNonQuery();

                conn.Close();
            }
        }


    }
}
