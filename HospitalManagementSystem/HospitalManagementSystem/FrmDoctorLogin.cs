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
    public partial class FrmDoctorLogin : Form
    {
        public FrmDoctorLogin()
        {
            InitializeComponent();
        }

        SqlConnect sqlconnect = new SqlConnect();       

        private void BtnLogin_Click(object sender, EventArgs e)
        {
            SqlCommand cmdGetDoctor = new SqlCommand("SELECT * FROM Tbl_Doktorlar WHERE DoktorTc=@DoctorTc AND DoktorSifre=@DoctorPassword", sqlconnect.connection());
            cmdGetDoctor.Parameters.AddWithValue("DoctorTc", mskTc.Text);
            cmdGetDoctor.Parameters.AddWithValue("DoctorPassword", txtPassword.Text);
            SqlDataReader drDoctor = cmdGetDoctor.ExecuteReader();
            if (drDoctor.Read())
            {
               FrmDoctorDetail frmDoctorDetail = new FrmDoctorDetail();
               frmDoctorDetail.doctorTc = mskTc.Text;
               frmDoctorDetail.Show();
               this.Hide();
            }
            else
            {
                MessageBox.Show("Hatalı TC veya şifre girdiniz.", "Giriş Başarısız", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            sqlconnect.connection().Close();

        }
    }
}
