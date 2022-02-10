using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace newUserMaster
{
    public partial class frmCopyUser : Form
    {
        public frmCopyUser(int id)
        {
            InitializeComponent();
            string sql = "Select id as [ID],forename + ' ' + surname as [Full Name],job_title as [Job Title],actual_department as [Actual Department] from dbo.[user] WHERE id = " + id;
            using (SqlConnection conn = new SqlConnection(CONNECT.ConnectionStringUser))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    dataGridView1.DataSource = dt;
                }
                conn.Close();
                dataGridView1.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                dataGridView1.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                dataGridView1.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                dataGridView1.Columns[3].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                btnNewClockIn.PerformClick();
            }
        }

        private void btnNewClockIn_Click(object sender, EventArgs e)
        {
            string result;
            int rng_clock = 0;
            int _min = 100;
            int _max = 9999;
            Random rng = new Random();
            rng_clock = rng.Next(_min, _max);
            //MessageBox.Show(rng_clock.ToString());
            string sql = "SELECT clock_in_id FROM dbo.[user] where clock_in_id = " + rng_clock.ToString();
            using (SqlConnection con = new SqlConnection(CONNECT.ConnectionStringUser))
            {
                using (SqlCommand cmd = new SqlCommand(sql, con))
                {
                    con.Open();
                    var firstColumn = cmd.ExecuteScalar();

                    if (firstColumn != null)
                    {
                        result = firstColumn.ToString();
                        MessageBox.Show(rng_clock.ToString() + " is already in use. Generating new number.");
                        btnNewClockIn.PerformClick();

                    }
                    else
                    {
                        txtClockIn.Text = rng_clock.ToString();
                    }
                }
            }
        }

        private void btnNewUser_Click(object sender, EventArgs e)
        {
            int validation = 0;
            //check for null boxes
            if (txtForename.TextLength == 0)
                validation++;
            if (txtSurname.TextLength == 0)
                validation++;
            if (txtUsername.TextLength == 0)
                validation++;

            if (validation > 0)
            {
                MessageBox.Show("There are " + validation + " mandatory fields that are blank, please fill them out first!", "debuggy!", MessageBoxButtons.OK);
                return;
            }
            //check for duplicates
            using (SqlConnection conn = new SqlConnection(CONNECT.ConnectionStringUser))
            {
                conn.Open();
                string sql = "";
                //email first
                sql = "SELECT email_address FROM dbo.[user] WHERE email_address = '" + txtWorkEmail.Text + "'";
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    string temp = Convert.ToString(cmd.ExecuteScalar());
                    if (temp != "")
                    {
                        MessageBox.Show("The work email you have entered is already in use!", "debugoh", MessageBoxButtons.OK);
                        return;
                    }

                }
                //the other email
                sql = "SELECT email FROM dbo.[user] WHERE email = '" + txtWorkEmail.Text + "'";
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    string temp = Convert.ToString(cmd.ExecuteScalar());
                    if (temp != "")
                    {
                        MessageBox.Show("The work email you have entered is already in use!", "debuggy", MessageBoxButtons.OK);
                        return;
                    }
                }
                //username
                sql = "SELECT username FROM dbo.[user] WHERE username = '" + txtUsername.Text + "'";
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    string temp = Convert.ToString(cmd.ExecuteScalar());
                    if (temp != "")
                    {
                        MessageBox.Show("The username you have entered is already in use!", "debugage", MessageBoxButtons.OK);
                        return;
                    }
                }
                //clock in
                sql = "SELECT clock_in_id FROM dbo.[user] WHERE clock_in_id = '" + txtClockIn.Text + "'";
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    string temp = Convert.ToString(cmd.ExecuteScalar());
                    if (temp != "")
                    {
                        MessageBox.Show("The clock in ID you have entered is already in use, please click the new ID button!", "debugzey", MessageBoxButtons.OK);
                        return;
                    }
                }

                using (SqlCommand command = new SqlCommand("usp_new_user", conn)) //test
                {

                    
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add("@copiedID", SqlDbType.Int).Value = Convert.ToInt32(dataGridView1.Rows[0].Cells[0].Value.ToString());
                    command.Parameters.Add("@forename", SqlDbType.VarChar).Value = txtForename.Text;
                    command.Parameters.Add("@surname", SqlDbType.VarChar).Value = txtSurname.Text;
                    command.Parameters.Add("@username", SqlDbType.VarChar).Value = txtUsername.Text;
                    command.Parameters.Add("@workEmail", SqlDbType.VarChar).Value = txtWorkEmail.Text;
                    command.Parameters.Add("@personalEmail", SqlDbType.VarChar).Value = txtPersonalEmail.Text;
                    command.Parameters.Add("@jobTitle", SqlDbType.VarChar).Value = txtJobTitle.Text;
                    command.Parameters.Add("@startDate", SqlDbType.Date).Value = dteStartDate.Text;
                    command.Parameters.Add("@clockID", SqlDbType.Int).Value = txtClockIn.Text;
                    command.Parameters.Add("@firstHoliday", SqlDbType.Int).Value = chkFirstHoliday.Enabled;

                    command.ExecuteNonQuery();
                }


                //get the new users ID
                sql = "SELECT max(id) FROM dbo.[user]";
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                    validation = Convert.ToInt32(cmd.ExecuteScalar());

                    conn.Close();
            }



            MessageBox.Show("New User Created - their ID = " + validation.ToString(), "Great Success", MessageBoxButtons.OK);
            this.Close();
            //add the new user
        }

        private void frmCopyUser_Shown(object sender, EventArgs e)
        {
            btnNewClockIn.PerformClick();
        }
    }
}
