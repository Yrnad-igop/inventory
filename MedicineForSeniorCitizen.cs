using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Dashboard
{
    public partial class MedicineForSeniorCitizen : Form
    {
         string connectionString = "Data Source=YRNAD21\\SQLEXPRESS;Initial Catalog=MedicineInventoryDB;Integrated Security=True;Encrypt=False; ";
       
        public MedicineForSeniorCitizen()
        {
            InitializeComponent();
        }

        private void LoadData()
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string query = "SELECT ItemNo, Quantity, Unit, UnitPrice, TotalCost, AvailableStock, Description FROM tbMedicineForSeniorCitizen";
                    SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);

                    dataGridView1.DataSource = dataTable;

                    FormatDataGridView();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message);
            }
        }


        private void MedicineForSeniorCitizen_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'medicineInventoryDBDataSet9.tbMedicineForSeniorCitizen' table. You can move, or remove it, as needed.
            this.tbMedicineForSeniorCitizenTableAdapter.Fill(this.medicineInventoryDBDataSet9.tbMedicineForSeniorCitizen);
            LoadData();

        }


        private void MedicineForSeniorCitizen_FormClosing(object sender, FormClosingEventArgs e)
        {
            FrmDashBoard dashboard = new FrmDashBoard();
            dashboard.Show();
            this.Hide(); // Optional: Prevents the current form from staying open in memory
        }

        private void label1_Click(object sender, EventArgs e)
        {

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

        private void SearchRecord(string searchValue)
        {
            string query = "SELECT * FROM tbMedicineForSeniorCitizen WHERE ItemNo LIKE @SearchValue OR Description LIKE @SearchValue";

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


        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string query = "INSERT INTO tbMedicineForSeniorCitizen (ItemNo,Quantity, Unit, UnitPrice, TotalCost, AvailableStock, Description) " +
                                   "VALUES (@ItemNo,@Quantity, @Unit, @UnitPrice, @TotalCost, @AvailableStock, @Description)";

                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@ItemNo", txtItemno.Text);
                    command.Parameters.AddWithValue("@Quantity", txtQuantity.Text);
                    command.Parameters.AddWithValue("@Unit", txtUnit.Text);
                    command.Parameters.AddWithValue("@UnitPrice", txtUnitPrice.Text);
                    command.Parameters.AddWithValue("@TotalCost", txtTotalCost.Text);
                    command.Parameters.AddWithValue("@AvailableStock", txtAvailableStock.Text);
                    command.Parameters.AddWithValue("@Description", rtbDescription.Text);

                    connection.Open();
                    command.ExecuteNonQuery();
                    MessageBox.Show("Record added successfully.");

                    txtItemno.Clear();
                    txtQuantity.Clear();
                    txtUnit.Clear();
                    txtUnitPrice.Clear();
                    txtTotalCost.Clear();
                    rtbDescription.Clear();
                    txtAvailableStock.Clear();

                    LoadData();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message);
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                // Validate that the ItemNo textbox is not empty
                if (string.IsNullOrWhiteSpace(txtItemno.Text))
                {
                    MessageBox.Show("Please enter the ItemNo to update the record.");
                    return;
                }

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    // SQL Update query
                    string query = "UPDATE tbMedicineForSeniorCitizen SET Quantity = @Quantity, Unit = @Unit, UnitPrice = @UnitPrice, " +
                                   "TotalCost = @TotalCost, AvailableStock = @AvailableStock, Description = @Description " +
                                   "WHERE ItemNo = @ItemNo";

                    SqlCommand command = new SqlCommand(query, connection);

                    // Bind parameters from the textboxes
                    command.Parameters.AddWithValue("@ItemNo", txtItemno.Text);
                    command.Parameters.AddWithValue("@Quantity", txtQuantity.Text);
                    command.Parameters.AddWithValue("@Unit", txtUnit.Text);
                    command.Parameters.AddWithValue("@UnitPrice", txtUnitPrice.Text);
                    command.Parameters.AddWithValue("@TotalCost", txtTotalCost.Text);
                    command.Parameters.AddWithValue("@AvailableStock", txtAvailableStock.Text);
                    command.Parameters.AddWithValue("@Description", rtbDescription.Text);

                    // Open the connection and execute the update
                    connection.Open();
                    int rowsAffected = command.ExecuteNonQuery();

                    // Check if the update was successful
                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Record updated successfully.");
                        LoadData(); // Refresh the DataGridView
                    }
                    else
                    {
                        MessageBox.Show("No record found with the provided ItemNo.");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("PLease fill all the details!");
                MessageBox.Show(ex.Message);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string query = "DELETE FROM tbMedicineForSeniorCitizen WHERE ItemNo = @ItemNo";
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@ItemNo", txtItemno.Text);

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
                        txtItemno.Clear();
                    }

                    LoadData();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message);
            }
        }


        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            
        }

        private void FormatDataGridView()
        {
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill; // Auto-fit columns
            dataGridView1.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridView1.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridView1.DefaultCellStyle.WrapMode = DataGridViewTriState.True; // Enable text wrapping
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

       
    }
}

    

