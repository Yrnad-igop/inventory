using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Dashboard
{
    public partial class FrmDashBoard : Form
    {
        public FrmDashBoard()
        {
            InitializeComponent();

           
            // Populate ComboBox with available options
            cbEnter.Items.Add("Medicine for Senior Citizens");
            cbEnter.Items.Add("Medicine for Pregnant Women");
            cbEnter.Items.Add("Medicine Received for Senior Citizens");
            cbEnter.Items.Add("Medicine Received for Pregnant Women");
            cbEnter.Items.Add("Medicine for Pre_Schoolers");
            cbEnter.Items.Add("Medicine for Mental Illness");
            cbEnter.Items.Add("Medicine for Indigent Families/Idividuals");
            cbEnter.Items.Add("Medicine for Dental");
            cbEnter.Items.Add("Medicine Received for Pre_Schoolers");
            cbEnter.Items.Add("Medicine Received for Mental Illness");
            cbEnter.Items.Add("Medicine Received of IndigentFamilies/Individuals");
            cbEnter.Items.Add("Medicine Received for Dental");
            cbEnter.Items.Add("GSO Stock File");
            cbEnter.Items.Add("Received for GSO Stock File");
            cbEnter.Items.Add("GSO Supplies");
            cbEnter.Items.Add("Received for GSO Supplies");
            // Set default selected index
            cbEnter.SelectedIndex = 0;
        }

        private void FrmDashBoard_Load(object sender, EventArgs e)
        {

        }

        private void btnEnter_Click(object sender, EventArgs e)
        {

            if (cbEnter.SelectedItem == null)
            {
                MessageBox.Show("Please select an option.");
                return;
            }

            string selectedOption = cbEnter.SelectedItem.ToString();

            // Open the corresponding form based on the ComboBox selection
            if (selectedOption == "Medicine for Senior Citizens")
            {
                MedicineForSeniorCitizen frm = new MedicineForSeniorCitizen();
                frm.Show();
                this.Hide();
            }
            else if (selectedOption == "Medicine for Pregnant Women")
            {
                MedicineForPregnantWoman frm = new MedicineForPregnantWoman();
                frm.Show();
                this.Hide();
            }
            else if (selectedOption == "Medicine Received for Senior Citizens")
            {
                MedicineReceivedForSeniorCitizen frm = new MedicineReceivedForSeniorCitizen();
                frm.Show();
                this.Hide();
            }
            else if (selectedOption == "Medicine Received for Pregnant Women")
            {
                MedicineReceivedForPregnantWoman frm = new MedicineReceivedForPregnantWoman();
                frm.Show();
                this.Hide();
            }
            else if (selectedOption == "Medicine for Pre_Schoolers")
            {
                MedicineforPreSchoolers frm = new MedicineforPreSchoolers();
                frm.Show();
                this.Hide();
            }

            else if (selectedOption == "Medicine for Mental Illness")
            {
                MedicineforMentalIllness frm = new MedicineforMentalIllness();
                frm.Show();
                this.Hide();
            }

            else if (selectedOption == "Medicine for Indigent Families/Idividuals")
            {
                MedicineforIndigentFamiliesorIndividuals frm = new MedicineforIndigentFamiliesorIndividuals();
                frm.Show();
                this.Hide();
            }
            else if (selectedOption == "Medicine Received of IndigentFamilies/Individuals")
            {
                MedicineReceivedIndigentFamiliesOrIndividuals frm = new MedicineReceivedIndigentFamiliesOrIndividuals();
                frm.Show();
                this.Hide();
            }
            
            else if (selectedOption == "Medicine for Dental")
            {
                MedicineforDental frm = new MedicineforDental();
                frm.Show();
                this.Hide();
            }
            else if (selectedOption == "Medicine Received for Pre_Schoolers")
            {
                MedicineReceivedforPreSchoolers frm = new MedicineReceivedforPreSchoolers();
                frm.Show();
                this.Hide();
            }
            else if (selectedOption == "Medicine Received for Mental Illness")
            {
                MedicineReceivedMentalIllness frm = new MedicineReceivedMentalIllness();
                frm.Show();
                this.Hide();
            }
            else if (selectedOption == "Medicine Received for Dental")
            {
                MedicineReceivedForDental frm = new MedicineReceivedForDental();
                frm.Show();
                this.Hide();
            }
            else if (selectedOption == "GSO Stock File")
            {
                GSOStockFile frm = new GSOStockFile();
                frm.Show();
                this.Hide();
            }
            else if (selectedOption == "Received for GSO Stock File")
            {
                RecievedforGSOStockFile frm = new RecievedforGSOStockFile();
                frm.Show();
                this.Hide();
            }
            else if (selectedOption == "GSO Supplies")
            {
                GSOSupplies frm = new GSOSupplies();
                frm.Show();
                this.Hide();
            }
            else if (selectedOption == "Received for GSO Supplies")
            {
                ReceivedforGSOSupplies frm = new ReceivedforGSOSupplies();
                frm.Show();
                this.Hide();
            }
        }

        private void cbEnter_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
