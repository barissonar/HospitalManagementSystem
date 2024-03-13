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
    public partial class FrmDoctorDetail : Form
    {
        public FrmDoctorDetail()
        {
            InitializeComponent();
        }

        SqlConnect sqlconnection = new SqlConnect();
        public string doctorTc;

        private void FrmDoctorDetail_Load(object sender, EventArgs e)
        {
            // Bring doctor information.

            SqlCommand getDoctorInfo = new SqlCommand("SELECT DoktorAd,DoktorSoyad FROM Tbl_Doktorlar WHERE DoktorTc=@DoctorTc", sqlconnection.connection());
            getDoctorInfo.Parameters.AddWithValue("DoctorTc", doctorTc);
            SqlDataReader drDoctor = getDoctorInfo.ExecuteReader();
            if (drDoctor.Read())
            {
                lblTc.Text = doctorTc;
                lblNameSurname.Text = drDoctor[0] + " " + drDoctor[1];
            }

            // Bring doctor's appointments.

            DataTable dtAppointmentsByDoctor = new DataTable();
            SqlDataAdapter daGetAppointmentsByDoctor = new SqlDataAdapter("SELECT * FROM Tbl_Randevular WHERE RandevuDoktor='"+lblNameSurname.Text+"'",sqlconnection.connection());
            daGetAppointmentsByDoctor.Fill(dtAppointmentsByDoctor);
            dataGridView1.DataSource = dtAppointmentsByDoctor;
            sqlconnection.connection().Close();


        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int selectedrow = dataGridView1.SelectedCells[0].RowIndex;
            rchDetail.Text = dataGridView1.Rows[selectedrow].Cells[7].Value.ToString();

        }

        private void btnInfoEdit_Click(object sender, EventArgs e)
        {
            FrmDoctorInfoEdit frmDoctorInfoEdit = new FrmDoctorInfoEdit();
            frmDoctorInfoEdit.doctorTc = doctorTc;
            frmDoctorInfoEdit.Show();
        }

        private void btnAnnouncement_Click(object sender, EventArgs e)
        {
            FrmAnnouncements frmAnnouncements = new FrmAnnouncements();
            frmAnnouncements.Show();
            
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
