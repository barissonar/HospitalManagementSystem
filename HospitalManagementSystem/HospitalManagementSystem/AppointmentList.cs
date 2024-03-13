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

namespace HospitalManagementSystem
{
    public partial class AppointmentList : Form
    {
        public AppointmentList()
        {
            InitializeComponent();
        }

        SqlConnect sqlconnect = new SqlConnect();    

        private void AppointmentList_Load(object sender, EventArgs e)
        {
            DataTable dtAppointmentList = new DataTable();
            SqlDataAdapter daGetAppointment = new SqlDataAdapter("SELECT * FROM Tbl_Randevular", sqlconnect.connection());
            daGetAppointment.Fill(dtAppointmentList);
            dataGridView1.DataSource = dtAppointmentList;

        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int selectedrow = dataGridView1.SelectedCells[0].RowIndex;
            FrmSecretaryDetail frmSecretayDetail = new FrmSecretaryDetail();
           
            
        }
    }
}
