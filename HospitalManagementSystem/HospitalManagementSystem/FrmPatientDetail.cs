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
using System.Data.Common;

namespace HospitalManagementSystem
{
    public partial class FrmPatientDetail : Form
    {
        public FrmPatientDetail()
        {
            InitializeComponent();
        }

        SqlConnect sqlconnect = new SqlConnect();   
        public string patientTc;
        

        private void FrmPatientDetail_Load(object sender, EventArgs e)
        {
            // Getting the patient's name and surname information from the database.

            SqlCommand command = new SqlCommand("SELECT HastaAd,HastaSoyad FROM Tbl_Hastalar WHERE HastaTc=@PatientTc", sqlconnect.connection());
            command.Parameters.AddWithValue("PatientTc", patientTc);
            SqlDataReader dr = command.ExecuteReader();
            if (dr.Read())
            {
                lblTcNo.Text = patientTc;
                lblNameSurname.Text = dr[0] + " " + dr[1];

            }

            // Patient's appointment history.

            DataTable dtAppointment = new DataTable();
            SqlDataAdapter daGetAppointment = new SqlDataAdapter("SELECT * FROM Tbl_Randevular WHERE HastaTC='"+patientTc+"'",sqlconnect.connection());
            daGetAppointment.Fill(dtAppointment);
            dataGridView1.DataSource = dtAppointment;
            sqlconnect.connection().Close();



            // Bring branches.

            SqlCommand cmdGetBranch = new SqlCommand("SELECT BransAd FROM Branslar",sqlconnect.connection());
            SqlDataReader drBranch = cmdGetBranch.ExecuteReader(); 
            while(drBranch.Read())
            {
                cmbBranch.Items.Add(drBranch[0]);
            }
            sqlconnect.connection().Close();



        }

        // Doctors according to the selected branch knowledge.

        private void cmbBranch_SelectedIndexChanged(object sender, EventArgs e)
        {
            cmbDoctor.Items.Clear();
            SqlCommand cmdGetDoctor = new SqlCommand("SELECT DoktorAd,DoktorSoyad FROM Tbl_Doktorlar WHERE DoktorBrans=@DoctorBranch",sqlconnect.connection());
            cmdGetDoctor.Parameters.AddWithValue("DoctorBranch",cmbBranch.Text);
            SqlDataReader drDoctor = cmdGetDoctor.ExecuteReader();  
            while(drDoctor.Read()) 
            {
                cmbDoctor.Items.Add(drDoctor[0] + " " + drDoctor[1]);
            }
            sqlconnect.connection().Close();    
        }

        // Active Appointments according to the selected doctor AND BRANCH information.

        private void cmbDoctor_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataTable dtActiveAppointment = new DataTable();
            SqlDataAdapter daGetActiveAppointment = new SqlDataAdapter("SELECT * FROM Tbl_Randevular WHERE RandevuBrans = '" + cmbBranch.Text + "' AND RandevuDoktor = '" + cmbDoctor.Text + "' AND RandevuDurum = 0", sqlconnect.connection());
            daGetActiveAppointment.Fill(dtActiveAppointment);
            dataGridView2.DataSource = dtActiveAppointment;
            sqlconnect.connection().Close();
        }

        private void dataGridView2_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int selectedRow = dataGridView2.SelectedCells[0].RowIndex;
            txtId.Text = dataGridView2.Rows[selectedRow].Cells[0].Value.ToString();
            

        }

        private void lnkInfoEdit_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            FrmPatientInfoEdit frmPatientInfoEdit = new FrmPatientInfoEdit();
            frmPatientInfoEdit.patientTc = patientTc;
            frmPatientInfoEdit.Show();
            

        }

        private void btnAppointment_Click(object sender, EventArgs e)
        {
            SqlCommand cmdUpdateAppointment = new SqlCommand("UPDATE Tbl_Randevular SET RandevuDurum=1,HastaTc=@PatientTc,HastaSikayet=@PatientComplaint WHERE id=@AppointmentId",sqlconnect.connection());
            cmdUpdateAppointment.Parameters.AddWithValue("AppointmentId", txtId.Text);
            cmdUpdateAppointment.Parameters.AddWithValue("PatientTc", lblTcNo.Text);
            cmdUpdateAppointment.Parameters.AddWithValue("PatientComplaint", rchComplaint.Text);
            cmdUpdateAppointment.ExecuteNonQuery();
            sqlconnect.connection().Close();
            MessageBox.Show("Randevu Başarıyla Oluşturuldu","Randevu Alındı",MessageBoxButtons.OK,MessageBoxIcon.Information);
        }
    }
}
