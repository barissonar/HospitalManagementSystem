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
    public partial class FrmPatientInfoEdit : Form
    {
        public FrmPatientInfoEdit()
        {
            InitializeComponent();
        }

        SqlConnect sqlconnect = new SqlConnect();
        public string patientTc;

        private void FrmPatientInfoEdit_Load(object sender, EventArgs e)
        {
            SqlCommand cmdGetPatientInfo = new SqlCommand("SELECT * FROM Tbl_Hastalar WHERE HastaTc=@PatientTc", sqlconnect.connection());
            cmdGetPatientInfo.Parameters.AddWithValue("PatientTc", patientTc);
            SqlDataReader rd = cmdGetPatientInfo.ExecuteReader();   
            if (rd.Read())
            {
                txtName.Text = rd[1].ToString();
                txtSurname.Text = rd[2].ToString();
                mskTc.Text = rd[3].ToString();
                mskTel.Text = rd[4].ToString();
                txtPassword.Text = rd[5].ToString();
                cmbGender.Text = rd[6].ToString();
            }
            sqlconnect.connection().Close();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            SqlCommand cmdUpdatePatientInfo = new SqlCommand("UPDATE Tbl_Hastalar SET HastaAd=@PatientName,HastaSoyad=@PatientSurname,HastaTc=@PatientTc,HastaTelefon=@PatientTel,HastaSifre=@PatientPassword,HastaCinsiyet=@PatientGender WHERE HastaTc=@PatientTc",sqlconnect.connection());
            cmdUpdatePatientInfo.Parameters.AddWithValue("PatientName", txtName.Text);
            cmdUpdatePatientInfo.Parameters.AddWithValue("PatientSurname", txtSurname.Text);
            cmdUpdatePatientInfo.Parameters.AddWithValue("PatientTc", mskTc.Text);
            cmdUpdatePatientInfo.Parameters.AddWithValue("PatientTel", mskTel.Text);
            cmdUpdatePatientInfo.Parameters.AddWithValue("PatientPassword", txtPassword.Text);
            cmdUpdatePatientInfo.Parameters.AddWithValue("PatientGender", cmbGender.Text);
            cmdUpdatePatientInfo.ExecuteNonQuery();
            sqlconnect.connection().Close();
            MessageBox.Show("Güncelleme Başarılı", "Güncelleme Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
