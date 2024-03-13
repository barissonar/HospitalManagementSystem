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
using Microsoft.SqlServer.Server;

namespace HospitalManagementSystem
{
    public partial class FrmSecretaryLogin : Form
    {
        public FrmSecretaryLogin()
        {
            InitializeComponent();
        }

        SqlConnect sqlconnect = new SqlConnect();

        private void BtnLogin_Click(object sender, EventArgs e)
        {
            SqlCommand command = new SqlCommand("SELECT * FROM Sekreterler WHERE SekreterTc=@SecretaryTc AND SekreterSifre=@SecretaryPassword", sqlconnect.connection());
            command.Parameters.AddWithValue("@SecretaryTc", mskTc.Text);
            command.Parameters.AddWithValue("@SecretaryPassword", txtPassword.Text);
            SqlDataReader rd = command.ExecuteReader();
            if (rd.Read())
            {
                FrmSecretaryDetail frmSecretaryDetail = new FrmSecretaryDetail();
                frmSecretaryDetail.secretaryTc = mskTc.Text;
                frmSecretaryDetail.Show();
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
