using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HospitalManagementSystem
{
    public partial class FrmSecretaryDetail : Form
    {
        public FrmSecretaryDetail()
        {
            InitializeComponent();
        }

        SqlConnect sqlconnect = new SqlConnect();   
        public string secretaryTc;
        
        
        private void FrmSecretaryDetail_Load(object sender, EventArgs e)
        {
            // Get secretary information.

            SqlCommand cmdGetSecretaryDetail = new SqlCommand("SELECT SekreterAdSoyad FROM Sekreterler WHERE SekreterTc=@SecretaryTc", sqlconnect.connection());
            cmdGetSecretaryDetail.Parameters.AddWithValue("SecretaryTc", secretaryTc);
            SqlDataReader drSecretaryInfo = cmdGetSecretaryDetail.ExecuteReader();       
            if(drSecretaryInfo.Read())
            {
                lblTc.Text = secretaryTc;
                lblNameSurname.Text = drSecretaryInfo[0].ToString();
            }
            sqlconnect.connection().Close();

            // Bring branches.

            SqlCommand cmdGetBranchCmbbx = new SqlCommand("SELECT * FROM Branslar", sqlconnect.connection());
            SqlDataReader drBranch = cmdGetBranchCmbbx.ExecuteReader();
            while (drBranch.Read())
            {
                cmbBranch.Items.Add(drBranch[1].ToString());

            }
            sqlconnect.connection().Close();



            // import branches into datagrid.

            DataTable dt = new DataTable();
            SqlDataAdapter daGetBranch = new SqlDataAdapter("SELECT * FROM Branslar", sqlconnect.connection());
            daGetBranch.Fill(dt);
            dataGridView1.DataSource = dt;
            sqlconnect.connection().Close();

            // import doctors into datagrid.

            DataTable dtDoctor = new DataTable();
            SqlDataAdapter daGetDoctor = new SqlDataAdapter("SELECT * FROM Tbl_Doktorlar", sqlconnect.connection());
            daGetDoctor.Fill(dtDoctor);
            dataGridView2.DataSource = dtDoctor;
            sqlconnect.connection().Close();    

        }

        private void btnCreate_Click(object sender, EventArgs e)
        {
            SqlCommand cmdCreateAnnouncement = new SqlCommand("INSERT INTO Duyurular (Duyuru) VALUES (@Announcement)", sqlconnect.connection());
            cmdCreateAnnouncement.Parameters.AddWithValue("Announcement", rchAnnouncement.Text);
            cmdCreateAnnouncement.ExecuteNonQuery();
            sqlconnect.connection().Close();
            MessageBox.Show("Duyuru Başarıyla Eklendi.", "Duyuru Eklendi",MessageBoxButtons.OK,MessageBoxIcon.Information);
        }

        private void btnDoctorPanel_Click(object sender, EventArgs e)
        {
            FrmDoctorPanel frmDoctorPanel = new FrmDoctorPanel();
            frmDoctorPanel.Show();

        }

        private void btnBranchPanel_Click(object sender, EventArgs e)
        {
            FrmBranchPanel frmBranchPanel = new FrmBranchPanel();
            frmBranchPanel.Show();

        }

        private void cmbBranch_SelectedIndexChanged(object sender, EventArgs e)
        {
            cmbDoctor.Items.Clear();
            cmbDoctor.Text = "";
            SqlCommand cmdDoctorByBranch = new SqlCommand("SELECT DoktorAd,DoktorSoyad FROM Tbl_Doktorlar WHERE DoktorBrans=@Branch", sqlconnect.connection());
            cmdDoctorByBranch.Parameters.AddWithValue("Branch", cmbBranch.Text);
            SqlDataReader drDoctor = cmdDoctorByBranch.ExecuteReader();
            while (drDoctor.Read())
            {
                cmbDoctor.Items.Add(drDoctor[0] + " " + drDoctor[1]);
            }
            sqlconnect.connection().Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            SqlCommand cmdCreateAppointment = new SqlCommand("INSERT INTO Tbl_Randevular (RandevuTarih,RandevuSaat,RandevuBrans,RandevuDoktor,RandevuDurum,HastaTc) VALUES (@AppointmentDate,@AppointmentHour,@AppointmentBranch,@AppointmentDoctor,@AppointmentStatus,@AppointmentTc)", sqlconnect.connection());
            cmdCreateAppointment.Parameters.AddWithValue("AppointmentDate", mskDate.Text);
            cmdCreateAppointment.Parameters.AddWithValue("AppointmentHour", mskHour.Text);
            cmdCreateAppointment.Parameters.AddWithValue("AppointmentBranch", cmbBranch.Text);
            cmdCreateAppointment.Parameters.AddWithValue("AppointmentDoctor", cmbDoctor.Text);
            cmdCreateAppointment.Parameters.AddWithValue("AppointmentStatus", chckStatus.Checked);
            cmdCreateAppointment.Parameters.AddWithValue("AppointmentTc", mskTc.Text);
            cmdCreateAppointment.ExecuteNonQuery();
            sqlconnect.connection().Close();
            mskDate.Text = "";
            mskHour.Text = "";
            cmbBranch.Text = "";
            cmbDoctor.Text = "";
            chckStatus.Checked = false;
            mskTc.Text = "";
            MessageBox.Show("Randevu Oluşturuldu.","Randevu Oluşturuldu",MessageBoxButtons.OK,MessageBoxIcon.Information);
            

        }

        private void btnAppointmentList_Click(object sender, EventArgs e)
        {
            AppointmentList frmAppointmentList = new AppointmentList();
            frmAppointmentList.Show();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            FrmAnnouncements frmAnnouncements = new FrmAnnouncements(); 
            frmAnnouncements.Show();

        }
    }
}
