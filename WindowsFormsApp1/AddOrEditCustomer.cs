using System.Windows.Forms;
using System.Data.SqlClient;

namespace WindowsFormsApp1
{
    public partial class AddOrEditCustomer : Form
    {
        public int customerId = 0;

        private string connectionString = "Data Source=.;Initial Catalog=SampleEslami;Integrated Security=True;MultipleActiveResultSets=true";

        public AddOrEditCustomer(int selectedRowId)
        {
            InitializeComponent();
            customerId = selectedRowId;
            if (customerId != 0)
            {
                LoadCustomerDetails();
            }
        }
        private void LoadCustomerDetails()
        {

            string query = "SELECT * FROM Users WHERE IsDelete = 0 AND Id=@Id";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@Id", customerId);
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    textBox2.Text = reader["UserName"].ToString();
                    textBox1.Text = reader["lastName"].ToString();
                    textBox4.Text = reader["Name"].ToString();
                    textBox3.Text = reader["nationalCode"].ToString();
                }
                connection.Close();
            }
        }

        private void BtnSave_Click(object sender, System.EventArgs e)

        {
            string UserName = textBox2.Text;
            string lastName = textBox1.Text;
            string name = textBox4.Text;
            int nationalCode; 
            if (!int.TryParse(textBox3.Text, out nationalCode))
            {
                MessageBox.Show("فقط عدد");
                return;
            }

            if (customerId == 0)
            {
                string query = " INSERT INTO Users (UserName, lastName, Name,nationalCode,IsDelete) VALUES (@UserName, @lastName, @Name,@nationalCode,0)";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@UserName", UserName);
                    command.Parameters.AddWithValue("@lastName", lastName);
                    command.Parameters.AddWithValue("@Name", name);
                    command.Parameters.AddWithValue("@nationalCode", nationalCode);
                    connection.Open();
                    command.ExecuteNonQuery();
                    connection.Close();
                }

                MessageBox.Show("شد");
            }
            else
            {
                string query = @"
                    UPDATE Users
                    SET UserName = @UserName, Name = @Name, lastName = @lastName, nationalCode=@nationalCode
                    WHERE Id = @Id";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@UserName", UserName);
                    command.Parameters.AddWithValue("@lastName", lastName);
                    command.Parameters.AddWithValue("@Name", name);
                    command.Parameters.AddWithValue("@nationalCode", nationalCode);
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
