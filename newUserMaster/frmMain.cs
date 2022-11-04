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
    public partial class frmMain : Form
    {
        public frmMain()
        {
            InitializeComponent();

            chkCurrent.Checked = true;
            apply_filter();
        }

        private void apply_filter()
        {
            string sql = "Select id as [ID],forename + ' ' + surname as [Full Name],job_title as [Job Title],actual_department as [Actual Department] from dbo.[user]   WHERE ";

            if (txtName.Text.Length > 0)
                sql = sql + " forename + ' ' + surname LIKE '%" + txtName.Text + "%'    AND ";

            if (chkCurrent.Checked == true)
                sql = sql + "[Current] = 1    AND ";

            sql = sql.Substring(0, sql.Length - 6);


            using (SqlConnection conn = new SqlConnection(CONNECT.ConnectionStringUser))
            {
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    conn.Open();
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    dataGridView1.DataSource = dt;
                    conn.Close();
                }
            }
            dataGridView1.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dataGridView1.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dataGridView1.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dataGridView1.Columns[3].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
        }

        private void txtName_TextChanged(object sender, EventArgs e)
        {
            apply_filter();
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView1.Rows[e.RowIndex].DefaultCellStyle.BackColor == Color.CornflowerBlue)
                dataGridView1.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.Empty;
            else
            {
                foreach (DataGridViewRow row in dataGridView1.Rows)
                    row.DefaultCellStyle.BackColor = Color.Empty;
                dataGridView1.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.CornflowerBlue;
            }

        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            int id = 0;
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                if (row.DefaultCellStyle.BackColor == Color.CornflowerBlue)
                    id = Convert.ToInt32(row.Cells[0].Value);
            }
            if (id == 0)
                return;
            frmCopyUser frm = new frmCopyUser(id);
            frm.ShowDialog();
            apply_filter();
        }

        private void chkCurrent_CheckedChanged(object sender, EventArgs e)
        {
            apply_filter();
        }

        private void btnEndUser_Click(object sender, EventArgs e)
        {

            int id = 0;
            string staff = "";
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                if (row.DefaultCellStyle.BackColor == Color.CornflowerBlue)
                {
                    id = Convert.ToInt32(row.Cells[0].Value);
                    staff = row.Cells[1].Value.ToString();
                }
            }
            if (id == 0)
                return;

            frmEndUser frm = new frmEndUser(id,staff);
            frm.ShowDialog();
            apply_filter();
        }
    }
}
