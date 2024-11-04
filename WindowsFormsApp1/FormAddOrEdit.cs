using System;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace WindowsFormsApp1
{
    public partial class FormAddOrEdit : Form
    {
        public int productId = 0;

        private string connectionString = "Data Source=.;Initial Catalog=SampleEslami;Integrated Security=True;MultipleActiveResultSets=true";

        public FormAddOrEdit(int selectedRowId)
        {
            InitializeComponent();
            productId = selectedRowId;
            if (productId != 0)
            {
                LoadProductDetails();
            }
        }
        private void LoadProductDetails()
        {
            string query = @"
            SELECT 
            p.Id, 
            p.Name, 
            p.IsDelete, 
            (  SELECT TOP 1 pp.Price 
                FROM ProductPrices pp 
                WHERE pp.ProductId = p.Id 
                ORDER BY pp.startDate DESC) AS Price 
        FROM    Products p
        WHERE p.Id = @ProductId AND p.IsDelete = 0";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@ProductId", productId);
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    textBox1.Text = reader["Name"].ToString();
                    textBox2.Text = reader["Price"].ToString();
                }
                connection.Close();
            }
        }

        

        private void BtnSave_Click_1(object sender, EventArgs e)
        {
                string name = textBox1.Text;
            int price; ;
            if (!int.TryParse(textBox2.Text, out price))
            {
                MessageBox.Show("فقط عدد");
                return;
            }

            if (productId == 0)
            {
                string query = "INSERT INTO Products (Name, IsDelete) VALUES (@Name,0);" +
                    " INSERT INTO ProductPrices (Price, ProductId, startDate) VALUES (@Price, SCOPE_IDENTITY(), GETDATE())";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@Name", name);
                    command.Parameters.AddWithValue("@Price", price);
                    connection.Open();
                    command.ExecuteNonQuery();
                    connection.Close();
                }

                MessageBox.Show("شد");
            }
            else 
            {
                string query = "UPDATE Products SET Name = @Name WHERE Id = @Id;" +
                    " UPDATE ProductPrices SET Price = @Price WHERE ProductId = @Id AND startDate = GETDATE()";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@Name", name);
                    command.Parameters.AddWithValue("@Price", price);
                    command.Parameters.AddWithValue("@Id", productId);
                    connection.Open();
                    command.ExecuteNonQuery();
                    connection.Close();
                }

                MessageBox.Show("اچدیت شد");
            }
            
            Close(); 
        }
    }
}
