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
    public partial class FrmPatientRegistration : Form
    {
        public FrmPatientRegistration()
        {
            InitializeComponent();
        }

        SqlConnect sqlconnect = new SqlConnect();
        
        private void btnRegister_Click(object sender, EventArgs e)
        {
            SqlCommand command = new SqlCommand("INSERT INTO Tbl_Hastalar (HastaAd,HastaSoyad,HastaTc,HastaTelefon,HastaSifre,HastaCinsiyet) VALUES (@PatientName,@PatientSurname,@PatientTc,@PatientTel,@PatientPassword,@PatientGender)",sqlconnect.connection());
            command.Parameters.AddWithValue("@PatientName",txtName.Text);
            command.Parameters.AddWithValue("@PatientSurname", txtSurname.Text);
            command.Parameters.AddWithValue("@PatientTc", mskTc.Text);
            command.Parameters.AddWithValue("@PatientTel", mskTel.Text);
            command.Parameters.AddWithValue("@PatientPassword", txtPassword.Text);
            command.Parameters.AddWithValue("@PatientGender", cmbGender.Text);
            command.ExecuteNonQuery();
            sqlconnect.connection().Close();
            MessageBox.Show("Hasta Kayıt Edildi.","Bilgi",MessageBoxButtons.OK,MessageBoxIcon.Information);

        }
    }
}
