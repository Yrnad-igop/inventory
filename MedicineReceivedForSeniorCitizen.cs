using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Dashboard
{
    public partial class MedicineReceivedForSeniorCitizen : Form
    {
        string connectionString = "Data Source=YRNAD21\\SQLEXPRESS;Initial Catalog=MedicineInventoryDB;Integrated Security=True;Encrypt=False;";
        public MedicineReceivedForSeniorCitizen()
        {
            InitializeComponent();
        }

        private void MedicineReceivedForSeniorCitizen_FormClosing(object sender, FormClosingEventArgs e)
        {
            FrmDashBoard dashboard = new FrmDashBoard();
            dashboard.Show();
            this.Hide(); // Optional: Prevents the current form from staying open in memory
        }

        private void MedicineReceivedForSeniorCitizen_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'medicineInventoryDBDataSet2.tbMedicineReceivedForSeniorCitizen' table. You can move, or remove it, as needed.
            this.tbMedicineReceivedForSeniorCitizenTableAdapter.Fill(this.medicineInventoryDBDataSet2.tbMedicineReceivedForSeniorCitizen);
            LoadData(); 
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                LoadData(); // Show all data when the CheckBox is checked
            }
            else
            {

            }
        }

        private void LoadData()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "SELECT * FROM tbMedicineReceivedForSeniorCitizen";
                SqlDataAdapter adapter = new SqlDataAdapter(query, conn);
                DataTable dt = new DataTable();
                adapter.Fill(dt);
                dataGridView1.DataSource = dt;

                FormatDataGridView();
            }
        }


        private void FormatDataGridView()
        {
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill; // Auto-fit columns
            dataGridView1.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridView1.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridView1.DefaultCellStyle.WrapMode = DataGridViewTriState.True; // Enable text wrapping
        }



        private void SearchRecord(string searchValue)
        {
            string query = "SELECT * FROM tbMedicineReceivedForSeniorCitizen WHERE ItemId LIKE @SearchValue OR Description LIKE @SearchValue";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    using (SqlDataAdapter adapter = new SqlDataAdapter(query, connection))
                    {
                        adapter.SelectCommand.Parameters.AddWithValue("@SearchValue", "%" + searchValue + "%");

                        DataTable dt = new DataTable();
                        adapter.Fill(dt);

                        if (dt.Rows.Count > 0)
                        {
                            dataGridView1.DataSource = dt; // Display results in DataGridView
                            MessageBox.Show("Search Complete!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                        {
                            MessageBox.Show("No record found.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtSearch.Text))
            {
                SearchRecord(txtSearch.Text);
                txtSearch.Clear();
            }
            else
            {
                MessageBox.Show("Please enter a search term.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "INSERT INTO tbMedicineReceivedForSeniorCitizen ( ItemId, DateReceived, Description, TotalReceived, TotalCost, AvailableStock) VALUES (@ItemId, @DateReceived, @Description, @TotalReceived, @TotalCost, @AvailableStock)";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@ItemId", txtItemNum.Text);
                    cmd.Parameters.AddWithValue("@DateReceived", dtrReceived.Value);
                    cmd.Parameters.AddWithValue("@Description", rtbDescription.Text);
                    cmd.Parameters.AddWithValue("@TotalReceived", txtTotalReceived.Text);
                    cmd.Parameters.AddWithValue("@TotalCost", txtTotalCost.Text);
                    cmd.Parameters.AddWithValue("@AvailableStock", txtAvailableStock.Text);

                    conn.Open();
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Record added successfully.");
                    LoadData();
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "UPDATE tbMedicineReceivedForSeniorCitizen SET Description = @Description, TotalReceived = @TotalReceived, TotalCost = @TotalCost, AvailableStock = @AvailableStock, DateReceived = @DateReceived WHERE ItemId = @ItemId";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@ItemId", txtItemNum.Text);
                    cmd.Parameters.AddWithValue("@DateReceived", dtrReceived.Value);
                    cmd.Parameters.AddWithValue("@Description", rtbDescription.Text);
                    cmd.Parameters.AddWithValue("@TotalReceived", txtTotalReceived.Text);
                    cmd.Parameters.AddWithValue("@TotalCost", txtTotalCost.Text);
                    cmd.Parameters.AddWithValue("@AvailableStock", txtAvailableStock.Text);

                    conn.Open();
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Record updated successfully.");
                    LoadData();
                }
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string query = "DELETE FROM tbMedicineReceivedForSeniorCitizen WHERE ItemId = @ItemId OR Description = @Description";
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@ItemId", txtItemNum.Text);
                    command.Parameters.AddWithValue("@Description", rtbDescription.Text);

                    connection.Open();
                    command.ExecuteNonQuery();

                    DialogResult result = MessageBox.Show("Are you sure you want to delete this record?",
                                      "Confirm Deletion",
                                      MessageBoxButtons.YesNo,
                                      MessageBoxIcon.Question);

                    if (result == DialogResult.Yes)
                    {
                        // Proceed with deletion
                        MessageBox.Show("Record deleted successfully.");
                        txtItemNum.Clear();
                    }

                    LoadData();
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
    
}
