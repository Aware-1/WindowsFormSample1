using System;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Data;
namespace WindowsFormsApp1
{

    public partial class DetailOrder : Form
    {
        private string connectionString = "Data Source=.;Initial Catalog=SampleEslami;Integrated Security=True;MultipleActiveResultSets=true";
        public int orderId = 0;
        public DetailOrder(int order)
        {
            orderId= order ;
            InitializeComponent();
        }

        private void DetailOrder_Load(object sender, EventArgs e)
        {
            string query = @"
        SELECT
            od.Id,
            od.OrderId,
            od.Amount,
            u.Name AS UserName,
            p.Name AS ProductName,
            (
                SELECT TOP 1 pp.Price
                FROM ProductPrices pp
                WHERE pp.ProductId = p.Id
                ORDER BY pp.startDate DESC
            ) AS Price
        FROM
            OrderDetails od
            JOIN Orders o ON od.OrderId = o.Id
            JOIN Users u ON o.UserId = u.Id
            JOIN Products p ON od.ProductId = p.Id
        WHERE
            od.IsDelete = 0 AND od.OrderId = @orderId ";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@orderId", orderId);
               // connection.Open();
                SqlDataAdapter adapter = new SqlDataAdapter(command); 
                DataTable usersTable = new DataTable();
                adapter.Fill(usersTable);
                dataGridView1.DataSource = usersTable;
               // connection.Close();
            }
        }

    }
}
