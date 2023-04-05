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
    public partial class frmEndUser : Form
    {
        public int staff_id { get; set; }
        public string staff_name { get; set; }
        public frmEndUser(int _staff_id, string _staff_name)
        {
            InitializeComponent();
            staff_id = _staff_id;
            staff_name = _staff_name;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnEnd_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Are you sure you want to end " + staff_name + "?", "Are you sure?", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (result == DialogResult.Yes)
            {

                string sql = "UPDATE [user_info].dbo.[user] SET [current] = 0 , end_date = '" + dteEndDate.Value.ToString("yyyyMMdd") + "' WHERE id = " + staff_id.ToString() + ";";

                using (SqlConnection conn = new SqlConnection(CONNECT.ConnectionString))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(sql, conn))
                        cmd.ExecuteNonQuery();

                    sql = "DELETE s from dbo.power_plan_staff  s " +
                        "left join dbo.power_plan_date d on s.date_id = d.id " +
                        "where staff_id = " + staff_id.ToString() + " and date_plan > '" + dteEndDate.Value.ToString("yyyyMMdd") + "' ";    
                    
                    using (SqlCommand cmd = new SqlCommand(sql, conn))
                        cmd.ExecuteNonQuery();

                    //also remove any extra data they have in dbo.absent
                    sql = "DELETE from dbo.absent_holidays where staff_id = " + staff_id.ToString() + " AND date_absent > '" + dteEndDate.Value.ToString("yyyyMMdd") + "'";

                    using (SqlCommand cmd = new SqlCommand(sql, conn)) // I HAVENT TESTED THIS BUT HONESTLY IT LOOKS OK
                        cmd.ExecuteNonQuery();


                        conn.Close();
                    this.Close();
                }
            }
        }
    }
}
