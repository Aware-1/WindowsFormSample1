using System;
using System.Data;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Linq;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        private string connectionString = "Data Source=.;Initial Catalog=SampleEslami;Integrated Security=True;MultipleActiveResultSets=true";

        public Form1()
        {
            InitializeComponent();
            LoadProducts();
            LoadCustomer();
            LoadOrders();

        }
        #region Products


        private void LoadProducts()
        {

            dataGridView1.Columns.Add("Id", "شناسه");
            dataGridView1.Columns.Add("Name", "نام");
            dataGridView1.Columns.Add("Price", "قیمت");

            string query = @"SELECT 
             p.Id, 
             p.Name, 
             (SELECT TOP 1 pp.Price
               FROM ProductPrices pp
                 WHERE pp.ProductId = p.Id
                 ORDER BY pp.startDate DESC) AS Price
               FROM Products p
               WHERE  p.IsDelete = 0; ";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    dataGridView1.Rows.Add(
                        reader["Id"].ToString(),
                        reader["Name"].ToString(),
                        reader["Price"].ToString()
                    );
                }
                reader.Close();
            }
            //using (SqlConnection connection = new SqlConnection(connectionString))
            //{
            //    SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
            //    DataTable productsTable = new DataTable();
            //    adapter.Fill(productsTable);
            //    dataGridView1.DataSource = productsTable;
            //} 

            button11.Click += new EventHandler(Button11_Click);

            dataGridView1.ContextMenuStrip = Strip1;
            dataGridView1.CellMouseDown += new DataGridViewCellMouseEventHandler(dataGridView1_CellMouseDown);
            Strip1.ItemClicked += new ToolStripItemClickedEventHandler(contextMenu1_ItemClicked);
        }

        private void Button11_Click(object sender, EventArgs e)
        {
            dataGridView1.Rows.Clear();
            string searchText = textBox1.Text;
            string query = $@"SELECT 
         p.Id, 
         p.Name, 
         (SELECT TOP 1 pp.Price
           FROM ProductPrices pp
             WHERE pp.ProductId = p.Id
             ORDER BY pp.startDate DESC) AS Price
           FROM Products p
           WHERE p.IsDelete = 0 AND 
           (p.Name LIKE N'%{searchText}%' OR 
            p.Id LIKE '%{searchText}%' OR 
            (SELECT TOP 1 pp.Price FROM ProductPrices pp WHERE pp.ProductId = p.Id ORDER BY pp.startDate DESC) LIKE '%{searchText}%')";


            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    dataGridView1.Rows.Add(
                        reader["Id"].ToString(),
                        reader["Name"].ToString(),
                        reader["Price"].ToString()
                    );
                }
                reader.Close();
            }
        }
        //private void buttonSearch1_Click(object sender, EventArgs e)
        //{
        //    (dataGridView1.DataSource as DataTable).DefaultView.RowFilter =
        //        string.Format("Name LIKE '%{0}%'", textBox1.Text);
        //}
        #region click1
        private void dataGridView1_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right && e.RowIndex >= 0)
            {
                dataGridView1.ClearSelection();
                dataGridView1.Rows[e.RowIndex].Selected = true;
            }
        }

        private void contextMenu1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                int selectedRowId = Convert.ToInt32(dataGridView1.CurrentRow.Cells["Id"].Value);

                if (e.ClickedItem.Text == "ویرایش")
                {
                    FormAddOrEdit productForm = new FormAddOrEdit(selectedRowId);
                    productForm.ShowDialog();
                }
                else if (e.ClickedItem.Text == "حذف")
                {
                    {
                        if (dataGridView1.SelectedRows.Count > 0)
                        {
                            var message = new FrmShowMessage();
                            message.ShowWarn("مطمعنی؟؟؟؟", "مطمعنی ", MssgBoxBttn.OKCancel);
                            message.ShowDialog();

                            if (message.DialogResult == DialogResult.OK)
                            {
                                string query = "UPDATE Products SET IsDelete = 1 WHERE Id = @Id";

                                using (SqlConnection connection = new SqlConnection(connectionString))
                                {
                                    SqlCommand command = new SqlCommand(query, connection);
                                    command.Parameters.AddWithValue("@Id", selectedRowId);
                                    connection.Open();
                                    command.ExecuteNonQuery();
                                    connection.Close();
                                }

                                LoadProducts();
                            }
                            else if (message.DialogResult == DialogResult.Cancel)
                            {
                                var message1 = new FrmShowMessage();
                                message1.ShowWarn("خوبه چاک نکردی ", "آفرین", MssgBoxBttn.OK);
                                message1.ShowDialog();
                            }
                        }
                        else
                        {
                            var message2 = new FrmShowMessage();
                            message2.ShowWarn("یه ردیف انتخاب کن", "اشتباه کردی", MssgBoxBttn.OK);
                            message2.ShowDialog();
                        }
                    }
                }
            }
        }
        #endregion

        //private void NewMethod(string query, DataGridView dg)
        //{

        //}

        //حذف
        private void Button2_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                var message = new FrmShowMessage();
                message.ShowWarn("مطمعنی؟؟؟؟", "مطمعنی ", MssgBoxBttn.OKCancel);
                message.ShowDialog();

                if (message.DialogResult == DialogResult.OK)
                {

                    int selectedRowId = Convert.ToInt32(dataGridView1.CurrentRow.Cells["Id"].Value);

                    string query = "UPDATE Products SET IsDelete = 1 WHERE Id = @Id";

                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        SqlCommand command = new SqlCommand(query, connection);
                        command.Parameters.AddWithValue("@Id", selectedRowId);
                        connection.Open();
                        command.ExecuteNonQuery();
                        connection.Close();
                    }

                    LoadProducts();
                }
                else if (message.DialogResult == DialogResult.Cancel)
                {
                    var message1 = new FrmShowMessage();
                    message1.ShowWarn("خوبه چاک نکردی ", "آفرین", MssgBoxBttn.OK);
                    message1.ShowDialog();
                }
            }
            else
            {
                var message2 = new FrmShowMessage();
                message2.ShowWarn("یه ردیف انتخاب کن", "اشتباه کردی", MssgBoxBttn.OK);
                message2.ShowDialog();
            }
        }

        //add
        private void Button3_Click(object sender, EventArgs e)
        {
            FormAddOrEdit productForm = new FormAddOrEdit(0);
            productForm.ShowDialog();
        }
        //edit
        private void Button1_Click_1(object sender, EventArgs e)
        {
            int selectedRowId = Convert.ToInt32(dataGridView1.CurrentRow.Cells["Id"].Value);

            FormAddOrEdit productForm = new FormAddOrEdit(selectedRowId);
            productForm.ShowDialog();
        }

        private void ButtonRef_Click(object sender, EventArgs e)
        {
            dataGridView1.Columns.Clear();
            dataGridView1.Rows.Clear();

            LoadProducts();
        }


        #endregion

        #region customer

        private void LoadCustomer()
        {
            dataGridView2.Columns.Add("Id", "شناسه");
            dataGridView2.Columns.Add("Name", "نام");
            dataGridView2.Columns.Add("lastName", "نام خانوادگی");
            dataGridView2.Columns.Add("UserName", "نام کاربری");
            dataGridView2.Columns.Add("nationalCode", "کد ملی");


            string query = "SELECT * FROM Users WHERE IsDelete = 0";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    dataGridView2.Rows.Add(
                        reader["Id"].ToString(),
                        reader["Name"].ToString(),
                        reader["lastName"].ToString(),
                        reader["UserName"].ToString(),
                        reader["nationalCode"].ToString()
                    );
                }

                reader.Close();
            }
            //using (SqlConnection connection = new SqlConnection(connectionString))
            //{
            //    SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
            //    DataTable usersTable = new DataTable();
            //    adapter.Fill(usersTable);
            //    dataGridView2.DataSource = usersTable;
            //}
            button12.Click += new EventHandler(buttonSearch2_Click);

            dataGridView2.ContextMenuStrip = Strip1;
            dataGridView2.CellMouseDown += new DataGridViewCellMouseEventHandler(dataGridView2_CellMouseDown);
            Strip1.ItemClicked += new ToolStripItemClickedEventHandler(contextMenu2_ItemClicked);

        }


        private void buttonSearch2_Click(object sender, EventArgs e)
        {

            {
                dataGridView2.Rows.Clear();
                string searchText = textBox2.Text;
                string query = $@"SELECT * FROM Users u WHERE IsDelete = 0 AND 
           (u.Name LIKE N'%{searchText}%' OR 
           u.UserName LIKE N'%{searchText}%' OR 
           u.lastName LIKE N'%{searchText}%' OR 
           u.nationalCode LIKE '%{searchText}%' OR 
            u.Id LIKE '%{searchText}%')";


                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    SqlCommand command = new SqlCommand(query, connection);
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        dataGridView2.Rows.Add(
                           reader["Id"].ToString(),
                        reader["Name"].ToString(),
                        reader["lastName"].ToString(),
                        reader["UserName"].ToString(),
                        reader["nationalCode"].ToString()
                        );
                    }
                    reader.Close();
                }
            }
        }
        #region click2
        private void dataGridView2_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right && e.RowIndex >= 0)
            {
                dataGridView2.ClearSelection();
                dataGridView2.Rows[e.RowIndex].Selected = true;
            }
        }

        private void contextMenu2_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            if (dataGridView2.SelectedRows.Count > 0)
            {
                int selectedRowId = Convert.ToInt32(dataGridView2.CurrentRow.Cells["Id"].Value);

                if (e.ClickedItem.Text == "ویرایش")
                {
                    AddOrEditCustomer customer = new AddOrEditCustomer(selectedRowId);
                    customer.ShowDialog();
                }
                else if (e.ClickedItem.Text == "حذف")
                {

                    if (dataGridView2.SelectedRows.Count > 0)
                    {
                        var message = new FrmShowMessage();
                        message.ShowWarn("مطمعنی؟؟؟؟", "مطمعنی ", MssgBoxBttn.OKCancel);
                        message.ShowDialog();
                        if (message.DialogResult == DialogResult.OK)
                        {



                            string query = "UPDATE Users SET IsDelete = 1 WHERE Id = @Id";

                            using (SqlConnection connection = new SqlConnection(connectionString))
                            {
                                SqlCommand command = new SqlCommand(query, connection);
                                command.Parameters.AddWithValue("@Id", selectedRowId);
                                connection.Open();
                                command.ExecuteNonQuery();
                                connection.Close();
                            }

                            LoadCustomer();
                        }
                        else if (message.DialogResult == DialogResult.Cancel)
                        {
                            var message1 = new FrmShowMessage();
                            message1.ShowWarn("خوبه چاک نکردی ", "آفرین", MssgBoxBttn.OK);
                            message1.ShowDialog();
                        }

                    }
                    else
                    {
                        var message2 = new FrmShowMessage();
                        message2.ShowWarn("یه ردیف انتخاب کن", "اشتباه کردی", MssgBoxBttn.OK);
                        message2.ShowDialog();
                    }

                }
            }
        }
        #endregion
        //حذف
        private void Button6_Click(object sender, EventArgs e)
        {
            if (dataGridView2.SelectedRows.Count > 0)
            {
                var message = new FrmShowMessage();
                message.ShowWarn("مطمعنی؟؟؟؟", "مطمعنی ", MssgBoxBttn.OKCancel);
                message.ShowDialog();
                if (message.DialogResult == DialogResult.OK)
                {


                    int selectedRowId = Convert.ToInt32(dataGridView2.CurrentRow.Cells["Id"].Value);

                    string query = "UPDATE Users SET IsDelete = 1 WHERE Id = @Id";

                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        SqlCommand command = new SqlCommand(query, connection);
                        command.Parameters.AddWithValue("@Id", selectedRowId);
                        connection.Open();
                        command.ExecuteNonQuery();
                        connection.Close();
                    }

                    LoadCustomer();
                }
                else if (message.DialogResult == DialogResult.Cancel)
                {
                    var message1 = new FrmShowMessage();
                    message1.ShowWarn("خوبه چاک نکردی ", "آفرین", MssgBoxBttn.OK);
                    message1.ShowDialog();
                }

            }
            else
            {
                var message2 = new FrmShowMessage();
                message2.ShowWarn("یه ردیف انتخاب کن", "اشتباه کردی", MssgBoxBttn.OK);
                message2.ShowDialog();
            }
        }
        private void Button4_Click(object sender, EventArgs e)
        {
            dataGridView2.Columns.Clear();
            dataGridView2.Rows.Clear();
            LoadCustomer();
        }

        //add
        private void Button5_Click(object sender, EventArgs e)
        {
            AddOrEditCustomer customer = new AddOrEditCustomer(0);
            customer.ShowDialog();
        }

        //edit
        private void Button7_Click(object sender, EventArgs e)
        {
            int selectedRowId = Convert.ToInt32(dataGridView2.CurrentRow.Cells["Id"].Value);
            AddOrEditCustomer customer = new AddOrEditCustomer(selectedRowId);
            customer.ShowDialog();
        }
        #endregion


        #region Order

        private void LoadOrders()
        {
            dataGridView3.Columns.Add("Id", "شناسه");
            dataGridView3.Columns.Add("UserId", "شناسه خریدار");
            dataGridView3.Columns.Add("IsFinally", "پرداخت شده");
            dataGridView3.Columns.Add("CustomerName", "نام خریدار");


            string query = @"SELECT
                o.Id,
                o.UserId,
                o.IsFinally,
                (SELECT u.Name 
                 FROM Users u 
                 WHERE u.Id = o.UserId) AS CustomerName
                 FROM Orders o
                 WHERE  o.IsDelete = 0; ";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    dataGridView3.Rows.Add(
                        reader["Id"].ToString(),
                        reader["UserId"].ToString(),
                        reader["IsFinally"].ToString(),
                        reader["CustomerName"].ToString()
                    );
                }

                reader.Close();
            }





            //using (SqlConnection connection = new SqlConnection(connectionString))
            //{
            //    SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
            //    DataTable orderTable = new DataTable();
            //    adapter.Fill(orderTable);
            //    dataGridView3.DataSource = orderTable;
            //}
            button13.Click += new EventHandler(buttonSearch3_Click);

            dataGridView3.ContextMenuStrip = Strip2;
            dataGridView3.CellMouseDown += new DataGridViewCellMouseEventHandler(dataGridView3_CellMouseDown);
            Strip2.ItemClicked += new ToolStripItemClickedEventHandler(contextMenu3_ItemClicked);

        }


        #region click1
        private void buttonSearch3_Click(object sender, EventArgs e)
        {
            {
                dataGridView3.Rows.Clear();
                string searchText = textBox3.Text;
                string query = $@"SELECT
        o.Id,
        o.UserId,
        o.IsFinally,
        (SELECT u.Name 
         FROM Users u 
         WHERE u.Id = o.UserId) AS CustomerName
        FROM Orders o
        WHERE o.IsDelete = 0 AND 
        (CAST(o.Id AS NVARCHAR) LIKE N'%{searchText}%' OR
         CAST(o.UserId AS NVARCHAR) LIKE N'%{searchText}%' OR
         CAST(o.IsFinally AS NVARCHAR) LIKE N'%{searchText}%' OR
         (SELECT u.Name 
          FROM Users u 
          WHERE u.Id = o.UserId AND u.Name LIKE N'%{searchText}%') LIKE N'%{searchText}%')";


                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    SqlCommand command = new SqlCommand(query, connection);
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        dataGridView3.Rows.Add(
                          reader["Id"].ToString(),
                        reader["UserId"].ToString(),
                        reader["IsFinally"].ToString(),
                        reader["CustomerName"].ToString()
                        );
                    }
                    reader.Close();
                }
            }
        }
        private void dataGridView3_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right && e.RowIndex >= 0)
            {
                dataGridView3.ClearSelection();
                dataGridView3.Rows[e.RowIndex].Selected = true;
            }
        }

        private void contextMenu3_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            if (dataGridView3.SelectedRows.Count > 0)
            {
                int selectedRowId = Convert.ToInt32(dataGridView3.CurrentRow.Cells["Id"].Value);

                if (e.ClickedItem.Text == "تایید")
                {
                    string query = "UPDATE Orders SET IsFinally = 1 WHERE Id = @Id";

                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        SqlCommand command = new SqlCommand(query, connection);
                        command.Parameters.AddWithValue("@Id", selectedRowId);
                        connection.Open();
                        command.ExecuteNonQuery();
                        connection.Close();
                    }
                    LoadOrders();
                }
            }
        }

        #endregion


        private void Button8_Click(object sender, EventArgs e)
        {
            dataGridView3.Columns.Clear();
            dataGridView3.Rows.Clear();
            LoadOrders();
        }
        //تایید

        private void Button9_Click(object sender, EventArgs e)
        {
            if (dataGridView3.SelectedRows.Count > 0)
            {
                int selectedRowId = Convert.ToInt32(dataGridView3.CurrentRow.Cells["Id"].Value);

                string query = "UPDATE Orders SET IsFinally = 1 WHERE Id = @Id";

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@Id", selectedRowId);
                    connection.Open();
                    command.ExecuteNonQuery();
                    connection.Close();
                }

                LoadOrders();
            }
            else
            {
                //  new FrmShowMessage().ShowSuccess("یه ردیف انتخاب کن", "اشتباه کردی", MssgBoxBttn.OK).ShowDialog();

                MessageBox.Show("Please select a row to delete.");
            }
        }
        //details
        private void Button10_Click(object sender, EventArgs e)
        {
            int selectedRowId = Convert.ToInt32(dataGridView3.CurrentRow.Cells["Id"].Value);
            DetailOrder order = new DetailOrder(selectedRowId);
            order.ShowDialog();
        }
        #endregion


    }
}
