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
    public partial class FrmPatientLogin : Form
    {
        public FrmPatientLogin()
        {
            InitializeComponent();
        }

        SqlConnect sqlconnect = new SqlConnect();

        private void btnLogin_Click(object sender, EventArgs e)
        {
            SqlCommand command = new SqlCommand("SELECT * FROM Tbl_Hastalar WHERE HastaTc=@PatientTc AND HastaSifre=@PatientPassword",sqlconnect.connection());
            command.Parameters.AddWithValue("@PatientTc", mskTc.Text);
            command.Parameters.AddWithValue("@PatientPassword", txtPassword.Text);
            SqlDataReader rd = command.ExecuteReader();
            if (rd.Read())
            {
                FrmPatientDetail frmPatientDetail = new FrmPatientDetail();
                frmPatientDetail.patientTc = mskTc.Text;
                frmPatientDetail.Show();
                this.Hide();
            }
            else 
            {
                MessageBox.Show("Hatalı TC veya şifre girdiniz.", "Giriş Başarısız", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            sqlconnect.connection().Close();
            
        }

        private void lnkSignUp_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            FrmPatientRegistration frmPatientRegistration = new FrmPatientRegistration();
            frmPatientRegistration.Show();
        }
    }
}
