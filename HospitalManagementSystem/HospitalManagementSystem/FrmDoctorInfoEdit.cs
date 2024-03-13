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
    public partial class FrmDoctorInfoEdit : Form
    {
        public FrmDoctorInfoEdit()
        {
            InitializeComponent();
        }

        public string doctorTc;
        SqlConnect sqlconnection = new SqlConnect();

        private void FrmDoctorInfoEdit_Load(object sender, EventArgs e)
        {
            // Bring branches.

            SqlCommand cmdGetBranch = new SqlCommand("SELECT BransAd FROM Branslar", sqlconnection.connection());
            SqlDataReader drBranch = cmdGetBranch.ExecuteReader();  
            while (drBranch.Read())
            {
                cmbBranch.Items.Add(drBranch[0].ToString());
            }
            sqlconnection.connection().Close();

            // Bring doctor information.

            SqlCommand cmdGetDoctorInfo = new SqlCommand("SELECT * FROM Tbl_Doktorlar WHERE DoktorTc=@DoctorTc", sqlconnection.connection());
            cmdGetDoctorInfo.Parameters.AddWithValue("DoctorTc", doctorTc);
            SqlDataReader drDoctor = cmdGetDoctorInfo.ExecuteReader();
            while (drDoctor.Read())
            {
                txtName.Text = drDoctor[1].ToString();
                txtSurname.Text = drDoctor[2].ToString();
                mskTc.Text = drDoctor[4].ToString();
                cmbBranch.Text = drDoctor[3].ToString();
                txtPassword.Text = drDoctor[5].ToString();
            }
            sqlconnection.connection().Close();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            SqlCommand cmdUpdateDoctor = new SqlCommand("UPDATE Tbl_Doktorlar SET DoktorAd=@DoctorName,DoktorSoyad=@DoctorSurname,DoktorBrans=@DoctorBranch,DoktorTc=@DoctorTc,DoktorSifre=@DoctorPassword WHERE DoktorTc=@DoctorTc",sqlconnection.connection());
            cmdUpdateDoctor.Parameters.AddWithValue("DoctorName",txtName.Text);
            cmdUpdateDoctor.Parameters.AddWithValue("DoctorSurname", txtSurname.Text);
            cmdUpdateDoctor.Parameters.AddWithValue("DoctorBranch", cmbBranch.Text);
            cmdUpdateDoctor.Parameters.AddWithValue("DoctorPassword", txtPassword.Text);
            cmdUpdateDoctor.Parameters.AddWithValue("DoctorTc", mskTc.Text);
            cmdUpdateDoctor.ExecuteNonQuery();
            sqlconnection.connection().Close();
            MessageBox.Show("Güncelleme Başarılı", "Güncelleme Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);

        }
    }
}
