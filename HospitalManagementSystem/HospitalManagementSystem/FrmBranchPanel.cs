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
    public partial class FrmBranchPanel : Form
    {
        public FrmBranchPanel()
        {
            InitializeComponent();
        }

        SqlConnect sqlconnect = new SqlConnect();   

        private void btnAdd_Click(object sender, EventArgs e)
        {
            SqlCommand cmdCreateBranch = new SqlCommand("INSERT INTO Branslar (BransAd) VALUES (@BranchName)",sqlconnect.connection());
            cmdCreateBranch.Parameters.AddWithValue("BranchName", txtBranch.Text);
            cmdCreateBranch.ExecuteNonQuery();
            MessageBox.Show("Branş Başarıyla Eklendi", "Branş Eklendi", MessageBoxButtons.OK, MessageBoxIcon.Information);


        }

        private void FrmBranchPanel_Load(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter daGetBranch = new SqlDataAdapter("SELECT * FROM Branslar", sqlconnect.connection());
            daGetBranch.Fill(dt);
            dataGridView1.DataSource = dt;
            sqlconnect.connection().Close();

        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int selectedrow = dataGridView1.SelectedCells[0].RowIndex;
            txtId.Text = dataGridView1.Rows[selectedrow].Cells[0].Value.ToString();
            txtBranch.Text = dataGridView1.Rows[selectedrow].Cells[1].Value.ToString();

        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            SqlCommand cmdDeleteBranch = new SqlCommand("DELETE FROM Branslar WHERE id=@BranchId", sqlconnect.connection());
            cmdDeleteBranch.Parameters.AddWithValue("BranchId", txtId.Text);
            cmdDeleteBranch.ExecuteNonQuery();
            sqlconnect.connection().Close();
            MessageBox.Show("Branş Başarıyla Silindi.", "Branş Silindi", MessageBoxButtons.OK, MessageBoxIcon.Information);

        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            SqlCommand cmdUpdateBranch = new SqlCommand("UPDATE Branslar SET BransAd = @BranchName WHERE id=@BranchId", sqlconnect.connection());
            cmdUpdateBranch.Parameters.AddWithValue("BranchName", txtBranch.Text);
            cmdUpdateBranch.Parameters.AddWithValue("BranchId",  txtId.Text);
            cmdUpdateBranch.ExecuteNonQuery();
            sqlconnect.connection().Close();
            MessageBox.Show("Branş Başarıla Güncellendi","Branş Güncellendi",MessageBoxButtons.OK,MessageBoxIcon.Information);
           
            
        }
    }
}
