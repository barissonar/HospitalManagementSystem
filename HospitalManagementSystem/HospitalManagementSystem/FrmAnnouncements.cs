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
    public partial class FrmAnnouncements : Form
    {
        public FrmAnnouncements()
        {
            InitializeComponent();
        }

        SqlConnect sqlconnect = new SqlConnect();

        private void FrmAnnouncements_Load(object sender, EventArgs e)
        {
            DataTable dtAnnouncements = new DataTable();    
            SqlDataAdapter daGetAnnouncements = new SqlDataAdapter("SELECT * FROM Duyurular", sqlconnect.connection());
            daGetAnnouncements.Fill(dtAnnouncements);
            dataGridView1.DataSource = dtAnnouncements;
            sqlconnect.connection().Close();
           
        }
    }
}
