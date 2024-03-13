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
    public partial class FrmDoctorPanel : Form
    {
        public FrmDoctorPanel()
        {
            InitializeComponent();
        }
        
        SqlConnect sqlconnect = new SqlConnect();

        private void FrmDoctorPanel_Load(object sender, EventArgs e)
        {
            // Bring branches.

            SqlCommand cmdGetBranch = new SqlCommand("SELECT BransAd FROM Branslar", sqlconnect.connection());
            SqlDataReader rd = cmdGetBranch.ExecuteReader();
            while (rd.Read())
            {
                cmbBranch.Items.Add(rd[0].ToString());
            }
            sqlconnect.connection().Close();

            // İmport doctors into datagrid.

            DataTable dt = new DataTable();
            SqlDataAdapter daGetDoctor = new SqlDataAdapter("SELECT * FROM Tbl_Doktorlar", sqlconnect.connection());
            daGetDoctor.Fill(dt);
            dataGridView1.DataSource = dt;
            sqlconnect.connection().Close();

        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            SqlCommand cmdCreateDoctor = new SqlCommand("INSERT INTO Tbl_Doktorlar (DoktorAd,DoktorSoyad,DoktorBrans,DoktorTc,DoktorSifre) VALUES (@DoctorName,@DoctorSurname,@DoctorBranch,@DoctorTc,@DoctorPassword)", sqlconnect.connection());
            cmdCreateDoctor.Parameters.AddWithValue("DoctorName", txtName.Text);
            cmdCreateDoctor.Parameters.AddWithValue("DoctorSurname", txtSurname.Text);
            cmdCreateDoctor.Parameters.AddWithValue("DoctorBranch", cmbBranch.Text);
            cmdCreateDoctor.Parameters.AddWithValue("DoctorTc", mskTc.Text);
            cmdCreateDoctor.Parameters.AddWithValue("DoctorPassword", txtPassword.Text);
            cmdCreateDoctor.ExecuteNonQuery();
            sqlconnect.connection().Close();
            MessageBox.Show("Doktor Eklendi.","Doktor Eklendi",MessageBoxButtons.OK,MessageBoxIcon.Information);

        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int selectedrow = dataGridView1.SelectedCells[0].RowIndex;
            txtId.Text = dataGridView1.Rows[selectedrow].Cells[0].Value.ToString();
            txtName.Text = dataGridView1.Rows[selectedrow].Cells[1].Value.ToString();
            txtSurname.Text = dataGridView1.Rows[selectedrow].Cells[2].Value.ToString();
            cmbBranch.Text = dataGridView1.Rows[selectedrow].Cells[3].Value.ToString();
            mskTc.Text = dataGridView1.Rows[selectedrow].Cells[4].Value.ToString();
            txtPassword.Text = dataGridView1.Rows[selectedrow].Cells[5].Value.ToString();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            SqlCommand cmdDeleteDoctor = new SqlCommand("DELETE FROM Tbl_Doktorlar WHERE id=@DoctorId", sqlconnect.connection());
            cmdDeleteDoctor.Parameters.AddWithValue("DoctorId", txtId.Text);
            cmdDeleteDoctor.ExecuteNonQuery();
            sqlconnect.connection().Close();
            MessageBox.Show("Doktor Silindi", "Doktor Silindi", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            SqlCommand updateDoctor = new SqlCommand("UPDATE Tbl_Doktorlar SET DoktorAd=@DoctorName, DoktorSoyad=@DoctorSurname, DoktorBrans=@DoctorBranch, DoktorTc=@DoctorTc, DoktorSifre=@DoctorPassword WHERE id=@DoctorId",sqlconnect.connection());
            updateDoctor.Parameters.AddWithValue("DoctorId", txtId.Text);
            updateDoctor.Parameters.AddWithValue("DoctorName", txtName.Text);
            updateDoctor.Parameters.AddWithValue("DoctorSurname", txtSurname.Text);
            updateDoctor.Parameters.AddWithValue("DoctorBranch", cmbBranch.Text);
            updateDoctor.Parameters.AddWithValue("DoctorTc", mskTc.Text);
            updateDoctor.Parameters.AddWithValue("DoctorPassword", txtPassword.Text);
            updateDoctor.ExecuteNonQuery();
            sqlconnect.connection().Close();
            MessageBox.Show("Doktor Güncellendi.","Doktor Güncellendi",MessageBoxButtons.OK,MessageBoxIcon.Information);
        }
    }
}
